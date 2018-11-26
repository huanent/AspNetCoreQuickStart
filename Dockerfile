FROM microsoft/dotnet:2.1-sdk AS sdk
COPY . /work
WORKDIR /work
RUN dotnet restore && dotnet build -c release
WORKDIR /work/src/MyCompany.MyProject.Web
RUN dotnet publish -c release -o /release

FROM microsoft/dotnet:2.1-aspnetcore-runtime as run
WORKDIR /app
COPY --from=sdk /release .
EXPOSE 80
VOLUME ["/app/data/logs"]
ENTRYPOINT [ "dotnet","MyCompany.MyProject.Web.dll" ]