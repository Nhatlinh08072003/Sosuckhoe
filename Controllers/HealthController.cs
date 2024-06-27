using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SoSucKhoe.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SoSucKhoe.Models.Main;
using SoSucKhoe.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace SoSucKhoe.Controllers;

public class HealthController : Controller
{
    private readonly SosuckhoeDbContext _sosuckhoeDbContext;
    private readonly IHttpContextAccessor _httpContext;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IConfiguration _configuration;

    // Constructor
    public HealthController(SosuckhoeDbContext sosuckhoeDbContext, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
    {
        _sosuckhoeDbContext = sosuckhoeDbContext;
        _httpContext = httpContextAccessor;
        _hostingEnvironment = hostingEnvironment;
        _configuration = configuration;
    }
    public IActionResult Suckhoehangngay()
    {
        return View();
    }
    public IActionResult Chiphi()
    {
        return View();
    }
    public IActionResult Theodoisuckhoe()
    {
        return View();
    }
    public IActionResult Lichtiemchung()
    {
        return View();
    }
    [HttpPost("/health/addlichtiem")]
    public async Task<IActionResult> AddLichTiem(
 [FromForm] string fullname,
 [FromForm] string ngay_tiem,
 [FromForm] string loai_vac_xin,
 [FromForm] string dia_diem,
 [FromForm] string chi_phi,
 [FromForm] string ghi_chu,
 [FromForm] string mo_ta,
 [FromForm] string tinh_trang
 )
    {
        if (ModelState.IsValid)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy thông tin người dùng." });
                }
                var userId = int.Parse(userIdClaim.Value);
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SosuckhoeConnectionString")))
                {
                    await connection.OpenAsync();

                    string sql = @"
                    INSERT INTO Lichtiem (PersonId, NgayTiem, LoaiVacxin, DiadiemTiem, TinhTrang, Mota, GhiChu, ChiPhi,Hoten)
VALUES   (@PersonId, @NgayTiem, @LoaiVacxin, @DiadiemTiem, @TinhTrang, @Mota, @GhiChu, @ChiPhi,@Hoten)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PersonId", userId);
                        command.Parameters.AddWithValue("@NgayTiem", ngay_tiem);
                        command.Parameters.AddWithValue("@LoaiVacxin", loai_vac_xin);
                        command.Parameters.AddWithValue("@DiadiemTiem", dia_diem);
                        command.Parameters.AddWithValue("@Mota", mo_ta);
                        command.Parameters.AddWithValue("@GhiChu", ghi_chu);
                        command.Parameters.AddWithValue("@ChiPhi", Double.Parse(chi_phi));
                        command.Parameters.AddWithValue("@Hoten", fullname);
                        command.Parameters.AddWithValue("@TinhTrang", tinh_trang);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { success = true, message = "Thêm lịch tiêm thành công." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false, message = $"Lỗi khi lưu lịch tiêm {ex.Message}" });
            }
        }

        return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
    }
    // sửa lịch tiêm
    [HttpPost("/health/editlichtiem")]
    public async Task<IActionResult> EditLichTiem(
   [FromForm] int idlichtiem,
   [FromForm] string fullname,
   [FromForm] string ngay_tiem,
   [FromForm] string loai_vac_xin,
   [FromForm] string dia_diem,
   [FromForm] string chi_phi,
   [FromForm] string ghi_chu,
   [FromForm] string mo_ta,
   [FromForm] string tinh_trang)
    {
        try
        {

            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SosuckhoeConnectionString")))
            {
                await connection.OpenAsync();

                string sql = @"
            UPDATE Lichtiem
            SET NgayTiem = @NgayTiem,
                LoaiVacxin = @LoaiVacxin,
                DiadiemTiem = @DiadiemTiem,
                TinhTrang = @TinhTrang,
                Mota = @Mota,
                GhiChu = @GhiChu,
                ChiPhi = @ChiPhi,
                Hoten = @Hoten
            WHERE Id = @Id";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@NgayTiem", ngay_tiem);
                    command.Parameters.AddWithValue("@LoaiVacxin", loai_vac_xin);
                    command.Parameters.AddWithValue("@DiadiemTiem", dia_diem);
                    command.Parameters.AddWithValue("@TinhTrang", tinh_trang);
                    command.Parameters.AddWithValue("@Mota", mo_ta);
                    command.Parameters.AddWithValue("@GhiChu", ghi_chu);
                    command.Parameters.AddWithValue("@ChiPhi", chi_phi);
                    command.Parameters.AddWithValue("@Hoten", fullname);
                    command.Parameters.AddWithValue("@Id", idlichtiem);
                    await command.ExecuteNonQueryAsync();
                }
            }

            return Json(new { success = true, message = "Lịch tiêm đã được cập nhật thành công." });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Json(new { success = false, message = $"Lỗi khi cập nhật Lịch tiêm: {ex.Message}" });
        }
    }
    public IActionResult Phieukhamsuckhoe()
    {
        return View();
    }
    [HttpPost("/health/addsuckhoe")]
    public async Task<IActionResult> AddSucKhoe(
    [FromForm] string fullname,
    [FromForm] string ngay_kham,
    [FromForm] string ten_benh,
    [FromForm] string loai_benh,
    [FromForm] string trieu_chung,
    [FromForm] string dia_diem,
    [FromForm] string dinh_benh,
    [FromForm] string mo_ta_toa_thuoc,
    [FromForm] string kham,
    [FromForm] string luu_y,
    [FromForm] string chi_phi,
    [FromForm] IFormFile toa_thuoc_image)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy thông tin người dùng." });
                }
                var userId = int.Parse(userIdClaim.Value);
                string imageUrl = await SaveImageAsync(toa_thuoc_image);
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SosuckhoeConnectionString")))
                {
                    await connection.OpenAsync();

                    string sql = @"
                    INSERT INTO PhieuSucKhoe (PersonId, NgayKham, Diadiem, TenBenh, ChiPhi, LuuY, LoaiBenh, TrieuChung, Kham, DinhBenh, MoTaToaThuoc, HinhToaThuoc, Hoten)
VALUES  (@PersonId, @NgayKham, @Diadiem, @TenBenh, @ChiPhi, @LuuY, @LoaiBenh, @TrieuChung, @Kham, @DinhBenh, @MoTaToaThuoc, @HinhToaThuoc, @Hoten)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PersonId", userId);
                        command.Parameters.AddWithValue("@NgayKham", ngay_kham);
                        command.Parameters.AddWithValue("@Diadiem", dia_diem);
                        command.Parameters.AddWithValue("@TenBenh", ten_benh);
                        command.Parameters.AddWithValue("@ChiPhi", Double.Parse(chi_phi));
                        command.Parameters.AddWithValue("@LuuY", luu_y);
                        command.Parameters.AddWithValue("@LoaiBenh", loai_benh);
                        command.Parameters.AddWithValue("@TrieuChung", trieu_chung);
                        command.Parameters.AddWithValue("@Kham", kham);
                        command.Parameters.AddWithValue("@DinhBenh", dinh_benh);
                        command.Parameters.AddWithValue("@MoTaToaThuoc", mo_ta_toa_thuoc);
                        command.Parameters.AddWithValue("@Hoten", fullname);
                        command.Parameters.AddWithValue("@HinhToaThuoc", imageUrl);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { success = true, message = "Thêm phiếu sức khỏe thành công." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false, message = $"Lỗi khi lưu phiếu sức khỏe {ex.Message}" });
            }
        }

        return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
    }
    // sửa phiếu sứa khỏe
    [HttpPost("/health/editsuckhoe")]
    public async Task<IActionResult> EditSucKhoe(
      [FromForm] int idsuckhoe,
      [FromForm] string fullname,
      [FromForm] string ngay_kham,
      [FromForm] string ten_benh,
      [FromForm] string loai_benh,
      [FromForm] string trieu_chung,
      [FromForm] string dia_diem,
      [FromForm] string dinh_benh,
      [FromForm] string mo_ta_toa_thuoc,
      [FromForm] string kham,
      [FromForm] string luu_y,
      [FromForm] string chi_phi,
      [FromForm] IFormFile toathuoc_image_url,
      [FromForm] string toathuoc_image)
    {
        if (ModelState.IsValid)
        {
            try
            {
                string imageUrl = toathuoc_image; // Mặc định là URL hiện tại

                if (toathuoc_image_url != null && toathuoc_image_url.Length > 0)
                {
                    // Nếu có file ảnh mới được chọn, lưu ảnh và lấy URL mới
                    imageUrl = await SaveImageAsync(toathuoc_image_url);
                }
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SosuckhoeConnectionString")))
                {
                    await connection.OpenAsync();

                    string sql = @"
                  UPDATE PhieuSucKhoe
            SET NgayKham = @NgayKham,
                Diadiem = @Diadiem,
                TenBenh = @TenBenh,
                ChiPhi = @ChiPhi,
                LuuY = @LuuY,
                LoaiBenh = @LoaiBenh,
                TrieuChung = @TrieuChung,
                Kham = @Kham,
                DinhBenh = @DinhBenh,
                MoTaToaThuoc = @MoTaToaThuoc,
                HinhToaThuoc = @HinhToaThuoc,
                Hoten = @Hoten
            WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", idsuckhoe);
                        command.Parameters.AddWithValue("@NgayKham", ngay_kham);
                        command.Parameters.AddWithValue("@Diadiem", dia_diem);
                        command.Parameters.AddWithValue("@TenBenh", ten_benh);
                        command.Parameters.AddWithValue("@ChiPhi", Double.Parse(chi_phi));
                        command.Parameters.AddWithValue("@LuuY", luu_y);
                        command.Parameters.AddWithValue("@LoaiBenh", loai_benh);
                        command.Parameters.AddWithValue("@TrieuChung", trieu_chung);
                        command.Parameters.AddWithValue("@Kham", kham);
                        command.Parameters.AddWithValue("@DinhBenh", dinh_benh);
                        command.Parameters.AddWithValue("@MoTaToaThuoc", mo_ta_toa_thuoc);
                        command.Parameters.AddWithValue("@Hoten", fullname);
                        command.Parameters.AddWithValue("@HinhToaThuoc", imageUrl);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { success = true, message = "Sửa phiếu sức khỏe thành công." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false, message = $"Lỗi khi sửa phiếu sức khỏe {ex.Message}" });
            }
        }

        return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
    }
    private async Task<string> SaveImageAsync(IFormFile imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
        {
            throw new ArgumentException("File ảnh không hợp lệ.");
        }

        string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
        string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        }

        return "/images/" + uniqueFileName;
    }

    public IActionResult Phieusuckhoedinhki()
    {
        return View();
    }
    [HttpPost("/health/adddinhki")]
    public async Task<IActionResult> AddDinhKi(
  [FromForm] string fullname,
  [FromForm] string ngay_kham,
  [FromForm] string huyet_ap,
  [FromForm] string nhip_tim,
  [FromForm] string chieu_cao,
  [FromForm] string can_nang)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy thông tin người dùng." });
                }
                var userId = int.Parse(userIdClaim.Value);
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SosuckhoeConnectionString")))
                {
                    await connection.OpenAsync();

                    string sql = @"
                    INSERT INTO PhieuDinhKy (PersonId, HuyetAp, NhipTim, ChieuCao, CanNang, NgayKham,Hoten)
VALUES   (@PersonId, @HuyetAp, @NhipTim, @ChieuCao, @CanNang, @NgayKham,@Hoten)";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PersonId", userId);
                        command.Parameters.AddWithValue("@HuyetAp", int.Parse(huyet_ap));
                        command.Parameters.AddWithValue("@NhipTim", int.Parse(nhip_tim));
                        command.Parameters.AddWithValue("@ChieuCao", Double.Parse(chieu_cao));
                        command.Parameters.AddWithValue("@CanNang", Double.Parse(can_nang));
                        command.Parameters.AddWithValue("@NgayKham", ngay_kham);
                        command.Parameters.AddWithValue("@Hoten", fullname);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { success = true, message = "Thêm phiếu sức khỏe định kì thành công." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false, message = $"Lỗi khi lưu phiếu sức khỏe định kì {ex.Message}" });
            }
        }

        return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
    }
    // sửa phiếu sứa khỏe
    [HttpPost("/health/editdinhki")]
    public async Task<IActionResult> EditDinhKi(
      [FromForm] int iddinhki,
      [FromForm] string fullname,
     [FromForm] string ngay_kham,
  [FromForm] string huyet_ap,
  [FromForm] string nhip_tim,
  [FromForm] string chieu_cao,
  [FromForm] string can_nang)
    {
        if (ModelState.IsValid)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("SosuckhoeConnectionString")))
                {
                    await connection.OpenAsync();

                    string sql = @"
                  UPDATE PhieuDinhKy
            SET HuyetAp = @HuyetAp,
                NhipTim = @NhipTim,
                ChieuCao = @ChieuCao,
                CanNang = @CanNang,
                NgayKham = @NgayKham,
                Hoten = @Hoten
            WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", iddinhki);
                        command.Parameters.AddWithValue("@HuyetAp", int.Parse(huyet_ap));
                        command.Parameters.AddWithValue("@NhipTim", int.Parse(nhip_tim));
                        command.Parameters.AddWithValue("@ChieuCao", Double.Parse(chieu_cao));
                        command.Parameters.AddWithValue("@CanNang", Double.Parse(can_nang));
                        command.Parameters.AddWithValue("@NgayKham", ngay_kham);
                        command.Parameters.AddWithValue("@Hoten", fullname);
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return Json(new { success = true, message = "Sửa phiếu sức khỏe định kì thành công." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return Json(new { success = false, message = $"Lỗi khi sửa phiếu sức khỏe định kì {ex.Message}" });
            }
        }

        return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
    }
    public IActionResult Thongketiemchung()
    {
        var lichtiem = _sosuckhoeDbContext.Lichtiems.ToList();
        return View(lichtiem);
    }
    [Route("chitietphieusuckhoe/{id}")]
    public IActionResult Chitietphieusuckhoe(int id)
    {
        var phieusuckhoe = _sosuckhoeDbContext.PhieuSucKhoes.FirstOrDefault(p => p.Id == id);
        if (phieusuckhoe == null)
        {
            return NotFound();
        }
        return View(phieusuckhoe);
    }
    [Route("chitietphieusuckhoedinhki/{id}")]

    public IActionResult Chitietphieusuckhoedinhki(int id)
    {
        var phieusuckhoedinhki = _sosuckhoeDbContext.PhieuDinhKies.FirstOrDefault(p => p.Id == id);
        if (phieusuckhoedinhki == null)
        {
            return NotFound();
        }
        return View(phieusuckhoedinhki);
    }
    [Route("chitietlichtiem/{id}")]
    public IActionResult Chitietlichtiem(int id)
    {
        var lichtiem = _sosuckhoeDbContext.Lichtiems.FirstOrDefault(p => p.Id == id);
        if (lichtiem == null)
        {
            return NotFound();
        }
        return View(lichtiem);
    }
    public IActionResult Thongkephieusuckhoe()
    {
        var phieusuckhoe = _sosuckhoeDbContext.PhieuSucKhoes.ToList();
        return View(phieusuckhoe);
    }
    public IActionResult Thongkesuckhoedinhki()
    {
        var phieudinhky = _sosuckhoeDbContext.PhieuDinhKies.ToList();
        return View(phieudinhky);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
