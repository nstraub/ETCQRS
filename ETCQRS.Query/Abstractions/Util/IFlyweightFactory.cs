using System.ComponentModel;

namespace ETCQRS.Query.Abstractions.Util
{
	[Localizable(false)]
	public interface IFlyweightFactory<out T> where T : class
	{
		T Get(string key);
	}
}
