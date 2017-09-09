using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Factories;

using NUnit.Framework;
using AutoMapper.QueryableExtensions;
using TestFramework.NUnit.Ninject.Moq;

namespace ETCQRS.Query.Tests.Util.MutatorFlyweightFactorySpec

{
	[TestFixture]
	public class Method_Get : TestsFor<MutatorFlyweightFactory>
	{
		[Test]
		public void IT_SHOULD_RETURN_THE_CORRESPONDING_VALUE_WHEN_A_VALID_KEY_IS_PASSED()
		{
			var value = Subject.Get("Null");
			Assert.That(value, Is.InstanceOf<NullMutator>());
		}

		[Test]
		public void IT_SHOULD_THROW_A_KEY_NOT_FOUND_EXCEPTION_WHICH_SPECIFIES_GIVEN_KEY_IF_NON_EXISTENT_KEY_IS_PASSED()
		{
			Assert.That(() => Subject.Get("invalid"), Throws.Exception.TypeOf<KeyNotFoundException>().With.Message.EqualTo("The given key, \"invalid\" is not present in the dictionary.").And.InnerException.InstanceOf<KeyNotFoundException>());
		}
	}
}
