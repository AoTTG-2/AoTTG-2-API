using AoTTG2.IDS.Controllers.NSwag;
using AoTTG2.IDS.Data.Dao;
using AoTTG2.IDS.Data.Repositories.Interfaces;
using AoTTG2.IDS.Services.Interfaces;
using AutoMapper;
using FluentValidation;
using System;
using System.Threading.Tasks;

namespace AoTTG2.IDS.Services
{
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _repository;
        private readonly IValidatorFactory _validatorFactory;

        public ReportService(IMapper mapper, IReportRepository repository, IValidatorFactory validatorFactory)
        {
            _mapper = mapper;
            _repository = repository;
            _validatorFactory = validatorFactory;
        }

        public async Task<Report> AddAsync(Report report)
        {
            var validator = _validatorFactory.GetValidator<Report>();
            await validator.ValidateAndThrowAsync(report);

            var dao = _mapper.Map<ReportDao>(report);
            dao.Id = Guid.NewGuid();
            dao.Status = ReportStatus.Active;
            await _repository.AddAsync(dao);

            return _mapper.Map<Report>(dao);
        }
    }
}
