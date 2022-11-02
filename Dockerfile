FROM mcr.microsoft.com/dotnet/sdk:6.0 AS sdk-6
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS main
COPY --from=sdk-6 /usr/share/dotnet/ /usr/share/dotnet/
COPY . /app
ENV BASE_PATH="/test"
RUN mkdir /test
RUN /app/bootstrap.sh /test

WORKDIR /app/FileSystemWatcherIssueReproduce
RUN dotnet publish -c Release -f net7.0 -o out
ENTRYPOINT /app/FileSystemWatcherIssueReproduce/out/FileSystemWatcherIssueReproduce
