using System;
using System.Collections.Generic;

namespace SoSucKhoe.Models.Main;

public partial class PhieuDinhKy
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public TimeSpan? Thoigian { get; set; }

    public int? HuyetAp { get; set; }

    public int? NhipTim { get; set; }

    public double? ChieuCao { get; set; }

    public double? CanNang { get; set; }

    public DateTime? NgayKham { get; set; }

    public virtual Person? Person { get; set; }
}
