using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryCommunityHelper.DTO
{
    class PlayerCreateDto
    {
        //private int Id { get; set; }
        private int userId { get; set; }
        //public static readonly DependencyProperty NickProperty;
        private string nick { get; set; }
        private int invite { get; set; }
        private string motivater { get; set; }
        private long lastAccess { get; set; }
        private int factionId { get; set; }
        private string avatar;
    }
}
