using Wd3eCore.DisplayManagement.Views;

namespace Wd3eCore.ContentManagement.Display.ViewModels
{
    public class ContentPartViewModel : ShapeViewModel
    {
        public ContentPartViewModel()
        {
        }

        public ContentPartViewModel(ContentPart contentPart)
        {
            ContentPart = contentPart;
        }

        public ContentPart ContentPart { get; set; }
    }
}
