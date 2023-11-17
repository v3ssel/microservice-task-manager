using System.ComponentModel.DataAnnotations;

namespace TaskManager.Autorization.Models.DTO
{
    public class ValidationErrorResponse : ErrorResponse
    {
        public ICollection<ValidationResult>? Errors { get; set; }

        public ValidationErrorResponse(bool isError, string? errorText, ICollection<ValidationResult>? errors) : base(isError, errorText)
        {
            Errors = errors;
        }

        public override void Dispose()
        {
            GC.SuppressFinalize(this);
            Errors?.Clear();
        }
    }
}
