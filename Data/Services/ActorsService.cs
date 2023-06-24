﻿using Microsoft.EntityFrameworkCore;
using MovieHub.Data.Base;
using MovieHub.Models;

namespace MovieHub.Data.Services
{
    public class ActorsService :EntityBaseRepository<Actor>, IActorsService
    {
        
        public ActorsService(AppDbContext context):base(context)
        {
           
        }
        
    }
}
