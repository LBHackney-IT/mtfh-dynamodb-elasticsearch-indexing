FROM mcr.microsoft.com/dotnet/aspnet:5.0

RUN dotnet publish -c Release

COPY bin/Release/net5.0/publish/ App/

WORKDIR /App

ENTRYPOINT dotnet DynamoDBIndexingCore.dll ${DYNAMODB_TABLE} ${ELASTICSEARCH_HOST} ${ELASTICSEARCH_INDEX}
