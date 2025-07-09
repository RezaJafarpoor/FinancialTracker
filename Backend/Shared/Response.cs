namespace Backend.Shared;

/// <summary>
/// A class represent Result pattern
/// </summary>
/// <typeparam name="T">T is Type of Response</typeparam>
/// <param name="IsSuccess">it is <c>tur</c> if operation is sucessful </param>
public record Response<T>
{
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    private Response(T data)
    {
        IsSuccess = true;
        Data = data;
    }
    private Response()
    {
        IsSuccess = true;
    }

    private Response(List<string> errors)
    {
        IsSuccess = false;
        Errors = errors;
    }


    public static Response<T> Success(T data) => new(data);
    public static Response<T> Success() => new();
    public static Response<T> Failure(List<string> errors) => new(errors);
    public static Response<T> Failure(string error) => new([error]);


}
