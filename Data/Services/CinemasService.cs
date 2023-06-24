using Microsoft.EntityFrameworkCore;
using MovieHub.Data.Base;
using MovieHub.Models;

namespace MovieHub.Data.Services
{
    public class CinemasService :EntityBaseRepository<Cinema>, ICinemasService
    {
        
        public CinemasService(AppDbContext context):base(context)
        {
           
        }
        
    }
}
