﻿using AutoMapper;
using Blog.Business.Extensions;
using Blog.Business.Helpers.Images;
using Blog.Business.Services.Abstract;
using Blog.DataAccess.UnitOfWorks;
using Blog.Entity.DTOs.Users;
using Blog.Entity.Entities;
using Blog.Entity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog.Business.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClaimsPrincipal _user;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IImageHelper _imageHelper;

        public UserService(IMapper mapper, RoleManager<AppRole> roleManager, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager, IUnitOfWork unitOfWork, SignInManager<AppUser> signInManager, IImageHelper imageHelper)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _user = httpContextAccessor.HttpContext.User;
            _signInManager = signInManager;
            _imageHelper = imageHelper;
        }

        private async Task<Guid> UploadImageForUser(UserProfileDto userProfileDto)
        {
            var userEmail = _user.GetLoggedInEmail();
            var imageUpload = await _imageHelper.Upload($"{userProfileDto.FirstName}{userProfileDto.LastName}", userProfileDto.Photo, ImageTypes.User);
            Image image = new(imageUpload.FullName, userProfileDto.Photo.ContentType, userEmail);
            await _unitOfWork.GetRepository<Image>().AddAsync(image);

            return image.Id;
        }

        public async Task<IdentityResult> CreateUserAsync(UserAddDto userAddDto)
        {
            var map = _mapper.Map<AppUser>(userAddDto);
            map.UserName = userAddDto.Email;
            var result = await _userManager.CreateAsync(map, string.IsNullOrEmpty(userAddDto.Password) ? "" : userAddDto.Password);
            if (result.Succeeded)
            {
                var findRole = await _roleManager.FindByIdAsync(userAddDto.RoleId.ToString());
                await _userManager.AddToRoleAsync(map, findRole.ToString());
                return result;
            }
            else
                return result;
        }

        public async Task<(IdentityResult identityResult, string? email)> DeleteUserAsync(Guid userId)
        {
            var user = await GetAppUserByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return (result, user.Email);
            }

            else
                return (result, null);
        }

        public async Task<List<AppRole>> GetAllRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<List<UserDto>> GetAllUsersWithRoleAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var map = _mapper.Map<List<UserDto>>(users);

            foreach (var item in map)
            {
                var findUser = await _userManager.FindByIdAsync(item.Id.ToString());
                var role = string.Join("", await _userManager.GetRolesAsync(findUser));

                item.Role = role;

            }
            return map;
        }

        public async Task<AppUser> GetAppUserByIdAsync(Guid userId)
        {
            return await _userManager.FindByIdAsync(userId.ToString());
        }

        public async Task<UserProfileDto> GetUserProfileAsync()
        {
            var userId = _user.GetLoggedInUserId();
            var getUserWithImage = await _unitOfWork.GetRepository<AppUser>().GetAsync(x => x.Id == userId, x => x.Image);
            var map = _mapper.Map<UserProfileDto>(getUserWithImage);
            map.Image.FileName = getUserWithImage.Image.FileName;

            return map;
        }

        public async Task<string> GetUserRoleAsync(AppUser appUser)
        {
            return String.Join("", await _userManager.GetRolesAsync(appUser));
        }

        public async Task<IdentityResult> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            var user = await GetAppUserByIdAsync(userUpdateDto.Id);
            var userRole = await GetUserRoleAsync(user);
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _userManager.RemoveFromRoleAsync(user, userRole);
                var findRole = await _roleManager.FindByIdAsync(userUpdateDto.RoleId.ToString());
                await _userManager.AddToRoleAsync(user, findRole.Name);
                return result;
            }
            else
                return result;
        }

        public async Task<bool> UpdateUserProileAsync(UserProfileDto userProfileDto)
        {
            var userId = _user.GetLoggedInUserId();
            var user = await GetAppUserByIdAsync(userId);

            var isVerified = await _userManager.CheckPasswordAsync(user, userProfileDto.CurrentPassword);
            if (isVerified && userProfileDto.NewPassword != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, userProfileDto.CurrentPassword, userProfileDto.NewPassword);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.PasswordSignInAsync(user, userProfileDto.NewPassword, true, false);

                    _mapper.Map(userProfileDto, user);

                    if (userProfileDto.Photo != null)
                        user.ImageId = await UploadImageForUser(userProfileDto);

                    await _userManager.UpdateAsync(user);
                    await _unitOfWork.SaveAsync();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (isVerified)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                _mapper.Map(userProfileDto, user);

                if (userProfileDto.Photo != null)
                    user.ImageId = await UploadImageForUser(userProfileDto);

                await _userManager.UpdateAsync(user);
                await _unitOfWork.SaveAsync();

                return true;
            }
            else
                return false;
        }
    }
}
