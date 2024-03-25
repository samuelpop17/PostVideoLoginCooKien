using Microsoft.EntityFrameworkCore;
using VideoPostMvc.Models;

namespace VideoPostMvc.Data
{
    public class UsuContext: DbContext
    {

        public UsuContext(DbContextOptions<UsuContext> options) : base(options) { }
        
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
