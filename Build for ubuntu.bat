SET sln="E:\work\Projects\Zp\Zp.sln"
SET targetFolder=E:\work\Projects\Zp\Publish\

echo off

del /s /q "%targetFolder%*.*"

rem dotnet build ProjectName.csproj --runtime ubuntu.xx.xx-x64
dotnet publish %sln% -c Release -o %targetFolder% -r win-x64 -p:PublishSingleFile=true --self-contained true

pause