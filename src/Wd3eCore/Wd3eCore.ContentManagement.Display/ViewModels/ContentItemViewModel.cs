using Wd3eCore.DisplayManagement.Views;

namespace Wd3eCore.ContentManagement.Display.ViewModels
{
    public class ContentItemViewModel : ShapeViewModel
    {
        public ContentItemViewModel()
        {
        }

        public ContentItemViewModel(ContentItem contentItem)
        {
            ContentItem = contentItem;
        }

        public ContentItem ContentItem { get; set; }
    }
}
