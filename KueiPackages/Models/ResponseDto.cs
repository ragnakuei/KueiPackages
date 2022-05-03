namespace KueiPackages.Models;

public class ResponseDto : BaseResponseDto
{
    public object? Data { get; set; }
}

public class ResponseDto<T> : BaseResponseDto
{
    public T Data { get; set; }
}
