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
    
    public partial class Jizdenka
    {
        public string linka { get; set; }
        public string email { get; set; }
        public System.DateTime cas { get; set; }
        public long cislo { get; set; }
    
        public virtual Jizda Jizda { get; set; }
        public virtual Klient Klient { get; set; }
    }
}
