//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClientsChat
{
    using System;
    using System.Collections.Generic;
    
    public partial class Question
    {
        public int Id { get; set; }
        public Nullable<int> IdManager { get; set; }
        public Nullable<int> IdClient { get; set; }
        public string Direction { get; set; }
        public string Status { get; set; }
    
        public virtual Clients Clients { get; set; }
        public virtual Managers Managers { get; set; }
    }
}