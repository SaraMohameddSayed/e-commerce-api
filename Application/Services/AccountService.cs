using Infrastructure;
using Microsoft.AspNetCore.Identity;
using DTOs;
using Domain;

namespace Services
{
    public class AccountService:MainService<IdentityUser>
    {
        private SignInManager<IdentityUser> _signInManager;
        private UserManager<IdentityUser> _userManager;
        private TokenService _tokenService;  
        private CartService _cartService;
        public AccountService(dbContext dbContext, SignInManager<IdentityUser> signInManger,UserManager<IdentityUser> userManager, TokenService tokenService, CartService cartService):base(dbContext)
        {
            _signInManager = signInManger;
            _userManager = userManager;
            _tokenService = tokenService;
            _cartService = cartService;
        }

        public async Task<IdentityResult> Register(RegisterRequest registerViewModel)
        {
            try
            {
                IdentityUser user = new IdentityUser
                {
                    Email = registerViewModel.email,
                    UserName=registerViewModel.userName 
                };
                var result = await _userManager.CreateAsync(user, registerViewModel.password);
                var cartId= _cartService.GetAll().Where(c => c.userId == user.Id).Select(c => c.id).FirstOrDefault();
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
        public async Task<string> Login(LoginRequest loginViewModel)
        {
            IdentityUser user= await _userManager.FindByEmailAsync(loginViewModel.email);
            var result = await _userManager.CheckPasswordAsync(user, loginViewModel.password);
            if(user==null || !result)
                {
                throw new Exception("Invalid login attempt.");
            }
            return await _tokenService.GenerateToken(user);


        }
        public async void Logout()
        {
             await _signInManager.SignOutAsync();
        }

        public async Task<bool> createCart(string userId)
        {
            return await _cartService.Add(new Cart { userId = userId });
        }
    }
}
