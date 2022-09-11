namespace MeetingRoom.Models.Domain
{
    public class RoomDetailsDB
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public string? Location { get; set; }
        public string? ImageUrl { get; set; }
        public string Discription { get; set; }

    }
}
