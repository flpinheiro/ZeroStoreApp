using FluentValidation.Results;

namespace ZeroStoreApp.CommandService.Responses;


public record ApiResponse<T>
{
    public ApiResponse(T data, string message, params object[] args)
    {
        Data = data;
        if (args != null && args.Count() > 0)
        {
            message = string.Format(message, args);
        }

        Message = message;

    }
    public T? Data { get; set; }
    public string Message { get; set; }
    public bool IsSuccess => true;
}
public record ApiResponse
{
    public ApiResponse(List<ValidationFailure>? failures, string message, params object[] args) : this(message, args)
    {
        Failures = failures;
    }
    public ApiResponse(string message, params object[] args)
    {
        if (args != null && args.Count() > 0)
        {
            message = string.Format(message, args);
        }
        Message = message;
    }
    public string Message { get; }
    public List<ValidationFailure>? Failures { get; }

    public bool IsSuccess => false;
};
