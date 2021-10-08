using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UploadFiles.On.External.Domain.Model;

namespace UploadFiles.On.External.Domain.Code.Base
{
    public class UpLoadFile : IUpLoadFile
    {
        public UpLoadFile(IConfiguration configuration) { Configuration = configuration; }
        private readonly IConfiguration Configuration;
        private const int ImageFileLength = 1024 * 1024;
        private string FullPath;
        private string FullUrl;
        /// <summary>
        /// This function for upload files has following types(pdf, image, document).
        /// The responce data from this funcation is feedback and name of file with full path.
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Folder"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public (string, FeedBack) UpLoad(string Base64, string Folder, FilesType Type)
        {
            if (string.IsNullOrEmpty(Base64))
                return ("", FeedBack.NullOrEmpty);
            if (Type == FilesType.Image)
                return SaveImage(Base64, Folder);
            if (Type == FilesType.Doc)
                return SaveDoc(Base64, Folder);
            if (Type == FilesType.PDF)
                return SavePDF(Base64, Folder);
            return ("", FeedBack.IsNotFile);
        }
        /// <summary>
        /// This function for upload files has following types(pdf, image, document)
        /// The responce data from this funcation is feedback and name of file only
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Folder"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public (string, FeedBack) UpLoad2(string Base64, string Folder, FilesType Type)
        {
            if (string.IsNullOrEmpty(Base64))
                return ("", FeedBack.NullOrEmpty);
            if (Type == FilesType.Image)
                return SaveImage2(Base64, Folder);
            if (Type == FilesType.Doc)
                return SaveDoc2(Base64, Folder);
            if (Type == FilesType.PDF)
                return SavePDF2(Base64, Folder);
            return ("", FeedBack.IsNotFile);
        }
        /// <summary>
        /// This function for delete file from specific folder.
        /// The responce of this funcation in feedback only
        /// </summary>
        /// <param name="OldFile"></param>
        /// <param name="Folder"></param>
        /// <returns></returns>
        public FeedBack Delete(string OldFile, string Folder)
        {
            UploadFileSettings Settings = SubDomainSettings();
            if (!string.IsNullOrEmpty(OldFile))
            {
                var DeletedFile = string.Format("{0}{1}{2}{3}", Settings.Root, Folder, Settings.Separator, OldFile);
                if (File.Exists(DeletedFile))
                {
                    File.Delete(DeletedFile);
                    return FeedBack.FileDeleted;
                }
                else
                {
                    return FeedBack.NotExists;
                }
            }
            else
            {
                return FeedBack.NullOrEmpty;
            }
        }

        #region Configuration funcation
        /// <summary>
        /// this func for get settings data from appsettings.json
        /// </summary>
        /// <returns></returns>
        private UploadFileSettings SubDomainSettings()
        {
            var ServerSettingsSection = Configuration.GetSection("UploadFileSettings");
            return ServerSettingsSection.Get<UploadFileSettings>();
        }
        #endregion

