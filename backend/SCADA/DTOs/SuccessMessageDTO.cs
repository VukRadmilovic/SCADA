namespace SCADA.DTOs
{
    public class SuccessMessageDTO
    {
        public string Message { get; set; }

        public SuccessMessageDTO(string Message)
        {
            this.Message = Message;
        }
    }
}
