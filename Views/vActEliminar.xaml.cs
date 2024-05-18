using candinoS6.Models;
using System.Net;
using System.Text;
using System.Text.Json;

namespace candinoS6.Views;

public partial class vActEliminar : ContentPage
{
    private const string BaseUrl = "http://192.168.100.84/moviles/wsestudiantes.php";
    HttpClient cliente = new HttpClient();

    public vActEliminar(Estudiante estudiante)
	{
		InitializeComponent();
        txtCodigo.Text = estudiante.codigo.ToString();
        txtNombre.Text = estudiante.nombre.ToString();
        txtApellido.Text = estudiante.apellido.ToString();
        txtEdad.Text = estudiante.edad.ToString();
	}

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        string codEstudiante = txtCodigo.Text;
        string nombreEst = txtNombre.Text;
        string apellidoEst = txtApellido.Text;
        string edadEst = txtEdad.Text;
        string urlActualizar = BaseUrl + "?codigo=" + codEstudiante + "&nombre=" + nombreEst + "&apellido=" + apellidoEst + "&edad=" + edadEst;
      
        Dictionary<string, string> parametros = new Dictionary<string, string>
            {
                { "codigo", txtCodigo.Text },
            };

        var contenido = new FormUrlEncodedContent(parametros);

        try
        {
            HttpResponseMessage respuesta = await cliente.PutAsync(urlActualizar, contenido);

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
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        string codEstudiante = txtCodigo.Text;
        string urlBorrar = BaseUrl + "?codigo=" + codEstudiante;

        try
        {
            HttpResponseMessage respuesta = await cliente.DeleteAsync(urlBorrar);

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
    }
}