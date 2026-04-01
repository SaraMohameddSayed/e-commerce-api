using Infrastructure;
using Microsoft.AspNetCore.Identity;
using ViewModels;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Managers
{
    public class accountManager:MainManager<IdentityUser>
    {
        private SignInManager<IdentityUser> signInManager;
        private UserManager<IdentityUser> userManager;
        private tokenManager tokenManager;  
        private cartManager cartManager;
        public accountManager(dbContext _dbContext,SignInManager<IdentityUser> _signInManger,UserManager<IdentityUser> _userManager,tokenManager _tokenManager, cartManager _cartManager):base(_dbContext)
        {
            signInManager = _signInManger;
            userManager = _userManager;
            tokenManager = _tokenManager;
            cartManager = _cartManager;
        }

        public async Task<IdentityResult> register(registerViewModel registerViewModel)
        {
            try
            {
                IdentityUser user = new IdentityUser
                {
                    Email = registerViewModel.email,
                    UserName=registerViewModel.userName 
                };
                var result = await userManager.CreateAsync(user, registerViewModel.password);
                var cartId= cartManager.getAll().Where(c => c.userId == user.Id).Select(c => c.id).FirstOrDefault();
                if (cartId == 0) {
                    await createCart(user.Id);

                }
                return result;
            }
            catch
            {
                throw;
            }


        }
        public async Task<string> login(loginViewModel loginViewModel)
        {
            IdentityUser user= await userManager.FindByEmailAsync(loginViewModel.email);
            var result = await userManager.CheckPasswordAsync(user, loginViewModel.password);
            if(user==null || !result)
                {
                throw new Exception("Invalid login attempt.");
            }
            return await tokenManager.generateToken(user);


        }
        public async void logout()
        {
             await signInManager.SignOutAsync();
        }

        public async Task<bool> createCart(string userId)
        {
            return await cartManager.Add(new Cart { userId = userId });
        }
    }
}
