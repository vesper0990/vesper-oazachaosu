FROM microsoft/dotnet:latest
COPY . /app
WORKDIR /app

RUN dotnet restore
RUN dotnet build

EXPOSE 81
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIROMENT docker

ENTRYPOINT dotnet run