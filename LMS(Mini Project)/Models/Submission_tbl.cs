//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LMS_Mini_Project_.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class Submission_tbl
    {
        public int Sub_Id { get; set; }
        public string Sub_Name { get; set; }
        public string Sub_File_path { get; set; }
        public System.DateTime Sub_Date_Time { get; set; }
        public string Sub_Status { get; set; }
        public HttpPostedFileBase Sub_File { get; set; }
        public Nullable<int> St_Id { get; set; }
    
        public virtual Student_tbl Student_tbl { get; set; }
    }
}
