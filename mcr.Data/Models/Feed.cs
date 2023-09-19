using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace mcr.Data.Models;

public class Feed: BaseEntity<int>{
    [ForeignKey("EventId")]
    public int EventId {get; set;}
    
    [ForeignKey("SourceId")]
    public int SourceId {get; set;}
    public  virtual Source? Source{get; set;}

    [ForeignKey("SignalId")]
    public int SignalId {get; set;}
    public  virtual Signal? Signal{get; set;}
}