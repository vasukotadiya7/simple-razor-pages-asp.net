using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesUI.Data;
using RazorPagesUI.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.Json;

namespace RazorPagesUI.Pages
{
    public class CheckoutModel : PageModel

    {
        private readonly RazorContext context;
        [BindProperty, Required, Display(Name = "Your Email Address")]
        public string OrderEmail { get; set; }
        [BindProperty, Required, Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; }
        [TempData]
        public string Confirmation { get; set; }
        public CheckoutModel(RazorContext context)
        {
            this.context = context;
        }
        public Basket Basket { get; set; } = new();
        public List<Product> SelectedProducts { get; set; } = new();
        public async Task OnGetAsync()
        {
            if (Request.Cookies[nameof(Basket)] is not null)
            {
                Basket = JsonSerializer.Deserialize<Basket>(Request.Cookies[nameof(Basket)]);
                if (Basket.NumberOfItems > 0)
                {
                    var selectedProducts = Basket.Items.Select(b => b.ProductId).ToArray();
                    SelectedProducts = await context.Products.Where(p => selectedProducts.Contains(p.Id)).ToListAsync();
                }
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid && Request.Cookies[nameof(Basket)] is not null)
            {
                var basket = JsonSerializer.Deserialize<Basket>(Request.Cookies[nameof(Basket)]);
                if (basket is not null)
                {
                    var plural = basket.NumberOfItems == 1 ? string.Empty : "s";
                    Confirmation = $@"<p>Your order for {basket.NumberOfItems} item{plural} has been received and is being processed:</p>
            <p>It will be sent to {ShippingAddress}. We will notify you when it has been despatched</p>";
                    
                    Response.Cookies.Append(nameof(Basket), string.Empty, new CookieOptions { Expires = DateTime.Now.AddDays(-1) });
                    return RedirectToPage("/OrderSuccess");
                }
            }
            return Page();
        }
    }
}
