using System;
using System.Collections.Generic;
using System.Text;

namespace UploadFiles.On.External.Domain.Code.Base
{
    public interface IUpLoadFile
    {
        /// <summary>
        /// This mathod upload file from user
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Folder"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        (string, FeedBack) UpLoad(string Base64, string Folder, FilesType Type);
        /// <summary>
        /// This mathod delete file form a server 
        /// </summary>
        /// <param name="OldFile"></param>
        /// <param name="Folder"></param>
        /// <param name="Type"></param>
        void Delete(string OldFile, string Folder, FilesType Type);
    }
}
