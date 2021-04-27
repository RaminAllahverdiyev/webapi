using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class User : BaseEntity
    {
        [StringLength(25)]
        public string Surname { get; set; }
        public int Age { get; set; }

    }
}
