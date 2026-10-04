using System.ComponentModel.DataAnnotations;
namespace StudentManagementMVC.Models;
public class Student {
 public int Id {get;set;}
 [Display(Name="Mã sinh viên"),Required(ErrorMessage="Vui lòng nhập mã sinh viên"),StringLength(20)] public string StudentCode {get;set;}=string.Empty;
 [Display(Name="Họ và tên"),Required(ErrorMessage="Vui lòng nhập họ tên"),StringLength(100)] public string FullName {get;set;}=string.Empty;
 [Display(Name="Email"),Required(ErrorMessage="Vui lòng nhập email"),EmailAddress(ErrorMessage="Email không hợp lệ"),StringLength(150)] public string Email {get;set;}=string.Empty;
 [Display(Name="Số điện thoại"),Phone(ErrorMessage="Số điện thoại không hợp lệ"),StringLength(20)] public string? Phone {get;set;}
 [Display(Name="Giới tính"),Required(ErrorMessage="Vui lòng chọn giới tính")] public string Gender {get;set;}="Nam";
 [Display(Name="Ngày sinh"),DataType(DataType.Date)] public DateTime? DateOfBirth {get;set;}
 [Display(Name="Chuyên ngành"),Required(ErrorMessage="Vui lòng nhập chuyên ngành"),StringLength(100)] public string Major {get;set;}=string.Empty;
 [Display(Name="GPA"),Range(0,10,ErrorMessage="GPA phải từ 0 đến 10")] public double GPA {get;set;}
}