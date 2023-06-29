using Microsoft.EntityFrameworkCore;
using MovieHub.Data.Base;
using MovieHub.Models;

namespace MovieHub.Data.Services
{
    public class CinemasService :IOrdersService<Cinema>, ICinemasService
    {
        
        public CinemasService(AppDbContext context):base(context)
        {
           
        }
        
    }
}
