# 🗺️ Tourist Guide Labs

This repository contains 4 different C# projects related to a tourist guide system.

## 📌 Projects

1. **Console app with in-memory data**  
   - Data is stored in lists  
   - Search and route planning available
   - Unit-Tests

2. **Console app with SQLite storage**  
   - Uses Entity Framework Core with SQLite  
   - Allows storing and loading user history and attractions

3. **Web app with database and frontend**  
   - ASP.NET Core Minimal API  
   - SQLite for database  
   - Static HTML/CSS frontend (no JavaScript framework)

4. **WPF desktop app**  
   - Simple GUI using WPF  
   - No API controllers used  
   - Local logic with basic UI interactions

## 🛠️ Technologies

- C# / .NET
- SQLite + Entity Framework Core
- ASP.NET Core (Minimal API)
- WPF
- HTML + CSS + JS
- JSON serialization

## ▶️ How to Run

### Console Apps

```bash
cd TG1        
dotnet run

cd TG3Final
dotnet run

Open in browser:
http://localhost:5000

Swagger UI: http://localhost:5000/swagger