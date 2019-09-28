dotnet pack
cd nupkg
dotnet tool uninstall -g commandName
dotnet tool install -g commandName --version 1.0.0 --add-source .
cd ..
dotnet clean
cleanup -y