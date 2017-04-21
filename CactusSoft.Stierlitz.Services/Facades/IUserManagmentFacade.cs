using System.Threading.Tasks;
using CactusSoft.Stierlitz.Domain;

namespace CactusSoft.Stierlitz.Services.Facades
{
    public interface IUserManagmentFacade
    {
        Task LoginAsync(UserCredentials userCredentials);
        Task LogoutAsync();
    }
}
