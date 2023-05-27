namespace WHRoom.Models
{
    public class MailModel
    {
        public string? From
        {
            get;
            set;
        }
        public string? To
        {
            get;
            set;
        }
        public string? Subject
        {
            get;
            set;
        }
        public string? Body
        {
            get;
            set;
        }
        public string? Result { get; set; }
    }
    public class Message
    {
        public string? To { get; set; }
        public string? ContentMsg { get; set; }
        public string? Result { get; set; }
    }
}
