📝 Note Management API
A RESTful Web API built with .NET 8 and PostgreSQL.

🛠 Tech Stack
Framework: ASP.NET Core Web API

Database: PostgreSQL (via Entity Framework Core)

IDE: JetBrains Rider

1. Prerequisites

  Install the .NET SDK
  
  A running instance of PostgreSQL.

2. Configuration

  To keep the database credentials secure, I have not included my local appsettings.Development.json in the repository.
  
  Locate the appsettings.Example.json file in the root directory.
  
  Duplicate it and rename the copy to appsettings.Development.json.
  
  Update the ConnectionStrings section with your local PostgreSQL Username and Password.

3. Database Setup

  Once your connection string is set, run the following commands in your terminal to create the database schema:
  
  dotnet ef database update --project NoteApp.DbContext --startup-project NoteApp.WebApi  
4. Running the App

  Run the application using the .NET CLI or the "Run" button in Rider:
  
  Zsh
    dotnet run --project NoteApp.WebApi
  The API will be available at: http:5225, https:7152 (or check your console output for the specific port).

Curl commands to test endpoints:
  1. Insert (Create or Update)
  curl -k -X PUT "https://localhost:7152/notes/1?isFavorite=true" \
       -H "Content-Type: application/json" \
       -d '"This is the content of my note. It works."'
  
  2. Get a Specific Note by ID
  curl -k -X GET "https://localhost:7152/notes/1" \
       -H "Accept: application/json"
  
  3. Get All Note IDs
  curl -k -X GET "https://localhost:7152/notes" \
       -H "Accept: application/json"
  
  4. Get All Favorite Notes
  curl -k -X GET "https://localhost:7152/notes/favorites" \
       -H "Accept: application/json"
  
