using AlertasUnicorn.Application.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlertasUnicorn.Application.Services
{
    public interface IAlertaService
    {
        Task<IEnumerable<AlertaDto>> GetAllAlertaContratos();
        Task GenerarAlertasContratos();
    }
}
