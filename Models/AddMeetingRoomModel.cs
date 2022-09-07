namespace MeetingRoom.Models
{
    public class AddMeetingRoomModel
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public string Location { get; set; }
        public string ImageUrl { get; set; }
        public string Discription { get; set; }
    }
}
