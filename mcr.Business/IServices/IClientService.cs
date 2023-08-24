using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mcr.Data.DTO;
using mcr.Data.Models;

namespace mcr.Business.IServices
{
    public interface IClientService
    {

        /// <summary>
        /// Creates a new <see cref="Client"/> 
        /// </summary>
        /// <param name="client"></param> A new client
        /// <returns></returns>
        Task<BaseMessage<Client>> CreateClient(Client client);
    }
}