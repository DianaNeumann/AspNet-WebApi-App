using MPS.Domain.Dto;

namespace MPS.Domain.Modules.ReportCreationalModules.Interfaces;

public interface IReportCreator
{
    ReportMinDto CreateReport();
}