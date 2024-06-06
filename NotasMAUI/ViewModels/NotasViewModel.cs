using CommunityToolkit.Maui.Alerts;
using NotasMAUI.Models;
using NotasMAUI.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NotasMAUI.ViewModels
{
    public class NotasViewModel : INotifyPropertyChanged
    {
        public Nota? Nota { get; set; }
        public NotasRepository Repository { get; set; } = new();
        public ObservableCollection<Nota> Notas { get; set; } = new();

        public ICommand NuevaCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand VerDetallesCommand { get; set; }
        public ICommand CerrarSesionCommand { get; set; }


        public NotasViewModel()
        {
            NuevaCommand = new Command(NuevaNota);
            GuardarCommand = new Command(Guardar);
            VerDetallesCommand = new Command<Nota>(Ver);
            CerrarSesionCommand = new Command(Cerrar);
            CargarDatos();
        }

        private void Cerrar()
        {
            if (App.Current != null)
            {
                App.Current.MainPage = new AppShell();
            }
        }

        void CargarDatos()
        {
            var notas = Repository.GetByUser(Usuario.Id);
            Notas.Clear();
            foreach (var n in notas)
            {
                Notas.Add(n);
            }
        }

        private void Ver(Nota nota)
        {
            Nota = nota;
            Actualizar(nameof(Nota));
            Shell.Current.GoToAsync("Nota");

        }

        private void Guardar()
        {
            if (Nota != null)
            {
                if (Nota.Id == 0) //Si es cero, no ha sido insertada en la bd: Id es Autoincrement
                {
                    if (string.IsNullOrWhiteSpace(Nota.Titulo) && string.IsNullOrWhiteSpace(Nota.Descripcion))
                    {
                        //descartamos
                        Shell.Current.GoToAsync("..");
                    }
                    else if (!string.IsNullOrWhiteSpace(Nota.Titulo))
                    {
                        Nota.IdUsuario = Usuario.Id;
                        Repository.Insert(Nota);
                        Shell.Current.GoToAsync("..");
                    }
                    else
                    {
                        Toast.Make("Debe escribir el titulo de la nota").Show();
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(Nota.Titulo) && string.IsNullOrWhiteSpace(Nota.Descripcion))
                    {
                        Repository.Delete(Nota);
                    }
                    else
                    {
                        Repository.Update(Nota);
                    }

                    Shell.Current.GoToAsync("..");
                }

                CargarDatos();

            }
        }

        private void NuevaNota()
        {
            Nota = new();
            Actualizar(nameof(Nota));

            Shell.Current.GoToAsync("Nota");
        }

        public static Usuario Usuario { get; set; } = null!;

        void Actualizar(string? name = null)
        {
            PropertyChanged?.Invoke(this, new(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
