# Vending Machine API

This repository contains the implementation of a RESTful API for a vending machine. The API allows users with different roles to perform various actions such as managing products, making deposits, and purchasing items from the vending machine.

## Table of Contents

- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Architecture](#architecture)
- [Authentication and Authorization](#authentication-and-authorization)
- [Validation](#validation)
- [Database](#database)
- [Testing](#testing)

## Getting Started

To run the API and test its functionalities, follow these steps:

1. Clone the repository
2. Navigate to the project directory
3. Install the dependencies
4. Apply database migrations
5. Run the APIs

The API should now be running and accessible at the specified URL.

## API Documentation

The API endpoints and their functionalities are documented below.

### User Endpoints

- `POST /User/register`: Register a new user (buyer or seller).
- `POST /User/GetUser`: Authenticate and login a user. Returns token to use it in the other APIs.
- `PUT /User/UpdatePassword`: Change the user's password (requires authentication).
- `PUT /User/UpdateUser`: Update the user information(requires authentication and authorization).
- `DELETE /User/DeleteUser`: Delete a user (requires authentication and authorization).

### Product Endpoints

- `GET /product/GetAllProducts`: Get all products.
- `POST /product/AddProduct`: Create a new product (requires authentication and seller role).
- `PUT /products/UpdateProduct`: Update a product (requires authentication and seller role).
- `DELETE /products/DeleteProduct`: Delete a product (requires authentication and seller role).

### Vending Machine Endpoints

- `POST /BuyerResetDeposit/deposit`: Deposit coins into the vending machine account (requires authentication and buyer role).
- `POST /BuyerResetDeposit/buy`: Buy products from the vending machine (requires authentication and buyer role).
- `POST /BuyerResetDeposit/reset`: Reset the deposit in the vending machine account (requires authentication and buyer role).

## Architecture

The project follows a 3-tier architecture, consisting of the following layers:

1. **Data Access Layer (DAL)**: This layer handles data access and database operations. It includes the database context, entity models, and repositories for interacting with the database.

2. **Business Logic Layer (BLL)**: The BLL contains the business logic and rules of the application. It manages the operations and workflows related to users, products, and the vending machine.

3. **Controllers**: The controllers layer handles the API endpoints and acts as an interface between the client and the application. It receives requests, processes them using the BLL, and sends back the appropriate responses.

## Authentication and Authorization

The API uses JWT (JSON Web Tokens) for authentication and authorization. Users can register and login to obtain an access token, which they can include in the authorization header for authenticated API requests. The API differentiates between users with "buyer" and "seller" roles, allowing certain actions to be performed only by users with the appropriate role.

## Validation

The API includes validation mechanisms to ensure the integrity and validity of the data. Data annotations and custom attributes have been used to enforce constraints and validate input values. For example, the deposit amount must be a multiple of 5, 10, 50, or 100 cents.

## Database

The project utilizes a code-first approach for database management. The database schema is automatically generated based on the entity models and their relationships. Entity Framework Core is used as the ORM (Object-Relational Mapping) tool for interacting with the database.


## Testing

To test the API endpoints, you can use tools like Postman or any other HTTP client. Start by registering a buyer and a seller user. Then, authenticate and obtain an access token for the desired user. Include the access token in the authorization header for authenticated API requests.

Feel free to explore and test the available endpoints according to the provided API documentation.

Enjoy using the Vending Machine API!
