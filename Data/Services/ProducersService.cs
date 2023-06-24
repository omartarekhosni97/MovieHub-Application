using Microsoft.EntityFrameworkCore;
using MovieHub.Data.Base;
using MovieHub.Models;

namespace MovieHub.Data.Services
{
    public class ProducersService :EntityBaseRepository<Producer>, IProducersService
    {
        
        public ProducersService(AppDbContext context):base(context)
        {
           
        }
        
    }
}
