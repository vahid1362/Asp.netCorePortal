#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 63435
EXPOSE 44348

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["Portal.Web.Presentation2/Portal.Web.Presentation2.csproj", "Portal.Web.Presentation2/"]
RUN dotnet restore "Portal.Web.Presentation2/Portal.Web.Presentation2.csproj"
COPY . .
WORKDIR "/src/Portal.Web.Presentation2"
RUN dotnet build "Portal.Web.Presentation2.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Portal.Web.Presentation2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Portal.Web.Presentation2.dll"]