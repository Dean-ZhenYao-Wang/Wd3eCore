using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;

namespace Wd3eCore.Localization
{
    ///表示空<see cref="IHtmlLocalizerFactory"/>，默认情况下在禁用本地化模块时使用该空zzz。
    /// <remarks>
    /// LocalizedHtmlString的参数将是HTML编码的，而不是主字符串。因此，结果应该只包含包含格式化占位符{0…}。
    /// </remarks>
    public class NullHtmlLocalizerFactory : IHtmlLocalizerFactory
    {
        /// <inheritdocs />
        public IHtmlLocalizer Create(string baseName, string location) => NullLocalizer.Instance;

        /// <inheritdocs />
        public IHtmlLocalizer Create(Type resourceSource) => NullLocalizer.Instance;

        private class NullLocalizer : IHtmlLocalizer
        {
            private static readonly PluralizationRuleDelegate _defaultPluralRule = n => (n == 1) ? 0 : 1;

            public static NullLocalizer Instance { get; } = new NullLocalizer();

            public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
                => Enumerable.Empty<LocalizedString>();

            public LocalizedHtmlString this[string name] => new LocalizedHtmlString(name, name, true);

            public LocalizedHtmlString this[string name, params object[] arguments]
            {
                get
                {
                    var translation = name;

                    if (arguments.Length == 1 && arguments[0] is PluralizationArgument pluralArgument)
                    {
                        translation = pluralArgument.Forms[_defaultPluralRule(pluralArgument.Count)];

                        arguments = new object[pluralArgument.Arguments.Length + 1];
                        arguments[0] = pluralArgument.Count;
                        Array.Copy(pluralArgument.Arguments, 0, arguments, 1, pluralArgument.Arguments.Length);
                    }

                    return new LocalizedHtmlString(name, translation, false, arguments);
                }
            }

            public LocalizedString GetString(string name) =>
                NullStringLocalizerFactory.NullLocalizer.Instance.GetString(name);

            public LocalizedString GetString(string name, params object[] arguments) =>
                NullStringLocalizerFactory.NullLocalizer.Instance.GetString(name, arguments);

            IHtmlLocalizer IHtmlLocalizer.WithCulture(CultureInfo culture) => Instance;
        }
    }
}
