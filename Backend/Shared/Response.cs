namespace Backend.Shared;

/// <summary>
/// A class represent Result Pattern
/// </summary>
/// <typeparam name="T">T is Type of Response</typeparam>
public record Response<T>
{
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }

    private Response(T data) => Data = data;

    private Response(List<string> errors) => Errors = errors;


    public static Response<T> Success(T data) => new(data);
    public static Response<T> Failure(List<string> errors) => new(errors);
    public static Response<T> Failure(string error) => new([error]);


}
