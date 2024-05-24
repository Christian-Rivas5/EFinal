using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net;
using EFinal.model;

namespace EFinal.views;

public partial class login : ContentPage
{

    WebClient cliente = new WebClient();
    public login()
    {
        InitializeComponent();
    }

    private async void login_Clicked(object sender, EventArgs e)
    {
        string usuario = usuarioEntry.Text;
        string contraseana = contrasenaEntry.Text;

        try
        {
            string url = "http://localhost/moviles/webexamen.php?usuario=" + usuario;
            string respuestaJson = cliente.DownloadString(url);

            estudiante estudianteEncontrado = JsonConvert.DeserializeObject<estudiante>(respuestaJson);

            if (estudianteEncontrado != null)
            {
                if(contraseana == estudianteEncontrado.contrasena) {
                    await DisplayAlert("Éxito", "Inicio de sesión exitoso.", "OK");
                    await Navigation.PushAsync(new MainPage());
                }
                else { await DisplayAlert("Credenciales incorrectas", "Usuario o contraseña incorrectos.", "OK"); }
                
            }
            else
            {
                await DisplayAlert("Credenciales incorrectas", "Usuario o contraseña incorrectos.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Estudiante no existente","El estudiante no existe, pongase en contacto con el administrador", "OK");
        }

    }
}