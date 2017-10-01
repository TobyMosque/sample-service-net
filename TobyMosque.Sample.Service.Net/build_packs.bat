for /d /r . %%d in (bin,obj) do @if exist "%%d" rd /s/q "%%d"
cd "TobyMosque.Sample.Service.Net.Entities"
dotnet restore
dotnet build
cd ..
dotnet pack "TobyMosque.Sample.Service.Net.Entities\TobyMosque.Sample.Service.Net.Entities.csproj" --include-source --include-symbols --output "C:\Nuget Packages\TobyMosque.Sample.Service.Net.Entities"
cd "TobyMosque.Sample.Service.Net.SqlServer"
dotnet restore
dotnet build
cd ..
dotnet pack "TobyMosque.Sample.Service.Net.SqlServer\TobyMosque.Sample.Service.Net.SqlServer.csproj" --include-source --include-symbols --output "C:\Nuget Packages\TobyMosque.Sample.Service.Net.SqlServer"
cd "TobyMosque.Sample.Service.Net.Npgsql"
dotnet restore
dotnet build
cd ..
dotnet pack "TobyMosque.Sample.Service.Net.Npgsql\TobyMosque.Sample.Service.Net.Npgsql.csproj" --include-source --include-symbols --output "C:\Nuget Packages\TobyMosque.Sample.Service.Net.Npgsql"
cd "TobyMosque.Sample.Service.Net"
dotnet restore
dotnet build
cd ..
dotnet pack "TobyMosque.Sample.Service.Net\TobyMosque.Sample.Service.Net.csproj" --include-source --include-symbols --output "C:\Nuget Packages\TobyMosque.Sample.Service.Net"
