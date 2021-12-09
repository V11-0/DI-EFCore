using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DI_EFCore.Entities {

    [Index(nameof(Username), IsUnique = true)]
    public class User : Entity {

        public User(string username) {
            Username = username;
        }

        [StringLength(50)]
        public string Username { get; set; }
        public virtual ICollection<Post>? Posts { get; set; }
    }
}