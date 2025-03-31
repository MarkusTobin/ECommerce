info:
  title: ECommerce API
  version: 1.0.0
  description: |
    This API provides authentication, customer management, product management, and order processing functionality.
    
    ## Overview
    The ECommerce API allows managing users, customers, products, and orders. It includes authentication and authorization mechanisms.
    
    ## Authentication & Authorization
    This API uses JWT (JSON Web Tokens) for authentication. Clients must include a valid token in the `Authorization` header:
    
    ```
    Authorization: Bearer <token>
    ```
    
    ## Contact Information
    - **Support Email:** placeholder@placeholder.place

paths:
  /api/auth/login:
    post:
      summary: Authenticate user and generate JWT token
      description: |
        Validates user credentials and returns a JWT token if authentication is successful.
        This token must be included in the `Authorization` header for protected endpoints.
      operationId: loginAndAuth
      requestBody:
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/UserLoginDto'
      responses:
        "200":
          description: Successful authentication
          content:
            application/json:
              schema:
                type: object
                properties:
                  token:
                    type: string
                    description: JWT authentication token
        "401":
          description: Unauthorized - Invalid credentials
        "500":
          description: Internal server error

  /api/customers:
    get:
      summary: Retrieve all customers
      description: Returns a list of all registered customers.
      operationId: getAllCustomers
      responses:
        "200":
          description: A list of customers
          content:
            application/json:
              schema:
                type: array
                items:
                  $ref: '#/components/schemas/CustomerDto'
    post:
      summary: Create a new customer
      description: Adds a new customer to the system.
      operationId: createCustomer
      requestBody:
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/CustomerDto'
      responses:
        "201":
          description: Customer successfully created
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/CustomerDto'

  /api/customers/{id}:
    get:
      summary: Get a customer by ID
      description: Retrieves a customer's details using their unique ID.
      operationId: getCustomerById
      parameters:
        - name: id
          in: path
          required: true
          schema:
            type: string
      responses:
        "200":
          description: Customer details
          content:
            application/json:
              schema:
                $ref: '#/components/schemas/CustomerDto'
        "404":
          description: Customer not found

  /api/products:
    get:
      summary: Retrieve all products
      description: Returns a list of all available products.
      operationId: getAllProducts
      responses:
        "200":
          description: A list of products
    post:
      summary: Create a new product
      description: Adds a new product to the inventory.
      operationId: createProduct
      requestBody:
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/ProductDto'
      responses:
        "201":
          description: Product successfully created
      security:
     ## - AdminAuth: is required for this action

  /api/products/{id}:
    get:
      summary: Get a product by ID
      description: Retrieves details of a specific product by its ID.
      operationId: getProductById
      parameters:
        - name: id
          in: path
          required: true
          schema:
            type: string
      responses:
        "200":
          description: Product details
        "404":
          description: Product not found
    put:
      summary: Update an existing product
      description: Updates a product's details using its ID.
      operationId: updateProduct
      parameters:
        - name: id
          in: path
          required: true
          schema:
            type: string
      requestBody:
        required: true
        content:
          application/json:
            schema:
              $ref: '#/components/schemas/ProductDto'
      responses:
        "200":
          description: Product updated successfully
        "404":
          description: Product not found
    delete:
      summary: Delete a product by ID
      description: Removes a product from the inventory.
      operationId: deleteProduct
      parameters:
        - name: id
          in: path
          required: true
          schema:
            type: string
      responses:
        "200":
          description: Product deleted successfully
        "404":
          description: Product not found

components:
  schemas:
    UserLoginDto:
      type: object
      properties:
        email:
          type: string
          description: User's email address
        password:
          type: string
          description: User's password
    CustomerDto:
      type: object
      properties:
        firstName:
          type: string
          description: Customer's first name
        lastName:
          type: string
          description: Customer's last name
        email:
          type: string
          description: Customer's email address
    ProductDto:
      type: object
      properties:
        name:
          type: string
          description: Product name
        productNumber:
          type: string
          description: Unique identifier for the product

  responses:
    UnauthorizedError:
      description: Access token is missing or invalid
    NotFoundError:
      description: The specified resource was not found
    BadRequestError:
      description: The request contains invalid data