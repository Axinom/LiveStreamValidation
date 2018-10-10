# Live Stream Validation

This is a PowerShell module that provides the `Test-LiveStream` command. You can use it to validate basic timing characteristics of DASH live streams.

# System requirements

* (Windows) PowerShell 5 or newer
* (Linux) PowerShell Core (`pwsh`)

# Installation

1. `Install-Module LiveStreamValidation -Scope CurrentUser`

# Usage

1. `Test-LiveStream http://example.com/Manifest.mpd`