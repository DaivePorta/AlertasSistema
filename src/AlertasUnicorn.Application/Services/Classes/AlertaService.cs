using AlertasUnicorn.Application.Dtos;
using AlertasUnicorn.Domain.Repositories;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlertasUnicorn.Application.Services
{
    public class AlertaService : IAlertaService
    {
        private readonly ILogger<AlertaService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AlertaService(ILogger<AlertaService> logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task GenerarAlertasContratos()
        {
            try
            {
                await _unitOfWork.AlertaRepository.GenerarAlertasContratos();                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception: { ex.Message }");
                _logger.LogError($"StackTrace: { ex.StackTrace}");
            }
        }

        public async Task<IEnumerable<AlertaDto>> GetAllAlertaContratos()
        {
            var queryList = await _unitOfWork.AlertaRepository.GetAll();
            return _mapper.Map<IEnumerable<AlertaDto>>(queryList);
        }
    }
}
