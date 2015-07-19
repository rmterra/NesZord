Properties {
	$root_dir = Split-Path $psake.build_script_file
	$artifacts_dir = "$root_dir\bin"
	$source_dir = "$root_dir\src"
	$log_dir = "$root_dir\log"
	
	$testdll_dir = "$artifacts_dir\NesZord.Tests.dll"
	
	$solution_dir = "$source_dir\NesZord.sln"
	$nspecrunner_dir = "$source_dir\packages\nspec.0.9.68\tools\nspecrunner.exe"
	
	$isAppVeyor = $env:APPVEYOR -eq $true
}

FormatTaskName (("-"*25) + "[{0}]" + ("-"*25))

Task Default -Depends Setup, Clean, Build, Test

Task Setup {
	CreateCleanDir "build artifacts" $artifacts_dir
	CreateCleanDir "logs" $log_dir
}

Task Clean {
	Write-Host "Cleaning NesZord.sln" -ForegroundColor Green
	Exec { msbuild $solution_dir /t:Clean /p:Configuration=Debug /v:quiet } 
}

Task Build -Depends Clean {	
	Write-Host "Building NesZord.sln" -ForegroundColor Green
	if($isAppVeyor) {
		Exec { msbuild $solution_dir /t:Build /p:Configuration=Debug /v:quiet /p:OutDir=$artifacts_dir /logger:"C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll" } 
	}
	else {
		Exec { msbuild $solution_dir /t:Build /p:Configuration=Debug /v:quiet /p:OutDir=$artifacts_dir } 
	}
}

Task Test {	
	Write-Host "Testing NesZord.sln" -ForegroundColor Green
	Exec { & $nspecrunner_dir $testdll_dir } 
	Exec { & $nspecrunner_dir $testdll_dir --formatter=XUnitFormatter > "$log_dir\xunit-results.xml" } 
	
	if($isAppVeyor) {
		$uri = "https://ci.appveyor.com/api/testresults/xunit/$($env:APPVEYOR_JOB_ID)"
		$wc = New-Object 'System.Net.WebClient'
		$wc.UploadFile($uri, (Resolve-Path "$log_dir\xunit-results.xml"))
		Write-Host "Test results uploaded to $uri" -ForegroundColor Green
	}
}

function CreateCleanDir($dirName, $dirPath) {
	Write-Host "Creating $dirName directory" -ForegroundColor Green
	if (Test-Path $dirPath) { rd $dirPath -rec -force | out-null }
	mkdir $dirPath | out-null
}