using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Service.UI.Utilize
{
    public interface IApiService
    {
        Task<string> PostAsync(object Model, string ApiEndPoint);
        Task<string> GetByIdToken(string ApiEndPoint, string Token);
        Task<string> GetByIdToken(string ApiEndPoint, int id, string Token);

        Task<string> GetByIdAsync(string ApiEndPoint, int ID);
        Task<string> GetJsonAsync(string ApiEndPoint);
        //Task<string> GetWithGuidsAsync(List<Guid?> guids, string ApiEndPoint);

        Task<bool> PutAsync(object Model, string ApiEndPoint, string Token);
        Task<bool> DeleteAsync(string ApiEndpoint, int Id);
        //Task<string> PostJsonAsync(object Model, string ApiEndPoint);
        //Task<VMResponse> DeleteJsonAsync(string ApiEndpoint);
        Task<string> PostWithFileAsync(object Model, byte[] fileBytes, string fileName, string ApiEndPoint);
        Task<string> PostWithMultiFilesAsync(object Model, List<byte[]> fileBytes, List<string> fileNames, string ApiEndPoint);
        //Task<List<IFormFile>> GetFilesAsync(string ApiEndPoint);
        //Task<VMResponse> PostWithFilesAsync(List<string> fileNames, List<byte[]> fileBytes, string ApiEndPoint);
        //Task<VMResponse> PuttWithFilesAsync(object Model, List<byte[]> fileBytes, List<string> fileNames, string ApiEndPoint);
        Task<string> PuttWithFileAsync(object Model, byte[] fileBytes, string fileName, string ApiEndPoint, string Token);
    }

    public class ApiService : IApiService
    {
        private readonly string BaseUrl;

        public ApiService()
        {
            BaseUrl = "https://localhost:7278/api/";
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
        //public async Task<List<IFormFile>> GetFilesAsync(string ApiEndPoint)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        HttpResponseMessage response = await httpClient.GetAsync(BaseUrl + ApiEndPoint);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var files = new List<IFormFile>();

        //            // Assuming the API returns a list of file paths or URLs
        //            var fileUrls = await response.Content.ReadAsAsync<List<string>>();

        //            foreach (var fileUrl in fileUrls)
        //            {
        //                // Download file content
        //                byte[] fileBytes = await httpClient.GetByteArrayAsync(fileUrl);

        //                // Create an IFormFile instance
        //                var file = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", Path.GetFileName(fileUrl));

        //                files.Add(file);
        //            }

        //            return files;
        //        }
        //        else
        //        {
        //            _errorHandlingService.TriggerError("An error occurred: " + response.ReasonPhrase);
        //            return null;
        //        }
        //    }
        //}

        //public async Task<string> GetWithGuidsAsync(List<Guid?> guids, string ApiEndPoint)
        //{
        //    using (var _httclient = new HttpClient())
        //    {

        //        string stringguids = string.Join(",", guids);
        //        string fullurl = BaseUrl + ApiEndPoint + stringguids;
        //        HttpResponseMessage response = await _httclient.GetAsync(fullurl);
        //        string jsonresponse = await response.Content.ReadAsStringAsync();
        //        return jsonresponse;
        //    }
        //}

        public async Task<string> PostWithMultiFilesAsync(object Model, List<byte[]> fileBytes, List<string> fileNames, string ApiEndPoint)
        {
            using (var _httpclient = new HttpClient())
            {
                var formData = new MultipartFormDataContent();

                // Add the JSON data
                string json = JsonConvert.SerializeObject(Model);
                var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
                formData.Add(jsonContent, "jsonData");

              
                //var fileContent = new ByteArrayContent(fileBytes);
                //string name = string.IsNullOrEmpty(fileName) ? "NoImage" : fileName;
                //formData.Add(fileContent, "file", name);

                for (int i = 0; i < fileBytes.Count; i++)
                {
                    var fileContent = new ByteArrayContent(fileBytes[i]);
                    string name = (fileNames != null && i < fileNames.Count) ? fileNames[i] : "NoImage";
                    formData.Add(fileContent, "file", name);

                }

                HttpResponseMessage response = await _httpclient.PostAsync(BaseUrl + ApiEndPoint, formData);
                string responsecontent = await response.Content.ReadAsStringAsync();
            
                return responsecontent;
            }
        }


        //public async Task<VMResponse> PostWithFilesAsync(List<string> fileNames, List<byte[]> fileBytes, string ApiEndPoint)
        //{
        //    using (var _httpclient = new HttpClient())
        //    {
        //        var formData = new MultipartFormDataContent();

        //        for (int i = 0; i < fileBytes.Count; i++)
        //        {
        //            var fileContent = new ByteArrayContent(fileBytes[i]);
        //            string name = (fileNames != null && i < fileNames.Count) ? fileNames[i] : "NoImage";
        //            formData.Add(fileContent, "files", name);
        //        }
        //        HttpResponseMessage response = await _httpclient.PostAsync(BaseUrl + ApiEndPoint, formData);
        //        string responsecontent = await response.Content.ReadAsStringAsync();
        //        VMResponse vMResponse = JsonConvert.DeserializeObject<VMResponse>(responsecontent) ?? new VMResponse();
        //        vMResponse.StatusCode = response.StatusCode;
        //        return vMResponse;
        //    }
        //}


        //public async Task<VMResponse> PuttWithFilesAsync(object Model, List<byte[]> fileBytes, List<string> fileNames, string ApiEndPoint)
        //{
        //    using (var _httpclient = new HttpClient())
        //    {
        //        var formData = new MultipartFormDataContent();

        //        // Add the JSON data
        //        string json = JsonConvert.SerializeObject(Model);
        //        var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
        //        formData.Add(jsonContent, "jsonData");

        //        // Add the file content
        //        for (int i = 0; i < fileBytes.Count; i++)
        //        {
        //            var fileContent = new ByteArrayContent(fileBytes[i]);
        //            string name = (fileNames != null && i < fileNames.Count) ? fileNames[i] : "NoImage";
        //            formData.Add(fileContent, "file", name);
        //        }

        //        //var fileContent = new ByteArrayContent(fileBytes);
        //        //string name = string.IsNullOrEmpty(fileName) ? "NoImage" : fileName;
        //        //formData.Add(fileContent, "file", name);

        //        HttpResponseMessage response = await _httpclient.PutAsync(BaseUrl + ApiEndPoint, formData);
        //        string responsecontent = await response.Content.ReadAsStringAsync();
        //        VMResponse vMResponse = JsonConvert.DeserializeObject<VMResponse>(responsecontent) ?? new VMResponse();
        //        vMResponse.StatusCode = response.StatusCode;
        //        return vMResponse;
        //    }
        //}
        public async Task<string> PuttWithFileAsync(object Model, byte[] fileBytes, string fileName, string ApiEndPoint, string Token)
        {
            using (var _httpclient = new HttpClient())
            {
                var formData = new MultipartFormDataContent();

                // Add the JSON data
                string json = JsonConvert.SerializeObject(Model);
                var jsonContent = new StringContent(json, Encoding.UTF8, "application/json");
                formData.Add(jsonContent, "jsonData");

                // Add the file content
                var fileContent = new ByteArrayContent(fileBytes);
                string name = string.IsNullOrEmpty(fileName) ? "NoImage" : fileName;
                formData.Add(fileContent, "file", name);
                _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                HttpResponseMessage response = await _httpclient.PutAsync(BaseUrl + ApiEndPoint, formData);
                string responsecontent = await response.Content.ReadAsStringAsync();
                string vMResponse = JsonConvert.DeserializeObject<string>(responsecontent) ?? "";
                return vMResponse;
            }
        }
        public async Task<string> GetByIdToken(string ApiEndPoint, string Token)
        {
            using (var _httpclient = new HttpClient())
            {
                _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                 HttpResponseMessage respone = await _httpclient.GetAsync(BaseUrl + ApiEndPoint);
                if (respone.IsSuccessStatusCode)
                {
                    string jsonresponse = await respone.Content.ReadAsStringAsync();
                    return jsonresponse;
                }
                else
                    return "";
            }
        }
        public async Task<string> GetByIdToken(string ApiEndPoint, int iD, string Token)
        {
            using (var _httpclient = new HttpClient())
            {
                _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
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
        public async Task<bool> PutAsync(object Model, string ApiEndpoint,string Token)
        {
            using (var _httpclient = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(Model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                HttpResponseMessage response = await _httpclient.PutAsync(BaseUrl + ApiEndpoint, content);
                if (!response.IsSuccessStatusCode && response.StatusCode != System.Net.HttpStatusCode.BadRequest)
                {
                    return false;
                }
                else
                {
                    string responsecontent = await response.Content.ReadAsStringAsync();
                    return true;
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






        //public async Task<VMResponse> DeleteJsonAsync(string ApiEndpoint)
        //{
        //    using (var _httpclient = new HttpClient())
        //    {
        //        HttpResponseMessage response = await _httpclient.DeleteAsync(BaseUrl + ApiEndpoint);
        //        string responsecontent = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<VMResponse>(responsecontent) ?? new VMResponse();
        //    }
        //}

        //public async Task<string> PostJsonAsync(object Model, string ApiEndPoint)
        //{
        //    using (var _httpclient = new HttpClient())
        //    {
        //        string json = JsonConvert.SerializeObject(Model);
        //        StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //        HttpResponseMessage response = await _httpclient.PostAsync(BaseUrl + ApiEndPoint, content);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string jsonresponse = await response.Content.ReadAsStringAsync();
        //            return jsonresponse;
        //        }
        //        else
        //        {
        //            _errorHandlingService.TriggerError("An error occurred: " + response.ReasonPhrase);
        //            return string.Empty;
        //        }
        // }
        //  }
        // }

    }
}
