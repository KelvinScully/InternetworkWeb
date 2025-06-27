using ACommon.Objects;
using Repository.Objects.Account;

namespace Repository.Interfaces
{
    public interface IAccountRepository
    {
        Task<ApiResult<User>> ApiRegister(Register user);
        Task<ApiResult<User>> ApiLogin(Authenticate user);
    }
}
