using System.Collections.Generic;

namespace SecurityApi.Domain.Responses
{
    public interface IAppResponseBase
    {
        bool Success { get; set; }
        List<string> Errors { get; set; }
        string Message { get; set; }
        int? StatusCode { get; set; }
    }

    public interface IAppResponseBase<TData> : IAppResponseBase
    {
        TData Data { get; set; }
    }

    public class AppResponseBase : IAppResponseBase<object>
    {
        public object Data { get; set; }
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public int? StatusCode { get; set; }
    }

    public class AppResponseBase<TData> : AppResponseBase, IAppResponseBase<TData>
    {
        public new TData Data { get; set; }
    }
}