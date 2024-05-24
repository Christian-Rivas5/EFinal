
using EFinal.model;
using EFinal.views;

namespace EFinal
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            CargarEstudiantes();
        }

        private void CargarEstudiantes()
        {
            List<estudiante> estudiantes = new List<estudiante>
            {
                new estudiante { id = 0, identificacion = "010102546", nombre = "Juan", apellido = "perez", }
            };

            EstudiantesCollectionView.ItemsSource = estudiantes;
        }
    
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new nuevo());
        }

        private async void elimnar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new eliminar());
        }
    }
}
