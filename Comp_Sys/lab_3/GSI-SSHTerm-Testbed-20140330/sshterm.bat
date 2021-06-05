@echo on
"%~dp0"
cd "%~dp0"
set USER_HOME="%~dp0\home"
set X509_CERT_DIR="%~dp0\home\.globus\certificates"
set X509_VOMS_DIR="%~dp0\home\.globus\vomsdir"
del /Q %USER_HOME%\.sshterm\GSI-SSHTerm.*
rem echo %USER_HOME%
cd bin
call sshterm.bat
echo %USER_HOME%
