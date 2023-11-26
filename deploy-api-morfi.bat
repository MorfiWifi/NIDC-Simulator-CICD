@echo off

REM Create or overwrite liara.json with specified content
echo { > liara.json
echo   "netcore": { >> liara.json
echo "version": "6.0", >> liara.json
echo "finalDllName": "HafariApi", >> liara.json
echo "csprojectFile": "HafariApi/HafariApi.csproj" >> liara.json
echo  } >> liara.json
echo } >> liara.json

REM Execute the liara deploy command

liara deploy --app sample-api --port 80 --debug