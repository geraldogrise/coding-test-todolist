using Azure;
using Todo.Data.Core;
using Todo.Domain.Aggreagates.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Todo.Data.Mappings
{
    public class UserMap : MappingCore<User>
    {
        public override void Map(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("TBL_USUARIO");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(@"ID_USUARIO").IsRequired().HasColumnType("int");
            builder.Property(x => x.Name).HasColumnName(@"NOME").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Email).HasColumnName(@"EMAIL").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Login).HasColumnName(@"LOGIN").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(20);
            builder.Property(x => x.Password).HasColumnName(@"SENHA").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(20);
            base.Map(builder);
        }
    }
}
