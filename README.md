# Customer API Console Application

This is a .NET 8.0 console application that processes customer data from an external API and stores it in a local database.

## Project Structure

The solution follows a clean architecture approach and consists of the following projects:

1. CustomerApiConsoleApp.Domain
2. CustomerApiConsoleApp.Application
3. CustomerApiConsoleApp.Infrastructure
4. CustomerApiConsoleApp.Presentation
5. CustomerApiConsoleApp.Test

## Features

- Fetches customer data from an external API
- Processes and stores customer data in a local database
- Implements logging using NLog
- Uses Entity Framework Core for database operations
- Follows SOLID principles and clean architecture

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- SQL Server (LocalDB or a full instance)

### Configuration

1. Update the connection string in `appsettings.json`:

