using System.IO;
using System.Web;

namespace NextItemBuy.Services.Utils
{
    public class CustomPostedFile : HttpPostedFileBase
    {
        private readonly byte[] fileBytes;
        private readonly string fileName;

        public CustomPostedFile(byte[] fileBytes, string fileName)
        {
            this.fileBytes = fileBytes;
            this.fileName = fileName;
        }

        public override string ContentType => MimeMapping.GetMimeMapping(fileName);

        public override int ContentLength => fileBytes.Length;

        public override string FileName => fileName;

        public override Stream InputStream => new MemoryStream(fileBytes);
    }
}
