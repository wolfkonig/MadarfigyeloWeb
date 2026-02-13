# ASP.NET Core Identity + JWT Authentication - Implementation Guide

## Overview
ASP.NET Core Identity has been successfully integrated with JWT Bearer token authentication for the MadarfigyeloWeb API.

## What Was Added

### 1. NuGet Packages
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (8.0.11)
- `Microsoft.AspNetCore.Authentication.JwtBearer` (8.0.11)
- `System.IdentityModel.Tokens.Jwt` (8.2.1)

### 2. New Files Created
- **Models/ApplicationUser.cs** - Custom user model extending IdentityUser
- **Models/DTOs/AuthDtos.cs** - DTOs for registration, login, and auth responses
- **API/AuthController.cs** - Authentication endpoints for register/login

### 3. Modified Files
- **Data/TerepnaploContext.cs** - Now inherits from `IdentityDbContext<ApplicationUser>`
- **Program.cs** - Configured Identity and JWT authentication
- **appsettings.json** - Added JWT configuration section
- **API Controllers** - All API controllers now have `[Authorize]` attribute

### 4. Database Migration
- Migration "AddIdentity" created (needs to be applied to database)

## Configuration

### JWT Settings (appsettings.json)
```json
"Jwt": {
  "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
  "Issuer": "MadarfigyeloWeb",
  "Audience": "MadarfigyeloWebUsers",
  "DurationInMinutes": 60
}
```

⚠️ **IMPORTANT**: Change the JWT Key in production to a secure, randomly generated value!

## API Endpoints

### Authentication Endpoints (No Authorization Required)

#### Register New User
```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "Password123!",
  "firstName": "John",
  "lastName": "Doe"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiration": "2025-02-13T18:30:00Z",
  "email": "user@example.com"
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "Password123!"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "expiration": "2025-02-13T18:30:00Z",
  "email": "user@example.com"
}
```

### Protected API Endpoints (Require Authorization)

All existing API endpoints now require a valid JWT token:
- `/api/odutelep` - All CRUD operations
- `/api/odu` - All CRUD operations
- `/api/latogatas` - All CRUD operations

#### Example Protected Request
```http
GET /api/odutelep
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

## Password Requirements

The default password policy requires:
- Minimum 6 characters
- At least one digit
- At least one lowercase letter
- At least one uppercase letter
- Non-alphanumeric characters are NOT required

You can modify these in `Program.cs` in the `AddIdentity` configuration.

## Next Steps

### 1. Apply Database Migration
Run the following command to update your database with Identity tables:
```bash
dotnet ef database update
```

### 2. Test Authentication
1. Use a tool like Postman, Insomnia, or curl to test the endpoints
2. Register a new user via `/api/auth/register`
3. Copy the returned JWT token
4. Use the token in the `Authorization: Bearer <token>` header for protected endpoints

### 3. Optional Enhancements

Consider adding:
- **Role-based authorization** - Add roles like "Admin", "User", etc.
- **Refresh tokens** - For longer-lived sessions
- **Email confirmation** - Require email verification on registration
- **Password reset** - Via email
- **User profile endpoints** - GET/UPDATE user information
- **CORS policy** - If you'll access the API from a web frontend
- **Rate limiting** - Prevent brute force attacks on login

### 4. Production Security

Before deploying to production:
1. **Change the JWT Key** in appsettings.json to a secure random value
2. Store secrets in **Azure Key Vault** or **User Secrets** (not in appsettings.json)
3. Enable **HTTPS only**
4. Consider implementing **refresh tokens**
5. Add **rate limiting** on authentication endpoints
6. Enable **account lockout** after failed login attempts
7. Implement **logging and monitoring** for security events

## Testing with Postman/Insomnia

### 1. Register a User
- Method: POST
- URL: `https://localhost:7xxx/api/auth/register`
- Body (JSON):
```json
{
  "email": "test@test.com",
  "password": "Test123!",
  "firstName": "Test",
  "lastName": "User"
}
```

### 2. Copy the Token from Response

### 3. Use Token for Protected Endpoints
- Method: GET (or POST/PUT/DELETE)
- URL: `https://localhost:7xxx/api/odutelep`
- Headers:
  - Key: `Authorization`
  - Value: `Bearer <paste-token-here>`

## Troubleshooting

### "401 Unauthorized" on API calls
- Ensure you're sending the token in the Authorization header
- Check that the token hasn't expired (default: 60 minutes)
- Verify the token format: `Bearer <token>`

### "Database update failed"
- Make sure to run `dotnet ef database update` to apply migrations
- Check your connection string in appsettings.json

### Password validation errors
- Ensure password meets the requirements (min 6 chars, upper, lower, digit)
- Modify password policy in Program.cs if needed

## Architecture

```
User Registration/Login
        ↓
   AuthController
        ↓
   UserManager/SignInManager (Identity)
        ↓
   JWT Token Generation
        ↓
   Return Token to Client
        ↓
Client stores token
        ↓
Client sends token with API requests
        ↓
JWT Middleware validates token
        ↓
Protected API Controller processes request
```

## Database Tables Added by Identity

The Identity migration adds these tables:
- `AspNetUsers` - User accounts
- `AspNetRoles` - Roles (if using role-based authorization)
- `AspNetUserRoles` - User-role relationships
- `AspNetUserClaims` - Custom user claims
- `AspNetUserLogins` - External login providers (Google, Facebook, etc.)
- `AspNetUserTokens` - Authentication tokens
- `AspNetRoleClaims` - Role-based claims

Your existing tables (Odutelep, Odu, Latogatas) remain unchanged.
