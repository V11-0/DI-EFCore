namespace DI_EFCore.Entities {

    public class Post : Entity {

        public string PostContent { get; set; } = null!;

        public int AuthorId { get; set; }
        public virtual User? Author { get; set; } = null!;
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}