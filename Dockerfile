FROM mcr.microsoft.com/dotnet/sdk:8.0 AS stable
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS prerelease
COPY --from=stable /usr/share/dotnet/ /usr/share/dotnet/
COPY . /app
ENV BASE_PATH="/test"
RUN mkdir /test
RUN /app/bootstrap.sh /test

WORKDIR /app/FileSystemWatcherIssueReproduce
RUN dotnet publish -c Release -f net8.0 -o out
ENTRYPOINT ["/app/FileSystemWatcherIssueReproduce/out/FileSystemWatcherIssueReproduce"]
