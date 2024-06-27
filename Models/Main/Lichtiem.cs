using System;
using System.Collections.Generic;

namespace SoSucKhoe.Models.Main;

public partial class Lichtiem
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public string? NgayTiem { get; set; }
    public string? Hoten { get; set; }


    public string? LoaiVacxin { get; set; }

    public string? DiadiemTiem { get; set; }

    public string? TinhTrang { get; set; }

    public string? Mota { get; set; }

    public string? GhiChu { get; set; }
    public double? ChiPhi { get; set; }

    public virtual Person? Person { get; set; }
}
