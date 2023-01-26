namespace MPS.Domain.Exceptions;

public class EntityNotFoundException<T> : NotFoundException
{
    private EntityNotFoundException(string message) : base(message) { }

    public static EntityNotFoundException<T> Create(Guid id)
        => new EntityNotFoundException<T>($"{typeof(T).Name} with id {id} was not found.");
}