# StudentManagementMVC

ASP.NET Core MVC + EF Core Code First + SQL Server LocalDB.

Chức năng: CRUD sinh viên, tìm kiếm, validation, dữ liệu mẫu. Ứng dụng tự tạo database `StudentManagementMVC` và bảng `Students` khi chạy lần đầu bằng `Database.EnsureCreated()`.

## Chạy
1. Visual Studio 2022: cài workload **ASP.NET and web development** và .NET 8 SDK.
2. Có SQL Server Express LocalDB (thường đi kèm Visual Studio).
3. Giải nén và mở `StudentManagementMVC.sln`.
4. Ctrl+Shift+B để Build, Ctrl+F5 để chạy.
5. Nếu LocalDB của bạn khác, sửa chuỗi kết nối trong `appsettings.json`.

## SQL Server
Chuỗi mặc định: `(localdb)\MSSQLLocalDB`. Database sẽ được tạo tự động, không cần chạy script SQL.
