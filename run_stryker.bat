PUSHD .

CD tests\VideoGamesRental.Api.Tests\
RMDIR /S /Q StrykerOutput
dotnet stryker -p "src/VideoGamesRental.Api/VideoGamesRental.Api.csproj" -v trace -o --mutate "!**/Program.cs"

POPD
PUSHD .

CD tests\VideoGamesRental.Application.Tests\
RMDIR /S /Q StrykerOutput
dotnet stryker -p "src/VideoGamesRental.Application/VideoGamesRental.Application.csproj" -v trace -o

POPD

CD tests\VideoGamesRental.Infrastructure.Tests\
RMDIR /S /Q StrykerOutput
dotnet stryker -p "src/VideoGamesRental.Infrastructure/VideoGamesRental.Infrastructure.csproj" -v trace --mutate "!**/Migrations/**" --mutate "!**/*DataModelConfiguration.cs" -c 1 -o
