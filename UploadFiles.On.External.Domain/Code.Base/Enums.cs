using System;
using System.Collections.Generic;
using System.Text;

namespace UploadFiles.On.External.Domain.Code.Base
{

    public enum FilesType : int
    {
        Image = 1,
        PDF = 2,
        Excel = 3
    }

    public enum FeedBack : int
    {
        ImageUploaded = 11,
        ImageUploadedFail = 22,
        IsNotImage = 33,
        NullOrEmpty = 34,
        largeSize = 35
    }
    public enum ImageFormat
    {
        bmp,
        jpeg,
        gif,
        tiff,
        png,
        unknown
    }
}
