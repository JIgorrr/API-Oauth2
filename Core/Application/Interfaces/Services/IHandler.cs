namespace Application.Interfaces.Services;

public interface IHandler
{
    IHandler? SetNext(IHandler? handler);

    object? Handle(object request);
}
