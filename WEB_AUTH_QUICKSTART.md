# Web Authentication - Quick Start Guide

## ✅ What's Been Implemented

Your MadarfigyeloWeb application now has **complete authentication** for both the website and API:

### 🌐 Web (Cookie-Based)
- Login/Register pages with Bootstrap 5 styling
- Automatic redirect to login when accessing protected pages
- Navigation bar with Login/Logout links
- User greeting showing logged-in username
- 24-hour session with sliding expiration

### 🔑 API (JWT Bearer Token)
- RESTful authentication endpoints
- 60-minute JWT tokens
- Secure API access with Bearer authentication

## 🚀 Quick Start

### Step 1: Apply Database Migration
```bash
dotnet ef database update
```

### Step 2: Run the Application
```bash
dotnet run
```
Or press `F5` in Visual Studio

### Step 3: Register a New User
1. Navigate to `https://localhost:7xxx`
2. Click **Register** in the top right
3. Fill in the form:
   - Email: `your@email.com`
   - Password: `Test123!` (or your own - must have uppercase, lowercase, and digit)
   - First Name & Last Name (optional)
4. Click **Register**
5. You'll be automatically logged in!

### Step 4: Access Protected Pages
Try clicking on the navigation menu items:
- **Ódutelepek** - View/manage nesting sites
- **Óduak** - View/manage nest boxes
- **Látogatások** - View/manage observations

## 🎯 What's Protected

### Protected MVC Pages (Require Login)
- `/Odutelep` - All CRUD operations for nesting sites
- `/Odu` - All CRUD operations for nest boxes
- `/Latogatas` - All CRUD operations for observations

### Public Pages (No Login Required)
- `/` (Home)
- `/Home/Privacy`
- `/Account/Login`
- `/Account/Register`

### Protected API Endpoints (Require JWT Token)
- `/api/odutelep` - All operations
- `/api/odu` - All operations
- `/api/latogatas` - All operations

### Public API Endpoints (No Token Required)
- `/api/auth/register` - User registration
- `/api/auth/login` - User login

## 💡 User Experience

### Anonymous User
1. Visits the website
2. Sees "Login" and "Register" in navigation
3. Can browse Home and Privacy pages
4. **Cannot access** Ódutelepek, Óduak, or Látogatások
5. Clicking those links redirects to login page

### Logged-In User
1. Sees "Hello, [email]!" and "Logout" in navigation
2. **Can access** all pages (Ódutelepek, Óduak, Látogatások)
3. Can view, create, edit, and delete records
4. Session lasts 24 hours (or until logout)
5. Click "Logout" to sign out

## 🔧 Common Scenarios

### "I forgot my password"
Currently not implemented. Consider adding:
- Password reset flow
- Email verification
- "Forgot Password" link on login page

### "I want to change user permissions"
Add role-based authorization:
```csharp
// In controller
[Authorize(Roles = "Admin")]
public IActionResult Delete(int id) { ... }
```

### "I want to restrict who can register"
Modify `AccountController.Register` to add validation logic:
```csharp
// Only allow certain email domains
if (!model.Email.EndsWith("@yourcompany.com"))
{
    ModelState.AddModelError("", "Only company emails allowed");
    return View(model);
}
```

### "I want different session durations"
Modify in `Program.cs`:
```csharp
builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(7); // 7 days instead of 24 hours
    options.SlidingExpiration = true;
});
```

## 🔐 Security Notes

### Development
- JWT secret key is in `appsettings.json` (acceptable for development)
- LocalDB connection string is in `appsettings.json`
- HTTPS is enabled by default

### Production (Before Deploying!)
1. **Change JWT Key** - Use a secure random value (32+ characters)
2. **Use Azure Key Vault** - Store secrets securely
3. **Enable Account Lockout** - Protect against brute force
4. **Add Rate Limiting** - Limit login attempts
5. **Enable Email Confirmation** - Verify user emails
6. **Implement Password Reset** - Via secure email link
7. **Add Logging** - Monitor authentication events
8. **Use Strong Connection Strings** - For production database

## 📊 What Data Is Stored

The Identity migration adds these tables to your database:

| Table | Purpose |
|-------|---------|
| `AspNetUsers` | User accounts (email, password hash, first/last name) |
| `AspNetRoles` | Roles (if you add role-based auth) |
| `AspNetUserRoles` | User-role assignments |
| `AspNetUserClaims` | Additional user claims |
| `AspNetUserLogins` | External login providers (Google, Microsoft, etc.) |
| `AspNetUserTokens` | Authentication tokens |
| `AspNetRoleClaims` | Role-based claims |

Your existing tables are **unchanged**:
- `Odutelep`
- `Odu`
- `Latogatas`

## 🛠️ Troubleshooting

### "401 Unauthorized" when accessing pages
- **Web**: You'll be redirected to login - this is normal
- **API**: Make sure you're sending the JWT token in the header

### "The migration has already been applied"
- Already done! Just run the application

### "Invalid login attempt"
- Check that password meets requirements (6 chars, uppercase, lowercase, digit)
- Verify email is correct
- Try registering a new user

### Login page has no styling
- Ensure Bootstrap is loaded in `_Layout.cshtml`
- Check browser console for errors
- Clear browser cache

### After login, still can't access pages
- Check that user was created successfully
- Verify `[Authorize]` is on the controller, not specific actions
- Check browser cookies are enabled

## 🎨 Customization Ideas

### Change Password Requirements
Edit `Program.cs`:
```csharp
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireDigit = true;
    // ... etc
})
```

### Add User Profile Page
Create `/Account/Profile` to display/edit:
- Email
- First Name / Last Name
- Change Password
- Account created date

### Add "Remember Me" Duration
Modify `AccountController.Login`:
```csharp
await _signInManager.PasswordSignInAsync(
    model.Email,
    model.Password,
    isPersistent: model.RememberMe, // This makes the cookie persistent
    lockoutOnFailure: false);
```

### Customize Login Page Appearance
Edit `Views/Account/Login.cshtml` to:
- Change colors/styling
- Add company logo
- Add "Forgot Password" link
- Add social login buttons

## 📝 Next Steps

1. ✅ Apply migration (`dotnet ef database update`)
2. ✅ Test registration and login
3. ✅ Try accessing protected pages
4. 📋 Consider adding roles (Admin, User, ReadOnly)
5. 📋 Add email confirmation for production
6. 📋 Implement password reset
7. 📋 Add user profile management
8. 📋 Set up external authentication (Google, Microsoft)

## 📚 Related Files

- `Controllers/AccountController.cs` - Web authentication logic
- `API/AuthController.cs` - API authentication logic
- `Views/Account/*.cshtml` - Login/Register pages
- `Models/ViewModels/AccountViewModels.cs` - Form models
- `Models/ApplicationUser.cs` - User entity
- `Program.cs` - Authentication configuration
- `AUTHENTICATION_GUIDE.md` - Detailed technical documentation

---

**Need help?** Check `AUTHENTICATION_GUIDE.md` for detailed technical information.
