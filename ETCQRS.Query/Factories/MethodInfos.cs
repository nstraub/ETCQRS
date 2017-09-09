using System.Linq;
using System.Reflection;

namespace ETCQRS.Query.Factories
{
	public static class MethodInfos
	{
		public static readonly MethodInfo Where;
		public static readonly MethodInfo SelectMany;
		public static readonly MethodInfo Select;
		public static readonly MethodInfo OfType;
		public static readonly MethodInfo Single;
		public static readonly MethodInfo Project;

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
