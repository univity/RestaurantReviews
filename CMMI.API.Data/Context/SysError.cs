//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CMMI.API.Data.Context
{
    using System;
    using System.Collections.Generic;
    
    public partial class SysError
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SysError()
        {
            this.SysErrors1 = new HashSet<SysError>();
        }
    
        public int Id { get; set; }
        public string Exception { get; set; }
        public string Message { get; set; }
        public string Stack { get; set; }
        public string Details { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ParentId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SysError> SysErrors1 { get; set; }
        public virtual SysError SysError1 { get; set; }
    }
}