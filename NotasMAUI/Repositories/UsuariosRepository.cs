using NotasMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasMAUI.Repositories
{
    public class UsuariosRepository
    {
        SQLiteConnection conexion;

        public UsuariosRepository()
        {
            string ruta = FileSystem.AppDataDirectory + "/notas.sqlite";
            conexion = new(ruta);
            conexion.CreateTable<Usuario>();
        }

        public Usuario? Get(string nombre, string contraseña)
        {
            return conexion.Table<Usuario>()
                .Where(x => x.Nombre == nombre && x.Contraseña == contraseña)
                .FirstOrDefault();
        }

        public bool Exists(string nombre)
        {
            return conexion.Table<Usuario>().Any(x=>x.Nombre==nombre);
        }

        public void Insert(Usuario usuario)
        {
            conexion.Insert(usuario);
        }
    }
}