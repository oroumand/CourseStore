using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CourseStore.Core.Domain.Dtos
{
    public class CreateCustomerDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "حداکثر طول نام 100 کاراکتر است.")]
        public virtual string FirstName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "حداکثر طول نام خانوادگی 100 کاراکتر است.")]
        public virtual string LastName { get; set; }

        [Required]
        [RegularExpression(@"^(.+)@(.+)$", ErrorMessage = "ایمیل وارد شده قابل قبول نمی‌باشد.")]
        public virtual string Email { get; set; }
    }
}
