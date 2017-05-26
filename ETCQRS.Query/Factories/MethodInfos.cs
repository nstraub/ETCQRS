using System.Linq;
using System.Reflection;


namespace ETCQRS.Query.Factories
{
    internal static class MethodInfos
    {
        internal static readonly MethodInfo Select;
        internal static readonly MethodInfo Where;
        internal static readonly MethodInfo SelectMany;
        internal static readonly MethodInfo OfType;

        static MethodInfos ()
        {
            var methodInfos = typeof(Queryable).GetMethods(BindingFlags.Static|BindingFlags.Public);

            Select = methodInfos.First(m => m.Name == "Select" && m.GetParameters().Length == 2);
            SelectMany = methodInfos.First(m => m.Name == "SelectMany" && m.GetParameters().Length == 2);
            Where = methodInfos.First(m => m.Name == "Where" && m.GetParameters().Length == 2);
            OfType = methodInfos.First(m => m.Name == "OfType" && m.GetParameters().Length == 1);
        }
    }
}
