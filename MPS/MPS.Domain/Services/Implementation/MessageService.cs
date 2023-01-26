using Microsoft.EntityFrameworkCore;
using MPS.DAL;
using MPS.DAL.Models;
using MPS.DAL.Models.Tools;
using MPS.Domain.Dto;
using MPS.Domain.Mapping;
using MPS.Domain.Services.Interfaces;

namespace MPS.Domain.Services.Implementation;

public class MessageService : IMessageService
{
    private readonly AppDbContext _context;

    public MessageService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MessageDto> CreateMessageAsync(string content, MessageSource source, CancellationToken cancellationToken)
    {
        var message = new Message(Guid.NewGuid(), content, source)
        {
            Type = MessageType.Recieved,
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        return message.AsDto();
    }

    public async Task<MessageDto> ProcessMessageAsync(Guid messageId, string accountName, CancellationToken cancellationToken)
    {
        Account account = await _context.Accounts.FirstOrDefaultAsync(a => a.Login.Equals(accountName), cancellationToken: cancellationToken);
        Message message = await _context.Messages.FirstOrDefaultAsync(m => m.Id.Equals(messageId), cancellationToken: cancellationToken);

        if (account == null || message == null)
            throw new ApplicationException();

        message.ProcessedAccount = account;
        message.Type = MessageType.Processed;
        account.ProcessedMessages.Add(message);

        await _context.SaveChangesAsync(cancellationToken);

        return message.AsDto();
    }

    public async Task<IReadOnlyCollection<MessageDto>> GetProcessedMessagesAsync(Guid id, CancellationToken cancellationToken)
    {
        Account account = await _context.Accounts.FirstOrDefaultAsync(a => a.Id.Equals(id), cancellationToken: cancellationToken);

        return account.ProcessedMessages.Select(message => message.AsDto()).ToArray();
    }

    public async Task<IReadOnlyCollection<MessageDto>> GetUnprocessedMessagesAsync(CancellationToken cancellationToken)
    {
        return await _context.Messages
            .Where(m => m.Type == MessageType.New)
            .Select(message => message.AsDto())
            .ToArrayAsync(cancellationToken: cancellationToken);
    }

    public async Task<IReadOnlyCollection<MessageDto>> GetSmsUnprocessedMessagesAsync(CancellationToken cancellationToken)
    {
        return await GetSpecifyUnprocessedMessages(MessageSource.Sms).ToArrayAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<MessageDto>> GetEmailUnprocessedMessagesAsync(CancellationToken cancellationToken)
    {
        return await GetSpecifyUnprocessedMessages(MessageSource.Email).ToArrayAsync(cancellationToken);
    }

    public async Task<IReadOnlyCollection<MessageDto>> GetCellphoneUnprocessedMessagesAsync(CancellationToken cancellationToken)
    {
        return await GetSpecifyUnprocessedMessages(MessageSource.Cellphone).ToArrayAsync(cancellationToken);
    }

    private IQueryable<MessageDto> GetSpecifyUnprocessedMessages(MessageSource source)
    {
        return _context.Messages
            .Where(m => m.Type == MessageType.New && m.Source == source)
            .Select(message => message.AsDto());
    }
}