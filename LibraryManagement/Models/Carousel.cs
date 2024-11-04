namespace LibraryManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Carousel
    {
        [Key]
        public int CarouselId { get; set; }  // Primary Key, Auto-Increment

        [Required]
        public string ImageUrl { get; set; } // URL of the image (Required)

        [Required]
        [StringLength(200)]
        public string Title { get; set; }    // Title for the item (Required)

        public string Description { get; set; } // Additional details (Optional)

        public string LinkUrl { get; set; }     // URL linked to the item (Optional)

        [Required]
        public int Order { get; set; }          // Display order of items (Required)

        [Required]
        public bool IsActive { get; set; } = true; // Indicates if the item is active (Default to true)

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now; // Timestamp of creation

        public DateTime? UpdatedDate { get; set; } // Timestamp of last update (Nullable)
    }

}
