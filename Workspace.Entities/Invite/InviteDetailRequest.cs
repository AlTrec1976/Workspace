using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workspace.Entities
{
    public class InviteDetailRequest
    {
        public Guid InviteID { get; set; }
        public Guid UserID { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string Comments { get; set; }
    }
}
