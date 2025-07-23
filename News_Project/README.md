# News Project

## Mô tả
Dự án News Project là hệ thống quản lý tin tức hiện đại, gồm backend .NET (API) và frontend React (roadmap). Hỗ trợ đầy đủ chức năng: quản lý bài viết, danh mục, thẻ, bình luận, media, phân quyền, quản trị, responsive, SEO, social login, thống kê...

## Công nghệ sử dụng
- Backend: ASP.NET Core, Entity Framework Core, SQL Server
- Frontend: React (roadmap, có thể dùng Vite/CRA/Next.js)
- Database: SQL Server

## Cấu trúc thư mục
- `Controllers/` - Các API controller cho backend
- `Models/` - Các model code first
- `DTOs/` - Data Transfer Object
- `Migrations/` - Migration EF Core
- `NewsFeatures.md` - Checklist chức năng backend/API
- `FrontendRoadmap.md` - Roadmap phát triển frontend React

## Hướng dẫn chạy backend
1. Clone repo về máy
2. Cài đặt .NET 8 SDK, SQL Server
3. Cấu hình connection string trong `appsettings.json` nếu cần
4. Chạy migration để tạo database:
   ```bash
   dotnet ef database update
   ```
5. Chạy ứng dụng:
   ```bash
   dotnet run
   ```
6. Truy cập Swagger UI tại: `https://localhost:xxxx/swagger` để test API

## Roadmap phát triển frontend
Xem chi tiết trong file [`FrontendRoadmap.md`](./FrontendRoadmap.md)

## Tác giả
- Project scaffolded & documented by AI (ChatGPT)
- Bạn có thể tuỳ biến, phát triển thêm theo nhu cầu! 