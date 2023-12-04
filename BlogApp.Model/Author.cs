using BlogApp.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Model
{
    public class Author : BaseEntityWithDate
    {
        [Column(TypeName = "nvarchar(150)")]
        public string Fullname { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(150)")]
        public string Email { get; set; } = string.Empty;
        [Column(TypeName = "nvarchar(15)")]
        public string Phone { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
