param(
    [string]$ChannelVersion
)

# Prebuild script to update AssemblyInfo with version info from GitVersion
# For PoC, we can just keep static version. Implement later if needed.
Write-Host "Prebuild: ChannelVersion=$ChannelVersion"
