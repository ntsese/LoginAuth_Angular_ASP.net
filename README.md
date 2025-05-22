# LoginAuth - Angular & ASP.NET Core Authentication System

A full-stack authentication system built with **Angular** frontend and **ASP.NET Core** Web API backend, featuring secure user registration, login, and JWT token-based authentication.

## 🚀 Features

- **User Registration** with password strength validation
- **Secure Login** with JWT token generation
- **Password Hashing** using secure hashing algorithms
- **Input Validation** for usernames and passwords
- **JWT Authentication** for secure API access
- **Role-based Access Control** ready implementation
- **RESTful API** design
- **Entity Framework Core** for database operations

## 🛠️ Technologies Used

### Backend (ASP.NET Core Web API)
- ASP.NET Core
- Entity Framework Core
- JWT (JSON Web Tokens)
- SQL Server / Database of choice
- Password Hashing utilities

### Frontend (Angular)
- Angular 7.2.0
- TypeScript ~3.2.2
- RxJS ~6.3.3
- Angular Forms & Router
- SASS for styling
- HTTP Client for API communication
- JWT token handling

## 📋 Prerequisites

Before running this application, make sure you have the following installed:

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later
- [Node.js](https://nodejs.org/) (v8.9.4 or later, recommended v10+)
- [Angular CLI](https://angular.io/cli) v7.3.10 (install with `npm install -g @angular/cli@7.3.10`)
- [SQL Server](https://www.microsoft.com/en-us/sql-server) or your preferred database
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## 📦 Installation & Setup

### Backend Setup (ASP.NET Core API)

1. **Clone the repository**
   ```bash
   git clone https://github.com/ntsese/LoginAuth_Angular_ASP.net.git
   cd LoginAuth_Angular_ASP.net
   ```

2. **Navigate to the API project**
   ```bash
   cd AngularAuthAPI
   ```

3. **Install dependencies**
   ```bash
   dotnet restore
   ```

4. **Update Connection String**
   - Open `appsettings.json`
   - Update the connection string to match your database configuration
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server;Database=AuthDB;Trusted_Connection=true;"
     }
   }
   ```

5. **Run Database Migrations**
   ```bash
   dotnet ef database update
   ```

6. **Run the API**
   ```bash
   dotnet run
   ```
   The API will be available at `https://localhost:5001` or `http://localhost:5000`

### Frontend Setup (Angular 7)

1. **Navigate to the Angular project**
   ```bash
   cd login-auth
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Update API URL**
   - Update the API base URL in your Angular service files to match your backend URL
   - Typically in a service file like `auth.service.ts` or environment files

4. **Run the Angular application**
   ```bash
   npm start
   # or
   ng serve
   ```
   The application will be available at `http://localhost:4200`

### Available Scripts

- `npm start` or `ng serve` - Start development server
- `npm run build` or `ng build` - Build the project for production
- `npm test` or `ng test` - Run unit tests
- `npm run lint` or `ng lint` - Run code linting
- `npm run e2e` or `ng e2e` - Run end-to-end tests

## 📁 Project Structure

```
LoginAuth_Angular_ASP.net/
├── AngularAuthAPI/              # ASP.NET Core Web API
│   ├── Controllers/
│   │   └── UsersController.cs   # Authentication endpoints
│   ├── Context/
│   │   └── AppDbContext.cs      # Entity Framework context
│   ├── Models/
│   │   └── Users.cs             # User model
│   ├── Helpers/
│   │   └── PasswordHasher.cs    # Password hashing utilities
│   └── ...
├── login-auth/                  # Angular 7 Frontend
│   ├── src/
│   │   ├── app/
│   │   ├── assets/
│   │   └── environments/
│   ├── package.json
│   └── ...
└── README.md
```

### Authentication Endpoints

| Method | Endpoint | Description | Request Body |
|--------|----------|-------------|--------------|
| POST   | `/api/Users/Register` | Register a new user | `{ "username": "string", "password": "string", "firstName": "string", "lastName": "string", "email": "string" }` |
| POST   | `/api/Users/Login` | Authenticate user and get JWT token | `{ "username": "string", "password": "string" }` |

### Request/Response Examples

#### Register User
```json
POST /api/Users/Register
{
  "username": "john_doe",
  "password": "SecurePass123!",
  "firstName": "John",
  "lastName": "Doe",
  "email": "john@example.com"
}
```

**Response:**
```json
{
  "message": "User registered"
}
```

#### Login User
```json
POST /api/Users/Login
{
  "username": "john_doe",
  "password": "SecurePass123!"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "message": "Login Success"
}
```

## 🔒 Security Features

### Password Requirements
- Minimum length: 8 characters
- Must contain uppercase and lowercase letters
- Must contain at least one number
- Must contain at least one special character

### Security Implementations
- **Password Hashing**: Passwords are securely hashed before storage
- **JWT Tokens**: Stateless authentication with configurable expiration
- **Input Validation**: Server-side validation for all user inputs
- **Role-based Access**: Ready for role-based authorization implementation

## 🗃️ Database Schema

The application uses Entity Framework Core with the following main entity:

### Users Table
- `Id` (Primary Key)
- `Username` (Unique)
- `Password` (Hashed)
- `FirstName`
- `LastName`
- `Email`
- `Role`
- `Token`

## ⚙️ Configuration

### JWT Configuration
Update the JWT secret key in your startup configuration:
```csharp
// In your appsettings.json or environment variables
{
  "JwtSettings": {
    "SecretKey": "your-very-secure-secret-key-here",
    "ExpirationDays": 1
  }
}
```

### CORS Configuration
Ensure CORS is properly configured to allow requests from your Angular application.

## 🚦 Usage

1. **Register a new user** using the registration endpoint with a strong password
2. **Login** with your credentials to receive a JWT token
3. **Include the JWT token** in the Authorization header for protected routes:
   ```
   Authorization: Bearer your-jwt-token-here
   ```

## 🤝 Contributing

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📝 TODO / Future Enhancements

- [ ] Email verification for user registration
- [ ] Password reset functionality
- [ ] OAuth integration (Google, Facebook, etc.)
- [ ] Two-factor authentication (2FA)
- [ ] User profile management
- [ ] Admin dashboard
- [ ] API rate limiting
- [ ] Logging and monitoring

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

**ntsese samuel sesing**
- GitHub: [@ntsese](https://github.com/ntsese)

## 🐛 Issues

If you encounter any issues or have suggestions, please [open an issue](https://github.com/ntsese/LoginAuth_Angular_ASP.net/issues) on GitHub.

---

⭐ If you found this project helpful, please give it a star on GitHub!
