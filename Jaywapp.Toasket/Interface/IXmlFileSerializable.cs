using System;
using System.Xml.Linq;

namespace Jaywapp.Toasket.Interface
{
    public interface IXmlFileSerializable
    {
        /// <summary>
        /// <see cref="XElement"/> 객체로 동기화합니다.
        /// </summary>
        /// <returns></returns>
        XElement Serialize();
        /// <summary>
        /// <see cref="XElement"/> 객체를 읽어 동기화합니다.
        /// </summary>
        /// <param name="element"></param>
        void Serialize(XElement element);
    }

    public static class XmlFileSerializableExt
    {
        /// <summary>
        /// <paramref name="element"/>을 읽어 <typeparamref name="T"/>객체를 생성합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T Load<T>(this XElement element)
            where T : IXmlFileSerializable, new()
        {
            var target = new T();
            target.Serialize(element);

            return target;
        }

        /// <summary>
        /// <paramref name="element"/>에서 <paramref name="attributeName"/> 이름의  Attribute를 수집 시도합니다.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetAttributeValue(this XElement element, string attributeName, out string value)
        {
            value = element.Attribute(attributeName)?.Value?.ToString();
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// <paramref name="element"/>에서 <paramref name="attributeName"/> 이름의  Attribute를 <see cref="int"/>로 수집 시도합니다.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetAttributeInteger(this XElement element, string attributeName, out int value)
        {
            value = default;
            
            return TryGetAttributeValue(element, attributeName, out string str)
                && int.TryParse(str, out value);
        }

        /// <summary>
        /// <paramref name="element"/>에서 <paramref name="attributeName"/> 이름의  Attribute를 <see cref="double"/>로 수집 시도합니다.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetAttributeDouble(this XElement element, string attributeName, out double value)
        {
            value = default;

            return TryGetAttributeValue(element, attributeName, out string str)
                && double.TryParse(str, out value);
        }

        /// <summary>
        /// <paramref name="element"/>에서 <paramref name="attributeName"/> 이름의  Attribute를 <typeparamref name="TEnum"/>로 수집 시도합니다.
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetAttributeEnum<TEnum>(this XElement element, string attributeName, out TEnum value)
            where TEnum : struct, IConvertible
        {
            value = default;

            return TryGetAttributeValue(element, attributeName, out string str)
                && Enum.TryParse(str, out value);
        }

        /// <summary>
        /// <paramref name="element"/>에서 <paramref name="attributeName"/> 이름의  Attribute를 <see cref="DateTime"/>로 수집 시도합니다.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetAttributeDateTime(this XElement element, string attributeName, out DateTime value)
        {
            value = default;

            return TryGetAttributeValue(element, attributeName, out string str)
                && DateTime.TryParse(str, out value);
        }
    }
}
