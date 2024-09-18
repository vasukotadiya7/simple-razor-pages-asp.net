using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPagesUI.Data;
using RazorPagesUI.Models;
using System.Text.Json;

namespace RazorPagesUI;

public class BasketViewComponent:ViewComponent
{
    private readonly RazorContext context;
    public BasketViewComponent(RazorContext context)
    {
        this.context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        Models.Basket basket = new();
        if (Request.Cookies[nameof(Basket)] is not null)
        {
            basket = JsonSerializer.Deserialize<Models.Basket?>(Request.Cookies[nameof(Basket)]!);
        }
        return View(basket);
    }
}
