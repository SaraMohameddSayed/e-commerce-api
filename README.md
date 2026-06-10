# 🛒 El-Beqala — E-Commerce Management System API

[![Build](https://img.shields.io/badge/build-passing-brightgreen)](#)
[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![EF Core](https://img.shields.io/badge/EF_Core-9.0-512BD4?logo=dotnet&logoColor=white)](https://learn.microsoft.com/en-us/ef/core/)
[![SignalR](https://img.shields.io/badge/SignalR-Real--time-0079D4?logo=microsoft&logoColor=white)](https://learn.microsoft.com/en-us/aspnet/core/signalr/introduction)
[![License](https://img.shields.io/badge/license-MIT-blue)](LICENSE)

A production-ready RESTful API for a full-featured e-commerce platform. Built with **ASP.NET Core 9** following **Clean Architecture** principles — featuring JWT authentication, role-based order lifecycle management, a custom event-driven notification system, real-time SignalR updates, offers & promotions, and Egyptian delivery region management.

🖥️ **Frontend Client:** [El-Beqala Angular Client](https://github.com/SaraMohameddSayed/ecommerce-angular-client)

---

## ✨ Features

### 🔐 Authentication & Authorization
- **ASP.NET Core Identity** with JWT Bearer token authentication
- Role-based authorization
- Secure password hashing and claims-based identity
- Registration, login, logout
- Auto cart creation on user registration

### 🛍️ Product & Category Management
- Full CRUD for products and categories
- Image upload via **Cloudinary**
- Soft delete for products via `IsActive` flag
- Stock tracking and availability
- Products linked to active offers

### 🏷️ Offers & Promotions
- Create and manage offers
- Assign multiple products to an offer
- Offer-aware product queries for the storefront

### 🛒 Cart System
- Per-user cart auto-created on registration
- Add, update, and remove cart items
- Cart totals calculated server-side

### 📦 Order Lifecycle Management
- Complete order state machine:
  `Pending → Confirmed → Shipped → Delivered → Cancelled`
- Role-based state transitions — only Admin can advance order states
- Order details with product name and image snapshots
- Tracking number per order

### 🚚 Delivery & Governorates
- Manage Egyptian **governorates** and **areas** with delivery fees per area
- `IsActive` flag for enabling/disabling delivery regions
- Orders tied to selected governorate and area at checkout
- Seeded with real Egyptian governorate data on first run

### 🔔 Event-Driven Notification System
- Custom **InMemoryEventBus** — built from scratch without MediatR
- **4 domain events:** `NewOrderAdded`, `OrderStatusChanged`, `NewProductAdded`, `NewOfferAdded`
- Each event has **2 dedicated handlers:**
  - `NotificationHandler` — persists notification to DB
  - `SignalRHandler` — pushes real-time update via SignalR hub
- Role-based targeting — Admins and Customers receive different notifications
- Notification history with read/unread state

### 💳 Payment & Checkout
- Multiple payment methods: **Cash on Delivery**, **Card** (To Do)
- Subtotal, delivery fee, and total calculated per order
- `IsPaid` tracking flag

### 📬 Contact & Messaging
- Contact form submission endpoint

### ⚡ Performance & Code Quality
- EF Core **projection** and **eager loading** to prevent N+1 queries
- Normalized SQL Server schema with referential integrity
- Extension methods for clean `Request → Entity` and `Entity → Response` mapping
- `MainService` base class for common CRUD operations
- `PagedResult<T>` for paginated endpoints

---

## 🏗️ Architecture

```
┌─────────────────────────────────────────────────────────┐
│                  Angular 20 Client                      │
└─────────────────────────┬───────────────────────────────┘
                          │  REST  /  SignalR
┌─────────────────────────▼───────────────────────────────┐
│              API  (Presentation Layer)                  │
│        Controllers · NotificationHub · Program.cs       │
├─────────────────────────────────────────────────────────┤
│           Application  (Business Logic Layer)           │
│         Services · EventBus · Events · Handlers         │
│  Abstractions · DTOs (Requests/Responses) · Extensions  │
├─────────────────────────────────────────────────────────┤
│           Infrastructure  (Data Layer)                  │
│         AppDbContext · Migrations · Seeders             │
├─────────────────────────────────────────────────────────┤
│              Domain  (Core Layer)                       │
│           Entities · Enums — zero dependencies          │
└─────────────────────────────────────────────────────────┘
```

### Event-Driven Flow

```
User places order
      │
      ▼
 OrderService  ──publishes──▶  InMemoryEventBus
                                      │
                    ┌─────────────────┴──────────────────┐
                    ▼                                     ▼
   NewOrderAddedNotificationHandler        NewOrderAddedSignalRHandler
   (saves Notification to DB)              (pushes to Admin via SignalR)
```

---

## 📁 Project Structure

```
e-commerce-api/
│
├── API/                              # Presentation Layer
│   ├── Controllers/                  # 11 controllers
│   │   ├── BaseController.cs
│   │   ├── AuthController.cs
│   │   ├── ProductController.cs
│   │   ├── OrderController.cs
│   │   └── ...
│   ├── Hubs/
│   │   └── NotificationHub.cs        # SignalR hub
│   ├── RealTime/
│   │   └── SignalRNotifier.cs        # IRealtimeNotifier implementation
│   └── Program.cs
│
├── Application/                      # Business Logic Layer
│   ├── Abstractions/
│   │   ├── IEventBus.cs
│   │   ├── IEventHandler.cs
│   │   └── IRealtimeNotifier.cs
│   ├── Features/                     # Organized by domain feature
│   │   ├── Auth/
│   │   │   └── DTOs/                 # LoginRequest, RegisterRequest
│   │   ├── Products/
│   │   │   └── DTOs/                 # AddProductRequest, UpdateProductRequest
│   │   │                             # ProductResponse, ProductExtensions
│   │   ├── Orders/
│   │   │   └── DTOs/                 # AddOrderRequest, OrderResponse
│   │   ├── Cart/
│   │   ├── Categories/
│   │   ├── Offers/
│   │   ├── Delivery/
│   │   │   ├── Governorate/
│   │   │   └── Area/
│   │   ├── Notifications/
│   │   └── Messages/
│   ├── Services/                     # Business logic per entity
│   │   ├── MainService.cs            # Base service with common CRUD
│   │   ├── AuthService.cs
│   │   ├── OrderService.cs
│   │   ├── ProductService.cs
│   │   └── ...
│   ├── EventBus/
│   │   └── InMemoryEventBus.cs       # Custom event bus implementation
│   ├── Events/
│   │   ├── NewOrderAddedEvent.cs
│   │   ├── OrderStatusChangedEvent.cs
│   │   ├── NewProductAddedEvent.cs
│   │   └── NewOfferAddedEvent.cs
│   ├── Handlers/                     # 2 handlers per event
│   │   ├── NewOrderAddedNotificationHandler.cs
│   │   ├── NewOrderAddedSignalRHandler.cs
│   │   └── ...
│   └── Shared/
│       ├── Common/
│       │   ├── ApiResponse.cs
│       │   └── PagedResult.cs
│       └── Contracts/
│           └── SignalREvents.cs
│
├── Infrastructure/                   # Data Layer
│   ├── AppDbContext.cs
│   ├── Migrations/                   # 9 migrations
│   └── Seeders/
│       ├── AdminSeeder.cs
│       └── GovernorateAreaSeeder.cs
│
└── Domain/                           # Core Layer — zero dependencies
    ├── Enums/
    │   ├── OrderStatus.cs
    │   ├── PaymentMethod.cs
    │   ├── NotificationType.cs
    │   └── NotificationSubType.cs
    ├── Order.cs
    ├── Product.cs
    ├── Cart.cs
    ├── Offer.cs
    ├── Governorate.cs
    ├── Area.cs
    └── ...
```

---

## 📡 API Overview (30+ Endpoints)

| Module | Key Endpoints |
|--------|--------------|
| **Auth** | Register, Login, Logout |
| **Products** | CRUD, Get with Offers, Image Upload (Cloudinary) |
| **Categories** | CRUD |
| **Cart** | Get Cart, Add Item, Update Quantity, Remove Item |
| **Offers** | Create Offer, Add Products to Offer, List Offers |
| **Orders** | Place Order, Get My Orders, Get Details, Update Status |
| **Governorates** | CRUD |
| **Notifications** | Get All, Mark as Read |
| **Contact** | Submit Message |

> Full interactive documentation available on the live **Swagger UI**.

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|-----------|
| Framework | ASP.NET Core 9 |
| Language | C# |
| ORM | Entity Framework Core 9 |
| Database | SQL Server |
| Auth | ASP.NET Core Identity + JWT |
| Real-time | SignalR |
| Image Storage | Cloudinary |
| API Docs | Swagger |
| Architecture | Clean Architecture (4 layers) |

---

## 🚀 Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- SQL Server
- Cloudinary account (free tier works)

### Setup

```bash
# 1. Clone the repo
git clone https://github.com/SaraMohameddSayed/e-commerce-api.git
cd e-commerce-api

# 2. Update appsettings.json
{
  "ConnectionStrings": {
    "TheConnection": "Server=.;Database=ElBeqalaDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "CloudinarySettings": {
    "CloudName": "your_cloud_name",
    "ApiKey": "your_api_key",
    "ApiSecret": "your_api_secret"
  },
  "Jwtsettings": {
    "Key": "your_secret_key_minimum_32_characters"
  },
  "AdminSettings": {
    "Email": "admin@example.com",
    "Password": "Admin@123"
  }
}

# 3. Apply migrations
dotnet ef database update --project Infrastructure --startup-project API

# 4. Run the API
dotnet run --project API
```

Swagger UI available at `https://localhost:7086/swagger`

> On first run, the database is automatically seeded with a default admin account and Egyptian governorate/area data.

---

## 🔧 Environment Variables (for deployment)

```env
ConnectionStrings__TheConnection=your_connection_string
Jwtsettings__Key=your_secret_key_min_32_chars
CloudinarySettings__CloudName=your_cloud_name
CloudinarySettings__ApiKey=your_api_key
CloudinarySettings__ApiSecret=your_api_secret
AdminSettings__Email=admin@yourdomain.com
AdminSettings__Password=StrongPassword@123
```


## 🤝 Related Repository

**Frontend (Angular 20):** [SaraMohameddSayed/ecommerce-angular-client](https://github.com/SaraMohameddSayed/ecommerce-angular-client)

---

## 📄 License

This project is licensed under the MIT License.
