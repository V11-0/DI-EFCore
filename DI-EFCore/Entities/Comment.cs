namespace DI_EFCore.Entities {
    public class Comment : Entity {

        public string CommentContent { get; set; } = null!;

        public int AuthorId { get; set; }
        public int PostId { get; set; }
    }
}