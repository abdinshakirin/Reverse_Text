using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Reverse_Text.Models;

namespace Reverse_Text.Data
{
    public class Reverse_TextContext : DbContext
    {
        public Reverse_TextContext (DbContextOptions<Reverse_TextContext> options)
            : base(options)
        {
        }

        public DbSet<Reverse_Text.Models.ReverseTextModel> ReverseTextModel { get; set; } = default!;
    }
}
