if %computername%==MISHENKO (
echo on
echo %2 - 2
echo create nuget
D:\Projects\.nuget\nuget.exe pack  %2Wiki.Sms.nuspec -BasePath %1 -OutputDirectory D:\Work\nuget

)
echo "OK"