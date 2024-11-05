using BasicExtensions;

namespace OrderManagementSystem
{
    public class ServiceResponse
    {
        public bool IsSuccess => ErrorMessages.HasItems();
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public List<string> Messages { get; set; } = new List<string>();
    }
    public class ServiceResponse<T> : ServiceResponse
    {
        public ServiceResponse() { }
        public ServiceResponse(T result) { Result = result; }
        public T Result { get; set; }
        public Page Page { get; set; }
    }
}
