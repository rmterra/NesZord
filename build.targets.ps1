Properties {
	$root_dir = Split-Path $psake.build_script_file
	$artifacts_dir = "$root_dir\bin"
	$source_dir = "$root_dir\src"
	$log_dir = "$root_dir\log"
	
	$testdll_dir = "$artifacts_dir\NesZord.Tests.dll"
	
	$solution_dir = "$source_dir\NesZord.sln"

	$nspec_dir_pattern = "^nspec(\.\d){3}"
	$nspec_dir = Get-ChildItem "$source_dir\packages\" -Directory | ?{ $_.Name -match $nspec_dir_pattern } | select -First 1
	$nspecrunner_dir = "$nspec_dir\tools\nspecrunner.exe"
	
	$isAppVeyor = $env:APPVEYOR -eq $true
}

FormatTaskName (("-"*25) + "[{0}]" + ("-"*25))

Task Default -Depends Setup, Clean, Build, Test

Task Setup {
	CreateCleanDir "build artifacts" $artifacts_dir
	CreateCleanDir "logs" $log_dir
}

Task Clean {
	Print "Cleaning NesZord.sln"
	Exec { msbuild $solution_dir /t:Clean /p:Configuration=Debug /v:quiet } 
}

Task Build -Depends Clean {	
	Print "Building NesZord.sln"
	if($isAppVeyor) {
		Exec { msbuild $solution_dir /t:Build /p:Configuration=Debug /v:quiet /p:OutDir=$artifacts_dir /logger:"C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll" } 
	}
	else {
		Exec { msbuild $solution_dir /t:Build /p:Configuration=Debug /v:quiet /p:OutDir=$artifacts_dir } 
	}
}

Task Test {	
	Print "Testing NesZord.sln"
	Exec { & $nspecrunner_dir $testdll_dir } 
	PostTestsToAppVeyor
}

function CreateCleanDir($dirName, $dirPath) {
	Print "Creating $dirName directory"
	if (Test-Path $dirPath) { rd $dirPath -rec -force | out-null }
	mkdir $dirPath | out-null
}

function Print($message) {
	Write-Host $message -ForegroundColor Green
}

function PostTestsToAppVeyor() {
	if($isAppVeyor -ne $true) { return }

	Print "Posting test results"
	[xml]$xml = Exec { & $nspecrunner_dir $testdll_dir --formatter=XUnitFormatter } 
	Foreach($testSuite in $xml.testsuites.testsuite) {
		Foreach($testCase in $testSuite.testcase) { 
			$testName = $testSuite.name +" >> "+ $testCase.classname +" >> "+ $testCase.name
			Add-AppveyorTest -Name $testName -Framework NSpec -FileName $testSuite.name -Outcome Passed
		}
	}
}