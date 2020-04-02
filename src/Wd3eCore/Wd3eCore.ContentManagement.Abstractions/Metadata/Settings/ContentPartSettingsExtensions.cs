using System;
using Wd3eCore.ContentManagement.Metadata.Builders;
using Wd3eCore.ContentManagement.Metadata.Models;
using Wd3eCore.ContentManagement.Utilities;

namespace Wd3eCore.ContentManagement.Metadata.Settings
{
    public static class ContentPartSettingsExtensions
    {
        public static ContentPartDefinitionBuilder Attachable(this ContentPartDefinitionBuilder builder, bool attachable = true)
        {
            return builder.MergeSettings<ContentPartSettings>(x => x.Attachable = attachable);
        }

        public static bool IsAttachable(this ContentPartDefinition part)
        {
            return part.GetSettings<ContentPartSettings>().Attachable;
        }

        public static ContentPartDefinitionBuilder Reusable(this ContentPartDefinitionBuilder builder, bool reusable = true)
        {
            return builder.MergeSettings<ContentPartSettings>(x => x.Reusable = reusable);
        }

        public static bool IsReusable(this ContentPartDefinition part)
        {
            return part.GetSettings<ContentPartSettings>().Reusable;
        }

        public static ContentPartDefinitionBuilder WithDescription(this ContentPartDefinitionBuilder builder, string description)
        {
            return builder.MergeSettings<ContentPartSettings>(x => x.Description = description);
        }

        public static ContentPartDefinitionBuilder WithDisplayName(this ContentPartDefinitionBuilder builder, string description)
        {
            return builder.MergeSettings<ContentPartSettings>(x => x.DisplayName = description);
        }

        public static ContentPartDefinitionBuilder WithDefaultPosition(this ContentPartDefinitionBuilder builder, string position)
        {
            return builder.MergeSettings<ContentPartSettings>(x => x.DefaultPosition = position);
        }

        public static string DefaultPosition(this ContentPartDefinition part)
        {
            return part.GetSettings<ContentPartSettings>().DefaultPosition;
        }

        public static string Description(this ContentPartDefinition part)
        {
            return part.GetSettings<ContentPartSettings>().Description;
        }

        public static string DisplayName(this ContentPartDefinition part)
        {
            var displayName = part.GetSettings<ContentPartSettings>().DisplayName;

            if (String.IsNullOrEmpty(displayName))
            {
                displayName = part.Name.TrimEnd("Part");
            }

            return displayName;
        }
    }
}
