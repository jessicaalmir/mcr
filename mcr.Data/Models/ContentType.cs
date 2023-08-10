using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr.Data.Models;

public class ContentType: BaseEntity<int>{
    public string Name{get; set;}="";
}