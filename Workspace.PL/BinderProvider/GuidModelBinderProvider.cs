using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Workspace.PL;
public class GuidModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(Guid?) || context.Metadata.ModelType == typeof(Guid))
        {
            return new GuidModelBinder();
        }

        return null;
    }
}
