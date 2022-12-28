using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CinemaCore.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
    }
}
