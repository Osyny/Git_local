//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Server
{
    using System;
    using System.Collections.Generic;
    
    public partial class Orders
    {
        public int Id { get; set; }
        public int Category_Id { get; set; }
        public int User_Id { get; set; }
        public decimal Price { get; set; }
        public System.DateTime Date { get; set; }
        public string Description { get; set; }
    
        public virtual Categories Categories { get; set; }
        public virtual Users Users { get; set; }
    }
}
