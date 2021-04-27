using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities
{
    public class Employer : BaseEntity
    {
        [ForeignKey("User")]
        public int UserId { get; set; }    }
}
