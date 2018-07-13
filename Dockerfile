# BUILD PHASE
FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /app
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c Release -o out
# RUN PHASE
FROM microsoft/dotnet:2.1-runtime AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet", "gcpe-azure-aks-sample.dll" ]