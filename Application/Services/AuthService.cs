using Infrastructure;
using Microsoft.AspNetCore.Identity;
using DTOs;
using Domain;

namespace Application.Services

{
    public class AuthService:MainService<IdentityUser>
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TokenService _tokenService;  
        private readonly CartService _cartService;
        public AuthService(AppDbContext  AppDbContext , SignInManager<IdentityUser> signInManger,UserManager<IdentityUser> userManager, TokenService tokenService, CartService cartService):base(AppDbContext )
        {
            _signInManager = signInManger;
            _userManager = userManager;
            _tokenService = tokenService;
            _cartService = cartService;
        }

        public async Task<IdentityResult> Register(RegisterRequest registerrequest)
        {
            try
            {
                IdentityUser user = new IdentityUser
                {
                    Email = registerrequest.Email,
                    UserName = registerrequest.UserName
                };
                var result = await _userManager.CreateAsync(user, registerrequest.Password);
                var cartId= _cartService.GetAll().Where(c => c.UserId == user.Id).Select(c => c.Id).FirstOrDefault();
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
        public async Task<string> Login(LoginRequest loginrequest)
        {
            IdentityUser user= await _userManager.FindByEmailAsync(loginrequest.Email);
            var result = await _userManager.CheckPasswordAsync(user, loginrequest.Password);
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
            return await _cartService.Add(new Cart { UserId = userId });
        }
    }
}
