namespace ParsaLibraryManagement.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }       
        public required string Name { get; set; }           
        public bool isDeleted { get; set; }
        public virtual ICollection<Role>? Roles { get; set; }  
        public virtual ICollection<User>? Users { get; set; }  
    }

}
