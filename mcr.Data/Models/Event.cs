using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mcr.Data.Models;

public class Event:BaseEntity<int>{
    public string? Code{get; set;}
    public string? Name{get; set;}
    public TimeOnly Duration{get; set;}
    public DateOnly Date{get; set;}
    public TimeOnly TxStart{get; set;}
    public TimeOnly TxEnd{get; set;}
    public TimeOnly IntTxStart{get; set;}
    public TimeOnly IntTxEnd{get; set;}
    public string? Note{get; set;}

    [ForeignKey("EncoderId")]
    public int EncoderId{get; set;}
    public virtual Encoder? CC{get; set;}
    
    public List<Fee> FeeList{get; set;} = new List<Fee>();
}