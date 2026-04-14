using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Models;

public class Country
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Abbreviation { get; set; }
    public virtual IList<Hotel> Hotels { get; set; } = [];
}
