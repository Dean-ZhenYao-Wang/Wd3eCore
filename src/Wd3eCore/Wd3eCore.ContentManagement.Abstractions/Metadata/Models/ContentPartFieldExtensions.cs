using System;
using Wd3eCore.ContentManagement.Metadata.Settings;

namespace Wd3eCore.ContentManagement.Metadata.Models
{
    public static class ContentPartFieldSettingsExtensions
    {
        public static string DisplayName(this ContentPartFieldDefinition partField)
        {
            var displayName = partField.GetSettings<ContentPartFieldSettings>().DisplayName;

            if (String.IsNullOrEmpty(displayName))
            {
                displayName = partField.FieldDefinition.Name;
            }

            return displayName;
        }

        public static string Description(this ContentPartFieldDefinition partField)
        {
            return partField.GetSettings<ContentPartFieldSettings>().Description;
        }

        public static string Editor(this ContentPartFieldDefinition partField)
        {
            return partField.GetSettings<ContentPartFieldSettings>().Editor;
        }

        public static string DisplayMode(this ContentPartFieldDefinition partField)
        {
            return partField.GetSettings<ContentPartFieldSettings>().DisplayMode;
        }
    }
}
