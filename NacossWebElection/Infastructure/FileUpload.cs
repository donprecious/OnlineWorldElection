using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Web.Mvc;
using System.Drawing;

namespace Infastructure
{
    public class FileUpload
    {
        public static string ReturnMessage;
        public static string filePath;
        public bool UploadAvatar(string UserID, string Base64String)
        {
            if (string.IsNullOrEmpty(Base64String))
            {
                ReturnMessage = "Profile picture upload failed. No data from photo!";
                return false;
            }
      
            string filename = HttpContext.Current.Server.MapPath($"~/Avatars/{UserID}.jpg");
            filePath = $"~/Avatars/{UserID}.jpg";
          
            if (Base64ImageToFile(Base64String, filename, System.Drawing.Imaging.ImageFormat.Jpeg))
            {
                ReturnMessage = "Profile picture uploaded successfully!";
                return true;
            }

            ReturnMessage = "Failure uploading your profile picture!";
            return false;
        }

        public bool Base64ImageToFile(string Base64String, string SaveAs, System.Drawing.Imaging.ImageFormat Format)
        {
            try
            {
                //Remove this part "data:image/jpeg;base64,"
                Base64String = Base64String.Split(',')[1];
                byte[] bytes = Convert.FromBase64String(Base64String);

                Image image;
                using (var ms = new MemoryStream(bytes))
                {
                    image = Image.FromStream(ms);
                }
                image.Save(SaveAs, Format);
                return true;
            }
            catch (Exception ex)
            {
                ReturnMessage = ex.Message;
                return false;
            }
        }

        public static bool uploadToNet(string base64String, string folder)
        {
            Account account = new Account()
            {
                ApiKey = "849621861927721",
                ApiSecret = "0ofuuFUGk_6zt4lmaXTsXayy07k",
                Cloud = "votel"
            };

            Cloudinary cloud = new Cloudinary(account);

            var uploadParam = new ImageUploadParams()
            {
                File = new FileDescription(base64String),
                Folder = folder
            };
            var uploadResult = cloud.Upload(uploadParam);
            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                filePath = uploadResult.Uri.ToString();
                return true;
            }
            return false;
        }

    }
}
