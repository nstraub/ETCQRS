using System.Linq;
using System.Reflection;
using AutoMapper.QueryableExtensions;

namespace ETCQRS.Query.Factories
{
	public static class MethodInfos
	{
		internal static readonly MethodInfo Select;
		internal static readonly MethodInfo Where;
		internal static readonly MethodInfo SelectMany;
		internal static readonly MethodInfo OfType;
		internal static readonly MethodInfo Single;
		internal static readonly MethodInfo Project;

		static MethodInfos()
		{
			var methodInfos = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public);

			Select = methodInfos.First(m => m.Name == "Select" && m.GetParameters().Length == 2);
			SelectMany = methodInfos.First(m => m.Name == "SelectMany" && m.GetParameters().Length == 2);
			Where = methodInfos.First(m => m.Name == "Where" && m.GetParameters().Length == 2);
			Single = methodInfos.First(m => m.Name == "Single" && m.GetParameters().Length == 2);
			OfType = methodInfos.First(m => m.Name == "OfType" && m.GetParameters().Length == 1);
			Project = typeof(AutoMapper.QueryableExtensions.Extensions)
				.GetMethods(BindingFlags.Static | BindingFlags.Public)
				.Single(q => q.Name == "ProjectTo" && q.GetParameters().Length == 3 && q.GetParameters()[1].Name == "configuration");
		}
	}
}
