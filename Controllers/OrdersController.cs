using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using MovieHub.Data.Base;
using MovieHub.Data.Cart;
using MovieHub.Data.Services;
using MovieHub.Data.ViewModels;
using System.Security.Claims;

namespace MovieHub.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        public readonly IMoviesService _moviesService;
        public readonly ShoppingCart _ShoppingCart;
        public readonly IOrdersService _OrdersService;
        public OrdersController(IMoviesService moviesService, ShoppingCart ShoppingCart, IOrdersService OrdersService)
        {
            _moviesService = moviesService;
            _ShoppingCart = ShoppingCart;
            _OrdersService = OrdersService;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _OrdersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }
        public IActionResult ShoppingCart()
        {
            var items = _ShoppingCart.GetShoppingCartItems();
            _ShoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _ShoppingCart,
                ShoppingCartTotal = _ShoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _ShoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);

            if (item != null)
            {
                _ShoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _ShoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _OrdersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _ShoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

    }
}
