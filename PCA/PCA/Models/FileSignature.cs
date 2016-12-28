using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PCA.Models
{
    public class FileSignature
    {
        public int FileSignatureId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileTypeSignature FileTypeSignature { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}