//BeXide 2022-11-29
//by MurakamiKazuki

using System.Reflection;

namespace BxUni.ScenarioBuilderInternal
{
    internal static class AttributeUtility
    {
        /// <summary>
        /// ClassについているAttributeのデータを取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classType"></param>
        /// <returns></returns>
        internal static T GetClassAttribute<T>(System.Type classType)
            where T : System.Attribute
        {
            return classType.GetCustomAttribute<T>();
        }

        /// <summary>
        /// Classに指定のAttributeが付いているかどうか
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classType"></param>
        /// <returns></returns>
        internal static bool HasClassAttribute<T>(System.Type classType)
            where T : System.Attribute
        {
            return classType.GetCustomAttribute<T>() != null;
        }

        /// <summary>
        /// Classに指定のAttributeがついているかどうか
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="classType"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        internal static bool TryGetClassAttribute<T>(System.Type classType, out T attribute)
            where T : System.Attribute
        {
            attribute = GetClassAttribute<T>(classType);
            return attribute != null;
        }

        /// <summary>
        /// MethodについてるAttributeのデータを取得
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        internal static T GetMethodAttribute<T>(MethodInfo methodInfo)
            where T : System.Attribute
        {
            return methodInfo.GetCustomAttribute<T>();
        }

        /// <summary>
        /// Methodに指定のAttributeがついているかどうか
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        internal static bool HasMethodAttribute<T>(MethodInfo methodInfo)
            where T : System.Attribute
        {
            return GetMethodAttribute<T>(methodInfo) != null;
        }

        /// <summary>
        /// Methodに指定のAttributeがついているかどうか
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodInfo"></param>
        /// <param name="attribute">MethodについてるAttributeのデータ</param>
        /// <returns></returns>
        internal static bool TryGetMethodAttribute<T>(MethodInfo methodInfo, out T attribute)
            where T : System.Attribute
        {
            attribute = GetMethodAttribute<T>(methodInfo);
            return attribute != null;
        }
    }
}