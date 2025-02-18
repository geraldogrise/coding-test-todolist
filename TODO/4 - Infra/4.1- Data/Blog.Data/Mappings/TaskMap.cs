using Azure;
using Todo.Data.Core;
using Todo.Domain.Aggreagates.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Todo.Data.Mappings
{
    public class TaskMap : MappingCore<Domain.Aggreagates.Tasks.Task>
    {
        public override void Map(EntityTypeBuilder<Domain.Aggreagates.Tasks.Task> builder)
        {
            builder.ToTable("TBL_ATIVIDADE");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName(@"ID_ATIVIDADE").IsRequired().HasColumnType("int");
            builder.Property(x => x.Name).HasColumnName(@"NOME").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(x => x.Description).HasColumnName(@"DESCRICAO").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(400);
            builder.Property(x => x.RegistrationDate).HasColumnName(@"DATA_CADASTRO").IsRequired().IsUnicode(false).HasColumnType("datetime");
            builder.Property(x => x.EndDate).HasColumnName(@"DATA_TERMINO").IsUnicode(false).HasColumnType("datetime");
            builder.Property(x => x.Id_user).HasColumnName(@"ID_USUARIO").IsRequired().HasColumnType("int");
            builder.HasOne(x => x.User).WithMany(y => y.TaskList).HasForeignKey(x => x.Id_user);
            base.Map(builder);
        }
    }
}
