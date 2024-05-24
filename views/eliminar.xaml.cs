using EFinal.model;
using Newtonsoft.Json;
using System.Net;

namespace EFinal.views;

public partial class eliminar : ContentPage
{

    WebClient cliente = new WebClient();
    public eliminar()
	{
		InitializeComponent();
	}

    private async void buscar_cliked(object sender, EventArgs e)
    {
        string identificacion = TxtCedula.Text;

        try
        {
            string url = "http://localhost/moviles/webexamen.php?identificacion=" + identificacion;
            string respuestaJson = cliente.DownloadString(url);

            estudiante estudiante = JsonConvert.DeserializeObject<estudiante>(respuestaJson);

            if (estudiante != null)
            {
                LblId.Text = estudiante.id.ToString();
                LblCedula.Text = estudiante.identificacion;
                LblNombre.Text = estudiante.nombre;
                LblApellido.Text = estudiante.apellido;
            }
            else
            {
                await DisplayAlert("Alerta", "Estudiante no encontrado", "OK");
                await Navigation.PopAsync();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al buscar estudiante: {ex.Message}", "OK");
        }


    }

    private void Eliminar_Clicked(object sender, EventArgs e)
    {
        string identificacion = TxtCedula.Text;
        try
        {
            string url = "http://localhost/moviles/webexamen.php?identificacion=" + identificacion;


            using (WebClient client = new WebClient())
            {
                client.UploadStringAsync(new Uri(url), "DELETE", "");
                client.UploadStringCompleted += (s, ev) =>
                {
                    if (ev.Error != null)
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await DisplayAlert("Error", $"Error al eliminar estudiante: {ev.Error.Message}", "OK");
                        });
                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await DisplayAlert("Éxito", "Estudiante eliminado correctamente.", "OK");
                        });
                    }
                };

                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
               
            }
        }
        catch (Exception ex)
        {
        }


    }
}