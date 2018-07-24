FROM microsoft/aspnetcore-build:latest
ARG BUILD_CONFIG=Debug
ARG BUILD_VERSION=0.0.1
ARG BUILD_LOCATION=/app/out
ENV NUGET_XMLDOC_MODE skip

WORKDIR /app
COPY *.sln .
COPY src/Oazachaosu.Api/Oazachaosu.Api.csproj ./src/Oazachaosu.Api/
COPY src/Oazachaosu.Core/Oazachaosu.Core.csproj ./src/Oazachaosu.Core/
COPY src/Oazachaosu.Core.Common/Oazachaosu.Core.Common.csproj ./src/Oazachaosu.Core.Common/
run dotnet restore
COPY . /app
run dotnet publish ./src/Oazachaosu.Api/Oazachaosu.Api.csproj --output ${BUILD_LOCATION} --configuration ${BUILD_CONFIG}

