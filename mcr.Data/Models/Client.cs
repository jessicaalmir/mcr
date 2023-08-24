using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr.Data.Models;

public class Client: BaseEntity<int>{
    public string? Name{get; set;}

    public virtual List<Encoder>? Encoders{get; set;} = new List<Encoder>();
}           