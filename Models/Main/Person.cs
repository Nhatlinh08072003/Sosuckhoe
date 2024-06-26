using System;
using System.Collections.Generic;

namespace SoSucKhoe.Models.Main;

public partial class Person
{
    public int Id { get; set; }
    public int Age { get; set; }


    public string? Hoten { get; set; }

    public string? Username { get; set; }

    public int? Phone { get; set; }

    public string? AddressUser { get; set; }

    public string? Pass { get; set; }

    public string? RoleUser { get; set; }
    public string? Gender { get; set; }


    public virtual ICollection<Lichtiem> Lichtiems { get; set; } = new List<Lichtiem>();

    public virtual ICollection<PhieuDinhKy> PhieuDinhKies { get; set; } = new List<PhieuDinhKy>();

    public virtual ICollection<PhieuSucKhoe> PhieuSucKhoes { get; set; } = new List<PhieuSucKhoe>();
}
