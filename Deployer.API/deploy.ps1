dotnet publish -c Release
Compress-Archive -Update -Path '.\bin\Release\net5.0\publish\*' -DestinationPath archive.zip
ssh root@srv10.mikr.us -p10271 "rm -rf /var/deployer/*"
scp -P10271 .\archive.zip root@srv10.mikr.us:/var/deployer/archive.zip
ssh root@srv10.mikr.us -p10271 "cd /var/deployer && unzip -o archive.zip; rm -f /var/deployer/archive.zip;systemctl stop deployer >/dev/null 2>&1; cp /var/deployer/deployer.service /etc/systemd/system/deployer.service;systemctl enable deployer; systemctl start deployer;systemctl daemon-reload"