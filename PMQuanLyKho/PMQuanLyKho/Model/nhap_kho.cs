//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PMQuanLyKho.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class nhap_kho
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public nhap_kho()
        {
            this.xuat_Kho = new HashSet<xuat_Kho>();
        }
    
        public string id { get; set; }
        public string id_vat_tu { get; set; }
        public int id_loai { get; set; }
        public int id_nguoi_dung { get; set; }
        public Nullable<int> so_luong { get; set; }
        public Nullable<System.DateTime> ngay_nhap { get; set; }
        public Nullable<double> gia_nhap { get; set; }
        public Nullable<double> gia_ban { get; set; }
        public string mo_ta { get; set; }
    
        public virtual loai loai { get; set; }
        public virtual nguoi_dung nguoi_dung { get; set; }
        public virtual vat_tu vat_tu { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<xuat_Kho> xuat_Kho { get; set; }
    }
}
