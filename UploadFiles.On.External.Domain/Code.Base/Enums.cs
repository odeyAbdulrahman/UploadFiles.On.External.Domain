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
        Audio = 4,
        Json = 5,
    }
    public enum FeedBack : int
    {
        ImageUploaded = 11,
        PdfUploaded = 12,
        DocUploaded = 13,
        AudioFileUploaded = 14,
        JsonUploaded = 15,
        FileDeleted = 20,

        ImageUploadedFail = 22,
        PdfUploadedFail = 23,
        DocUploadedFail = 24,
        AudioFileUploadedFail = 25,
        JsonUploadedFail = 26,

        IsNotImage = 33,
        IsNotPdf = 36,
        IsNotDoc = 37,
        IsNotJson = 38,
        IsNotAudioFile = 39,
        IsNotFile = 40,

        NullOrEmpty = 44,
        largeSize = 45,
        NotExists = 46
    }
    public enum FileExt
    {
        jpeg,
        pdf,
        docx,
        wav,
        json
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
    public enum JsonFormat
    {
        json,
        unknown
    }
}
