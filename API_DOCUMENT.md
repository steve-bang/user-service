# 📘 API Documentation

## Table of Contents
- [API Reference](#api-reference)
- [Authentication](#authentication)
- [Pagination](#pagination)
- [Error Handling](#error-handling)
- [Rate Limiting](#rate-limiting)
- [Endpoints](#endpoints)
  - [Authentication Endpoints](#authentication-endpoints)
  - [User Management](#user-management)
  - [Role Management](#role-management)

## API Reference

### Base URL
```
https://your-domain/api/v1
```

### API Versioning
The API uses URL versioning. The current version is v1, which is included in the base URL.

### Content Types
- All requests must use `Content-Type: application/json`
- All responses are returned in JSON format

### Date Format
All dates in the API are returned in ISO 8601 format:
```
YYYY-MM-DDTHH:mm:ss.sssZ
```

## Authentication

### JWT Authentication
The API uses JWT (JSON Web Tokens) for authentication. To authenticate:

1. Obtain a token by calling the `/auth/authenticate` endpoint
2. Include the token in the Authorization header of subsequent requests:
```
Authorization: Bearer <your_token>
```

### Token Expiration
- Access tokens expire after 1 hour
- Refresh tokens expire after 7 days
- When a token expires, you'll receive a 401 Unauthorized response

### Security Best Practices
- Always use HTTPS
- Never store tokens in client-side storage
- Implement token refresh mechanism
- Log out when the user closes the application

## Pagination

### Pagination Parameters
All list endpoints support pagination with the following query parameters:

- `pageNumber`: The page number to retrieve (default: 1)
- `pageSize`: Number of items per page (default: 10, max: 100)

### Pagination Response
Paginated responses include the following metadata:

```json
{
  "success": true,
  "statusCode": 200,
  "message": "The request was success.",
  "data": {
    "items": [...],
    "totalCount": 100
  }
}
```

## Error Handling

### Error Response Format
All error responses follow this format:

```json
{
  "success": false,
  "statusCode": 400,
  "message": "The request was failure.",
  "code": "User.Email_Address_Already_Exists",
  "summary": "The email address already exists in the system. Please try another email address."
}
```

### Success Response Format
All success responses follow this format:

```json
{
  "success": true,
  "statusCode": 200,
  "message": "The request was success.",
  "data": {
    // Response data here
  }
}
```

### HTTP Status Codes
- `200 OK`: Request succeeded
- `201 Created`: Resource created successfully
- `204 No Content`: Request succeeded, no response body
- `400 Bad Request`: Invalid request parameters
- `401 Unauthorized`: Authentication required
- `403 Forbidden`: Insufficient permissions
- `404 Not Found`: Resource not found
- `500 Internal Server Error`: Server error

### Common Error Codes
| Code | Description | HTTP Status |
|------|-------------|-------------|
| `Input_Invalid` | Invalid input parameters | 400 |
| `token_invalid` | Token expired or invalid | 400 |
| `User.Email_Address_Already_Exists` | Email address already exists | 400 |
| `User.Login_Password_Failed` | Invalid email or password | 400 |
| `User.Not_Found` | User not found | 404 |
| `User.Password_Incorrect` | Incorrect password | 400 |
| `user.already_has_role` | User already has this role | 400 |
| `User.Email_already_verified` | Email already verified | 400 |
| `role.not_found` | Role not found | 404 |
| `permission.not_found` | Permission not found | 404 |

## Rate Limiting

### Rate Limits
- 100 requests per minute per IP address
- 1000 requests per hour per user
- 10000 requests per day per API key

### Rate Limit Headers
The following headers are included in all responses:

```
X-RateLimit-Limit: 100
X-RateLimit-Remaining: 95
X-RateLimit-Reset: 1619123456
```

### Rate Limit Exceeded
When rate limit is exceeded, the API returns:
- Status code: 429 Too Many Requests
- Retry-After header with seconds to wait

## Endpoints

### Authentication Endpoints

Base URL: `/api/v1/auth`

#### Register New User
- **POST** `/register`
- **Description**: Register a new user account
- **Request Body**:
  ```json
  {
    "emailAddress": "string",
    "password": "string",
    "confirmPassword": "string",
    "firstName": "string",
    "lastName": "string"
  }
  ```
- **Response**: 201 Created with UserDto

#### Authenticate
- **POST** `/authenticate`
- **Description**: Authenticate user with email and password
- **Request Body**:
  ```json
  {
    "emailAddress": "string",
    "password": "string"
  }
  ```
- **Response**: 200 OK with AuthenticationResponseDto

#### Forgot Password
- **POST** `/forgot-password`
- **Description**: Request password reset link
- **Request Body**:
  ```json
  {
    "emailAddress": "string"
  }
  ```
- **Response**: 200 OK with success message

#### Validate Reset Password Token
- **GET** `/reset-password/validate`
- **Description**: Validate password reset token
- **Query Parameters**: `token`
- **Response**: 200 OK with TokenValidateDto

#### Reset Password
- **POST** `/reset-password`
- **Description**: Reset password using token
- **Request Body**:
  ```json
  {
    "token": "string",
    "newPassword": "string",
    "confirmPassword": "string"
  }
  ```
- **Response**: 200 OK with boolean result

#### Verify Email Address
- **POST** `/verification-email-address`
- **Description**: Verify email address using token
- **Request Body**:
  ```json
  {
    "token": "string"
  }
  ```
- **Response**: 200 OK with boolean result

#### Logout
- **POST** `/logout`
- **Description**: Logout current user
- **Authentication**: Required
- **Response**: 200 OK with boolean result

### User Management

Base URL: `/api/v1/users`

#### Get User by ID
- **GET** `/{id}`
- **Description**: Get user details by ID
- **Parameters**: 
  - `id`: User ID or 'me' for current user
- **Authentication**: Required
- **Response**: 200 OK with UserDto

#### Change Password
- **PUT** `/{id}/change-password`
- **Description**: Change user password
- **Authentication**: Required
- **Request Body**:
  ```json
  {
    "currentPassword": "string",
    "newPassword": "string",
    "confirmPassword": "string"
  }
  ```
- **Response**: 200 OK with boolean result

#### Update User
- **PUT** `/{id}`
- **Description**: Update user details
- **Authentication**: Required
- **Request Body**:
  ```json
  {
    "emailAddress": "string",
    "secondaryEmailAddress": "string",
    "firstName": "string",
    "lastName": "string",
    "displayName": "string",
    "phoneNumber": "string",
    "address": "string"
  }
  ```
- **Response**: 200 OK with UserDto

#### Send Verification Email
- **POST** `/{id}/send-verification-email`
- **Description**: Send email verification link
- **Authentication**: Required
- **Response**: 200 OK with EmailVerificationSendDto

#### Delete User
- **DELETE** `/{id}`
- **Description**: Delete user account
- **Authentication**: Required (Admin only)
- **Response**: 204 No Content

#### Get Users List
- **GET** `/`
- **Description**: Get paginated list of users
- **Authentication**: Required
- **Query Parameters**:
  - `filter`: Optional search filter
  - `pageNumber`: Page number (default: 1)
  - `pageSize`: Items per page (default: 10)
- **Response**: 200 OK with PaginatedList<UserDto>

#### Get User Roles
- **GET** `/{id}/roles`
- **Description**: Get roles assigned to user
- **Authentication**: Required
- **Response**: 200 OK with list of RoleDto

#### Assign Role to User
- **POST** `/{userId}/roles/{roleId}`
- **Description**: Assign role to user
- **Authentication**: Required (Admin only)
- **Response**: 200 OK with boolean result

#### Remove Roles from User
- **DELETE** `/{userId}/roles`
- **Description**: Remove roles from user
- **Authentication**: Required (Admin only)
- **Request Body**:
  ```json
  {
    "roleIds": ["guid"]
  }
  ```
- **Response**: 200 OK with boolean result

### Role Management

Base URL: `/api/v1/roles`

#### Create Role
- **POST** `/`
- **Description**: Create new role
- **Authentication**: Required (Admin only)
- **Request Body**:
  ```json
  {
    "name": "string",
    "description": "string"
  }
  ```
- **Response**: 201 Created with RoleDto

#### Get Role by ID
- **GET** `/{id}`
- **Description**: Get role details
- **Authentication**: Required
- **Response**: 200 OK with RoleDto

#### Get Users by Role
- **GET** `/{id}/users`
- **Description**: Get users assigned to role
- **Authentication**: Required
- **Query Parameters**:
  - `pageNumber`: Page number (default: 1)
  - `pageSize`: Items per page (default: 10)
- **Response**: 200 OK with PaginatedList<UserDto>

#### Get Roles List
- **GET** `/`
- **Description**: Get paginated list of roles
- **Authentication**: Required
- **Query Parameters**:
  - `filter`: Optional search filter
  - `pageNumber`: Page number (default: 1)
  - `pageSize`: Items per page (default: 10)
- **Response**: 200 OK with PaginatedList<RoleDto>

#### Update Role
- **PUT** `/{id}`
- **Description**: Update role details
- **Authentication**: Required (Admin only)
- **Request Body**:
  ```json
  {
    "name": "string",
    "description": "string"
  }
  ```
- **Response**: 200 OK with RoleDto

#### Delete Role
- **DELETE** `/{id}`
- **Description**: Delete role
- **Authentication**: Required (Admin only)
- **Response**: 204 No Content

#### Assign User to Role
- **POST** `/{roleId}/users/{userId}`
- **Description**: Assign user to role
- **Authentication**: Required (Admin only)
- **Response**: 200 OK with boolean result

#### Assign Permissions to Role
- **POST** `/{roleId}/permissions`
- **Description**: Assign permissions to role
- **Authentication**: Required (Admin only)
- **Request Body**:
  ```json
  {
    "permissionIds": ["guid"]
  }
  ```
- **Response**: 200 OK with boolean result

#### Get Role Permissions
- **GET** `/{roleId}/permissions`
- **Description**: Get permissions assigned to role
- **Query Parameters**:
  - `pageNumber`: Page number
  - `pageSize`: Items per page
- **Response**: 200 OK with PaginatedList<PermissionDto>

#### Remove Permissions from Role
- **DELETE** `/{roleId}/permissions`
- **Description**: Remove permissions from role
- **Authentication**: Required (Admin only)
- **Request Body**:
  ```json
  {
    "permissionIds": ["guid"]
  }
  ```
- **Response**: 204 No Content

