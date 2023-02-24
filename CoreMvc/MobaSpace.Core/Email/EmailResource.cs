using Microsoft.Extensions.FileProviders;


namespace MobaSpace.Core.Email
{
    public class EmailResource
    {

        public readonly string ContentId;
        public readonly byte[] Content;

        public EmailResource(string contentId, IFileInfo info)
        {
            this.ContentId = contentId;
            this.Content = System.IO.File.ReadAllBytes(info.PhysicalPath);
        }
    }
}
