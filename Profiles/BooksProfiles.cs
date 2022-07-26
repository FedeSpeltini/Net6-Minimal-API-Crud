using AutoMapper;
using BookShop.Dtos;
using BookShop.Models;

namespace BookShop.Profiles
{
    public class BooksProfile: Profile
    {
        public BooksProfile()
        {
            CreateMap<Book,  BookReadDto>();
            CreateMap<BookWriteDto, Book>();
            CreateMap<BookUpdateDto, Book>();
        }
        
    }
    
}