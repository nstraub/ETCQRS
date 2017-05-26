﻿using System.Linq.Expressions;

using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Builder;

using Moq;

using NUnit.Framework;


namespace ETCQRS.Query.Tests.Builder.QueryBuildDirectorSpec

{
    [TestFixture]
    public class Property_Or__Getter
    {
        [Test]
        public void IT_SHOULD_SET_BULDER_QUERY_LINKER_TO_OR_ELSE_EXPRESSION ()
        {
            var builderMock = new Mock<IQueryExpressionBuilder>(MockBehavior.Strict);
            var director = new QueryBuildFacade(new Mock<IQueryDescriptorFactory>().Object, builderMock.Object, new QueryComposite());

            builderMock.SetupSet(b => b.QueryLinker = Expression.OrElse);
            // ReSharper disable once UnusedVariable
            var queryBuildDirector = director.Or;
        }
    }
}

