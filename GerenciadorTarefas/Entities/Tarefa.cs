using GerenciadorTarefas.Enums;
using NHibernate.Type;
using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.Entities
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Data { get; set; }
        public EnumStatus Status { get; set; }
    }
}
