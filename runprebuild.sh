#!/bin/sh
mono bin/Prebuild.exe /target vs2008 /targetframework v3_5 /conditionals NET_3_5
if [ -d ".git" ]; then git log --pretty=format:"Virtual Reality:0.1.1.19 Dev:0.5.1 Rev:%h" -n 1 > bin/.version; fi