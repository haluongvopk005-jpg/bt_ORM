using StudentManagementMVC.Models;
namespace StudentManagementMVC.Data;
public static class DbInitializer { public static void Seed(ApplicationDbContext context){ if(context.Students.Any()) return; context.Students.AddRange(
 new Student{StudentCode="SV001",FullName="Nguyễn Văn An",Email="an@gmail.com",Phone="0901000001",Gender="Nam",Major="Công nghệ thông tin",GPA=8.20,DateOfBirth=new DateTime(2004,5,12)},
 new Student{StudentCode="SV002",FullName="Trần Thị Bình",Email="binh@gmail.com",Phone="0901000002",Gender="Nữ",Major="Kinh tế",GPA=8.70,DateOfBirth=new DateTime(2004,8,21)},
 new Student{StudentCode="SV003",FullName="Lê Minh Đức",Email="duc@gmail.com",Phone="0901000003",Gender="Nam",Major="Kỹ thuật phần mềm",GPA=7.90,DateOfBirth=new DateTime(2005,1,15)}); context.SaveChanges(); } }