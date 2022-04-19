using System.ComponentModel.DataAnnotations;

namespace login__df_core.Models
{
    public class loginClass
    {
        public int Id { get; set; }
     
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Pass { get; set; }
        public string Name { get; set; }
    }
}
