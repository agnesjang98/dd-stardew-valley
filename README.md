# dd-stardew-valley
monitoring farming

# Development Envrionment

## Creating a new project with the dotnet cli tool

You're going to want to install the dotnet cli via `brew install dotnet`
Then run the following incantations:

```
PROJECT_NAME="dd-stardew-valley"
dotnet new sln
dotnet new classlib -o $PROJECT_NAME
dotnet new xunit -o $PROJECT_NAME.Test
dotnet sln add $PROJECT_NAME/$PROJECT_NAME.csproj
dotnet sln add $PROJECT_NAME.Test/$PROJECT_NAME.csproj
```

## Building the project

You don't have to run `dotnet restore` because it's implicitly run by all of the commands that need it.

```
dotnet build
```

## Tests

Tests live in, you guessed it, `dd-stardew-valley.Test`

To run:

```
dotnet tests
```

If this is working you're in good shape