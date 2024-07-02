namespace LTHDOtNetCore.AjaxExampleInMvc.Models
{
    public class JsonResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
    }
}
