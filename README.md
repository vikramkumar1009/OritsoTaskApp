# Oritso Task App (Backend)

This is the **backend API** for the Oritso Task App. It is built using **C#**, **.NET Core Web API**, and **Entity Framework Core** for handling data storage. The application provides a secure task management system with full CRUD operations and JWT-based authentication.

---

## 🚀 Technologies Used

- **.NET Core Web API**
- **C#**
- **Entity Framework Core**
- **SQL Server** (or your choice of provider)
- **JWT Authentication**

---

## 🔐 Features

- ✅ User Signup and Login
- ✅ Secure JWT Authentication
- ✅ Add, Edit, Delete, and Search Tasks
- ✅ Only authenticated users can access task endpoints
- ✅ Clean and structured API design

---

## 📁 Project Structure

```
OritsoTaskApp/
├── Controllers/
│   ├── AuthController.cs
│   └── TaskController.cs

├── Models/
└── ApplicationDbContext.cs
│   ├── User.cs
│   └── Task.cs
├── Services/
│   └── JwtService.cs
├── DTOs/
├── Program.cs
├── Startup.cs
└── appsettings.json
```

---

## ⚙️ How to Run (Backend)

1. **Clone the Repository**

```bash
git clone https://github.com/vikramkumar1009/OritsoTaskApp.git
cd OritsoTaskApp
```

2. **Update `appsettings.json`**

Make sure your connection string and JWT secret are configured properly.

3. **Run the App**

```bash
⚙️ 2. Restore NuGet Packages
Inside the project folder (where .csproj is located):


dotnet restore
🧰 3. Set Up Database (EF Core Migrations)
If Migrations Already Exist
Just apply them:
update-database
If You Need to Create Migrations
Only do this if no Migrations/ folder exists:

add-migration InitialCreate
update-datABASE
⚠️ Make sure your connection string is correct in appsettings.json.

🏃‍♂️ 4. Run the Project


dotnet build
dotnet run
```

API will run at: `https://localhost:5001` or `http://localhost:5000`

4. **Use Swagger**

Visit `https://localhost:5001/swagger` to explore and test your endpoints.

---

## 🔗 Frontend Integration

To use the frontend with this backend, clone and run this React frontend:

**Frontend GitHub Repo:**  
👉 [Oristop Frontend](https://github.com/vikramkumar1009/Oristop-frontend)

---

## 🛡️ Authentication Workflow

- Users must **sign up or log in** to receive a JWT token.
- This token is stored on the frontend (e.g., in localStorage).
- All protected API endpoints require the `Authorization: Bearer <token>` header.
- Tasks (add/edit/delete/search) can only be performed after login.

---

## 📃 License

This project is open source and available under the [MIT License](LICENSE).

---

## 🧑‍💻 Author

**Vikram Kumar**  
[GitHub](https://github.com/vikramkumar1009)
