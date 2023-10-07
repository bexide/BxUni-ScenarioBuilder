//BeXide 2022-11-29
//by MurakamiKazuki

using System.Reflection;

namespace BxUni.ScenarioBuilderInternal
{
    internal static class AttributeUtility
    {
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