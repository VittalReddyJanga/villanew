using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTOS;

namespace MagicVilla_VillaAPI.Repository.IReposository
{
    public interface IUserRepository
    {
        bool IsuniqueUSer(string username);
        Task<LoginResponseDTO>Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser>Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
