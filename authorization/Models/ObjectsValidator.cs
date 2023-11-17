using System.ComponentModel.DataAnnotations;
using TaskManager.Autorization.Models.DTO;

namespace TaskManager.Autorization.Models
{
    public class ObjectsValidator
    {
        public static ErrorResponse ValidateObject(object obj)
        {
            var errors = new List<ValidationResult>();
            var res = Validator.TryValidateObject(obj, new ValidationContext(obj), errors, true);

            if (res)
            {
                return new ValidationErrorResponse(false, null, null);
            }

            return new ValidationErrorResponse(false, "Object validation failed. Missing required fields.", errors);
        }
    }
}