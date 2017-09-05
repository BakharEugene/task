using System.ComponentModel.DataAnnotations;

namespace task.DAL.Models.Users
{
    public class RegisterModel
    {
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",
      ErrorMessage = "Некорректный Email")]
        [Required(ErrorMessage = "Заполните поле")]
        [System.Web.Mvc.Remote("CheckEmail", "Validation", ErrorMessage = "Пользователь с таким Email зарегистрирован")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
