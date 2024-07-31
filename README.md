# Bookstore API

This project is a .NET 8 based API for managing books and authors. The API supports various operations related to book and author management.

## Table of Contents

- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Endpoints](#endpoints)
- [DTOs](#dtos)
- [Models and Data Structures](#models-and-data-structures)
- [AutoMapper and FluentValidation](#automapper-and-fluentvalidation)
- [Installation and Running](#installation-and-running)

## Getting Started

This project provides a simple API to manage books and authors. You can perform the following operations:

- Add, update, delete, list, and get specific authors
- Add, update, delete, list, and get specific books

## Project Structure

The project has the following file and folder structure:

- **Controllers**
  - `BookController.cs`: Manages operations related to books.
  - `AuthorController.cs`: Manages operations related to authors.
- **Models**
  - `Book.cs`: Defines the book model.
  - `Author.cs`: Defines the author model.
- **DTOs**
  - `AuthorDto.cs`: Defines the data transfer object for authors.
  - `AuthorCreateDto.cs`: DTO for creating a new author.
  - `AuthorUpdateDto.cs`: DTO for updating an existing author.
- **Profiles**
  - `MappingProfile.cs`: Configures AutoMapper for entity to DTO mapping.
- **Validators**
  - `AuthorCreateValidator.cs`: FluentValidation rules for author creation.
  - `AuthorUpdateValidator.cs`: FluentValidation rules for author updates.

## Endpoints

### Books

- **GET /books**: Retrieves a list of all books.
- **GET /books/{id}**: Retrieves a specific book by its ID.
- **POST /books**: Adds a new book.
- **PUT /books/{id}**: Updates an existing book.
- **DELETE /books/{id}**: Deletes a book by its ID.

### Authors

- **GET /authors**: Retrieves a list of all authors.
- **GET /authors/{id}**: Retrieves a specific author by ID.
- **POST /authors**: Adds a new author.
- **PUT /authors/{id}**: Updates an existing author.
- **DELETE /authors/{id}**: Deletes an author by ID.

## DTOs

- **AuthorDto**: Represents author data used for reading operations.
- **AuthorCreateDto**: Represents author data used for creating a new author.
- **AuthorUpdateDto**: Represents author data used for updating an existing author.

## Models and Data Structures

- **Book**: Represents the book entity.
  - `Id`: Unique identifier for the book.
  - `Title`: Title of the book.
  - `AuthorId`: ID of the author.
  - `GenreId`: ID of the genre.
  - `PageCount`: Number of pages in the book.
  - `PublishDate`: Publish date of the book.

- **Author**: Represents the author entity.
  - `Id`: Unique identifier for the author.
  - `FirstName`: First name of the author.
  - `LastName`: Last name of the author.
  - `BirthDate`: Birthdate of the author.

## AutoMapper and FluentValidation

- **AutoMapper** is used for mapping between entities and DTOs.
- **FluentValidation** is used to enforce validation rules for DTOs.

## Installation and Running

1. **Clone the Repository**

   ```sh
   git clone https://github.com/ksensazli/P324-Week4-Homework
   cd bookstore-api
   ```

2. **Restore Dependencies**

   ```sh
   dotnet restore
   ```

3. **Run the Application**

   ```sh
   dotnet run
   ```
