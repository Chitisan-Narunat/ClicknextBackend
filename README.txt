KanbanBoardAPI
RESTful API สำหรับจัดการ Kanban Board ที่พัฒนาโดยใช้ ASP.NET Core 8 และ PostgreSQL
รองรับการใช้งานหลายบอร์ด, คอลัมน์, การ์ด และผู้ใช้งาน พร้อมระบบยืนยันตัวตน (Authentication)
และรองรับการใช้งานผ่าน Docker และ Docker Compose

เทคโนโลยีที่ใช้
- ASP.NET Core 8 
- Entity Framework Core
- PostgreSQL
- Docker + Docker Compose
- Swagger 

การใช้งาน
ดาวน์โหลดโปรเจกต์ หรือ git clone จาก GitHub / Google Drive

เปิดโปรเจกต์ด้วย Visual Studio, VS Code หรือ IDE ที่รองรับ .NET

รันคำสั่ง: docker compose up --build ถ้าไม่ได้ลองอีกรอบนะครับ
คำสั่งนี้จะทำการ build และรัน container ทั้งหมดโดยอัตโนมัติ ซึ่งประกอบด้วย:
- ASP.NET API Container
- PostgreSQL Container

และจะทำการ migrate database อัตโนมัติด้วยโค้ดใน Program.cs: (ไม่ต้องทำ auto)

app.MapControllers();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

รอสักประมาณ30วินะครับ ค่อยเข้า

การเข้าถึง API
API พร้อมใช้งานที่:
http://localhost:5000/swagger

ตรวจสอบฐานข้อมูลผ่าน pgAdmin ได้ที่:
http://localhost:5050/
โดยใช้   Username: admin@admin.com
        Password: admin

วิธีตั้งค่าเชื่อมต่อ pgAdmin กับ PostgreSQL
เข้าไปที่ pgAdmin (ผ่าน URL ด้านบน)

เพิ่ม New Server
ในหน้า General > Name ตั้งชื่ออะไรก็ได้
ในหน้า Connection กรอกข้อมูลดังนี้:

Host name/address: db
Port: 5432
Maintenance database: KanBanClicknext1
Username: postgres
Password: wdsa12344

กด Save

เข้าไปดูข้อมูลฐานข้อมูลได้ที่:
Servers > [ชื่อ server ที่ตั้ง] > Databases > KanBanClicknext1 > Schemas > public > Tables
สามารถคลิกขวาเพื่อดูข้อมูล หรือรันคำสั่ง SQL query ได้

ขอบคุณครับ