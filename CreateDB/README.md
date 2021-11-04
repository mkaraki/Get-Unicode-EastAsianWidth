# Get-Unicode-EastAsianWidth Database Generator

## Docker

You have to build in parent directory (root of this git repositry).
```shell
$ docker build . --file CreateDB/Dockerfile --tag get-unicode-eastasianwidth-createdb
```

Or use [GitHub Packages](https://github.com/mkaraki/Get-Unicode-EastAsianWidth/pkgs/container/get-unicode-eastasianwidth-createdb).

## Usage
```shell
# With dotnet sdk
$ dotnet run
# With Docker image
$ docker run --rm -i -v /path/to/output/database.txt:/app/eawdb.txt ghcr.io/mkaraki/get-unicode-eastasianwidth-createdb
```
