using FluentValidation.Results;

namespace ZeroStoreApp.CrossCutting.Common;

public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public IEnumerable<ValidationFailure> Errors { get; set; } = [];
}
