﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PCA.Models
{
    public class File
    {
        [Key]
        public int FileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileType FileType { get; set; }
        public int? InvoiceId { get; set; }
        public int? AccountId { get; set; }
        public int? DailyReportPictureId { get; set; }
        public virtual Account Account { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual DailyReportPicture DailyReportPicture { get; set; }
    }
}