using Microsoft.AspNetCore.Mvc;
using OnlineHotel.BLL;
using OnlineHotel.ViewModels;

namespace OnlineHotel.Web.Controllers
{
    public class RoomTypeController : Controller
    {
        private ApplicationDbContext _context;

        public RoomTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var roomTypeList = _context.RoomTypes.ToList();
            List<RoomTypeViewModel> model = new List<RoomTypeViewModel>();
            foreach(var roomtype in roomTypeList)
            {
                model.Add(new RoomTypeViewModel { Name = roomtype.Name });
            }
            return View(model);
        }
    }
}
