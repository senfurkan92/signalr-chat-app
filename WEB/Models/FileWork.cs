namespace WEB.Models
{
    public interface IFileWork
    {
        string UploadFile(IFormFile file, params string[] folders);
    }

    public class FileWork : IFileWork
    {
        public string UploadFile(IFormFile file, params string[] folders) 
        { 
            var uniq = Guid.NewGuid().ToString();
            var ext = Path.GetExtension(file.FileName);
            var folderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\" + string.Join("\\", folders);
            var publicPath = "/" + string.Join("/",folders) + "/" + uniq + ext;
            var filePath = Directory.GetCurrentDirectory() + "\\wwwroot" + publicPath.Replace("/", "\\");

            if(!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            using (var fs = new FileStream(filePath, FileMode.Create))
            { 
                file.CopyTo(fs);
            }

            return publicPath;
        }
    }
}
