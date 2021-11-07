using System;
using System.Collections.Generic;
using System.Text;

namespace UploadFiles.On.External.Domain.Code.Base
{

    public enum FilesType : int
    {
        Image = 1,
        PDF = 2,
        Doc = 3,
        Audio = 4
    }
    public enum FeedBack : int
    {
        ImageUploaded = 11,
        PdfUploaded = 12,
        DocUploaded = 13,
        FileDeleted = 14,

        ImageUploadedFail = 22,
        PdfUploadedFail = 23,
        DocUploadedFail = 24,

        IsNotImage = 33,
        IsNotPdf = 36,
        IsNotDoc = 37,
        IsNotFile = 38,

        NullOrEmpty = 44,
        largeSize = 45,
        NotExists = 46
    }
    public enum FileExt
    {
        jpeg,
        pdf,
        docx,
        wav
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
    public enum PdfFormat
    {
        Pdf,
        unknown
    }
    public enum DocFormat
    {
        Doc,
        Docx,
        unknown
    }
    public enum AudioFormat
    {
        wmv,
        wav,
        mp3,
        mp4,
        unknown
    }
}
