using Wd3eCore.Email;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Provides an extension method for <see cref="Wd3eCoreBuilder"/>.
    /// </summary>
    public static partial class Wd3eCoreBuilderExtensions
    {
        /// <summary>
        /// Adds e-mail address validator service.
        /// </summary>
        /// <param name="builder">The <see cref="Wd3eCoreBuilder"/>.</param>
        public static Wd3eCoreBuilder AddEmailAddressValidator(this Wd3eCoreBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddTransient<IEmailAddressValidator, EmailAddressValidator>();
            });

            return builder;
        }
    }
}
