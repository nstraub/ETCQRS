namespace ETCQRS.Query.Abstractions.Util
{
    public interface IFlyweightFactory<out T> where T : class
    {
        T Get (string key);
    }
}
