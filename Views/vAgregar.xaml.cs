using System.Net;

namespace candinoS6.Views;

public partial class vAgregar : ContentPage
{
	public vAgregar()
	{
		InitializeComponent();
	}

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {

        #region httpclient
        HttpClient cliente = new HttpClient();

        Dictionary<string, string> parametros = new Dictionary<string, string>
            {
                { "nombre", txtNombre.Text },
                { "apellido", txtApellido.Text },
                { "edad", txtEdad.Text }
            };

        var contenido = new FormUrlEncodedContent(parametros);

        try
        {
            HttpResponseMessage respuesta = await cliente.PostAsync("http://192.168.100.84/moviles/wsestudiantes.php", contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                await Navigation.PushAsync(new vEstudiante());
            }
            else
            {
                Console.WriteLine("Error al enviar la solicitud: " + respuesta.StatusCode);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
        #endregion httpclient
    }
}