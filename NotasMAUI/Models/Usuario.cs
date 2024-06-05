using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasMAUI.Models
{
    [Table("usuarios")]
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull, MaxLength(150)]
        public string Nombre { get; set; } = null!;
        [NotNull, MaxLength(150)]
        public string Contraseña { get; set; } = null!;

        [OneToMany]
        public List<Nota> Notas { get; set; } = new();
    }
}
