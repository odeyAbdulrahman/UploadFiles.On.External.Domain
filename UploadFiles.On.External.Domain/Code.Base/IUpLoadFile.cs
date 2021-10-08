using System;
using System.Collections.Generic;
using System.Text;

namespace UploadFiles.On.External.Domain.Code.Base
{
    public interface IUpLoadFile
    {
        /// <summary>
        /// This function for upload files has following types(pdf, image, document).
        /// The responce data from this funcation is feedback and name of file with full path.
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Folder"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        (string, FeedBack) UpLoad(string Base64, string Folder, FilesType Type);
        /// <summary>
        /// This function for upload files has following types(pdf, image, document)
        /// The responce data from this funcation is feedback and name of file only
        /// </summary>
        /// <param name="Base64"></param>
        /// <param name="Folder"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        (string, FeedBack) UpLoad2(string Base64, string Folder, FilesType Type);
        /// <summary>
        /// This function for delete file from specific folder.
        /// The responce of this funcation in feedback only
        /// </summary>
        /// <param name="OldFile"></param>
        /// <param name="Folder"></param>
        FeedBack Delete(string OldFile, string Folder);
    }
}
