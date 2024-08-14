using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Workspace.PL;
public class GuidModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        string attemptedValue = valueProviderResult.FirstValue;

        // Treat empty strings as null
        if (string.IsNullOrWhiteSpace(attemptedValue))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        Guid.TryParse(attemptedValue, out Guid result);
        bindingContext.Result = ModelBindingResult.Success(result);
        return Task.CompletedTask;
    }
}
