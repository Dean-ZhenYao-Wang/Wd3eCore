using System;

namespace Wd3eCore.ContentManagement
{
    public abstract class ContentPartOptionBase
    {
        public ContentPartOptionBase(Type contentPartType)
        {
            if (contentPartType == null)
            {
                throw new ArgumentNullException(nameof(contentPartType));
            }

            Type = contentPartType;
        }

        public Type Type { get; }
    }
}
