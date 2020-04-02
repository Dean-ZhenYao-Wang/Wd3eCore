using Wd3eCore.Entities;

namespace Wd3eCore.ContentManagement
{
    public class DefaultContentItemIdGenerator : IContentItemIdGenerator
    {
        private readonly IIdGenerator _generator;

        public DefaultContentItemIdGenerator(IIdGenerator generator)
        {
            _generator = generator;
        }

        public string GenerateUniqueId(ContentItem contentItem)
        {
            return _generator.GenerateUniqueId();
        }
    }
}
