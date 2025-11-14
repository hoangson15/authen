using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ThisGameIsSoFun.Database;
using ThisGameIsSoFun.Extensions;
using ThisGameIsSoFun.Models;


// Tạo 1 builder mới, điểm khởi đầu để cấu hình ứng dụng .net core: 
// đọc cấu hình (appsetting, môi trường, secrets...), đăng ký dịch vụ (dependency injection), logging...
// => dựng bộ khung của app
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Thêm dịch vụ hỗ trợ các controller vào ứng dụng
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Thêm dịch vụ đọc các metadata từ các endpoint để tạo tài liệu API tự động cho swagger => trả về danh sách endpoint, ko vẽ UI
builder.Services.AddEndpointsApiExplorer();

// Thêm dịch vụ tạo UI cho swagger dựa trên metadata từ AddEndpointsApiExplorer
builder.Services.AddSwaggerGen();

// Kích hoạt middleware phân quyền
builder.Services.AddAuthorization();
builder.Services
// Kích hoạt middleware xác thực
    .AddAuthentication()
// App dùng cookie-based auth theo scheme mặc định của Identity
    .AddBearerToken();
    //.AddCookie(IdentityConstants.ApplicationScheme);
    
builder.Services
    // Khai báo Idenity với kiểu User tùy chỉnh
    .AddIdentityCore<User>()
    // Khai báo Identity rằng user, role sẽ được lưu bằng Entity Framework với DbContext là ApplicationDbContext
    .AddEntityFrameworkStores<ApplicationDbContext>()
    // của .NET 8, tự sinh ra các endpoint API cho 
    .AddApiEndpoints();

// Đăng ký DbContext với SQL Server, chuỗi kết nối lấy từ file cấu hình appsetting.json
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MainDBContext")));

// Dựng ứng dụng
// DI container khóa
// Middleware pipeline dựng xong
// Ứng dụng sẵn sàng chạy
var app = builder.Build();

// Configure the HTTP request pipeline.
// Nếu là môi trường dev - Bật swaggerUI và apply migrations 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // Apply migrations 
    app.ApplyMigrations();
}

// .NET 8 mới có
// Tự tạo ra bộ API endpoint cho Identity
app.MapIdentityApi<User>();

// Chuyển hướng tất cả các request HTTP sang HTTPS
app.UseHttpsRedirection();

// Bật middleware xác thực
// Phải đứng sau middleware UseAuthentication nếu có
app.UseAuthorization();

// Kích hoạt các route cho controller
// Nếu không gọi dòng này, các controller sẽ không hoạt động
app.MapControllers();

// Khởi động webserver Kestrel và chạy ứng dụng
app.Run();

// Dependency Injection: là kỹ thuật lập trình để quản lý việc khởi tạo và cung cấp các đối tượng (services) cho các thành phần khác trong ứng dụng một cách tự động và linh hoạt
// Swagger: là công cụ giúp tự động tạo tài liệu API và giao diện thử nghiệm API dựa trên các endpoint đã định nghĩa trong ứng dụng .NET

// Middleware: là danh sách các phần code bắt đầu = app.use
// Request pineline: là tập hợp các middleware được sắp xếp theo 1 thứ tự nhất định, 1 request sẽ đi qua từng middleware đến khi trả về response
// Kestrel: là webserver được tích hợp sẵn trong .NET, chịu trách nhiệm lắng nghe các request HTTP từ client gửi lên và chuyển vào trong ứng dụng .NET để xử lý
//          Khi chạy ứng dụng = http/https/WSL => ứng dụng sẽ sử dụng Kestrel làm server
//          Kestrel có thể chạy độc lập hoặc kết hợp với webserver khác như IIS, Nginx để tăng hiệu năng và bảo mật