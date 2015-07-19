if((Test-Path $PSScriptRoot\src\packages\psake.4.4.2\tools\psake.psm1) -ne $true) {
	nuget restore $PSScriptRoot\src\.nuget\packages.config -SolutionDirectory $PSScriptRoot\src
}

Import-Module $PSScriptRoot\src\packages\psake.4.4.2\tools\psake.psm1 -force
if($MyInvocation.UnboundArguments.Count -ne 0) {
	. $PSScriptRoot\psake.ps1 -tasklist ($MyInvocation.UnboundArguments -join " ")
}
else {
	. $PSScriptRoot\psake.ps1
}


exit !($psake.build_success)