namespace FunctionsDemo.Mutate.Models.Room
{
    public class MessageAddRequest
    {
        public string Username { get; set; }
        public string Body { get; set; }
        public int RoomId { get; set; }
        public short ColorId { get; set; }
    }
}