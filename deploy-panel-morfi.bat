rem cd Panel
rem dotnet publish -c Release -o bin\pub

rem cd bin\pub\wwwroot

rem @echo off

rem REM Create or overwrite liara.json with specified content
rem echo {> liara.json
rem echo "app": "nidc">> liara.json
rem echo }>> liara.json


REM Define a variable containing multiple lines of text
echo gzip             on; > liara_nginx.conf
echo gzip_disable     "msie6"; >> liara_nginx.conf
echo gzip_vary        on;>> liara_nginx.conf
echo gzip_proxied     any;>> liara_nginx.conf
echo gzip_comp_level  6;>> liara_nginx.conf
echo gzip_types       text/plain text/css application/json application/javascript application/x-javascript text/xml application/xml application/xml+rss text/javascript image/svg+xml;>> liara_nginx.conf
echo location / {>> liara_nginx.conf
echo         try_files $uri $uri/ /index.html; # For handling client-side routing SPA>> liara_nginx.conf
echo }>> liara_nginx.conf
echo location ~ /\.well-known {>> liara_nginx.conf
echo   allow all;>> liara_nginx.conf
echo }>> liara_nginx.conf
echo # cache.appcache, your document html and data>> liara_nginx.conf
echo "location ~* \.(?:manifest|appcache|html?|xml|json)$ {">> liara_nginx.conf
echo   expires -1;>> liara_nginx.conf
echo }>> liara_nginx.conf
echo # Media: images, icons, video, audio, HTC>> liara_nginx.conf
echo "location ~* \.(?:jpg|jpeg|gif|png|ico|cur|gz|svg|svgz|mp4|ogg|ogv|webm|htc)$ {">> liara_nginx.conf
echo   expires 1M;>> liara_nginx.conf
echo   access_log off;>> liara_nginx.conf
echo   add_header Cache-Control "public";>> liara_nginx.conf
echo }>> liara_nginx.conf
echo # CSS, Javascript and Fonts>> liara_nginx.conf
echo location ~* \.(?:css|js|otf|ttf|eot|woff|woff2)$ {>> liara_nginx.conf
echo   expires 1y;>> liara_nginx.conf
echo   access_log off;>> liara_nginx.conf
echo   add_header Cache-Control "public";>> liara_nginx.conf
echo }>> liara_nginx.conf

REM Write the contents of the variable to a file
rem echo %multiline_text% > liara_nginx.conf

REM Execute the liara deploy command
rem liara deploy --app nidc --port 80 --debug