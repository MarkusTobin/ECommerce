# ECommerce API Specification

## Overview & Metadata

- **API Name**: ECommerce API
- **Version**: v1
- **Contact**: [support@example.com](mailto:support@example.com)

---

## Authentication & Authorization

This API uses **JWT (JSON Web Token)** for authentication.

### Supported Authentication Methods:
- Bearer Tokens (JWT)
- Example Request with Authorization header:
    ```
    Authorization: Bearer <your-jwt-token>
    ```

---

## API Endpoints and HTTP Methods

Below is a list of the available API endpoints along with the supported HTTP methods (GET, POST, PUT, DELETE), request structures, and example responses.

---

### Authentication Endpoints

#### POST /api/Auth/login
- **Description**: Login using user credentials.
- **Request Body**:
    ```json
    {
      "email": "user@example.com",
      "password": "password123"
    }
    ```
- **Response**:
    - **200 OK**: Success.
    - **401 Unauthorized**: Invalid credentials.

---

### Customer Endpoints

#### GET /api/customers
- **Description**: Retrieve all customers.
- **Response**:
    - **200 OK**: List of customers.

#### POST /api/customers
- **Description**: Create a new customer.
- **Request Body**:
    ```json
    {
      "firstName": "John",
      "lastName": "Doe",
      "email": "johndoe@example.com",
      "phoneNumber": "123456789",
      "address": "123 Street",
      "userId": "1234"
    }
    ```
- **Response**:
    - **200 OK**: Customer created successfully.
    - **400 Bad Request**: Missing required fields.

#### GET /api/customers/{id}
- **Description**: Retrieve customer by ID.
- **Path Parameter**:
    - `id`: The ID of the customer.
- **Response**:
    - **200 OK**: Customer details.
    - **404 Not Found**: Customer not found.

#### PUT /api/customers/{id}
- **Description**: Update customer information.
- **Path Parameter**:
    - `id`: The ID of the customer.
- **Request Body**:
    ```json
    {
      "firstName": "Jane",
      "lastName": "Doe",
      "email": "janedoe@example.com",
      "phoneNumber": "987654321",
      "address": "456 Avenue"
    }
    ```
- **Response**:
    - **200 OK**: Customer updated successfully.

#### DELETE /api/customers/{id}
- **Description**: Delete a customer by ID.
- **Path Parameter**:
    - `id`: The ID of the customer.
- **Response**:
    - **200 OK**: Customer deleted.
    - **404 Not Found**: Customer not found.

#### GET /api/customers/search/{email}
- **Description**: Search for a customer by email.
- **Path Parameter**:
    - `email`: The email address of the customer.
- **Response**:
    - **200 OK**: Customer found.

---

### Order Endpoints

#### GET /api/orders
- **Description**: Retrieve all orders.
- **Response**:
    - **200 OK**: List of orders.

#### POST /api/orders
- **Description**: Create a new order.
- **Request Body**:
    ```json
    {
      "customerId": "1234",
      "customerFullName": "John Doe",
      "customerEmail": "johndoe@example.com",
      "orderDate": "2025-04-01T12:00:00",
      "totalPrice": 100.0,
      "orderDetails": [
        {
          "productId": "5678",
          "productName": "Laptop",
          "quantity": 1,
          "unitPrice": 1000.0
        }
      ]
    }
    ```
- **Response**:
    - **200 OK**: Order created successfully.

#### GET /api/orders/{id}
- **Description**: Retrieve order details by ID.
- **Path Parameter**:
    - `id`: The ID of the order.
- **Response**:
    - **200 OK**: Order details.

#### GET /api/orders/customer/{customerId}
- **Description**: Retrieve all orders for a specific customer.
- **Path Parameter**:
    - `customerId`: The ID of the customer.
- **Response**:
    - **200 OK**: List of orders for the customer.

---

### Product Endpoints

#### GET /api/products
- **Description**: Retrieve all products.
- **Response**:
    - **200 OK**: List of products.

#### POST /api/products
- **Description**: Add a new product.
- **Authorization**: Requires `Admin` role.
- **Request Body**:
    ```json
    {
      "name": "Smartphone",
      "productNumber": "12345",
      "price": 499.99,
      "productDescription": "Latest model smartphone",
      "productCategory": "Electronics",
      "quantity": 50,
      "isAvailable": true
    }
    ```
- **Response**:
    - **200 OK**: Product added successfully.

#### PUT /api/products/{id}
- **Description**: Update product details.
- **Path Parameter**:
    - `id`: The ID of the product.
- **Request Body**:
    ```json
    {
      "name": "Smartphone",
      "price": 499.99,
      "productDescription": "Updated model smartphone",
      "quantity": 45
    }
    ```
- **Response**:
    - **200 OK**: Product updated successfully.

#### DELETE /api/products/{id}
- **Description**: Delete a product by ID.
- **Path Parameter**:
    - `id`: The ID of the product.
- **Response**:
    - **200 OK**: Product deleted successfully.

---

### User Endpoints

#### POST /api/User/register
- **Description**: Register a new user.
- **Request Body**:
    ```json
    {
      "email": "user@example.com",
      "password": "password123",
      "role": "Admin"
    }
    ```
- **Response**:
    - **200 OK**: User registered successfully.

#### GET /api/User/{id}
- **Description**: Get user details by ID.
- **Path Parameter**:
    - `id`: The ID of the user.
- **Response**:
    - **200 OK**: User details.

#### PUT /api/User/{id}
- **Description**: Update user information.
- **Path Parameter**:
    - `id`: The ID of the user.
- **Request Body**:
    ```json
    {
      "email": "updateduser@example.com",
      "role": "User"
    }
    ```
- **Response**:
    - **200 OK**: User updated successfully.

#### DELETE /api/User/{id}
- **Description**: Delete a user by ID.
- **Path Parameter**:
    - `id`: The ID of the user.
- **Response**:
    - **200 OK**: User deleted successfully.

---

## Request Structure

Each request must include the proper **Content-Type** header based on the API’s endpoint:
- **application/json** for JSON payloads.
- **text/json** for plain JSON content.

For endpoints that require parameters, such as `GET /api/customers/{id}`, provide them in the URL path.

---

## Response Structure

A successful response will return a **200 OK** status with a relevant payload. For example:

```json
{
  "id": "123",
  "firstName": "John",
  "lastName": "Doe",
  "email": "johndoe@example.com"
}

Example Errors
400 Bad Request: The request was malformed or missing required fields.

401 Unauthorized: Missing or invalid authentication token.

404 Not Found: The requested resource could not be found.

500 Internal Server Error: Something went wrong on the server.