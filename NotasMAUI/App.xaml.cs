using NotasMAUI.Views;

namespace NotasMAUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            Routing.RegisterRoute("Registrar", typeof(RegistrarView));
            Routing.RegisterRoute("Nota", typeof(NotaView));


            MainPage = new AppShell();


        }
    }
}
