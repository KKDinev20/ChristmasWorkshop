using ChristmasApi.Data.Models;
using ChristmasApi.Main.Contracts;

namespace ChristmasApi.Main.Handlers;

public abstract class AbstractHandler : IValidationHandler
{
    private IValidationHandler? next;

    public IValidationHandler SetNext(IValidationHandler next)
    {
        this.next = next;
        return next;
    }

    public virtual async Task<bool> ValidateAsync(Light light)
    {
        if (this.next != null)
        {
            return await this.next.ValidateAsync(light);
        }

        return await Task.FromResult(true);
    }
}