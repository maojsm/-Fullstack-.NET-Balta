using Microsoft.EntityFrameworkCore;
using Dima.Api.Data;
using Dima.Core;
using Dima.Core.Dtos;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.TcHardwareInRealtime;
using Dima.Core.Responses;

namespace Dima.Api.Handlers;

public class TcHardwareInRealTimeHandler : ITcHardwareInRealtimeHandler
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TcHardwareInRealTimeHandler(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Response<TcHardwareInRealtime?>> CreateAsync(CreateTcHardwareInRealtimeRequest request)
    {
        await Task.Delay(100);

        // verificando se ID da request existe no cadastrod e controladores
        var trafficController = await _context.TrafficControllers
            .AsNoTracking()
            .FirstOrDefaultAsync(tc => tc.Id == request.TrafficControllerId);

        if (trafficController is null)
            return new Response<TcHardwareInRealtime?>(null, 404, "Controlador de tráfego não cadastrado");

        // Criando objeto para salvar no banco de dados
        var tcHardwareInRealTime = new TcHardwareInRealtime
        {
            TrafficControllerId = request.TrafficControllerId,
            VoltageAcIn = request.VoltageAcIn,
            Voltage9vIn = request.Voltage9vIn,
            DoorOpen = request.DoorOpen,
            FlashingYellowOn = request.FlashingYellowOn,
            CpuConnected = request.CpuConnected,
            TcpConnected = true, // não é gravado no banco de dados
            ClientIpV4 = VisitorPublicIP.GetIpV4(_httpContextAccessor.HttpContext),
            ClientPort = VisitorPublicIP.GetPort(_httpContextAccessor.HttpContext),
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            await _context.TcHardwareInRealtimes.AddAsync(tcHardwareInRealTime);
            await _context.SaveChangesAsync();

            return new Response<TcHardwareInRealtime?>(tcHardwareInRealTime, 201, "Hardware em tempo real criado com sucesso!");
        }
        catch (Exception)
        {
            return new Response<TcHardwareInRealtime?>(null, 500, "Não foi possível criar o hardware em tempo real");
        }
    }




    public async Task<Response<List<TcHardwareInRealtimeDto>?>> GetAllAsync(GetAllTcHardwareInRealtimeRequest request)
    {
        try
        {
            var trafficControllerIds = await _context.TrafficControllers
                .AsNoTracking()
                .Select(tc => tc.Id)
                .ToListAsync();

            var mostRecentHardwareList = new List<TcHardwareInRealtimeDto>();
            foreach (var controllerId in trafficControllerIds)
            {
                var mostRecentHardware = await _context.TcHardwareInRealtimes
                    .AsNoTracking()
                    .Include(h => h.TrafficController)
                    .Where(h => h.TrafficControllerId == controllerId)
                    .OrderByDescending(h => h.CreatedAt)
                    .FirstOrDefaultAsync();

                if (mostRecentHardware != null)
                {
                    var TempoDesdeUltimaAtualizacao = (DateTime.UtcNow - mostRecentHardware.CreatedAt).TotalSeconds;
                    mostRecentHardware.TcpConnected = TempoDesdeUltimaAtualizacao <= Configuration.TimeMaxForInvalidDataInSeconds;

                    var dto = new TcHardwareInRealtimeDto
                    {
                        Id = mostRecentHardware.Id,
                        TrafficControllerId = mostRecentHardware.TrafficControllerId,
                        TrafficController = new TrafficControllerDto
                        {
                            CodeLocal = mostRecentHardware.TrafficController.CodeLocal,
                            Name = mostRecentHardware.TrafficController.Name
                        },
                        VoltageAcIn = mostRecentHardware.VoltageAcIn,
                        Voltage9vIn = mostRecentHardware.Voltage9vIn,
                        DoorOpen = mostRecentHardware.DoorOpen,
                        FlashingYellowOn = mostRecentHardware.FlashingYellowOn,
                        CpuConnected = mostRecentHardware.CpuConnected,
                        TcpConnected = mostRecentHardware.TcpConnected,
                        ClientIpV4 = mostRecentHardware.ClientIpV4,
                        ClientPort = mostRecentHardware.ClientPort,
                        CreatedAt = mostRecentHardware.CreatedAt
                    };

                    mostRecentHardwareList.Add(dto);
                }
                else
                {
                    var tcHardDto = new TcHardwareInRealtimeDto
                    {
                        TrafficControllerId = controllerId,
                        TrafficController = new TrafficControllerDto
                        {
                            CodeLocal = "Unknown",
                            Name = "Unknown"
                        },
                        VoltageAcIn = 0,
                        Voltage9vIn = 0,
                        DoorOpen = false,
                        FlashingYellowOn = false,
                        CpuConnected = false,
                        TcpConnected = false,
                        ClientIpV4 = "0.0.0.0",
                        ClientPort = 0,
                        CreatedAt = DateTime.UtcNow
                    };

                    mostRecentHardwareList.Add(tcHardDto);
                }
            }
            return new Response<List<TcHardwareInRealtimeDto>?>(mostRecentHardwareList, 201, "Lista de hardwares consultados.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            return new Response<List<TcHardwareInRealtimeDto>?>(null, 500, "Não foi possível consultar os hardwares dos controladores.");
        }
    }
}