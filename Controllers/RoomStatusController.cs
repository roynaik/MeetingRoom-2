using MeetingRoom.Models;
using MeetingRoom.Models.Data;
using MeetingRoom.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoom.Controllers
{
    public class RoomStatusController : Controller
    {
        private readonly MeetingRoomDbContext meetingRoomDbContext;
        public RoomStatusController(MeetingRoomDbContext meetingRoomDbContext)
        {
            this.meetingRoomDbContext = meetingRoomDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> BookRoom(Guid id)
        {
            var room = await meetingRoomDbContext.RoomDetailsDB.FirstOrDefaultAsync(x => x.Id == id);
            var status = await meetingRoomDbContext.RoomStatusDB.FirstOrDefaultAsync(x => x.Id == id);
            if (room == null || status == null) return RedirectToAction("Index", "RoomDetails");
            var viewModel = new AddRoomStatusModel()
            {
                Id = room.Id,
             //   RoomId = status.RoomId,
                RoomName = room.RoomName,
             //   RoomStatus = status.RoomStatus,
             //   BookedBy = status.BookedBy,
             ImageUrl = room.ImageUrl,
                BookedFromDateTime = new DateTime(),
                BookedToDateTime =new DateTime(),
            };
            return await Task.Run(() => View("BookRoom", viewModel));

        }

        [HttpPost]
        public async Task<IActionResult> BookRoom(AddRoomStatusModel model)
        {
            var room = await meetingRoomDbContext.RoomDetailsDB.FirstOrDefaultAsync(x => x.Id == model.Id);
            var status = await meetingRoomDbContext.RoomStatusDB.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (room != null)
            {
                //  room.MeetingRoomName = model.MeetingRoomName;
                status.RoomId = model.RoomId;
                status.RoomStatus = Status.Pending;
                status.BookedBy = model.BookedBy;
                status.BookedFromDateTime = model.BookedFromDateTime;
                status.BookedToDateTime = model.BookedToDateTime;

                await meetingRoomDbContext.RoomStatusDB.AddAsync(status);
                await meetingRoomDbContext.SaveChangesAsync();

            }
            return RedirectToAction("Index", "RoomDetails");

        }

        [HttpGet]
        public async Task<IActionResult> AssignRoom()
        {
            var roomStatus = await meetingRoomDbContext.RoomStatusDB.Where(x => x.RoomStatus == Status.Pending).ToListAsync();
                var rooms = await meetingRoomDbContext.RoomDetailsDB.ToListAsync();
                var viewModel = new AddRoomStatusModel();

            foreach (var status in roomStatus)
            {
                viewModel.ImageUrl = rooms.FirstOrDefault(x => x.Id.ToString() == status.RoomId)?.ImageUrl;
                viewModel.RoomName = rooms.FirstOrDefault(x => x.Id.ToString() == status.RoomId)?.RoomName;
                viewModel.BookedBy = status.BookedBy;
                viewModel.BookedDateTime = status.BookedDateTime;
                viewModel.BookedFromDateTime = status.BookedFromDateTime;
                viewModel.BookedToDateTime = status.BookedToDateTime;
            }

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> RoomStatusApprove(Guid id)
        {
            var status = await meetingRoomDbContext.RoomStatusDB.FirstOrDefaultAsync(x => x.RoomId == id.ToString());
            if (status != null)
            {
                status.RoomStatus = Status.Booked;

                await meetingRoomDbContext.RoomStatusDB.AddAsync(status);
                await meetingRoomDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "RoomDetails");

        }  

        public async Task<IActionResult> RoomStatusDeny(Guid id)
        {
            var status = await meetingRoomDbContext.RoomStatusDB.FirstOrDefaultAsync(x => x.RoomId == id.ToString());
            if (status != null)
            {
                status.RoomStatus = Status.Booked;

                await meetingRoomDbContext.RoomStatusDB.AddAsync(status);
                await meetingRoomDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "RoomDetails");

        }

    }




}
