# 📘 API Documentation

## Table of Contents
- [Authentication](#authentication)
- [Users](#users)
- [Roles](#roles)

## Authentication

Base URL: `/api/v1/auth`

### Register New User
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

### Authenticate
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

### Forgot Password
- **POST** `/forgot-password`
- **Description**: Request password reset link
- **Request Body**:
  ```json
  {
    "emailAddress": "string"
  }
  ```
- **Response**: 200 OK with success message

### Validate Reset Password Token
- **GET** `/reset-password/validate`
- **Description**: Validate password reset token
- **Query Parameters**: `token`
- **Response**: 200 OK with TokenValidateDto

### Reset Password
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

### Verify Email Address
- **POST** `/verification-email-address`
- **Description**: Verify email address using token
- **Request Body**:
  ```json
  {
    "token": "string"
  }
  ```
- **Response**: 200 OK with boolean result

### Logout
- **POST** `/logout`
- **Description**: Logout current user
- **Authentication**: Required
- **Response**: 200 OK with boolean result

## Users

Base URL: `/api/v1/users`

### Get User by ID
- **GET** `/{id}`
- **Description**: Get user details by ID
- **Parameters**: 
  - `id`: User ID or 'me' for current user
- **Authentication**: Required
- **Response**: 200 OK with UserDto

### Change Password
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

### Update User
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

### Send Verification Email
- **POST** `/{id}/send-verification-email`
- **Description**: Send email verification link
- **Authentication**: Required
- **Response**: 200 OK with EmailVerificationSendDto

### Delete User
- **DELETE** `/{id}`
- **Description**: Delete user account
- **Authentication**: Required (Admin only)
- **Response**: 204 No Content

### Get Users List
- **GET** `/`
- **Description**: Get paginated list of users
- **Authentication**: Required
- **Query Parameters**:
  - `filter`: Optional search filter
  - `pageNumber`: Page number (default: 1)
  - `pageSize`: Items per page (default: 10)
- **Response**: 200 OK with PaginatedList<UserDto>

### Get User Roles
- **GET** `/{id}/roles`
- **Description**: Get roles assigned to user
- **Authentication**: Required
- **Response**: 200 OK with list of RoleDto

### Assign Role to User
- **POST** `/{userId}/roles/{roleId}`
- **Description**: Assign role to user
- **Authentication**: Required (Admin only)
- **Response**: 200 OK with boolean result

### Remove Roles from User
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

## Roles

Base URL: `/api/v1/roles`

### Create Role
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

### Get Role by ID
- **GET** `/{id}`
- **Description**: Get role details
- **Authentication**: Required
- **Response**: 200 OK with RoleDto

### Get Users by Role
- **GET** `/{id}/users`
- **Description**: Get users assigned to role
- **Authentication**: Required
- **Query Parameters**:
  - `pageNumber`: Page number (default: 1)
  - `pageSize`: Items per page (default: 10)
- **Response**: 200 OK with PaginatedList<UserDto>

### Get Roles List
- **GET** `/`
- **Description**: Get paginated list of roles
- **Authentication**: Required
- **Query Parameters**:
  - `filter`: Optional search filter
  - `pageNumber`: Page number (default: 1)
  - `pageSize`: Items per page (default: 10)
- **Response**: 200 OK with PaginatedList<RoleDto>

### Update Role
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

### Delete Role
- **DELETE** `/{id}`
- **Description**: Delete role
- **Authentication**: Required (Admin only)
- **Response**: 204 No Content

### Assign User to Role
- **POST** `/{roleId}/users/{userId}`
- **Description**: Assign user to role
- **Authentication**: Required (Admin only)
- **Response**: 200 OK with boolean result

### Assign Permissions to Role
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

### Get Role Permissions
- **GET** `/{roleId}/permissions`
- **Description**: Get permissions assigned to role
- **Query Parameters**:
  - `pageNumber`: Page number
  - `pageSize`: Items per page
- **Response**: 200 OK with PaginatedList<PermissionDto>

### Remove Permissions from Role
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

