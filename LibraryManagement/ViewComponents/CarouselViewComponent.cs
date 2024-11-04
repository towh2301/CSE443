using LibraryManagement.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.ViewComponents
{
    public class CarouselViewComponent : ViewComponent
    {
        private readonly ILogger<CarouselViewComponent> _logger;
        private readonly ApplicationDbContext _context;

        public CarouselViewComponent(ApplicationDbContext context, ILogger<CarouselViewComponent> logger)
        {
            _context = context;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carouselItems = await _context.Carousel.ToListAsync(); 
            _logger.LogInformation("Carousel model size: {Count}", carouselItems.Count);
            return View(carouselItems);
        }
    }
}
