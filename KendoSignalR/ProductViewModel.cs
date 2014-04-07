using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace KendoSignalR
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
    }
}
