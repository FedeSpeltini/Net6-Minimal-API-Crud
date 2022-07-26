using System.ComponentModel.DataAnnotations;
using BookShop.Models;

namespace BookShop.Dtos;

public class BookWriteDto {

  [Required]
  public string? Title {get; set;}

  [Required]
  public string? Author { get; set; }

  [Required]
  public string? ISBN { get; set; }

}