using LibraryManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.ViewComponents
{
    public class CarouselViewComponent(ApplicationDbContext context, ILogger<CarouselViewComponent> logger)
        : ViewComponent
    {
        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var carouselItems = await context.Carousel.ToListAsync(); 
            logger.LogInformation("Carousel model size: {Count}", carouselItems.Count);
            return View(carouselItems);
        }
    }
}
