FROM microsoft/aspnetcore-build:latest
ARG BUILD_CONFIG=Debug
ARG BUILD_VERSION=0.0.1
ARG BUILD_LOCATION=/app/out
ENV NUGET_XMLDOC_MODE skip
WORKDIR /app
COPY *.csproj .
run dotnet restore
COPY . /app
run dotnet publish --output ${BUILD_LOCATION} --configuration ${BUILD_CONFIG}

