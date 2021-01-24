

using CMSTekrar.Data.Context;
using CMSTekrar.Web.Models.Dtos;
using CMSTekrar.Web.Models.Extensions;
using CMSTekrar.Web.Models.Vms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace CMSTekrar.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CartController(ApplicationDbContext applicationDbContext) => _applicationDbContext = applicationDbContext;

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartViewModel = new CartViewModel
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Price * x.Quantity)
            };
            return View(cartViewModel);
        }
    }
}
