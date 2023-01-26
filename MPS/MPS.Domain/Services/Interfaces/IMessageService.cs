using MPS.DAL.Models.Tools;
using MPS.Domain.Dto;

namespace MPS.Domain.Services.Interfaces;

public interface IMessageService
{
    Task<MessageDto> CreateMessageAsync(string content, MessageSource source, CancellationToken cancellationToken);
    Task<MessageDto> ProcessMessageAsync(Guid messageId, string accountName, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<MessageDto>> GetProcessedMessagesAsync(Guid id, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<MessageDto>> GetUnprocessedMessagesAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<MessageDto>> GetSmsUnprocessedMessagesAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<MessageDto>> GetEmailUnprocessedMessagesAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<MessageDto>> GetCellphoneUnprocessedMessagesAsync(CancellationToken cancellationToken);

}