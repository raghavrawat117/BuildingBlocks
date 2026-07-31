- Create Blank Solution<br>
`dotnet new sln -o BuildingBlock`

- Create Console App<br>
`dotnet new console -n RedisConsoleAppv1`

- Create WebAPI Project <br>
`dotnet new webapi -n TestWebAPI`
> This is for minimal API

`dotnet new webapi -f net10.0 --use-controllers -o LearnersApi`
> This is API with controller

- Add app to solution<br>
`dotnet sln add dev3/Redis/RedisConsoleAppv1/RedisConsoleAppv1.csproj`

- `dotnet sln add C:\Codes\BuildingBlocks\dev3\WebAPI\LearnersApi\LearnersApi.csproj`

> Note : It is path to the .csproj file.

[Template for namespaced code](https://www.geeksforgeeks.org/c-sharp/c-sharp-hello-world/) 

[How to Debug](https://www.youtube.com/watch?v=XKCzdFOxOwA)