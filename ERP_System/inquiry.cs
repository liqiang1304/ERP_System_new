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
    
    public partial class inquiry
    {
        public inquiry()
        {
            this.inquriy_ma = new HashSet<inquriy_ma>();
            this.quo_ma = new HashSet<quo_ma>();
            this.quotation = new HashSet<quotation>();
        }
    
        public int companyCode { get; set; }
        public int inquiryID { get; set; }
        public string currency { get; set; }
        public decimal amount { get; set; }
        public Nullable<System.DateTime> creationDate { get; set; }
    
        public virtual customers customers { get; set; }
        public virtual ICollection<inquriy_ma> inquriy_ma { get; set; }
        public virtual ICollection<quo_ma> quo_ma { get; set; }
        public virtual ICollection<quotation> quotation { get; set; }
    }
}