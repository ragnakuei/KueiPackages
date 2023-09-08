﻿namespace KueiPackages.Models;

public class ApiResponseException<T> : Exception
{
    /// <summary>
    /// 表單是否驗証成功
    /// </summary>
    public bool IsFormValid { get; set; }

    public string Message { get; set; }

    /// <summary>
    /// 顯示 Alert Message
    /// </summary>
    public string AlertMessage { get; set; }

    public string ErrorCode { get; set; }

    public T Data { get; set; }
}
