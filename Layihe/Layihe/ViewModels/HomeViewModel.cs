using Layihe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Layihe.ViewModels
{
    public class HomeViewModel
    {
        public List<Slider> Slider { get; set; }
        public List<NoticeBoard> NoticeBoards { get; set; }
        public Video Video { get; set; }
    }
}
