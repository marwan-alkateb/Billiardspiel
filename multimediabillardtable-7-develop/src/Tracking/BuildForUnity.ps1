<#
.SYNOPSIS
    Builds the Tracking API and integrates it into the Unity project

.DESCRIPTION
    Due to the libraries and frameworks required by the Tracking API, 
    it cannot be easily integrated with the Unity project if we want to
    use a Package Manager such as NuGet. Therefore, we build it in a separate
    VS project. This script automatically downloads MSBuild, builds the 
    Tracking API and copies the output files into the Unity project.
#>

<# Configuration
   ====================== #>
$MsBuildUrl = 'https://jb.gg/msbuild'
$MsBuildDir = './build'
$MsBuildZipFile = 'msbuild_redist.zip'
$MsBuildExeFile = 'MsBuild/15.0/Bin/MsBuild.exe'

$SrcDir = './Tracking.API/bin'
$DstDir = '../Project/Assets/TrackingApi/Assemblys'
$FileFilter = '*.dll'

$TargetConfig = 'Debug'
$TargetArch = 'x64'

$ProgressPreference = 'SilentlyContinue'

<# MSBuild handling
   ====================== #>
function DownloadMsBuild {
    if (!(Test-Path $MsBuildDir)) {
        $msBuildZipPath = Join-Path $MsBuildDir $MsBuildZipFile

        Write-Host '[Info] Downloading MSBuild...'
        New-Item -ItemType Directory -Force -Path $MsBuildDir
        Invoke-WebRequest -Uri $MsBuildUrl -OutFile $msBuildZipPath

        Write-Host '[Info] Unpacking MSBuild...'
        Expand-Archive -Path $msBuildZipPath -DestinationPath $MsBuildDir
        Remove-Item -Force -Path $msBuildZipPath
    }
    else {
        Write-Host '[Info] MSBuild already installed'
    }
}

function Restore-Packages($MsBuildPath, $ProjectName) {
    & "$MsBuildPath" "$ProjectName/$ProjectName.csproj" -t:restore
}

function InvokeMsBuild {
    $msBuildExePath = Join-Path $MsBuildDir $MsBuildExeFile
    
    if (!(Test-Path $msBuildExePath)) {
        Write-Error 'MSBuild is corrupted.' -ErrorAction Stop
    }

    Write-Host '[Info] Restoring NuGet...'
    # Add references to other projects that need to have their NuGet restored here.
    Restore-Packages -MsBuildPath $msBuildExePath -ProjectName Tracking.API
    Restore-Packages -MsBuildPath $msBuildExePath -ProjectName Tracking.Core
    Restore-Packages -MsBuildPath $msBuildExePath -ProjectName Tracking.Debug
    Restore-Packages -MsBuildPath $msBuildExePath -ProjectName Tracking.Server
    
    Write-Host '[Info] Building...'
    & "$msBuildExePath" /p:Configuration=$TargetConfig /p:Platform=$TargetArch
}

<# Unity handling
   ====================== #>
function CopyToUnity {
    Write-Host '[Info] Copying ...'

    if (!(Test-Path $DstDir)) {
        New-Item -ItemType Directory -Path $DstDir
    }

    # Build the path: $SrcDir/$TargetArch/$TargetConfig
    #   ex.: bin/x64/Debug -> That's where the output files are
    $platformSpecificSrcDir = Join-Path $SrcDir $TargetArch
    $platformSpecificSrcDir = Join-Path $platformSpecificSrcDir $TargetConfig

    if (!(Test-Path $platformSpecificSrcDir)) {
        Write-Error 'Source directory does not exist. Did the build fail?' -ErrorAction Stop
    }

    & robocopy "$platformSpecificSrcDir" "$DstDir" "$FileFilter" /MIR
}

<# Entry point
   ====================== #>
DownloadMsBuild
InvokeMsBuild
CopyToUnity