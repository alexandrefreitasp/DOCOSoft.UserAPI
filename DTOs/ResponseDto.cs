using System.Text.Json.Serialization;

namespace DOCOSoft.UserAPI.DTOs
{
    public class ResponseDto<TData>(
        TData? data,
        string? message = null)
        {
            [JsonConstructor]
            public ResponseDto() : this(default(TData?))
        {
        }

        public TData? Data { get; set; } = data;
        public string? Message { get; set; } = message;

    }
}
