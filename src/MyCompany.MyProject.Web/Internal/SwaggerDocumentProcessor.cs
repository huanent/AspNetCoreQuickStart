using System.Threading.Tasks;
using NSwag.SwaggerGeneration.Processors;
using NSwag.SwaggerGeneration.Processors.Contexts;

namespace MyCompany.MyProject.Web.Internal
{
    public class SwaggerDocumentProcessor : IDocumentProcessor
    {
        public Task ProcessAsync(DocumentProcessorContext context)
        {
            context.Document.Info.Title = Constants.AppName;
            return Task.CompletedTask;
        }
    }
}
