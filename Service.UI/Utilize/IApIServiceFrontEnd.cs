using Newtonsoft.Json;
using System.Text;

namespace Service.UI.Utilize
{

    public interface IApiServiceFrontEnd
    {
        Task<string> PostAsync(object Model, string ApiEndPoint);
        Task<string> GetByIdAsync(string ApiEndPoint, int iD);
        Task<string> GetJsonAsync(string ApiEndPoint);
        Task<string> PostWithFileAsync(object Model, byte[] fileBytes, string fileName, string ApiEndPoint);
        Task<bool> DeleteAsync(string ApiEndPoint, int iD);
    }
    public class IApIServiceFrontEnd : IApiServiceFrontEnd
    {
        private readonly string BaseUrl;

        public IApIServiceFrontEnd()
        {
            BaseUrl = "https://localhost:7219/api/";
        }




        public async Task<string> PostAsync(object Model, string ApiEndPoint)
        {
            using (var _httpclient = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(Model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpclient.PostAsync(BaseUrl + ApiEndPoint, content);
                if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.BadRequest)
                {
                    return "";
                }
                string responsecontent = await response.Content.ReadAsStringAsync();
                //string vMResponse = JsonConvert.DeserializeObject<string>(responsecontent);
                //return vMResponse;
                return responsecontent;
            }
        }
        public async Task<string> GetByIdAsync(string ApiEndPoint, int iD)
        {
            using (var _httpclient = new HttpClient())
            {
                HttpResponseMessage respone = await _httpclient.GetAsync(BaseUrl + ApiEndPoint + iD);
                if (respone.IsSuccessStatusCode)
                {
                    string jsonresponse = await respone.Content.ReadAsStringAsync();
                    return jsonresponse;
                }
                else
                    return "";
            }
        }
        public async Task<string> PostWithFileAsync(object Model, byte[] fileBytes, string fileName, string ApiEndPoint)
        {
            using (var _httpclient = new HttpClient())
            {
                var formData = new MultipartFormDataContent();

                // Add the JSON data
                string json = JsonConvert.SerializeObject(Model);
                var jsonContent = new StringContent(json, Encoding.UTF8, "multipart/form-data");
                formData.Add(jsonContent, "jsonData");

                // Add the file content
                var fileContent = new ByteArrayContent(fileBytes);
                string name = string.IsNullOrEmpty(fileName) ? "NoImage" : fileName;
                formData.Add(fileContent, "file", name);

                HttpResponseMessage response = await _httpclient.PostAsync(BaseUrl + ApiEndPoint, formData);
                string responsecontent = await response.Content.ReadAsStringAsync();
                string vMResponse = JsonConvert.DeserializeObject<string>(responsecontent) ?? "";
                return vMResponse ?? "";
            }
        }
        public async Task<string> GetJsonAsync(string ApiEndPoint)
        {
            using (var _httpclient = new HttpClient())
            {
                HttpResponseMessage respone = await _httpclient.GetAsync(BaseUrl + ApiEndPoint);

                if (respone.IsSuccessStatusCode)
                {
                    var jsonresponse = await respone.Content.ReadAsStringAsync();
                    return jsonresponse;
                }
                else
                {
                    return "";
                }
            }
        }
        public async Task<bool> DeleteAsync(string ApiEndPoint, int iD)
        {
            using (var _httpclient = new HttpClient())
            {
                HttpResponseMessage respone = await _httpclient.DeleteAsync(BaseUrl + ApiEndPoint + iD);
                if (respone.IsSuccessStatusCode)
                {
                    string jsonresponse = await respone.Content.ReadAsStringAsync();
                    return true;
                }
                else
                    return false;
            }
        }

    }
}
