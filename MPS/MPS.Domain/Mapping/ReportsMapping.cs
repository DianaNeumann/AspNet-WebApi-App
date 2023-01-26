using MPS.DAL.Models;
using MPS.Domain.Dto;

namespace MPS.Domain.Mapping;

public static class ReportsMapping
{
    public static ReportDto AsDto(this Report report)
        => new ReportDto(
            report.Id,
            report.Link,
            report.CreationDate,
            report.Account.AsDto());
}