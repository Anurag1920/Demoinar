//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineTestDataAssess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Result
    {
        public long ResultId { get; set; }
        public string UserId { get; set; }
        public Nullable<long> QuestionId { get; set; }
        public Nullable<long> AnswerId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<long> TestId { get; set; }
        public Nullable<bool> IsCorrect { get; set; }
    }
}
