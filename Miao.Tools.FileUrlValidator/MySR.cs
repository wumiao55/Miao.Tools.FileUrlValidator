using System;
#nullable enable

namespace Miao.Tools.FileUrlValidator
{
    /// <summary>
    /// MySR
    /// </summary>
    internal static class MySR
    {
        private static System.Resources.ResourceManager? s_resourceManager;
        internal static System.Resources.ResourceManager ResourceManager => s_resourceManager ??= new System.Resources.ResourceManager(typeof(MySR));

        private static readonly bool s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out bool usingResourceKeys) && usingResourceKeys;

        private static bool UsingResourceKeys() => s_usingResourceKeys;

        internal static string GetResourceString(string resourceKey)
        {
            if (UsingResourceKeys())
            {
                return resourceKey;
            }

            string? resourceString = null;
            try
            {
                resourceString =
#if SYSTEM_PRIVATE_CORELIB || NATIVEAOT
                    InternalGetResourceString(resourceKey);
#else
                    ResourceManager.GetString(resourceKey);
#endif
            }
            catch (System.Resources.MissingManifestResourceException) { }

            return resourceString!;
        }

        public static string GetResourceString(string resourceKey, string defaultString)
        {
            string resourceString = GetResourceString(resourceKey);
            return resourceKey == resourceString || resourceString == null ? defaultString : resourceString;
        }

        internal static string Format(string resourceFormat, object? p1)
        {
            if (UsingResourceKeys())
            {
                return string.Join(", ", resourceFormat, p1);
            }

            return string.Format(resourceFormat, p1);
        }

        internal static string Format(string resourceFormat, object? p1, object? p2)
        {
            if (UsingResourceKeys())
            {
                return string.Join(", ", resourceFormat, p1, p2);
            }

            return string.Format(resourceFormat, p1, p2);
        }
        /// <summary>The field {0} is validated fail. {1}</summary>
        internal static string @FileUrlValidationAttribute_ValidationError => GetResourceString("FileUrlValidationAttribute_ValidationError", @"The field {0} is validated fail. {1}");
    }
}
