FROM microsoft/dotnet:2.2-sdk AS sdk
COPY . /app
WORKDIR /app
RUN dotnet restore && dotnet build -c release
WORKDIR /work/src/MyCompany.MyProject.Web
RUN dotnet publish -c release -o /release

FROM microsoft/dotnet:2.1-aspnetcore-runtime as run
WORKDIR /app
COPY --from=sdk /release .
EXPOSE 80
VOLUME ["/app/data"]
ENTRYPOINT [ "dotnet","MyCompany.MyProject.Web.dll" ]