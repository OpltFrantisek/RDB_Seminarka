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
    
    public partial class Jizda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Jizda()
        {
            this.Jizdenka = new HashSet<Jizdenka>();
        }
    
        public string linka { get; set; }
        public string spz { get; set; }
        public string cislo_rp { get; set; }
        public System.DateTime cas { get; set; }
    
        public virtual Autobus Autobus { get; set; }
        public virtual Ridic Ridic { get; set; }
        public virtual Trasy Trasy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Jizdenka> Jizdenka { get; set; }
    }
}
