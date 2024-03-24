using Blog.Entity.DTOs.Users;
using Blog.Entity.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blog.Business.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsersWithRoleAsync();
        Task<List<AppRole>> GetAllRolesAsync();
        Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto);
        Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto);
        Task<AppUser> GetAppUserByIdAsync(Guid userId);
        Task<string> GetUserRoleAsync(AppUser appUser);
        Task<(IdentityResult identityResult,string? email)> DeleteUserAsync(Guid userId);
        Task<UserProfileDto> GetUserProfileAsync();
        Task<bool> UpdateUserProileAsync(UserProfileDto userProfileDto);

    }
}
