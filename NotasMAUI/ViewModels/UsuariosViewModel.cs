using Microsoft.Extensions.Logging.Abstractions;
using NotasMAUI.Models;
using NotasMAUI.Repositories;
using NotasMAUI.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotasMAUI.ViewModels
{
    public class UsuariosViewModel : INotifyPropertyChanged
    {
        public Usuario? Usuario { get; set; } = new();
        public string Error { get; set; } = "";

        UsuariosRepository repository = new();

        public ICommand VerRegistrarCommand { get; set; }
        public ICommand RegistrarCommand { get; set; }
        public ICommand EntrarCommand { get; set; }


        public UsuariosViewModel()
        {
            VerRegistrarCommand = new Command(VerRegistrar);
            RegistrarCommand = new Command(Registrar);
            EntrarCommand = new Command(Entrar);
        }

        private void Entrar()
        {
            if (Usuario != null)
            {
                var user = repository.Get(Usuario.Nombre, Usuario.Contraseña);
                if (user == null)
                {
                    Error = "Nombre de usuario o contraseña incorrecta.";
                    Actualizar(nameof(Error));
                }
                else
                {
                   
                    if (App.Current != null)
                    {
                        //Guardar el usuario logueado

                        NotasViewModel.Usuario = user; //paso por referencia
                        //Cambiar de shell

                        App.Current.MainPage = new NotasShell();
                    }
                }

            }
        }

        private void Registrar()
        {
            if (Usuario != null)
            {
                Error = "";
                if (string.IsNullOrWhiteSpace(Usuario.Nombre))
                {
                    Error += "El nombre de usuario esta vacio";
                }
                if (string.IsNullOrWhiteSpace(Usuario.Contraseña))
                {
                    Error += "La contraseña esta vacia";
                }
                if (repository.Exists(Usuario.Nombre))
                {
                    Error += "Ya existe un usuario con el mismo nombre";
                }

                if (Error == "")
                {
                    repository.Insert(Usuario);


                    Usuario = new();
                    Actualizar(nameof(Usuario));

                    Shell.Current.GoToAsync(".."); //Ir atras

                }


            }
        }

        private void VerRegistrar()
        {
            Usuario = new();
            Actualizar(nameof(Usuario));
            Shell.Current.GoToAsync("Registrar");
        }

        void Actualizar(string? name = null)
        {
            PropertyChanged?.Invoke(this, new(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
