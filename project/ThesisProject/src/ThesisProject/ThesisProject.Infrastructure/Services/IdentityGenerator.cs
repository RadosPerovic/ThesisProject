using ThesisProject.Application.Services;

namespace ThesisProject.Infrastructure.Services;
public class IdentityGenerator : IIdentityGenerator
{
    public Guid GenerateGuidId()
    {
        return Guid.NewGuid();
    }
}
