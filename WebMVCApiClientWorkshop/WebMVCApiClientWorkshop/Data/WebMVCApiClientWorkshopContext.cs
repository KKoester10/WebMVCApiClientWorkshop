using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DnDCharacter.Models;

namespace WebMVCApiClientWorkshop.Data
{
    public class WebMVCApiClientWorkshopContext : DbContext
    {
        public WebMVCApiClientWorkshopContext (DbContextOptions<WebMVCApiClientWorkshopContext> options)
            : base(options)
        {
        }

        public DbSet<DnDCharacter.Models.Party> Party { get; set; } = default!;

        public DbSet<DnDCharacter.Models.Character> Character { get; set; }

        public DbSet<DnDCharacter.Models.Abilities> Abilities { get; set; }

        public DbSet<DnDCharacter.Models.CharacterInventory> CharacterInventory { get; set; }
    }
}
