FROM microsoft/dotnet:latest
COPY . /app2
WORKDIR /app2

RUN dotnet restore
RUN dotnet build

EXPOSE 81
ENV ASPNETCORE_URLS http://*:5000
ENV ASPNETCORE_ENVIROMENT docker

WORKDIR /app2/src/Oazachaosu.Api/

ENTRYPOINT dotnet run