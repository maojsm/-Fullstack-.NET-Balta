using Dima.Core.Dtos;
using Dima.Core.Models;
using Dima.Core.Requests.TcHardwareInRealtime;
using Dima.Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dima.Core.Handlers
{
    public interface ITcHardwareInRealtimeHandler
    {
        Task<Response<TcHardwareInRealtime?>> CreateAsync(CreateTcHardwareInRealtimeRequest request);
        Task<Response<List<TcHardwareInRealtimeDto>?>> GetAllAsync(GetAllTcHardwareInRealtimeRequest request);
    }
}
