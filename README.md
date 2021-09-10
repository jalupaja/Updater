# Updater
Source: https://github.com/jalupaja/Updater
 
# Features:
- silent install mode
- download File from Link
- unzip File if necessary (via hidden powershell)
- if programm still open: 
	- if silent: retry every 5 seconds 
	- if not silent: show retry message
- automatically excludes itself
- can exclude specific file extensions

# Usage:
Updater.exe (fileUrl) [destinationPath] ["silent"] ["question"] ["EXCLUDE:excludeExtension1,excludeExtension2,..."]

fileUrl: the direct Url to the updated file
destinationPath: where to safe/ extract the file(s) to. Default is the filepath of Updater.exe
"silent": start without any output to the user
"question": ask if User want to update
"EXCLUDE:": exclude specific file extensions (see Examples)

# Examples
start Updater.exe, download dumbManager, ask the user if he wants to update and excludes ".ini" and ".conf" Extensions:
`Updater.exe "https://github.com/jalupaja/dumbManager/releases/latest/download/dumbManager.zip" question EXCLUDE:ini,conf`

start Updater.exe, download dumbManager quietly to C:\Users\ and excludes ".ini" and ".conf" Extensions:
`Updater.exe "https://github.com/jalupaja/dumbManager/releases/latest/download/dumbManager.zip" EXCLUDE:ini,conf silent "C:\Users\"`

