namespace DI_EFCore.Entities
{
    public class Comment : Entity
    {
        public string CommentContent { get; set; } = null!;
        public User Author { get; set; } = null!;
        public Post Post { get; set; } = null!;
    }
}