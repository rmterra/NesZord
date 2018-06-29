#tool "xunit.runner.console"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var outDir = "./artifacts";
var solution = "./src/NesZord.sln";
var testProjects = $"./src/**/bin/{configuration}/*.Tests.New.dll";

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
		XUnit2(testProjects, new XUnit2Settings {
			XmlReport = true,
			OutputDirectory = outDir
		});
	});

Task("Default")
	.IsDependentOn("RunTests");

RunTarget(target);