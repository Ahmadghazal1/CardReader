# Business Card Management System

A modern web application for managing business card information, built using .NET Core for the backend and Angular for the frontend. This application supports features like file import/export, data filtering, and optional QR code integration.

## Overview

The Business Card Management System allows users to create, view, and manage business cards efficiently. Users can import business card data from various formats (XML, CSV, and QR codes) and export the information for further use. The application is designed to follow best practices for clean code and modular design.

## Technologies Used

- **Backend**: .NET Core (.NET 8)
- **Frontend**: Angular (latest version)
- **Database**: SQL Server

## Features

- Create, view, delete, and export business cards.
- Import business cards from XML, CSV, or QR codes.
- Filter business cards by name, date of birth (DOB), phone number, gender, or email.
- Preview business card details before submission.
- Clean and modular code adhering to SOLID principles.

## API Endpoints

### Business Cards

- **POST** `/api/businesscards`  
  Create a new business card. Accepts JSON input or file uploads (XML/CSV).

- **GET** `/api/businesscards`  
  Retrieve all business cards. Supports optional filtering.

- **DELETE** `/api/businesscards/{id}`  
  Delete a business card by ID.

- **GET** `/api/businesscards/export`  
  Export all business cards to CSV or XML format.

## Frontend Functionality

- **Add New Business Card**: A form for user input with support for drag-and-drop or file uploads.
- **List Business Cards**: Displays all stored business cards with options to delete or export.

## Setup Instructions

### Prerequisites

- [.NET SDK 8 or later](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (LTS version recommended)
- [Angular CLI](https://angular.io/cli) (install globally using `npm install -g @angular/cli`)
- A compatible database (SQL Server)

### Clone the Repository

1. Clone the repository to your local machine:
   ```bash
    git clone https://github.com/Ahmadghazal1/CardReader.git
2.Navigate to the backend project folder:
    `cd API`
3. Restore NuGet packages:
   `dotnet restore`
4.Run the migrations to set up the database: 
  `dotnet ef database update
`
5.Navigate to the frontend project folder:
`cd Client`
6.Install the required npm packages:
`npm install
`
7.tart the Angular application:
`ng serve`
