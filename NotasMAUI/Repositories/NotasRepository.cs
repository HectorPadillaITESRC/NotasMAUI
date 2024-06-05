using NotasMAUI.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotasMAUI.Repositories
{
    public class NotasRepository
    {
        SQLiteConnection conexion;

        public NotasRepository()
        {
            string ruta = FileSystem.AppDataDirectory + "/notas.sqlite";
            conexion = new(ruta);
            conexion.CreateTable<Nota>();
        }

        public IEnumerable<Nota> GetAll()
        {
            return conexion.Table<Nota>().OrderBy(x=>x.Titulo);
        }

        public Nota? Get(int id)
        {
            return conexion.Get<Nota>(id);
        }

        public void Insert(Nota Nota)
        {
            conexion.Insert(Nota);
        }
        public void Update(Nota Nota)
        {
            conexion.Update(Nota);
        }
        public void Delete(Nota Nota)
        {
            conexion.Delete(Nota);
        }

    }
}
