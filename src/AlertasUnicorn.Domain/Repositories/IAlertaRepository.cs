using AlertasUnicorn.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertasUnicorn.Domain.Repositories
{
    public interface IAlertaRepository : IRepository<Alerta>
    {
        Task GenerarAlertasContratos();
    }
}
