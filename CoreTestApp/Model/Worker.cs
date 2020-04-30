using CoreTestApp.Extensions;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTestApp.Model
{
    public class Worker : IdentityUser
    {

        [Required]
        public string Name { get; set; }
        
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [Column("RowVersion")]
        public override string ConcurrencyStamp { get => base.ConcurrencyStamp; set => base.ConcurrencyStamp = value; }

        [NotMapped]
        [JsonIgnore]
        public override int AccessFailedCount { get => base.AccessFailedCount; set => base.AccessFailedCount = value; }

    }
}
