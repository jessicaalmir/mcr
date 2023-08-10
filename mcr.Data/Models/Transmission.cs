using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr.Data.Models;

public class Transmission: BaseEntity<int>{
    public DateTime Date;
    public Client Client{get; set;}
    public Content Content{get; set;}
    public string Name{get; set;}="";
    public TimeOnly Duration{get; set;}
    public Encoder CC{get; set;}
    
}