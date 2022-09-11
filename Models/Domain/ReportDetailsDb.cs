namespace MeetingRoom.Models.Domain
{
    public class ReportDetailsDb
    {
        public Guid Id { get; set; }

        public string RoomName { get; set; }
        public string RoomId { get; set; }

        public Status RoomStatus { get; set; }

        public DateTime BookedDateTime { get; set; }
        public DateTime BookedFromDateTime { get; set; }
        public DateTime BookedToDateTime { get; set; }

        public string? BookedBy { get; set; }

    }
}
