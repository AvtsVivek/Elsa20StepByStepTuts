
Run the following command from this very folder, in which this read me file exists.

dotnet new web -n Elsa.Server.Web -o src\web
dotnet new classlib -n Elsa.CustomActivityLibrary -f netstandard2.1 --langVersion latest -o src\activities
dotnet new sln -n Elsa.BlockingActivities -o src
dotnet sln src/Elsa.BlockingActivities.sln add src/activities/Elsa.CustomActivityLibrary.csproj
dotnet sln src/Elsa.BlockingActivities.sln add src/web/Elsa.Server.Web.csproj


dotnet add src/web/Elsa.Server.Web.csproj reference src/activities/Elsa.CustomActivityLibrary.csproj


dotnet add src/activities/Elsa.CustomActivityLibrary.csproj package Elsa.Core
dotnet add src/web/Elsa.Server.Web.csproj package Elsa
dotnet add src/web/Elsa.Server.Web.csproj package Elsa.Server.Api
dotnet add src/web/Elsa.Server.Web.csproj package Elsa.Persistence.EntityFramework.SqlServer
dotnet add src/web/Elsa.Server.Web.csproj package Elsa.Activities.Email



