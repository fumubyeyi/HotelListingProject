using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 
using HotelListing.Api.Models;

namespace HotelListing.Api.Data;
public class HotelListingDbContext : DbContext
{
    public HotelListingDbContext(DbContextOptions options) : base(options)
    {      
    }
    
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<Country> Countries { get; set; }
}