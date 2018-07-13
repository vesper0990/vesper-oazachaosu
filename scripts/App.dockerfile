FROM microsoft/aspnetcore-build:latest
ARG URL_PORT
WORKDIR /app
ENV NUGET_XMLDOC_MODE skip
ENV ASPNETCORE_URLS http://0.0.0.0:${URL_PORT}
ENTRYPOINT [ "dotnet", "Oazachaosu.Api.dll" ]
