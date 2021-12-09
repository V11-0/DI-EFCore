namespace DI_EFCore.Entities {

    public class Post : Entity {

        public string PostContent { get; set; } = null!;
        public User Author { get; set; } = null!;
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}