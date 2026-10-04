using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementMVC.Data;
using StudentManagementMVC.Models;

namespace StudentManagementMVC.Controllers;

public class StudentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentsController(ApplicationDbContext context) => _context = context;

    public async Task<IActionResult> Index(string? searchString)
    {
        var query = _context.Students.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            searchString = searchString.Trim();
            query = query.Where(s => s.StudentCode.Contains(searchString) || s.FullName.Contains(searchString));
        }
        ViewBag.SearchString = searchString;
        return View(await query.OrderBy(s => s.Id).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        return student == null ? NotFound() : View(student);
    }

    [HttpGet]
    public IActionResult Create() => View(new Student());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        // Loại bỏ khoảng trắng thừa do người dùng nhập.
        student.StudentCode = student.StudentCode?.Trim() ?? string.Empty;
        student.FullName = student.FullName?.Trim() ?? string.Empty;
        student.Email = student.Email?.Trim() ?? string.Empty;
        student.Phone = string.IsNullOrWhiteSpace(student.Phone) ? null : student.Phone.Trim();
        student.Major = student.Major?.Trim() ?? string.Empty;

        if (ModelState.IsValid)
        {
            if (await _context.Students.AnyAsync(s => s.StudentCode == student.StudentCode))
                ModelState.AddModelError(nameof(Student.StudentCode), "Mã sinh viên đã tồn tại. Hãy nhập mã khác.");
        }

        if (!ModelState.IsValid)
            return View(student);

        try
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Thêm sinh viên thành công!";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Không thể lưu sinh viên vào SQL Server. Hãy kiểm tra kết nối database hoặc mã sinh viên có bị trùng không.");
            return View(student);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.FindAsync(id);
        return student == null ? NotFound() : View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id) return NotFound();

        student.StudentCode = student.StudentCode?.Trim() ?? string.Empty;
        student.FullName = student.FullName?.Trim() ?? string.Empty;
        student.Email = student.Email?.Trim() ?? string.Empty;
        student.Phone = string.IsNullOrWhiteSpace(student.Phone) ? null : student.Phone.Trim();
        student.Major = student.Major?.Trim() ?? string.Empty;

        if (ModelState.IsValid)
        {
            if (await _context.Students.AnyAsync(s => s.StudentCode == student.StudentCode && s.Id != id))
                ModelState.AddModelError(nameof(Student.StudentCode), "Mã sinh viên đã tồn tại. Hãy nhập mã khác.");
        }

        if (!ModelState.IsValid)
            return View(student);

        try
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật sinh viên thành công!";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Không thể cập nhật sinh viên vào SQL Server.");
            return View(student);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        return student == null ? NotFound() : View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }
        TempData["Success"] = "Xóa sinh viên thành công!";
        return RedirectToAction(nameof(Index));
    }
}
