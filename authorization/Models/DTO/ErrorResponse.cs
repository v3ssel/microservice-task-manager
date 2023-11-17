namespace TaskManager.Autorization.Models.DTO
{
    public class ErrorResponse : IDisposable
    {
        public bool IsError { get; set; }

        public string? ErrorText { get; set; }

        public ErrorResponse(bool isError, string? errorText)
        {
            IsError = isError;
            ErrorText = errorText;
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
