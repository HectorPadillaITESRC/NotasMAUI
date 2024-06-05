using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasMAUI.Models
{
    [Table("notas")]
    public class Nota
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
       [NotNull, ForeignKey(typeof(Usuario))]
        public int IdUsuario { get; set; }
        [NotNull, MaxLength(100)]
        public string Titulo { get; set; } = null!;
        [NotNull, MaxLength(255)]
        public string Descripcion { get; set; } = null!;

    }
}
