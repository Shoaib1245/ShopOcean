namespace Service.FrontEnd.APIs.Utility
{
    public interface IFileUploader
    {
        string savefile(IFormFile file);
    }
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _environment;
        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _environment = webHostEnvironment;
        }
        public string savefile(IFormFile file)
        {
            try
            {
                if (file != null)
                {

                    string filename = Path.Combine("https://localhost:7219/" + "ShopOcean/", file.FileName);
                    var physicalpath = Path.Combine(_environment.WebRootPath, "ShopOcean", file.FileName);
                    using (var stream = new FileStream(physicalpath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return filename;
                }
                return "anonymous.jpg";
            }
            catch (Exception ex)
            {
                return "anonymous.jpg";
            }
        }
    }
}


