# Mayan Artifacts Database API
This section contains the API layer that enables access to a collection records on Mayan artifacts. The data is exposed via a standard HTTP (REST-ish 😊)API. There is also a separate OData endpoint for additional querying capabilities.

## Requirements
- [.NET Core 3.1 or above](https://dotnet.microsoft.com/download/dotnet-core)
- Optional [MongoDB Server 3.6 or above](https://www.mongodb.com/download-center/community) (for local development only)
- Optional [Visual Studio Code](https://code.visualstudio.com) + [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)

## Local Setup

### Building the code
Before making any changes to the code, make sure the project builds locally on your machine.

Execute the following command via the command line. Make sure you're in the directory where the API project is located. It will output the version of the .NET SDK that will be used to build your project. This project requires version 3.1 or above.

```shell
dotnet --version
```

 This command will restore any necessary nuget packages and create a debug build. Make sure there are no errors before moving on.

```shell
dotnet build
```
### Running MongoDB in Docker
> This step is optional if you already have your own instance of mongodb that you can use for testing.
There's a Dockerfile, `Mongo.Dockerfile`, included in this folder that will help you build a mongodb container with some default values. Executing the commands below with build the image, run it, and make it available on your localhost via port 27017.

```shell
> docker build -f Mongo.Dockerfile -t mayanmongo .
> docker run -p 27017:27017 -d --name mayandb  mayanmongo
```

### Connecting to the database
The connection string to the MongoDB database is expected to be located in an environment variable, `COSMOSCONNECTION`. You can set this using your operating system environable variables or you can use the [secrets manager](https://docs.microsoft.com/aspnet/core/security/app-secrets) as shown below.

```shell
dotnet user-secrets set COSMOSCONNECTION <connection string name>
```

If you're using the Dockerfile mentioned above, you can use the MongoDB CLI to query the database directly using the following command.

```shell
docker exec -it mayandb  mongo
```

### Publish locally
```shell
dotnet publish -c Release
```