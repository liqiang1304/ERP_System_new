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
    
    public partial class quotation
    {
        public quotation()
        {
            this.quo_ma = new HashSet<quo_ma>();
            this.sales_ma = new HashSet<sales_ma>();
            this.salesOrder = new HashSet<salesOrder>();
        }
    
        public int companyCode { get; set; }
        public int inquiryID { get; set; }
        public int quotationID { get; set; }
        public Nullable<System.DateTime> effectiveDate { get; set; }
        public Nullable<System.DateTime> deadline { get; set; }
        public decimal amount { get; set; }
    
        public virtual customers customers { get; set; }
        public virtual inquiry inquiry { get; set; }
        public virtual ICollection<quo_ma> quo_ma { get; set; }
        public virtual ICollection<sales_ma> sales_ma { get; set; }
        public virtual ICollection<salesOrder> salesOrder { get; set; }
    }
}
