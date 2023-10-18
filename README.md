# Coding Challenge

## Overview

This project is a full-stack web application developed using .NET Core, Entity Framework, Angular, and NgRx for state management. It follows a code-first approach to database management.

## Prerequisites

Before running this project, make sure you have the following prerequisites installed:

- [Visual Studio](https://visualstudio.microsoft.com/): You will need Visual Studio to run the .NET Core API project.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads): You will need SQL Server to create a local database for the project.

## Getting Started

To get started with this project, follow these steps:

1. **Clone the repository to your local machine.**

   ```bash
   git clone https://github.com/dhaval16492/codingchallenge.git

2. **Open the solution in Visual Studio:**

  - Navigate to the CodingChallenge solution.
  - Select the CodingChallenge project as the startup project.


3. **Create the Database:**

  - Open the Package Manager Console in Visual Studio.

  - Select the **CodingChallenge.Infra** project as the default project in the Package Manager Console.
    
  - Run the following commands to create and apply the initial migration:
  ```bash
  add-migration initial-migration
  update-database
  ```
 
4. **Run the Project:**
  - Set the CodingChallenge project as the startup project.
  - Press F5 or use the "Start" button in Visual Studio to run the project.

