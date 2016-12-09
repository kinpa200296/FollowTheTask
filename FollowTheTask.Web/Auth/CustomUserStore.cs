using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Web.Mvc;
using FollowTheTask.BLL.Result;
using FollowTheTask.BLL.Services.User;
using FollowTheTask.TransferObjects.Model.Queries;
using FollowTheTask.TransferObjects.User.Queries;
using Microsoft.AspNet.Identity;

namespace FollowTheTask.Web.Auth
{
    public class CustomUserStore :
        IUserStore<AuthUser>,
        IUserPasswordStore<AuthUser>,
        IUserEmailStore<AuthUser>,
        IUserLockoutStore<AuthUser, string>,
        IUserTwoFactorStore<AuthUser, string>,
        IUserSecurityStampStore<AuthUser, string>
    {
        private readonly IUserService _userService;

        public CustomUserStore()
        {
            _userService = DependencyResolver.Current.GetService<IUserService>();
        }

        public async Task CreateAsync(AuthUser user)
        {
            var result = await _userService.CreateModelAsync(user.Dto);
            ProcessCommandResult(result);
        }

        public async Task UpdateAsync(AuthUser user)
        {
            var result = await _userService.UpdateModelAsync(user.Dto);
            ProcessCommandResult(result);
        }

        public async Task DeleteAsync(AuthUser user)
        {
            var result = await _userService.DeleteModelAsync(user.Dto.Id);
            ProcessCommandResult(result);
        }

        public async Task<AuthUser> FindByIdAsync(string userId)
        {
            var result = await _userService.GetModelDtoAsync(new ModelQuery {Id = int.Parse(userId)});
            return AuthUser.FromDto(ProcessQueryResult(result));
        }

        public async Task<AuthUser> FindByNameAsync(string userName)
        {
            var result = await _userService.GetUserDtoAsync(new UserQuery {Username = userName});
            return AuthUser.FromDto(ProcessQueryResult(result));
        }

        public void Dispose()
        {
            _userService.Dispose();
        }


        #region Password store

        public Task SetPasswordHashAsync(AuthUser user, string passwordHash)
        {
            var passwordParts = passwordHash.Split(' ');
            user.Dto.Auth.PasswordHash = passwordParts[0];
            user.Dto.Auth.Salt = passwordParts[1];
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(AuthUser user)
        {
            return Task.FromResult($"{user.Dto.Auth.PasswordHash} {user.Dto.Auth.Salt}");
        }

        public Task<bool> HasPasswordAsync(AuthUser user)
        {
            return Task.FromResult(user.HasPassword);
        }

        #endregion Password store


        #region Email store

        public Task SetEmailAsync(AuthUser user, string email)
        {
            user.Email = email;
            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(AuthUser user)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(AuthUser user)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(AuthUser user, bool confirmed)
        {
            user.Dto.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<AuthUser> FindByEmailAsync(string email)
        {
            var result = await _userService.GetUserDtoAsync(new UserQuery {Email = email});
            return AuthUser.FromDto(ProcessQueryResult(result));
        }

        #endregion Email store


        #region Lockout store

        public Task<DateTimeOffset> GetLockoutEndDateAsync(AuthUser user)
        {
            return Task.FromResult(user.Dto.Auth.LockoutDateUtc ?? DateTimeOffset.MinValue);
        }

        public Task SetLockoutEndDateAsync(AuthUser user, DateTimeOffset lockoutEnd)
        {
            user.Dto.Auth.LockoutDateUtc = lockoutEnd;
            return Task.FromResult(0);
        }

        public Task<int> IncrementAccessFailedCountAsync(AuthUser user)
        {
            user.Dto.Auth.AccessFailedCnt++;
            return Task.FromResult(user.Dto.Auth.AccessFailedCnt);
        }

        public Task ResetAccessFailedCountAsync(AuthUser user)
        {
            user.Dto.Auth.AccessFailedCnt = 0;
            return Task.FromResult(0);
        }

        public Task<int> GetAccessFailedCountAsync(AuthUser user)
        {
            return Task.FromResult(user.Dto.Auth.AccessFailedCnt);
        }

        public Task<bool> GetLockoutEnabledAsync(AuthUser user)
        {
            return Task.FromResult(user.Dto.Auth.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(AuthUser user, bool enabled)
        {
            user.Dto.Auth.LockoutEnabled = enabled;
            return Task.FromResult(0);
        }

        #endregion Lockout store


        #region Security stamp store

        public Task SetSecurityStampAsync(AuthUser user, string stamp)
        {
            user.Dto.Auth.SecurityStamp = stamp;
            return Task.FromResult(0);
        }

        public Task<string> GetSecurityStampAsync(AuthUser user)
        {
            return Task.FromResult(user.Dto.Auth.SecurityStamp);
        }

        #endregion Security stamp store


        #region Two factor store

        public Task SetTwoFactorEnabledAsync(AuthUser user, bool enabled)
        {
            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(AuthUser user)
        {
            return Task.FromResult(false);
        }

        #endregion Two factor store


        private void ProcessCommandResult(CommandResult result)
        {
            if (result.IsFailed)
            {
                Debug.WriteLine(result.Message);
            }
        }

        private T ProcessQueryResult<T>(QueryResult<T> result)
        {
            if (result.IsFailed)
            {
                Debug.WriteLine(result.Message);
            }
            return result.Value;
        }
    }
}