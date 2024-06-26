using System;
using System.Collections.Generic;

namespace SoSucKhoe.Models.Main;

public partial class PhieuSucKhoe
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public DateTime? NgayKham { get; set; }

    public string? Diadiem { get; set; }

    public string? TenBenh { get; set; }

    public double? ChiPhi { get; set; }

    public string? LuuY { get; set; }

    public string? LoaiBenh { get; set; }

    public string? TrieuChung { get; set; }

    public string? Kham { get; set; }

    public string? DinhBenh { get; set; }

    public string? MoTaToaThuoc { get; set; }

    public string? HinhToaThuoc { get; set; }

    public virtual Person? Person { get; set; }
}
