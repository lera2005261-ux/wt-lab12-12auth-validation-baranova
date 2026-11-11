using System.ComponentModel.DataAnnotations;

namespace wt_lab12_12auth_validation_baranova.Models;

public class Product
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Введите название товара")]
    [StringLength(100, ErrorMessage = "Название не длиннее 100 символов")]
    [Display(Name = "Название")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Введите категорию")]
    [StringLength(50, ErrorMessage = "Категория не длиннее 50 символов")]
    [Display(Name = "Категория")]
    public string Category { get; set; } = string.Empty;

    [Range(0.01, 1000000, ErrorMessage = "Цена должна быть от 0,01 до 1 000 000")]
    [Display(Name = "Цена, руб.")]
    public decimal Price { get; set; }

    [Display(Name = "Товар в наличии")]
    public bool IsAvailable { get; set; } = true;

    [StringLength(500, ErrorMessage = "Описание не длиннее 500 символов")]
    [Display(Name = "Описание")]
    [DisplayFormat(ConvertEmptyStringToNull = false)]   // пустое описание сохраняется как "", а не null
    public string Description { get; set; } = "";
}
