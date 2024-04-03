using AuthenticationSever.DTOs;

namespace AuthenticationSever.Interface
{
    public interface IGoogleAuthProvider : IExternalAuthProvider
    {
        Provider Provider { get; }
    }
}
