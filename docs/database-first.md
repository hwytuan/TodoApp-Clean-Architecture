# EF Core Database First trong TodoApp

Database First lấy cấu trúc bảng từ SQL Server để sinh entity và DbContext bằng EF Core. Trong TodoApp, `database/init.sql` định nghĩa schema và dữ liệu mẫu. Khi schema thay đổi, áp dụng SQL vào database rồi cập nhật mã C# bằng scaffold.

## 1. Khởi tạo SQL Server và kiểm tra bảng

Tại thư mục chứa `TodoApp.slnx`, chạy `docker compose up -d mssql`. Đợi log từ `docker compose logs --tail=50 mssql` báo SQL Server sẵn sàng. Với Compose chỉ có dịch vụ `mssql`, mở SSMS và chạy `database/init.sql` thủ công.

Kết nối bằng SQL Server Authentication, tài khoản `sa` và mật khẩu trong `.env`. Tên server là `127.0.0.1,1433` theo cấu hình hiện tại. Nếu đã đổi `MSSQL_PORT`, dùng cổng mới.

```sql
SELECT Id, Title, IsCompleted
FROM TodoAppDB.dbo.TodoItems
ORDER BY Id;
```

Lần khởi tạo đầu tiên, truy vấn sẽ trả về hai công việc mẫu.

## 2. Chuẩn bị Visual Studio

Đặt `TodoApp.API` làm Startup Project. Mở **Tools → NuGet Package Manager → Package Manager Console**, chọn `TodoApp.Infrastructure` trong ô **Default project**.

| Project | Gói NuGet |
| --- | --- |
| TodoApp.Infrastructure | Microsoft.EntityFrameworkCore.SqlServer, Microsoft.EntityFrameworkCore.Tools, Microsoft.EntityFrameworkCore.Design |
| TodoApp.API | Microsoft.EntityFrameworkCore.Design |

Dùng cùng phiên bản EF Core. Các project hiện khai báo `8.0.34`. Build solution trước khi scaffold.

Hai hướng tham chiếu cần thiết là `TodoApp.API → TodoApp.Infrastructure` để công cụ tải project đích và `TodoApp.Infrastructure → TodoApp.Domain` để DbContext dùng entity. Nếu báo `Could not load assembly 'TodoApp.Infrastructure'`, kiểm tra tham chiếu từ API trong **Dependencies → Projects**, rồi build lại.

## 3. Scaffold bảng TodoItems

Dán lệnh sau trên một dòng trong Package Manager Console và thay mật khẩu bằng giá trị thực tế.

```powershell
Scaffold-DbContext 'Server=127.0.0.1,1433;Database=TodoAppDB;User Id=sa;Password="<MAT_KHAU_SA>";Encrypt=True;TrustServerCertificate=True' Microsoft.EntityFrameworkCore.SqlServer -Project TodoApp.Infrastructure -StartupProject TodoApp.API -Tables dbo.TodoItems -OutputDir ..\TodoApp.Domain\Entities -Namespace TodoApp.Domain.Entities -ContextDir Data -ContextNamespace TodoApp.Infrastructure.Data -Context TodoAppDbContext -NoOnConfiguring
```

| File sinh ra | Vai trò |
| --- | --- |
| `TodoApp.Domain/Entities/TodoItem.cs` | Biểu diễn một dòng công việc. |
| `TodoApp.Infrastructure/Data/TodoAppDbContext.cs` | Cung cấp `DbSet<TodoItem>` và ánh xạ entity sang SQL Server. |

`-OutputDir` tính từ project Infrastructure. `-NoOnConfiguring` giữ chuỗi kết nối khỏi mã sinh ra. Khi cần scaffold lại, xem trước thay đổi và sao lưu hoặc commit các file đã sinh rồi mới dùng `-Force`. Đặt phần tùy chỉnh trong file partial riêng để giữ lại khi sinh mã lần sau.

## 4. Bước tiếp theo của TodoApp

Hai file scaffold đã có. `TodoApp.Infrastructure.csproj` hiện chưa tham chiếu `TodoApp.Domain`. Trong Visual Studio, nhấp phải `TodoApp.Infrastructure` → **Add → Project Reference…** → chọn `TodoApp.Domain`, rồi build solution. DbContext cần tham chiếu này để sử dụng `TodoItem`.

Sau khi build thành công, đăng ký DbContext để API có thể nhận nó qua DI. Thêm hai namespace sau ở đầu `TodoApp.API/Program.cs`.

```csharp
using Microsoft.EntityFrameworkCore;
using TodoApp.Infrastructure.Data;
```

Ngay sau `var builder = WebApplication.CreateBuilder(args);`, thêm đoạn sau trước `builder.Build()`.

```csharp
var connectionString = builder.Configuration.GetConnectionString("TodoApp")
    ?? throw new InvalidOperationException("Missing connection string 'TodoApp'.");

builder.Services.AddDbContext<TodoAppDbContext>(
    options => options.UseSqlServer(connectionString));
```

Chạy API từ PowerShell ở thư mục gốc. Thay placeholder cục bộ và dùng đúng cổng SQL Server.

```powershell
$env:ConnectionStrings__TodoApp = 'Server=127.0.0.1,1433;Database=TodoAppDB;User Id=sa;Password="<MAT_KHAU_SA>";Encrypt=True;TrustServerCertificate=True'
dotnet run --project TodoApp.API
```

`.env` phục vụ Docker Compose. Khi chạy API trực tiếp, biến `ConnectionStrings__TodoApp` cung cấp cấu hình `ConnectionStrings:TodoApp` cho ASP.NET Core. Giữ mật khẩu thật ngoài source. Khởi động API kiểm tra cấu hình đăng ký. Một truy vấn thực tế, chẳng hạn endpoint danh sách công việc, mới xác nhận API đọc được SQL Server.

## Tài liệu Microsoft

- [Reverse Engineering trong EF Core](https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/)
- [Cấu hình DbContext](https://learn.microsoft.com/en-us/ef/core/dbcontext-configuration/)
