using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mcr.Data.Models;

public class Encoder: BaseEntity<int>{
    public string? Name{get; set;}
    [ForeignKey("ClientId")]
    public int? ClientId{get; set;}
    public virtual Client? Client{get; set;}
}