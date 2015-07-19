Properties {
	$root_dir = Split-Path $psake.build_script_file
	$artifacts_dir = "$root_dir\bin"
	$testdll_dir = "$artifacts_dir\NesZord.Tests.dll"
	$source_dir = "$root_dir\src"
	$solution_dir = "$source_dir\NesZord.sln"
	$nspecrunner_dir = "$source_dir\packages\nspec.0.9.68\tools\nspecrunner.exe"
}

FormatTaskName (("-"*25) + "[{0}]" + ("-"*25))

Task Default -Depends Setup, Clean, Build, Test

Task Setup {
	Write-Host "Creating build artifacts directory" -ForegroundColor Green
	if (Test-Path $artifacts_dir) 
	{	
		rd $artifacts_dir -rec -force | out-null
	}
	
	mkdir $artifacts_dir | out-null
}

Task Clean {
	Write-Host "Cleaning NesZord.sln" -ForegroundColor Green
	Exec { msbuild $solution_dir /t:Clean /p:Configuration=Debug /v:quiet } 
}

Task Build -Depends Clean {	
	Write-Host "Building NesZord.sln" -ForegroundColor Green
	Exec { msbuild $solution_dir /t:Build /p:Configuration=Debug /v:quiet /p:OutDir=$artifacts_dir } 
}

Task Test {	
	Write-Host "Testing NesZord.sln" -ForegroundColor Green
	Exec { & $nspecrunner_dir $testdll_dir } 
}