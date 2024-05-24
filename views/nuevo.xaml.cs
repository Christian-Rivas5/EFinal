using System.Net;

namespace EFinal.views;

public partial class nuevo : ContentPage
{

    byte[] imageBytes;
    public nuevo()
	{
		InitializeComponent();
	}

    private async void guardar_Clicked(object sender, EventArgs e)
    {
        int id;
        if (int.TryParse(txtid.Text, out id) && !string.IsNullOrWhiteSpace(txtCedula.Text) && !string.IsNullOrWhiteSpace(txtNombre.Text) && !string.IsNullOrWhiteSpace(txtApellido.Text))
        {
            bool respuesta = await DisplayAlert("Confirmación", "¿Desea guardar la persona?", "Sí", "No");
            if (respuesta)
            {
                try
                {
                    WebClient cliente = new WebClient();
                    string imagenBase64 = Convert.ToBase64String(imageBytes);
                    var parametros = new System.Collections.Specialized.NameValueCollection();
                    parametros.Add("id", txtid.Text);
                    parametros.Add("identificacion", txtCedula.Text);
                    parametros.Add("nombre", txtNombre.Text);
                    parametros.Add("apellido", txtApellido.Text);
                    parametros.Add("usuario", txtUsuario.Text);
                    parametros.Add("contrasena", txtContrasena.Text);
                    parametros.Add("imagen", imagenBase64);
                    cliente.UploadValues("http://localhost/moviles/webexamen.php", "POST", parametros);

                    await DisplayAlert("Éxito", "Persona guardada correctamente.", "OK");

                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Alerta", ex.Message, "Cerrar");
                }
            }
        }
        else
        {
            await DisplayAlert("Error", "Por favor, complete todos los campos correctamente.", "OK");
        }
    }

    private async void uploadfile_Clicked(object sender, EventArgs e)
    {
        try
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Seleccione una imagen"
            });

            if (result != null)
            {
                RutaImagen.Text = result.FullPath;
                var stream = await result.OpenReadAsync();

                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);

                var image = new Image
                {
                    Source = ImageSource.FromStream(() => stream),
                    HeightRequest = 50,
                    WidthRequest = 50,
                    Aspect = Aspect.AspectFit,
                    Margin = new Thickness(10)
                };
                {
                    await stream.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al seleccionar la imagen: {ex.Message}", "OK");
        }

    }


}