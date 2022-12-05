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
    }
}