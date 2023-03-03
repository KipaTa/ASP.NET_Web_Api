# ASP.NET_Web_Api

ASP.NET_Web_Api is created with Entity Framework Code First workflow and ASP.NET Core in C#. It comprises a database made in SQL Server through EF Core
with a RESTful API to allow users to manipulate the data. The database stores information about characters, movies
they appear in, and the franchises these movies belong to. 

## Technologies used

* .NET 6
* ASP.NET Core Web Api
* C#
* Microsoft SQL Server 19.0.1
* Microsoft SQL Server Management Studio
* Swagger

## ASP.NET_Web_Api Folder Structure
```
.
│   .gitattributes
│   .gitignore
│   appsettings.Development.json
│   appsettings.json
│   MovieCharactersAPI.csproj
│   MovieCharactersAPI.csproj.user
│   MovieCharactersAPI.sln
│   Program.cs
│   README.md
│
├───.vs
│
├───bin
├───Controllers
│       CharactersController.cs         # Controller that communicates with CharacterServise
│       FranchisesController.cs         # Controller that communicates with FranchisesServise
│       MoviesController.cs             # Controller that communicates with MoviesServise
│
├───Exceptions                          # Custom made exceptions
│       CharacterNotFoundException.cs
│       FranchiseNotFoundException.cs
│       MovieNotFoundException.cs
│
├───Migrations                          # Migrations for database
│
├───Models                              # Contains database tables as structures.
│   │   Character.cs
│   │   Franchise.cs
│   │   Movie.cs
│   │   MovieCharactersDbContext.cs
│   │
│   └───Dtos                            # Contains DTO's of models.
│       ├───CharacterDtos
│       │       CharacterDto.cs
│       │       CreateCharacterDto.cs
│       │       EditCharacterDto.cs
│       │       NewCharacterDto.cs
│       │
│       ├───FranchiseDtos
│       │       CreateFranchiseDto.cs
│       │       EditFranchiseDto.cs
│       │       FranchiseDto.cs
│       │
│       └───MovieDtos
│               CreateMovieDto.cs
│               EditMovieDto.cs
│               MovieDto.cs
│
├───obj
├───Profiles                            # Contains profiles for Automapper
│       CharacterProfile.cs
│       FranchiseProfile.cs
│       MovieProfile.cs
│
├───Properties
│
└───Services
    │   ICrudService.cs                 # Interface of CRUD-queries
    │
    ├───Characters                      # Queries for character table
    │       CharacterService.cs
    │       ICharacterService.cs
    │
    ├───Franchises                      # Queries for franchise table
    │       FranchiceService.cs
    │       IFranchiseService.cs
    │
    └───Movies                          # Queries for movie table
            IMovieService.cs
            MovieService.cs
        
```

## MovieCharactersAPI Diagram
![MovieCharactersDb diagram](/MovieCharactersDbDiagram.PNG)



## Authors
[@Heidi J](https://github.com/HeidiJoensuu)
[@Kirsi T](https://github.com/KipaTa)

## Sources
Project was an assignment done during education program created by Noroff Education
