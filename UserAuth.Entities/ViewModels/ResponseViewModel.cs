namespace UserAuth.Entities.ViewModels;

public class ResponseViewModel<T>
{
    public T? Data { get; set; }

    public int? StatusCode { get; set; }

    public string? Status {  get; set; }

    public string? Error { get; set; }

    public string? Message { get; set; }
}
