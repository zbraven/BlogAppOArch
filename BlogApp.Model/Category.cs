using BlogApp.Model.Base;

namespace BlogApp.Model
{
    public class Category : BaseEntityWithDate
    {
        public string Name { get; set; }= string.Empty;
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
