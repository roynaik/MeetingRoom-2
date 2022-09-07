using System.Collections;
using MeetingRoom.Models.Domain;

namespace MeetingRoom.Models
{
    public class AddRoomStatusModel : IEnumerable
    {
        public Guid Id { get; set; }

        public string? RoomId { get; set; }
        public string? RoomName { get; set; }
        public string? ImageUrl { get; set; }

        public Status RoomStatus { get; set; }

        public DateTime BookedDateTime { get; set; }
        public DateTime BookedFromDateTime { get; set; }
        public DateTime BookedToDateTime { get; set; }

        public string? BookedBy { get; set; }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
