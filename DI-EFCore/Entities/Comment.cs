namespace DI_EFCore.Entities
{
    public class Comment : Entity
    {
        public string CommentContent { get; set; } = null!;

        public int AuthorId { get; set; }
        public User? Author { get; set; } = null!;
        public int PostId { get; set; }
        public Post? Post { get; set; } = null!;
    }
}