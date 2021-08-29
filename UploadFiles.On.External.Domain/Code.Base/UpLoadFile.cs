using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UploadFiles.On.External.Domain.Model;

namespace UploadFiles.On.External.Domain.Code.Base
{
    public class UpLoadFile: IUpLoadFile
    {
        public UpLoadFile(IConfiguration configuration) { Configuration = configuration; }
        private readonly IConfiguration Configuration;
        private const int ImageFileLength = 1024 * 1024;
        private string FullPath;

        public (string, FeedBack) UpLoad(string Base64, string Folder, FilesType Type)
        {
            if (string.IsNullOrEmpty(Base64))
                return ("", FeedBack.NullOrEmpty);
            if (Type == FilesType.Image)
                return SaveImage(Base64, SubDomainSettings(), Folder);
            return ("", FeedBack.IsNotImage);
        }

        public void Delete(string OldFile, string Folder, FilesType Type)
        {
            if (Type == FilesType.Image)
                DeleteImage(OldFile, SubDomainSettings(), Folder);
        }

        #region Configuration funcation
        private UploadFileSettings SubDomainSettings()
        {
            var ServerSettingsSection = Configuration.GetSection("UploadFileSettings");
            return ServerSettingsSection.Get<UploadFileSettings>();
        }
        #endregion

        #region Image Helper funcation
        /// <summary>
        /// Save image file
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Root"></param>
        /// <param name="Folder"></param>
        /// <returns></returns>
        private (string, FeedBack) SaveImage(string Base64, UploadFileSettings Settings, string Folder)
        {
            string FixBase64 = Regex.Replace(Base64, @"^data:.+;base64,", string.Empty);
            byte[] Length = Convert.FromBase64String(FixBase64);
            if (Length.Length > (ImageFileLength * Settings.MaximumSize))
                return ("", FeedBack.largeSize);
            ImageFormat Format = GetImageFormat(Length);
            if (Format == ImageFormat.jpeg || Format == ImageFormat.png)
            {
                Directory.CreateDirectory(string.Format("{0}{1}", Settings.Root, Folder));
                string SaveInDataBase = string.Format("{0}{1}{2}{3}", Guid.NewGuid().ToString().Substring(0, 4), DateTime.Now.Ticks.ToString(), '.', ImageFormat.jpeg);
                FullPath = string.Format("{0}{1}{2}{3}", Settings.Root, Folder, Settings.Separator, SaveInDataBase);
                File.WriteAllBytes(FullPath, Length);
                return (SaveInDataBase, FeedBack.ImageUploaded);
            }
            else
            {
                return ("", FeedBack.IsNotImage);
            }
        }
        /// <summary>
        /// Delete image file
        /// </summary>
        /// <param name="OldFile"></param>
        /// <param name="Root"></param>
        /// <param name="Folder"></param>
        private void DeleteImage(string OldFile, UploadFileSettings Settings, string Folder)
        {
            if (!string.IsNullOrEmpty(OldFile))
            {
                var DeletedFile = string.Format("{0}{1}{2}{3}", Settings.Root, Folder, Settings.Separator, OldFile);
                if (File.Exists(DeletedFile))
                    File.Delete(DeletedFile);
            }
        }
        /// <summary>
        /// Check image extantion
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static ImageFormat GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM"); // BMP
            var gif = Encoding.ASCII.GetBytes("GIF"); // GIF
            var png = new byte[] { 137, 80, 78, 71 }; // PNG
            var tiff = new byte[] { 73, 73, 42 }; // TIFF
            var tiff2 = new byte[] { 77, 77, 42 }; // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 }; // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 }; // jpeg canon
            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormat.bmp;
            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormat.gif;
            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormat.png;
            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormat.tiff;
            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormat.tiff;
            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormat.jpeg;
            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormat.jpeg;
            return ImageFormat.unknown;
        }
        #endregion
    }
}
