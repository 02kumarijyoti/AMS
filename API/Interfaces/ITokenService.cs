
using API.Dtos;
using API.Entintes;

namespace API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Resource resource);
    }
}