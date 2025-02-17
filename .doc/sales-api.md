[Back to README](../README.md)

### Sales

#### Overview

The Sales API allows managing sales transactions, including creating, retrieving, updating, and deleting sales and sale items.

#### Endpoints

#### POST /sales

 - Description: Creates a new sale record.

 - Request Body:

```json
{
  "date": "2024-02-17T12:00:00Z",
  "customer": "John Doe",
  "branch": "Main Store",
  "items": [
    {
      "product": "Laptop",
      "quantity": 1,
      "unitPrice": 1500.00,
      "discount": 0,
      "isCancelled": false
    }
  ],
  "totalAmount": 1500.00,
  "isCancelled": false
}

 - Responses:

 - 201 Created - Sale successfully created.
 - 400 Bad Request - Validation error.
```
#### GET /sales/{id}

 - Description: Retrieves a sale by its ID.

 - Path Parameters:

 - id (UUID) - The ID of the sale to retrieve.

 - Responses:

 - 200 OK - Returns sale details.
 - 404 Not Found - Sale not found.

 - Response Example:

 ```json
{
  "id": "550e8400-e29b-41d4-a716-446655440000",
  "date": "2024-02-17T12:00:00Z",
  "customer": "John Doe",
  "branch": "Main Store",
  "items": [
    {
      "id": "660e8400-e29b-41d4-a716-446655440000",
      "product": "Laptop",
      "quantity": 1,
      "unitPrice": 1500.00,
      "discount": 0,
      "isCancelled": false
    }
  ],
  "totalAmount": 1500.00,
  "isCancelled": false
}
```
#### GET /sales

 - Description: Retrieves all sales with optional pagination.

 - Query Parameters:

 - skip (int, optional) - Number of records to skip.
 - take (int, optional) - Number of records to retrieve.

 - Responses:

 - 200 OK - Returns a list of sales.
 - 404 Not Found - No sales found.


#### PUT /sales/{id}

 - Description: Updates an existing sale.

 - Path Parameters:

 - id (UUID) - The ID of the sale to update.

 - Request Body:

```json
{
  "date": "2024-02-17T12:00:00Z",
  "customer": "John Doe",
  "branch": "Main Store",
  "items": [
    {
      "product": "Laptop",
      "quantity": 2,
      "unitPrice": 1400.00,
      "discount": 100.00,
      "isCancelled": false
    }
  ],
  "totalAmount": 2800.00,
  "isCancelled": false
}
```
 - Responses:

 - 204 No Content - Sale updated successfully.
 - 400 Bad Request - Validation error.

#### DELETE /sales/{id}

 - Description: Marks a sale as cancelled instead of physically deleting it.

 - Path Parameters:

 - id (UUID) - The ID of the sale to cancel.
 - Responses:

 - 200 OK - Sale cancelled successfully.
 - 400 Bad Request - Error occurred while cancelling the sale.


#### DELETE /sales/{saleId}/items/{itemId}

 - Description: Cancels an item from a sale.

 - Path Parameters:

 - saleId (UUID) - The ID of the sale.
 - itemId (UUID) - The ID of the item to cancel.
 - Responses:

 - 200 OK - Sale item cancelled successfully.
 - 400 Bad Request - Error occurred while cancelling the sale item.

#### Models
 - CreateSaleRequest

```json
{
  "date": "DateTime",
  "customer": "string",
  "branch": "string",
  "items": [
    {
      "product": "string",
      "quantity": "int",
      "unitPrice": "decimal",
      "discount": "decimal",
      "isCancelled": "bool"
    }
  ],
  "totalAmount": "decimal",
  "isCancelled": "bool"
}
```

 - GetSaleResponse

```json
{
  "id": "UUID",
  "date": "DateTime",
  "customer": "string",
  "branch": "string",
  "items": [
    {
      "id": "UUID",
      "product": "string",
      "quantity": "int",
      "unitPrice": "decimal",
      "discount": "decimal",
      "isCancelled": "bool"
    }
  ],
  "totalAmount": "decimal",
  "isCancelled": "bool"
}
```

 - GetSalesResponse

```json
{
  "sales": [
    {
      "id": "UUID",
      "date": "DateTime",
      "customer": "string",
      "branch": "string",
      "totalAmount": "decimal",
      "isCancelled": "bool"
    }
  ]
}
```

<br/>
<div style="display: flex; justify-content: space-between;">
  <a href="./project-structure.md">Previous: Project Structure</a>
  <a href="./tech-stack.md">Next: Tech Stack</a>
</div>