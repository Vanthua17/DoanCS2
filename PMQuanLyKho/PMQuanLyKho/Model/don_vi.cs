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
    
    public partial class don_vi
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public don_vi()
        {
            this.vat_tu = new HashSet<vat_tu>();
        }
    
        public int id { get; set; }
        public string ten_don_vi { get; set; }
        public string mo_ta { get; set; }
        public Nullable<System.DateTime> ngay_tao { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vat_tu> vat_tu { get; set; }
    }
}
