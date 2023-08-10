using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr.Data.Models;

public class Content:BaseEntity<int>{
    public string Code{get; set;}="";
    public string Name{get; set;}="";
    public TimeOnly Duration{get; set;}
    public Encoder CC{get; set;}
    public Client Client{get; set;}
    public ContentType Type{get; set;}
}