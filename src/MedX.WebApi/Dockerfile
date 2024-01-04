#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
#WORKDIR /App
#COPY ./src/MedX.WebApi/MedX.WebApi.csproj ./Medx.WebApi/
#RUN dotnet restore ./MedX.WebApi/MedX.WebApi.csproj
#COPY . .
#RUN dotnet publish -c Release -o out
#
#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#WORKDIR /App
#COPY --from=build-env /App/out .
#EXPOSE 5000
#ENV ASPNETCORE_URLS=http://+:5000
#ENTRYPOINT ["dotnet", "MedX.WebApi.dll"]

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App
COPY ./src/MedX.WebApi/MedX.WebApi.csproj ./src/MedX.WebApi/
RUN dotnet restore ./src/MedX.WebApi/MedX.WebApi.csproj
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "MedX.WebApi.dll"]