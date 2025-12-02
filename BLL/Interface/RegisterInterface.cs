using Common.DTO;

namespace BLL.Interface
{
    public interface RegisterInterface
    {
        Task<ResponseDTO> Register(RegisterDTO register);

        Task<ResponseDTO> Login(LoginDTO login);
    }
}
