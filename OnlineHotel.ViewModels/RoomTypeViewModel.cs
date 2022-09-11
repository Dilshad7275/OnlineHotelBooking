using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineHotel.ViewModels
{
    public class RoomTypeViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}
