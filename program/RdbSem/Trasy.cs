//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RdbSem
{
    using System;
    using System.Collections.Generic;
    
    public partial class Trasy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trasy()
        {
            this.Jizda = new HashSet<Jizda>();
            this.Lokalita2 = new HashSet<Lokalita>();
        }
    
        public string linka { get; set; }
        public string odkud { get; set; }
        public string kam { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jizda> Jizda { get; set; }
        public virtual Lokalita Lokalita { get; set; }
        public virtual Lokalita Lokalita1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Lokalita> Lokalita2 { get; set; }
    }
}
