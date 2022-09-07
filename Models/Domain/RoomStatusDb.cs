namespace MeetingRoom.Models.Domain
{
    public enum Status
    {
        Available,
        Booked,
        Pending
    }
    public class RoomStatusDb
    {
        public Guid Id { get; set; }

        public string RoomId { get; set; }

        public Status RoomStatus { get; set; }

        public DateTime BookedDateTime { get; set; }
        public DateTime BookedFromDateTime { get; set; }
        public DateTime BookedToDateTime { get; set; }

        public string? BookedBy { get; set; }


    }
}
