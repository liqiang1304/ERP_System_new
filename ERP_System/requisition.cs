//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP_System
{
    using System;
    using System.Collections.Generic;
    
    public partial class requisition
    {
        public requisition()
        {
            this.re_ven_ma = new HashSet<re_ven_ma>();
        }
    
        public int requisitionID { get; set; }
        public Nullable<System.DateTime> bidDate { get; set; }
    
        public virtual ICollection<re_ven_ma> re_ven_ma { get; set; }
    }
}