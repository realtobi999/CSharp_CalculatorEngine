test:
	clear
	cd src/Calculator.Tests && dotnet test

build:
	clear
	cd src && dotnet build

run:
	clear
	cd src/Calculator.Core && dotnet run