using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BookMe.Areas.Identity.Data;

// Add profile data for application users by adding properties to the BookMeUser class
public class BookMeUser : IdentityUser
{
    
    public string? PhoneNumber { get; set; }
    
    public string? FirstName { get; set; }
   
    public string? LastName { get; set; }
}