        #region Helper funcation
        /// <summary>
        /// This func for save image file
        /// Returns from this function are the name of image with full path
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Root"></param>
        /// <param name="Folder"></param>
        /// <returns></returns>
        private (string, FeedBack) SaveImage(string Base64, string Folder)
        {
            UploadFileSettings Settings = SubDomainSettings();
            string FixBase64 = Regex.Replace(Base64, @"^data:.+;base64,", string.Empty);
            byte[] Length = Convert.FromBase64String(FixBase64);
            if (Length.Length > (ImageFileLength * Settings.MaximumSize))
                return ("", FeedBack.largeSize);
            ImageFormat ImageFormat = GetImageFormat(Length);
            if (ImageFormat == ImageFormat.jpeg || ImageFormat == ImageFormat.png)
                return (Save(ref Length, ref Folder, ref Settings, FileExt.jpeg), FeedBack.ImageUploaded);
            return ("", FeedBack.IsNotImage);
        }
        /// <summary>
        /// This func for save image file
        /// Returns from this function are the name of image only
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Folder"></param>
        /// <returns></returns>
        private (string, FeedBack) SaveImage2(string Base64, string Folder)
        {
            UploadFileSettings Settings = SubDomainSettings();
            string FixBase64 = Regex.Replace(Base64, @"^data:.+;base64,", string.Empty);
            byte[] Length = Convert.FromBase64String(FixBase64);
            if (Length.Length > (ImageFileLength * Settings.MaximumSize))
                return ("", FeedBack.largeSize);
            ImageFormat ImageFormat = GetImageFormat(Length);
            if (ImageFormat == ImageFormat.jpeg || ImageFormat == ImageFormat.png)
                return (Save2(ref Length, ref Folder, ref Settings, FileExt.jpeg), FeedBack.ImageUploaded);
            return ("", FeedBack.IsNotImage);
        }
        /// <summary>
        /// This func for save pdf file
        /// Returns from this function are the name of pdf with full path
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Folder"></param>
        /// <returns></returns>
        private (string, FeedBack) SavePDF(string Base64, string Folder)
        {
            UploadFileSettings Settings = SubDomainSettings();
            string FixBase64 = Regex.Replace(Base64, @"^data:.+;base64,", string.Empty);
            byte[] Length = Convert.FromBase64String(FixBase64);
            if (Length.Length > (ImageFileLength * Settings.MaximumSize))
                return ("", FeedBack.largeSize);
            PdfFormat PdfFormat = GetPdfFormat(Length);
            if (PdfFormat == PdfFormat.Pdf)
                return (Save(ref Length, ref Folder, ref Settings, FileExt.pdf), FeedBack.PdfUploaded);
            return ("", FeedBack.IsNotPdf);
        }
        /// <summary>
        /// This func for save pdf file
        /// Returns from this function are the name of pdf only
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Folder"></param>
        /// <returns></returns>
        private (string, FeedBack) SavePDF2(string Base64, string Folder)
        {
            UploadFileSettings Settings = SubDomainSettings();
            string FixBase64 = Regex.Replace(Base64, @"^data:.+;base64,", string.Empty);
            byte[] Length = Convert.FromBase64String(FixBase64);
            if (Length.Length > (ImageFileLength * Settings.MaximumSize))
                return ("", FeedBack.largeSize);
            PdfFormat PdfFormat = GetPdfFormat(Length);
            if (PdfFormat == PdfFormat.Pdf)
                return (Save2(ref Length, ref Folder, ref Settings, FileExt.pdf), FeedBack.PdfUploaded);
            return ("", FeedBack.IsNotPdf);
        }
        /// <summary>
        /// This func for save pdf file
        /// Returns from this function are the name of documant with full path
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Folder"></param>
        /// <returns></returns>
        private (string, FeedBack) SaveDoc(string Base64, string Folder)
        {
            UploadFileSettings Settings = SubDomainSettings();
            string FixBase64 = Regex.Replace(Base64, @"^data:.+;base64,", string.Empty);
            byte[] Length = Convert.FromBase64String(FixBase64);
            if (Length.Length > (ImageFileLength * Settings.MaximumSize))
                return ("", FeedBack.largeSize);
            DocFormat ExcelFormat = GetDocFormat(Length);
            if (ExcelFormat == DocFormat.Doc)
                return (Save(ref Length, ref Folder, ref Settings, FileExt.docx), FeedBack.DocUploaded);
            return ("", FeedBack.IsNotDoc);
        }
        /// <summary>
        /// This func for save pdf file
        /// Returns from this function are the name of documant only
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Folder"></param>
        /// <returns></returns>
        private (string, FeedBack) SaveDoc2(string Base64, string Folder)
        {
            UploadFileSettings Settings = SubDomainSettings();
            string FixBase64 = Regex.Replace(Base64, @"^data:.+;base64,", string.Empty);
            byte[] Length = Convert.FromBase64String(FixBase64);
            if (Length.Length > (ImageFileLength * Settings.MaximumSize))
                return ("", FeedBack.largeSize);
            DocFormat ExcelFormat = GetDocFormat(Length);
            if (ExcelFormat == DocFormat.Doc)
                return (Save2(ref Length, ref Folder, ref Settings, FileExt.docx), FeedBack.DocUploaded);
            return ("", FeedBack.IsNotDoc);
        }
        /// <summary>
        /// htis is base funcation for save file
        /// </summary>
        /// <param name="length"></param>
        /// <param name="folder"></param>
        /// <param name="settings"></param>
        /// <returns></returns>
        private string Save(ref byte[] length, ref string folder, ref UploadFileSettings settings, FileExt ext)
        {
            Directory.CreateDirectory(string.Format("{0}{1}", settings.Root, folder));
            string SaveInDataBase = string.Format("{0}{1}{2}{3}", Guid.NewGuid().ToString().Substring(0, 4), DateTime.Now.Ticks.ToString(), '.', ext);
            FullPath = string.Format("{0}{1}{2}{3}", settings.Root, folder, settings.Separator, SaveInDataBase);
            FullUrl = string.Format("{0}{1}{2}{3}{4}", settings.Domain, '/', folder, '/', SaveInDataBase);
            File.WriteAllBytes(FullPath, length);
            return FullUrl;
        }
        /// <summary>
        /// htis is base funcation for save file
        /// </summary>
        /// <param name="length"></param>
        /// <param name="folder"></param>
        /// <param name="settings"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        private string Save2(ref byte[] length, ref string folder, ref UploadFileSettings settings, FileExt ext)
        {
            Directory.CreateDirectory(string.Format("{0}{1}", settings.Root, folder));
            string SaveInDataBase = string.Format("{0}{1}{2}{3}", Guid.NewGuid().ToString().Substring(0, 4), DateTime.Now.Ticks.ToString(), '.', ext);
            FullPath = string.Format("{0}{1}{2}{3}", settings.Root, folder, settings.Separator, SaveInDataBase);
            File.WriteAllBytes(FullPath, length);
            return SaveInDataBase;
        }
        #endregion

        #region Validators funs
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static PdfFormat GetPdfFormat(byte[] bytes)
        {

            var Pdf = new byte[] { 37, 80, 68, 70, 45, 49, 46 };
            if (Pdf.SequenceEqual(bytes.Take(Pdf.Length)))
                return PdfFormat.Pdf;
            return PdfFormat.unknown;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static DocFormat GetDocFormat(byte[] bytes)
        {
            var DOC = new byte[] { 208, 207, 17, 224, 161, 177, 26, 225 };
            var DOCX = new byte[] { 80, 75, 3, 4 };
            if (DOC.SequenceEqual(bytes.Take(DOC.Length)) || DOCX.SequenceEqual(bytes.Take(DOCX.Length)))
                return DocFormat.Doc;
            return DocFormat.unknown;
        }
        #endregion
    }
}
