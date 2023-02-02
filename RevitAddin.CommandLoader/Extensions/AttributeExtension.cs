using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Reflection
{
    /// <summary>
    /// A static class containing methods for working with custom attributes.
    /// </summary>
    public static class AttributeExtension
    {
        /// <summary>
        /// Checks if a custom attribute provider has any attributes of the specified type.
        /// </summary>
        /// <typeparam name="TCustomAttributeType">The type of attribute to check for.</typeparam>
        /// <param name="customAttributeProvider">The custom attribute provider.</param>
        /// <returns>True if the custom attribute provider has any attributes of the specified type, false otherwise.</returns>
        public static bool AnyAttribute<TCustomAttributeType>(
            this ICustomAttributeProvider customAttributeProvider)
            where TCustomAttributeType : Attribute
        {
            return GetAttribute<TCustomAttributeType>(customAttributeProvider) is not null;
        }
        /// <summary>
        /// Gets attributes of the specified type from a custom attribute provider.
        /// </summary>
        /// <typeparam name="TCustomAttributeType">The type of attribute to get.</typeparam>
        /// <param name="customAttributeProvider">The custom attribute provider.</param>
        /// <returns>The attributes of the specified type from the custom attribute provider.</returns>
        public static IEnumerable<TCustomAttributeType> GetAttributes<TCustomAttributeType>(
            this ICustomAttributeProvider customAttributeProvider)
            where TCustomAttributeType : Attribute
        {
            return customAttributeProvider
                .GetCustomAttributes(typeof(TCustomAttributeType), true)
                .OfType<TCustomAttributeType>();
        }
        /// <summary>
        /// Gets the first attribute of the specified type from a custom attribute provider.
        /// </summary>
        /// <typeparam name="TCustomAttributeType">The type of attribute to get.</typeparam>
        /// <param name="customAttributeProvider">The custom attribute provider.</param>
        /// <returns>The first attribute of the specified type from the custom attribute provider.</returns>
        public static TCustomAttributeType GetAttribute<TCustomAttributeType>(
            this ICustomAttributeProvider customAttributeProvider)
            where TCustomAttributeType : Attribute
        {
            return customAttributeProvider
                .GetAttributes<TCustomAttributeType>()
                .FirstOrDefault();
        }
        /// <summary>
        /// Tries to get the first attribute of the specified type from a custom attribute provider.
        /// </summary>
        /// <typeparam name="TCustomAttributeType">The type of attribute to get.</typeparam>
        /// <param name="customAttributeProvider">The custom attribute provider.</param>
        /// <param name="customAttributeType">The first attribute of the specified type from the custom attribute provider.</param>
        /// <returns>True if the attribute was found, false otherwise.</returns>
        public static bool TryGetAttribute<TCustomAttributeType>(
                this ICustomAttributeProvider customAttributeProvider,
                out TCustomAttributeType customAttributeType)
            where TCustomAttributeType : Attribute
        {
            customAttributeType = GetAttribute<TCustomAttributeType>(customAttributeProvider);
            return customAttributeType is not null;
        }
    }
}