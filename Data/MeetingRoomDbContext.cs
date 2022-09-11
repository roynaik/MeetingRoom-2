using MeetingRoom.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoom.Models.Data
{
    public class MeetingRoomDbContext : DbContext
    {
        public MeetingRoomDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<RoomDetailsDB> RoomDetailsDB { get; set; }
        public DbSet<RoomStatusDb> RoomStatusDB { get; set; }
        public DbSet<UsersDb> UsersDb { get; set; }
        public DbSet<ReportDetailsDb> ReportDetailsDb { get; set; }
    }
}
