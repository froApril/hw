//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookStore.Business.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchasedHistory
    {
        public int Id { get; set; }
        public Nullable<bool> Rating { get; set; }
        public Nullable<int> RateValue { get; set; }
        public System.DateTime RateDate { get; set; }
    
        public virtual User User { get; set; }
        public virtual Media Media { get; set; }
    }
}
