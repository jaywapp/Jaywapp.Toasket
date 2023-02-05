using System;
using System.Xml.Linq;

namespace Jaywapp.Toasket.Interface
{
    public interface IXmlFileSerializable
    {
        XElement Serialize();
        void Serialize(XElement element);
    }

    public static class XmlFileSerializableExt
    {
        public static T Load<T>(this XElement element)
            where T : IXmlFileSerializable, new()
        {
            var target = new T();
            target.Serialize(element);

            return target;
        }

        public static bool TryGetAttributeValue(this XElement element, string attributeName, out string value)
        {
            value = element.Attribute(attributeName)?.Value?.ToString();
            return !string.IsNullOrEmpty(value);
        }

        public static bool TryGetAttributeInteger(this XElement element, string attributeName, out int value)
        {
            value = default;
            
            return TryGetAttributeValue(element, attributeName, out string str)
                && int.TryParse(str, out value);
        }

        public static bool TryGetAttributeDouble(this XElement element, string attributeName, out double value)
        {
            value = default;

            return TryGetAttributeValue(element, attributeName, out string str)
                && double.TryParse(str, out value);
        }

        public static bool TryGetAttributeEnum<TEnum>(this XElement element, string attributeName, out TEnum value)
            where TEnum : struct, IConvertible
        {
            value = default;

            return TryGetAttributeValue(element, attributeName, out string str)
                && Enum.TryParse(str, out value);
        }

        public static bool TryGetAttributeDateTime(this XElement element, string attributeName, out DateTime value)
        {
            value = default;

            return TryGetAttributeValue(element, attributeName, out string str)
                && DateTime.TryParse(str, out value);
        }
    }
}
