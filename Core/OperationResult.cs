namespace Core
{
    public class OperationResult
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public object ObjectValue { get; set; }
        public OperationResult(int status, string message, object objectvalue = null)
        {
            Status = status;
            Message = message;
            ObjectValue = objectvalue;
        }

        public enum OperationStatus
        {
            Success = 1,
            Failure = 2
        }
    }
}
