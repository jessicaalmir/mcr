using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr.Data.Models
{
    public class TransmissionSignal: BaseEntity<int>
    {
        private Transmission Transmission{get;set;} 
        private Signal Signal{get;set;} 
        private Source Source{get;set;} 
    }
}