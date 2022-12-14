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
            if (room == null) return RedirectToAction("Index", "RoomDetails");
            var viewModel = new AddRoomStatusModel()
            {
                Id = room.Id,
                RoomId = id.ToString(),
                RoomName = room.RoomName,
                //   RoomStatus = status.RoomStatus,
                //   BookedBy = status.BookedBy,
                ImageUrl = room.ImageUrl,
                BookedFromDateTime = new DateTime(),
                BookedToDateTime = new DateTime(),
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
                var meetingRoom = new RoomStatusDb()
                {
                    RoomId = model.RoomId,
                    RoomStatus = Status.Pending,
                    BookedBy = model.BookedBy,
                    BookedFromDateTime = model.BookedFromDateTime,
                    BookedToDateTime = model.BookedToDateTime
                };

                await meetingRoomDbContext.RoomStatusDB.AddAsync(meetingRoom);
                await meetingRoomDbContext.SaveChangesAsync();
                await AddToReport(model.Id, room.RoomName, Status.Pending, model.BookedBy, model.BookedDateTime, model.BookedFromDateTime, model.BookedToDateTime);

            }
            return RedirectToAction("Index", "RoomDetails");

        }

        [HttpGet]
        public async Task<IActionResult> AssignRoom()
        {
            var roomStatus = await meetingRoomDbContext.RoomStatusDB.Where(x => x.RoomStatus == Status.Pending).ToListAsync();

            var rooms = await meetingRoomDbContext.RoomDetailsDB.ToListAsync();
            var model = new List<AddRoomStatusModel>();

            if (roomStatus != null || roomStatus.Count == 0)
            {


                foreach (var status in roomStatus)
                {
                    var viewModel = new AddRoomStatusModel();

                    viewModel.RoomId = status.RoomId;
                    viewModel.ImageUrl = rooms.FirstOrDefault(x => x.Id.ToString() == status.RoomId)?.ImageUrl;
                    viewModel.RoomName = rooms.FirstOrDefault(x => x.Id.ToString() == status.RoomId)?.RoomName;

                    viewModel.BookedBy = status.BookedBy;
                    viewModel.BookedDateTime = status.BookedDateTime;
                    viewModel.BookedFromDateTime = status.BookedFromDateTime;
                    viewModel.BookedToDateTime = status.BookedToDateTime;

                    model.Add(viewModel);
                }
            }




            //   return View(viewModel);
            return View(model);

        }


        public async Task<IActionResult> RoomStatusApprove(Guid id)
        {
            var status = await meetingRoomDbContext.RoomStatusDB.FirstOrDefaultAsync(x => x.RoomId == id.ToString());
            var room = await meetingRoomDbContext.RoomDetailsDB.FirstOrDefaultAsync(x => x.Id == id);
            if (room != null)
            {
                //  room.MeetingRoomName = model.MeetingRoomName;

                status.RoomId = id.ToString();
                status.RoomStatus = Status.Booked;
                status.BookedBy = status.BookedBy;
                status.BookedFromDateTime = status.BookedFromDateTime;
                status.BookedToDateTime = status.BookedToDateTime;

                await meetingRoomDbContext.SaveChangesAsync();
                await AddToReport(id, room.RoomName, Status.Booked, status.BookedBy, status.BookedDateTime, status.BookedFromDateTime, status.BookedToDateTime);


            }
            return RedirectToAction("Index", "RoomDetails");

        }

        //     [HttpPost]
        public async Task<IActionResult> RoomStatusDeny(Guid id)
        {
            var status = await meetingRoomDbContext.RoomStatusDB.FirstOrDefaultAsync(x => x.RoomId == id.ToString());
            var room = await meetingRoomDbContext.RoomDetailsDB.FirstOrDefaultAsync(x => x.Id == id);
            if (room != null)
            {
                //  room.MeetingRoomName = model.MeetingRoomName;
                var meetingRoom = new RoomStatusDb()
                {
                    RoomId = id.ToString(),
                    RoomStatus = Status.Booked,
                    BookedBy = status.BookedBy,
                    BookedFromDateTime = status.BookedFromDateTime,
                    BookedToDateTime = status.BookedToDateTime
                };

                await meetingRoomDbContext.RoomStatusDB.AddAsync(meetingRoom);
                await meetingRoomDbContext.SaveChangesAsync();
                await AddToReport(id, room.RoomName, Status.Booked, status.BookedBy, status.BookedDateTime, status.BookedFromDateTime, status.BookedToDateTime);
            }
            return RedirectToAction("Index", "RoomDetails");

        }



        public async Task<IActionResult> AddToReport(Guid id, string roomName, Status status, string bookedBy, DateTime bookedDateTime, DateTime bookedFromDateTime, DateTime bookedToDateTime)
        {
            var report = await meetingRoomDbContext.ReportDetailsDb.ToListAsync();

            if (report != null)
            {
                //  room.MeetingRoomName = model.MeetingRoomName;
                var reports = new ReportDetailsDb()
                {
                    RoomId = id.ToString(),
                    RoomName = roomName,
                    RoomStatus = status,
                    BookedBy = bookedBy,
                    BookedDateTime = bookedDateTime,
                    BookedFromDateTime = bookedFromDateTime,
                    BookedToDateTime = bookedToDateTime
                };

                await meetingRoomDbContext.ReportDetailsDb.AddAsync(reports);
                await meetingRoomDbContext.SaveChangesAsync();

            }
            return RedirectToAction("Index", "RoomDetails");

        }


        public async Task<IActionResult> ViewReport()
        {
            var report = await meetingRoomDbContext.ReportDetailsDb.ToListAsync();
            return View(report);
        }
        
        public async Task<IActionResult> SortData(string val)
        {
            var report = await meetingRoomDbContext.ReportDetailsDb.ToListAsync();

            return RedirectToAction("ViewReport", "RoomStatus",report);
        }


    }




}
