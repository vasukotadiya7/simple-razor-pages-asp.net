using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesUI.Data;
using RazorPagesUI.Models;
using Microsoft.EntityFrameworkCore;


namespace RazorPagesUI.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string? Name { get; set; }

        private readonly RazorContext _context;

        public List<Product> Products { get; set; } = new();
        public IndexModel(ILogger<IndexModel> logger,RazorContext razorContext)
        {
            _logger = logger;
            _context = razorContext;
            //this.Name = name;
            Name = "User";
        }

        

        public async Task OnGetAsync() =>
            Products = await _context.Products.ToListAsync();

        public void OnGetNameChange()
        {
            Name = "Vasu";
        }
    }

    
        
    }

