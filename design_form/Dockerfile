#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["design_form.csproj", ""]
RUN dotnet restore "design_form.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "design_form.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "design_form.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 2288 
ENV ASPNETCORE_URLS http://<container ip>:2288
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "design_form.dll"]