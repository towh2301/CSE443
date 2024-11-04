namespace LibraryManagement.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }               

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "The Name cannot exceed 100 characters.")]
        public string Name { get; set; }                   

        [StringLength(500, ErrorMessage = "The Description cannot exceed 500 characters.")]
        public string Description { get; set; }                  

        public DateTime CreatedDate { get; set; } = DateTime.Now; 

        public DateTime? UpdatedDate { get; set; }                

        public bool IsActive { get; set; } = true;                

        [StringLength(255, ErrorMessage = "The Avatar path cannot exceed 255 characters.")]
        public string Avatar { get; set; }

        // Navigation Property
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }


}
