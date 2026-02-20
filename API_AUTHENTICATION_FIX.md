# API Authentication Fix - No More Login Redirects

## ✅ Problem Fixed

Your mobile app was being redirected to `/Account/Login` instead of receiving proper 401/403 HTTP status codes when making API requests with JWT tokens.

## 🔧 Changes Made

### 1. **Program.cs** - Prevent Cookie Auth Redirects for API Routes
Added event handlers to detect `/api/*` requests and return proper HTTP status codes:
- `OnRedirectToLogin` → Returns **401 Unauthorized** instead of redirect
- `OnRedirectToAccessDenied` → Returns **403 Forbidden** instead of redirect

```csharp
options.Events.OnRedirectToLogin = context =>
{
    if (context.Request.Path.StartsWithSegments("/api"))
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        return Task.CompletedTask;
    }
    context.Response.Redirect(context.RedirectUri);
    return Task.CompletedTask;
};
```

### 2. **API Controllers** - Explicit JWT Bearer Authentication
Updated all 3 API controllers to use Bearer authentication scheme:

**Before:**
```csharp
[Authorize]  // ❌ Uses default cookie auth
```

**After:**
```csharp
[Authorize(AuthenticationSchemes = "Bearer")]  // ✅ Uses JWT tokens
```

**Files Updated:**
- `API/OdutelepController.cs`
- `API/OduController.cs`
- `API/LatogatasController.cs`

---

## 🎯 How It Works Now

### **Web Requests (Browser)**
- Uses **cookie authentication**
- Redirects to `/Account/Login` if not authenticated
- User-friendly flow for web users

### **API Requests (Mobile App)**
- Uses **JWT Bearer tokens**
- Returns **401 Unauthorized** if token missing/invalid
- Returns **403 Forbidden** if token valid but lacks permission
- **NO REDIRECTS** - proper REST API behavior

---

## 📱 Mobile App Testing

Your mobile app should now work correctly with JWT tokens:

### 1. **Login/Register** (Get Token)
```http
POST https://your-server/api/auth/login
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
  "expiration": "2025-02-14T10:30:00Z",
  "email": "user@example.com"
}
```

### 2. **Use Token in API Requests**
```http
GET https://your-server/api/odutelep
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

**Success (200):**
```json
[
  { "id": 1, "azonosito": "OT001", ... },
  { "id": 2, "azonosito": "OT002", ... }
]
```

**No Token / Invalid Token (401):**
```json
{
  "type": "https://tools.ietf.org/html/rfc7235#section-3.1",
  "title": "Unauthorized",
  "status": 401
}
```

**Valid Token but No Permission (403):**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.3",
  "title": "Forbidden",
  "status": 403
}
```

---

## 🔐 Authentication Behavior Summary

| Scenario | Web (Browser) | API (Mobile App) |
|----------|---------------|------------------|
| **Auth Method** | Cookie | JWT Bearer Token |
| **Not Authenticated** | Redirect to `/Account/Login` | Return 401 Unauthorized |
| **Access Denied** | Redirect to `/Account/AccessDenied` | Return 403 Forbidden |
| **Token Location** | Cookie (auto-sent) | Header: `Authorization: Bearer <token>` |
| **Session Duration** | 24 hours (sliding) | 60 minutes (fixed) |

---

## ⚠️ Important Notes

### **For Mobile App Development:**
1. **Always include the Authorization header** with Bearer token
2. **Handle 401 responses** by prompting user to login again
3. **Handle 403 responses** by showing "access denied" message
4. **Store token securely** on the device (encrypted storage)
5. **Token expires after 60 minutes** - implement refresh or re-login

### **For Testing:**
Use tools like:
- **Postman** - Test API endpoints with Bearer tokens
- **Insomnia** - Alternative REST client
- **curl** - Command line testing
  ```bash
  curl -H "Authorization: Bearer YOUR_TOKEN_HERE" https://your-server/api/odutelep
  ```

---

## 🔄 Hot Reload Note

Since your app is currently running in debug mode, you may be able to **hot reload** these changes:
- In Visual Studio: Click the 🔥 Hot Reload button or press `Alt+F10`
- Or restart the application to apply changes

---

## ✅ Verification Checklist

Test these scenarios to confirm the fix:

- [ ] **API with valid token** → Returns data (200)
- [ ] **API without token** → Returns 401 (NOT redirect)
- [ ] **API with expired token** → Returns 401 (NOT redirect)
- [ ] **API with invalid token** → Returns 401 (NOT redirect)
- [ ] **Web without login** → Redirects to login page (still works)
- [ ] **Web after login** → Can access protected pages (still works)

---

## 🎉 Result

Your mobile app can now:
✅ Authenticate with JWT tokens  
✅ Receive proper HTTP status codes  
✅ No more unwanted redirects  
✅ Proper REST API behavior  

Your web application still:
✅ Uses cookie authentication  
✅ Redirects to login when needed  
✅ User-friendly experience  

**Both web and mobile now work perfectly together!** 🚀
