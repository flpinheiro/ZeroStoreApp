namespace ZeroStoreApp.CommandService.Responses;

public record ApiResponseWithData<T>(T Data, string Message);

