namespace MeetingRoom.Models.Domain
{
    public class UsersDb
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
    }
}
