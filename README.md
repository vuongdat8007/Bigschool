# Bigschool - Phần Mềm Quản Lý Trường Tiểu Học

## Giới thiệu
Bigschool_TH_11 là một dự án ASP.NET MVC 5 được xây dựng để quản lý trường tiểu học, sử dụng các công nghệ như Datatables, jQuery, Bootstrap 4 và jQuery Validate.

## Tính năng
- Quản lý thông tin cán bộ nhân viên (CBNV)
- Quản lý khen thưởng kỷ luật của CBNV
- Quản lý danh sách khen thưởng và kỷ luật
- Hiển thị, thêm, sửa, xóa dữ liệu thông qua giao diện người dùng
- Sử dụng Datatables để phân trang, tìm kiếm và sắp xếp dữ liệu
- Sử dụng Bootstrap 4 để thiết kế giao diện người dùng đẹp mắt và thân thiện
- Sử dụng jQuery và jQuery Validate để xử lý và kiểm tra dữ liệu trên phía người dùng

## Cài đặt
1. Sao chép dự án từ Github hoặc tải xuống mã nguồn
2. Mở dự án trong Visual Studio
3. Cài đặt các gói NuGet cần thiết (nếu chưa được cài đặt):
   - Microsoft.EntityFrameworkCore.Tools
   - Microsoft.EntityFrameworkCore.SqlServer
4. Cấu hình chuỗi kết nối đến cơ sở dữ liệu trong file `Web.config`
5. Chạy dự án bằng cách nhấn F5 trong Visual Studio

Để chạy dự án BigSchool, bạn cần thực hiện các bước sau:

1. Cài đặt Visual Studio: Đảm bảo bạn đã cài đặt Visual Studio với phiên bản phù hợp (nếu chưa có). Bạn có thể tải xuống Visual Studio từ trang chủ của Microsoft tại đây:
- https://visualstudio.microsoft.com/downloads/

2. Tải xuống mã nguồn của dự án: Tải xuống mã nguồn của dự án BigSchool tại phần Code > Download code phía trên hoặc Clone this repository (nếu bạn chưa có) và giải nén nó vào một thư mục trên máy tính của bạn.

3. Mở dự án trong Visual Studio: Mở Visual Studio và chọn "Open a project or solution" (Mở một dự án hoặc giải pháp) từ màn hình chào mừng hoặc chọn "File" > "Open" > "Project/Solution..." từ thanh menu. Tìm đến thư mục dự án BigSchool và mở tập tin .sln (giải pháp).

4. Cài đặt các gói NuGet cần thiết: Trong Visual Studio, mở "Solution Explorer" (Khám phá giải pháp), nhấp chuột phải vào tên giải pháp (Solution) và chọn "Restore NuGet Packages" (Khôi phục gói NuGet). Điều này sẽ cài đặt tất cả các gói NuGet cần thiết cho dự án.

5. Cấu hình chuỗi kết nối cơ sở dữ liệu: Mở tập tin Web.config trong dự án và tìm đến phần <connectionStrings>. Thay đổi giá trị của thuộc tính connectionString để nó trỏ đến cơ sở dữ liệu SQL Server của bạn.

6. Khởi tạo cơ sở dữ liệu: Trong Visual Studio, mở "Package Manager Console" (Bảng điều khiển quản lý gói) bằng cách chọn "Tools" > "NuGet Package Manager" > "Package Manager Console" từ thanh menu. Gõ lệnh sau và nhấn Enter: **Update-Database**. Điều này sẽ khởi tạo cơ sở dữ liệu dựa trên mã nguồn của bạn.

7. Chạy dự án: Nhấp chuột phải vào dự án web (thường có tên là BigSchool_TH_11) trong "Solution Explorer" và chọn "Set as StartUp Project" (Đặt làm dự án khởi động). Sau đó, nhấn F5 hoặc chọn "Debug" > "Start Debugging" từ thanh menu để khởi chạy dự án. Trình duyệt sẽ mở và hiển thị trang chủ của ứng dụng BigSchool.

Bây giờ bạn đã sẵn sàng để sử dụng và phát triển dự án BigSchool!

## Lưu ý
Trước khi chạy dự án, hãy đảm bảo rằng bạn đã cấu hình đúng chuỗi kết nối đến cơ sở dữ liệu SQL Server trong file `Web.config`.

## Tác giả
- Họ tên: Vương Quốc Đạt - MSSV: 2010060077
- Họ tên: Nguyễn Thị Trà My - MSSV: 
- Họ tên: Nguyễn Hoàng Yến - MSSV: 
- Lớp: 20TXTH02 - ĐH HuTech

## Giấy phép
Dự án này được phân phối theo giấy phép MIT. Xem tệp [LICENSE](LICENSE) để biết thêm thông tin.
