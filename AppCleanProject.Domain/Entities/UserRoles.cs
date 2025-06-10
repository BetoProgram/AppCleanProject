using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppCleanProject.Domain.Entities
{
    [Table(name: "userroles")]
    public class UserRoles
    {
        [Column(name: "user_id")]
        public long UserId { get; set; }

        [Column(name: "role_id")]
        public int RoleId { get; set; }

        public Roles Role { get; set; }
        public Users User { get; set; }
    }
}