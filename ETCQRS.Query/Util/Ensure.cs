using System;

using JetBrains.Annotations;


namespace ETCQRS.Query.Util
{
    internal static class Ensure
    {
        [AssertionMethod]
        internal static void ValidOperation (bool predicate, string message)
        {
            if (!predicate)
            {
                throw new InvalidOperationException(message);
            }
        }

        [AssertionMethod]
        public static void IsNotNull<T> (T predicate, string message)
        {
            if (predicate == null)
            {
                throw new NullReferenceException(message);
            }
        }
    }
}
