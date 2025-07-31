# Sales Management System

## Mục tiêu dự án
Xây dựng hệ thống Web API quản lý bán hàng gồm:
- Quản lý sản phẩm, khách hàng, đơn hàng
- Phân quyền người dùng (Admin / Nhân viên)
- Xem thống kê doanh thu theo ngày, tháng, sản phẩm
- Sát thực tế doanh nghiệp vừa và nhỏ

## Kiến trúc tổng quan
```
SalesManagementSystem/
│
├── Sales.API/              # Dự án Web API (.NET 8)
├── Sales.Application/      # Layer xử lý nghiệp vụ, DTO, Service
├── Sales.Domain/           # Entity, Enum, Interface
├── Sales.Infrastructure/   # EF Core, DbContext, Repositories
├── Sales.Tests/            # Unit test với xUnit, Moq
└── docker-compose.yml      # Docker Compose: API + SQL Server

```
**Mô hình phân tầng:**
- Presentation → Application → Infrastructure → Domain  
- Clean Architecture kết hợp DI, EF Core, AutoMapper

## Cách chạy project local
1. Clone project
```
git clone https://github.com/[your-username]/SalesManagementSystem.git
cd SalesManagementSystem

```

2. Cấu hình SQL Server
Kiểm tra chuỗi kết nối trong appsettings.Development.json:
```
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SalesDb;User Id=sa;Password=YourPassword123;"
}

```

3. Chạy migration và cập nhật DB
```
dotnet ef database update --project Sales.Infrastructure --startup-project Sales.API

```

4. Chạy API
```
dotnet run --project Sales.API

```

5. Hoặc dùng Docker
```
docker-compose up -d

```
Swagger sẽ hiển thị tại: http://localhost:5000/swagger

## Swagger + Auth
- Swagger UI hỗ trợ nhập token để test các API bảo mật.
- Đăng nhập và lấy token tại /api/auth/login
- Dán token vào nút "Authorize" trên Swagger:
```
Bearer <your_token_here>

```

## Tính năng đã hoàn thành
- CRUD Product, Customer
- CRUD Order + OrderDetail
- JWT Authentication
- Thống kê doanh thu theo ngày/tháng
- Unit Test + Code Coverage
- Docker hóa API + DB
- CI/CD với GitHub Actions

## Công nghệ sử dụng
| Công nghệ	| Mục đích sử dụng  |
|----------------------|----------------------|
| ASP.NET Core |	Xây dựng Web API |
| Entity Framework Core |	ORM thao tác DB |
| SQL Server	| Lưu trữ dữ liệu |
|AutoMapper |	Mapping DTO ↔ Entity |
| Swagger |	Test API trực tiếp |
| JWT |	Xác thực người dùng |
| xUnit + Moq |	Unit Test |
| Docker |	Đóng gói API và DB |
| GitHub Actions | CI/CD |





