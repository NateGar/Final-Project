using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public partial class Favorite
    {
        public int Id { get; set; }
        public string StartupName { get; set; }
        public int? Rank { get; set; }
        public string PrivateComments { get; set; }
        public string UserId { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
