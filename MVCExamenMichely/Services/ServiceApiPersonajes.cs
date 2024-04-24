using MVCExamenMichely.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;

namespace MVCExamenMichely.Services
{
    public class ServiceApiPersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;
        public ServiceApiPersonajes(IConfiguration configuration)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiExamen");
        }
        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        public async Task<List<PersonajeSerie>> GetPersonajesAsync()
        {
            string request = "api/Personajes";
            List<PersonajeSerie> data = await this.CallApiAsync<List<PersonajeSerie>>(request);
            return data;
        }
        public async Task<List<string>> GetSeriesAsync()
        {
            string request = "api/Personajes/PersonajesSerie/serie";
            List<string> data = await this.CallApiAsync<List<string>>(request);
            return data;
        }
        public async Task<PersonajeSerie> FindPersonajes(int id)
        {
            string request = "api/Personajes/" + id;
            PersonajeSerie data = await this.CallApiAsync<PersonajeSerie>(request);
            return data;
        }
        public async Task<PersonajeSerie> FindPersonajesSerie(string serie)
        {
            string request = "/api/Personajes/PersonajesSerie/" + serie;
            PersonajeSerie data = await this.CallApiAsync<PersonajeSerie>(request);
            return data;
        }
        public async Task DeletePersonajes(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/" + id;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);

            }
        }
        public async Task InsertPersonajes(int id, string nombre, string imagen, string serie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/InsertPersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                PersonajeSerie per = new PersonajeSerie();
                per.IdPersonaje = id;
                per.Nombre = nombre;
                per.Imagen = imagen;
                per.Serie = serie;
                string json = JsonConvert.SerializeObject(per);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);

            }
        }
        public async Task UpdatePersonajes(int id, string nombre, string imagen, string serie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/Personajes/UpdatePersonaje";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                PersonajeSerie per = new PersonajeSerie();
                per.IdPersonaje = id;
                per.Nombre = nombre;
                per.Imagen = imagen;
                per.Serie = serie;
                string json = JsonConvert.SerializeObject(per);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);

            }
        }
    }
}