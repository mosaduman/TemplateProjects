namespace OrderManagementSystem
{
    public class ServiceRequest<T>
    {
        public ServiceRequest() { }
        public ServiceRequest(T result) { Result = result; }
        public T Result { get; set; }
        public Page Page { get; set; }
    }
}
