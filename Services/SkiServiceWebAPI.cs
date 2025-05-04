using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SkiApp.Models;

namespace SkiApp.Services
{
    public class SkiServiceWebAPIProxy
    {
        #region without tunnel

        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        //private static string serverIP = "localhost";
        //private HttpClient client;
        //private string baseUrl;
        //public static string BaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
        //    DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110/api/" : $"http://{serverIP}:5110/api/";
        //private static string ImageBaseAddress = (DeviceInfo.Platform == DevicePlatform.Android &&
        //    DeviceInfo.DeviceType == DeviceType.Virtual) ? "http://10.0.2.2:5110" : $"http://{serverIP}:5110";

        #endregion

        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "mxw3x8zq-7171.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://mxw3x8zq-7171.euw.devtunnels.ms/api/SkiAppServerAPI/";
        public static string ImageBaseAddress = "https://mxw3x8zq-7171.euw.devtunnels.ms/";
        #endregion

        public SkiServiceWebAPIProxy()
        {
            //Set client handler to support cookies!!
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }
        public string GetImagesBaseAddress()
        {
            return SkiServiceWebAPIProxy.ImageBaseAddress;
        }

        public string GetDefaultProfilePhotoUrl()
        {
            return $"{SkiServiceWebAPIProxy.ImageBaseAddress}/profileImages/default.png";
        }

        public async Task<VisitorInfo?> UploadPostImage(string imagePath, int posterId)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}UploadPostImage?posterId={posterId}";
            try
            {
                //Create the form data
                MultipartFormDataContent form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
                form.Add(fileContent, "file", imagePath);
                //Call the server API
                HttpResponseMessage response = await client.PostAsync(url, form);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    VisitorInfo? result = JsonSerializer.Deserialize<VisitorInfo>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<VisitorInfo?> LoginAsync(VisitorInfo userInfo)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}login";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(userInfo);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    VisitorInfo? result = JsonSerializer.Deserialize<VisitorInfo>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        //This methos call the Register web API on the server and return the AppUser object with the given ID
        //or null if the call fails
        public async Task<VisitorInfo?> SignUp(VisitorInfo user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}register";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    VisitorInfo? result = JsonSerializer.Deserialize<VisitorInfo>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<ProfessionalInfo?> SignUpPro(ProfessionalInfo pro)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}registerPro";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(pro);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ProfessionalInfo? result = JsonSerializer.Deserialize<ProfessionalInfo>(resContent, options);
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<TipInfo>> GetTips()
        {
            string url = $"{this.baseUrl}getTips";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);
               
                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<TipInfo> result = JsonSerializer.Deserialize<List<TipInfo>>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<VisitorInfo>> GetAllUsers()
        {
            string url = $"{this.baseUrl}getAllUsers";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<VisitorInfo> result = JsonSerializer.Deserialize<List<VisitorInfo>>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<PostPhotoInfo>> GetAllPostPhotos()
        {
            string url = $"{this.baseUrl}getAllPostPhotos";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<PostPhotoInfo> result = JsonSerializer.Deserialize<List<PostPhotoInfo>>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<String>> GetPostPhotos(int Id)
        {
            string url = $"{this.baseUrl}GetPostImages?posterId={Id}";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<String> result = JsonSerializer.Deserialize<List<String>>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<List<ProfessionalInfo>> GetCoaches()
        {
            string url = $"{this.baseUrl}getCoaches";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<ProfessionalInfo> result = JsonSerializer.Deserialize<List<ProfessionalInfo>>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<List<TipInfo>> SortTips(int diff)
        {
            string url = $"{this.baseUrl}sortTips?diff={diff}";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    List<TipInfo> result = JsonSerializer.Deserialize<List<TipInfo>>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ProfessionalInfo?> GetPro(int Id)
        {
            string url = $"{this.baseUrl}getPro?Id={Id}";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    ProfessionalInfo? result = JsonSerializer.Deserialize<ProfessionalInfo>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<VisitorInfo?> GetUser(int Id)
        {
            string url = $"{this.baseUrl}getUser?Id={Id}";
            try
            {
                // Call the server API
                HttpResponseMessage response = await client.GetAsync(url);

                // Check status
                if (response.IsSuccessStatusCode)
                {
                    // Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();

                    // Deserialize result to List
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    VisitorInfo? result = JsonSerializer.Deserialize<VisitorInfo>(resContent, options);

                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //This method call the UpdateUser web API on the server and return true if the call was successful
        //    or false if the call fails
        public async Task<bool> UpdateUser(VisitorInfo user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}updateUser";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> UpdatePro(ProfessionalInfo user)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}updatePro";
            try
            {
                //Call the server API
                string json = JsonSerializer.Serialize(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }




    



    //This method call the check Web API and return a string with the server status

}
