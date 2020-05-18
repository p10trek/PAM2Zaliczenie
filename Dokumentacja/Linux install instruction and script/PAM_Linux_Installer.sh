wget https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
sudo add-apt-repository universe
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/18.04/mssql-server-2019.list)"
sudo apt-get update
sudo apt-get install -y apt-transport-https aspnetcore-runtime-3.1 mssql-server mssql-tools unixodbc-dev nginx
wget https://github.com/Karpfly2822/Test-VS-Repo/releases/download/1/iss_linux.tar.gz -O iss_linux.tar.gz
tar -xvzf iss_linux.tar.gz
rm iss_linux.tar.gz
sudo mkdir /var/www/PAM
sudo mv iss_linux/* /var/www/PAM/
rm -rf iss_linux
sudo mv /var/www/PAM/PAM.service /etc/systemd/system/
sudo mkdir /var/opt/mssql/backup
sudo mv /var/www/PAM/PAM_KillersDB.bak /var/opt/mssql/backup/PAM_KillersDB.bak
sudo chown -R mssql:mssql /var/opt/mssql/backup/
sudo systemctl enable PAM.service
sudo systemctl start PAM