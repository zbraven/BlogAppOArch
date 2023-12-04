using System.ComponentModel.DataAnnotations;

namespace BlogApp.Model.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
    public abstract class BaseEntityWithDate : BaseEntity
    {
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}