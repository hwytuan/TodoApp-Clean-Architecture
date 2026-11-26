USE TodoAppDB;
GO

IF OBJECT_ID(N'dbo.TodoItems', N'U') IS NULL
    THROW 50002, 'Table dbo.TodoItems does not exist. Run database/init.sql first.', 1;

SET XACT_ABORT ON;
BEGIN TRANSACTION;

INSERT INTO dbo.TodoItems (Title, Description, IsCompleted, CreatedAt, DueDate)
VALUES
    (N'Khởi tạo repository Git và cấu hình .gitignore', N'Loại bỏ các thư mục build bin/, obj/, .vs/ và file môi trường .env.', 1, '2026-11-10T01:30:00.000', '2026-11-10'),
    (N'Thiết lập SQL Server 2022 bằng Docker Compose', N'Cấu hình volume sqlserver_data để giữ dữ liệu khi khởi động lại container.', 1, '2026-11-10T03:15:00.000', '2026-11-11'),
    (N'Viết script khởi tạo bảng dbo.TodoItems', N'Thêm khóa chính IDENTITY, giá trị mặc định SYSUTCDATETIME() và check constraint cho Title.', 1, '2026-11-11T02:00:00.000', '2026-11-12'),
    (N'Cấu hình User Secrets cho chuỗi kết nối TodoApp', N'Lưu ConnectionStrings:TodoApp ngoài mã nguồn để bảo mật mật khẩu sa.', 1, '2026-11-11T08:45:00.000', '2026-11-12'),
    (N'Scaffold thực thể TodoItem và TodoAppDbContext', N'Áp dụng EF Core Database First sinh code sang tầng Domain và Infrastructure.', 1, '2026-11-12T01:20:00.000', '2026-11-13'),
    (N'Tách solution thành 4 tầng Clean Architecture', N'Tạo các project Domain, Application, Infrastructure và API theo đúng chiều phụ thuộc.', 1, '2026-11-12T06:10:00.000', '2026-11-14'),
    (N'Định nghĩa hợp đồng ITodoItemRepository', N'Khai báo các phương thức GetAllAsync, GetByIdAsync, Add và Delete ở tầng Application.', 1, '2026-11-13T02:05:00.000', '2026-11-15'),
    (N'Thiết kế giao diện IUnitOfWork tối giản', N'Chỉ giữ SaveChangesAsync để gom giao dịch nguyên tử mà không phụ thuộc chặt vào các Repository.', 1, '2026-11-13T09:30:00.000', '2026-11-15'),
    (N'Triển khai TodoItemRepository bằng EF Core', N'Sử dụng AsNoTracking() cho truy vấn danh sách và truyền CancellationToken đầy đủ.', 1, '2026-11-14T03:40:00.000', '2026-11-16'),
    (N'Xây dựng luồng xem danh sách công việc (GetAll)', N'Ánh xạ thực thể sang danh sách chỉ đọc IReadOnlyList<TodoItemDto>.', 1, '2026-11-15T01:50:00.000', '2026-11-17'),
    (N'Xây dựng luồng xem chi tiết công việc theo ID', N'Trả về 200 OK kèm TodoItemDto nếu tìm thấy hoặc 404 Not Found khi không tồn tại.', 1, '2026-11-15T07:15:00.000', '2026-11-17'),
    (N'Hoàn thiện Command tạo mới công việc', N'Kiểm tra Required, StringLength(200), cắt khoảng trắng thừa và trả về 201 Created.', 1, '2026-11-16T02:25:00.000', '2026-11-18'),
    (N'Tách riêng UpdateTodoItemCommand cho chức năng sửa', N'Bổ sung trường IsCompleted để hỗ trợ đánh dấu hoàn thành công việc.', 1, '2026-11-17T04:00:00.000', '2026-11-19'),
    (N'Xây dựng chức năng xóa công việc (Delete)', N'Trả về mã chuẩn 204 No Content sau khi xóa thành công bản ghi khỏi cơ sở dữ liệu.', 1, '2026-11-18T03:10:00.000', '2026-11-20'),
    (N'Vẽ sơ đồ ánh xạ kiến trúc từ MVC sang Clean Architecture', N'Thể hiện rõ đường nét đứt Interface sang Application và nét liền sang Infrastructure.', 1, '2026-11-26T06:00:00.000', '2026-11-26'),
    (N'Viết tài liệu kiến trúc arch.md và báo cáo dự án', N'Tổng hợp sơ đồ luồng Sequence Diagram, bảng ánh xạ tầng và đặc tả 5 API.', 1, '2026-11-26T06:30:00.000', '2026-11-26'),
    (N'Bổ sung phân trang (Pagination) cho API danh sách', N'Thêm tham số pageNumber, pageSize và trả về tổng số bản ghi để thử nghiệm với 50+ dữ liệu mẫu.', 0, '2026-11-20T01:15:00.000', '2026-11-28'),
    (N'Thêm bộ lọc theo trạng thái hoàn thành (IsCompleted)', N'Cho phép lọc riêng các công việc chưa xong hoặc đã hoàn thành trên query string.', 0, '2026-11-20T04:20:00.000', '2026-11-29'),
    (N'Tìm kiếm công việc theo từ khóa tiêu đề và mô tả', N'Hỗ trợ tham số search không phân biệt chữ hoa thường.', 0, '2026-11-21T02:30:00.000', '2026-11-30'),
    (N'Sắp xếp danh sách theo hạn hoàn thành (DueDate)', N'Cho phép sắp xếp tăng dần hoặc giảm dần theo DueDate và CreatedAt.', 0, '2026-11-21T08:00:00.000', '2026-12-01'),
    (N'Chuẩn hóa phản hồi lỗi bằng ProblemDetails (RFC 7807)', N'Đảm bảo mọi lỗi 400, 404, 500 đều trả về định dạng application/problem+json đồng nhất.', 0, '2026-11-22T01:45:00.000', '2026-11-27'),
    (N'Cài đặt Global Exception Handler (IExceptionHandler)', N'Bắt ngoại lệ tập trung ở tầng API và ghi log chi tiết không làm lộ stack trace.', 0, '2026-11-22T05:10:00.000', '2026-12-02'),
    (N'Tích hợp FluentValidation ở tầng Application', N'Tách logic kiểm tra dữ liệu đầu vào khỏi DataAnnotations để tái sử dụng ngoài HTTP.', 0, '2026-11-23T02:00:00.000', '2026-12-03'),
    (N'Tạo hàm mở rộng AddApplication() đăng ký DI gọn gàng', N'Chuyển các dòng đăng ký Handler từ Program.cs sang DependencyInjection của Application.', 0, '2026-11-23T06:40:00.000', '2026-11-27'),
    (N'Viết extension method ToDto() dùng chung cho TodoItem', N'Loại bỏ đoạn khởi tạo new TodoItemDto(...) bị lặp lại ở 4 Handler.', 0, '2026-11-23T09:15:00.000', NULL),
    (N'Đổi đường dẫn API sang chuẩn số nhiều /api/v1/todo-items', N'Áp dụng quy ước đặt tên RESTful và đánh phiên bản API ngay từ đầu.', 0, '2026-11-24T01:30:00.000', '2026-11-29'),
    (N'Bổ sung endpoint PATCH đánh dấu nhanh hoàn thành công việc', N'Chỉ cập nhật riêng trường IsCompleted mà không cần gửi lại toàn bộ Title và Description.', 0, '2026-11-24T03:50:00.000', '2026-12-04'),
    (N'Tạo project TodoApp.UnitTests với xUnit và Moq', N'Thiết lập cấu trúc kiểm thử tự động cho toàn bộ CommandHandlers và QueryHandlers.', 0, '2026-11-24T07:20:00.000', '2026-12-05'),
    (N'Viết Unit Test cho CreateTodoItemCommandHandler', N'Kiểm tra cắt khoảng trắng Title và xác nhận SaveChangesAsync được gọi đúng 1 lần.', 0, '2026-11-24T08:45:00.000', '2026-12-05'),
    (N'Viết Unit Test cho kịch bản không tìm thấy ID khi xóa', N'Đảm bảo DeleteTodoItemCommandHandler trả về false và không gọi SaveChangesAsync.', 0, '2026-11-25T01:10:00.000', '2026-12-06'),
    (N'Viết Integration Test với Testcontainers SQL Server', N'Kiểm thử xuyên suốt từ HTTP Controller xuống container SQL Server thật.', 0, '2026-11-25T03:35:00.000', '2026-12-10'),
    (N'Bật cơ chế EnableRetryOnFailure cho EF Core SQL Server', N'Tự động thử lại khi gặp lỗi kết nối mạng thoáng qua trong môi trường container.', 0, '2026-11-25T05:00:00.000', '2026-12-02'),
    (N'Thêm Health Checks (/health/live và /health/ready)', N'Kiểm tra trạng thái sống của ứng dụng và khả năng kết nối tới TodoAppDB.', 0, '2026-11-25T07:25:00.000', '2026-12-07'),
    (N'Cấu hình Serilog ghi Structured Logging ra Console', N'Gắn kèm TraceId và thời gian xử lý cho từng HTTP Request.', 0, '2026-11-25T09:00:00.000', '2026-12-08'),
    (N'Đóng gói Dockerfile cho dịch vụ TodoApp.API', N'Sử dụng multi-stage build với ảnh nền mcr.microsoft.com/dotnet/aspnet:8.0.', 0, '2026-11-25T11:15:00.000', '2026-12-12'),
    (N'Tích hợp dịch vụ TodoApp.API vào docker-compose.yml', N'Thiết lập mạng nội bộ và biến môi trường kết nối trực tiếp sang container mssql.', 0, '2026-11-25T13:30:00.000', '2026-12-13'),
    (N'Thiết lập GitHub Actions chạy build và test tự động', N'Chặn merge Pull Request nếu biên dịch lỗi hoặc có bài test thất bại.', 0, '2026-11-25T15:00:00.000', '2026-12-15'),
    (N'Bổ sung bảng Categories phân loại nhóm công việc', N'Thêm khóa ngoại CategoryId vào bảng TodoItems và scaffold lại mô hình.', 0, '2026-11-26T00:30:00.000', '2026-12-18'),
    (N'Thêm mức độ ưu tiên (Priority) cho công việc', N'Phân loại Thấp, Trung bình, Cao và Khẩn cấp.', 0, '2026-11-26T01:00:00.000', '2026-12-20'),
    (N'Tích hợp Redis Cache cho truy vấn xem chi tiết công việc', N'Áp dụng Decorator Pattern bọc quanh ITodoItemRepository mà không sửa tầng Application.', 0, '2026-11-26T01:40:00.000', '2026-12-22'),
    (N'Nghiên cứu mẫu Transactional Outbox trong Microservice', N'Đảm bảo lưu TodoItem và sự kiện thông báo trong cùng một giao dịch UnitOfWork.', 0, '2026-11-26T02:10:00.000', '2026-12-25'),
    (N'Chuẩn bị slide thuyết trình bảo vệ dự án TodoApp', N'Trình bày sơ đồ tiến hóa kiến trúc và demo trực tiếp 5 API trên Swagger.', 0, '2026-11-26T02:45:00.000', '2026-11-27'),
    (N'Cập nhật các ví dụ kiểm thử trong file TodoApp.API.http', NULL, 0, '2026-11-26T03:05:00.000', '2026-11-26'),
    (N'Kiểm tra hiệu năng truy vấn bằng SQL Server Execution Plan', N'Đánh giá index cần thiết khi lọc theo IsCompleted và sắp xếp theo DueDate.', 0, '2026-11-26T03:30:00.000', '2026-12-16'),
    (N'Đọc tài liệu Microsoft về IExceptionHandler trong .NET 8', NULL, 1, '2026-11-19T02:00:00.000', '2026-11-20'),
    (N'Ôn tập câu hỏi phỏng vấn về Dependency Inversion và DI Lifetimes', N'Phân biệt rõ Transient, Scoped và Singleton khi làm việc với DbContext.', 0, '2026-11-26T04:00:00.000', '2026-11-28'),
    (N'Rà soát các bản ghi quá hạn chưa hoàn thành tuần trước', N'Kiểm tra trường hợp DueDate nhỏ hơn ngày hiện tại để hiển thị cảnh báo quá hạn.', 0, '2026-11-18T01:00:00.000', '2026-11-22'),
    (N'Gửi biên bản họp nhóm thiết kế API tuần 47', N'Bản ghi mẫu quá hạn để kiểm thử giao diện lọc công việc trễ hạn.', 0, '2026-11-19T04:30:00.000', '2026-11-24'),
    (N'Sao lưu dữ liệu mẫu TodoAppDB trước khi demo', NULL, 0, '2026-11-26T05:00:00.000', '2026-11-27'),
    (N'Tổng kết lộ trình nâng cấp TodoApp sang kiến trúc Microservice', N'Hoàn thiện tài liệu đánh giá kỹ thuật và kế hoạch phát triển giai đoạn 2.', 0, '2026-11-26T06:50:00.000', '2026-12-30');

COMMIT TRANSACTION;
GO
