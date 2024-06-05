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
    public class NotasViewModel:INotifyPropertyChanged
    {
        public Nota? Nota { get; set; }
        public NotasRepository Repository { get; set; } = new();
        public ObservableCollection<Nota> Notas { get; set; } = new();

        public ICommand NuevaCommand { get; set; }
        public ICommand GuardarCommand { get; set; }
        public ICommand VerDetallesCommand { get; set; }


        public NotasViewModel()
        {
            NuevaCommand = new Command(NuevaNota);
            GuardarCommand = new Command(Guardar);
            VerDetallesCommand = new Command<Nota>(Ver);
        }

        private void Ver(Nota nota)
        {
        }

        private void Guardar()
        {
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
