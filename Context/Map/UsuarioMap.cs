using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteWebApi.Models;

namespace TesteWebApi.Context.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Email).HasMaxLength(256);
            builder.Property(x => x.Senha).HasMaxLength(256);
        }
    }
}
