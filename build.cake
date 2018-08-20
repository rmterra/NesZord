#addin Cake.Coveralls

#tool "xunit.runner.console"
#tool "nuget:?package=OpenCover"
#tool coveralls.io

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var outDir = "./artifacts";
var solution = "./src/NesZord.sln";
var testProjects = $"./src/**/bin/{configuration}/*.Tests.dll";

var coveralls_token = EnvironmentVariable("COVERALLS_REPO_TOKEN") ?? "undefined";

Task("Restore-NuGet-Packages")
    .Does(() =>
	{
	    NuGetRestore(solution);
	});

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
	.Does(() => 
	{
		MSBuild(solution, new MSBuildSettings {
			Configuration = configuration,
			Verbosity = Verbosity.Minimal,
			ToolVersion = MSBuildToolVersion.VS2017
		});
	});

Task("RunTests")
	.IsDependentOn("Build")
	.Does(() => 
	{
		OpenCover(tool => {
			tool.XUnit2(testProjects, 
			new XUnit2Settings 
			{ 
				ShadowCopy = false,
				XmlReport = true,
				OutputDirectory = outDir
			});
		},
		new FilePath("./coverage.xml"),
		new OpenCoverSettings()
			.WithFilter("+[NesZord.*]*")
			.WithFilter("-[NesZord.Tests.*]*"));
	});

Task("Upload-Coverage-Report")
	.IsDependentOn("RunTests")
	.Does(() =>
	{
		CoverallsIo("./coverage.xml", new CoverallsIoSettings()
		{
			RepoToken = coveralls_token
		});
	});

Task("Default")
	.IsDependentOn("RunTests");

Task("AppVeyor")
	.IsDependentOn("Upload-Coverage-Report");

RunTarget(target);