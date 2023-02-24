# dotNet Core installation

## Add repository

* Download package

```bash
  wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb
```

* Add source

`/etc/apt/sources.list.d/microsoft-prod.list`

```bash
  sudo dpkg -i packages-microsoft-prod.deb
```

## Install Asp Net Core runtime 3.1

```bash
  sudo apt-get update
  sudo apt-get install aspnetcore-runtime-3.1
```

## Create systemd unit

`/etc/systemd/system/mobaspaceweb.service`

```
[Unit]
Description=Mobaspace Web

[Service]
WorkingDirectory=/var/www/mobaspace
ExecStart=/usr/bin/dotnet /var/www/mobaspace/MobaSpace.Web.UI.dll
Restart=always
# Restart service after 10 seconds if the dotnet service crashes:
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=mobaspaceweb
User=exploit
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target
```

## Give rights to exploit

`/etc/sudoers.d/exploit`

```
# Allow exploit user to restart mobaspaceweb.service
#exploit ALL=(ALL) !ALL
exploit ALL=(ALL) NOPASSWD: /bin/systemctl restart mobaspaceweb.service
exploit ALL=(ALL) NOPASSWD: /bin/systemctl stop mobaspaceweb.service
exploit ALL=(ALL) NOPASSWD: /bin/systemctl start mobaspaceweb.service
```
## Add Apache with proxy

```bash
  sudo apt install apache2
  sudo a2enmod proxy proxy_http proxy_html
  sudo systemctl restart apache2
```

## Configure Apache as proxy for Asp Net Core

in ` /etc/apache2/sites-enabled/000-default.conf`

```
    ProxyPreserveHost On
    ProxyPass / http://127.0.0.1:5000/
    ProxyPassReverse / http://127.0.0.1:5000/
```

