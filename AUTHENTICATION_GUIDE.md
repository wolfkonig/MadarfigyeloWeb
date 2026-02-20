# ASP.NET Core Identity + JWT Authentication - Implementation Guide

## Overview
ASP.NET Core Identity has been successfully integrated with **dual authentication modes**:
- **Cookie-based authentication** for the web application (MVC views)
- **JWT Bearer token authentication** for API endpoints

## What Was Added

### 1. NuGet Packages
- `Microsoft.AspNetCore.Identity.EntityFrameworkCore` (8.0.11)
- `Microsoft.AspNetCore.Authentication.JwtBearer` (8.0.11)
- `System.IdentityModel.Tokens.Jwt` (8.2.1)

### 2. New Files Created

#### Web Authentication
- **Controllers/AccountController.cs** - Web login/register/logout endpoints
- **Models/ViewModels/AccountViewModels.cs** - View models for login/register forms
- **Views/Account/Login.cshtml** - Login page
- **Views/Account/Register.cshtml** - Registration page
- **Views/Account/AccessDenied.cshtml** - Access denied page
- **Views/Shared/_LoginPartial.cshtml** - Login/logout navigation partial

#### API Authentication
- **Models/ApplicationUser.cs** - Custom user model extending IdentityUser
- **Models/DTOs/AuthDtos.cs** - DTOs for API registration, login, and auth responses
- **API/AuthController.cs** - API authentication endpoints for register/login

### 3. Modified Files
- **Data/TerepnaploContext.cs** - Now inherits from `IdentityDbContext<ApplicationUser>`
- **Program.cs** - Configured Identity with both Cookie and JWT authentication
- **appsettings.json** - Added JWT configuration section
- **Views/Shared/_Layout.cshtml** - Added login/logout links and navigation menu
- **Controllers/** - All MVC controllers (Odutelep, Odu, Latogatas) now have `[Authorize]` attribute
- **API/** - All API controllers now have `[Authorize]` attribute with JWT Bearer

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

## Web Authentication (Cookie-Based)

The website uses traditional cookie-based authentication. Users can:

1. **Access the website** - Navigate to `https://localhost:7xxx`
2. **Register** - Click "Register" in the navigation or go to `/Account/Register`
3. **Login** - Click "Login" in the navigation or go to `/Account/Login`
4. **Access protected pages** - After logging in, access:
   - Ódutelepek (`/Odutelep`)
   - Óduak (`/Odu`)
   - Látogatások (`/Latogatas`)
5. **Logout** - Click "Logout" in the navigation

When a user tries to access a protected page without logging in, they'll be automatically redirected to the login page.

## API Authentication (JWT Bearer Token)

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

### 2. Test Web Authentication
1. Run the application (`F5` or `dotnet run`)
2. Navigate to `https://localhost:7xxx`
3. Click "Register" and create a new account
4. After registration, you'll be automatically logged in
5. Try accessing the protected pages (Ódutelepek, Óduak, Látogatások)
6. Click "Logout" to sign out

### 3. Test API Authentication
1. Use a tool like Postman, Insomnia, or curl to test the API endpoints
2. Register a new user via `/api/auth/register`
3. Copy the returned JWT token
4. Use the token in the `Authorization: Bearer <token>` header for protected API endpoints

### 4. Optional Enhancements

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

### Web Authentication Flow (Cookie-Based)
```
User visits protected page
        ↓
Not authenticated? → Redirect to /Account/Login
        ↓
User enters credentials
        ↓
AccountController validates with Identity
        ↓
SignInManager creates authentication cookie
        ↓
User redirected back to original page
        ↓
All subsequent requests include cookie
        ↓
Cookie middleware validates identity
        ↓
MVC Controller processes request
```

### API Authentication Flow (JWT)
```
User Registration/Login
        ↓
   AuthController (API)
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

## Authentication Summary

| Feature | Web (MVC) | API |
|---------|-----------|-----|
| **Authentication Method** | Cookie-based | JWT Bearer Token |
| **Login Endpoint** | `/Account/Login` (GET/POST) | `/api/auth/login` (POST) |
| **Register Endpoint** | `/Account/Register` (GET/POST) | `/api/auth/register` (POST) |
| **Logout** | `/Account/Logout` (POST) | Token expiration (no server-side logout) |
| **Token/Cookie Duration** | 24 hours (sliding) | 60 minutes (fixed) |
| **Authorization Header** | Not needed (cookie auto-sent) | `Authorization: Bearer <token>` required |
| **Protected Resources** | MVC Controllers/Views | API Controllers |
| **Auto-Redirect on Unauthorized** | Yes (to login page) | No (returns 401) |

## Key Features Implemented

✅ **Dual Authentication Modes**
- Cookie authentication for web pages
- JWT authentication for API endpoints
- Both use the same Identity user store

✅ **Protected Resources**
- All MVC controllers require authentication (Odutelep, Odu, Latogatas)
- All API controllers require JWT tokens
- Home and Account controllers are public

✅ **User Interface**
- Responsive login/register forms with Bootstrap 5
- Navigation bar shows login status
- Login/Register links for anonymous users
- User greeting and Logout button for authenticated users

✅ **Security Features**
- Password requirements enforced
- Anti-forgery tokens on forms
- HTTPS redirection
- Sliding expiration on cookies
- Automatic redirect to login for unauthorized web access

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
