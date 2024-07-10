using System;
using System.Collections.Generic;

namespace SoSucKhoe.Models.Main;

public partial class PhieuDinhKy
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public int? HuyetAp { get; set; }

    public int? NhipTim { get; set; }

    public double? ChieuCao { get; set; }

    public double? CanNang { get; set; }
    public string? Hoten { get; set; }
    public string? HinhPhieuKham { get; set; }
    public string? GhiChu { get; set; }



    public string? NgayKham { get; set; }

    public virtual Person? Person { get; set; }
}
