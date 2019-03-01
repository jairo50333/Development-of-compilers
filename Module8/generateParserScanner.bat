cls
gplex.exe /unicode SimpleLex.lex
gppg.exe /no-lines /gplex SimpleYacc.y
echo wscript.Sleep 30000>"%temp%\sleep30.vbs" 
cscript //nologo "%temp%\sleep30.vbs" 
del "%temp%\sleep30.vbs"

