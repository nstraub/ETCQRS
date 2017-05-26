using ETCQRS.Query.Abstractions.Base;
using ETCQRS.Query.Abstractions.Builder;
using ETCQRS.Query.Abstractions.Util;
using ETCQRS.Query.Builder;
using ETCQRS.Query.ExpressionOperatorMutator;
using ETCQRS.Query.Factories;

using Ninject.MockingKernel;
using Ninject.Modules;


namespace ETCQRS.Query.Tests.SUTUtils.NinjectModules
{
    public class BaseObjectGraph : NinjectModule
    {
        public override void Load() {
            Bind<QueryExpressionBuilder>().ToMock().InSingletonScope();

            Bind<IFlyweightFactory<IExpressionMutator>>().To<MutatorFlyweightFactory>().InSingletonScope();
            Bind<MutatorFlyweightFactory>().ToMock().InSingletonScope();

            Bind<IFlyweightFactory<IQuery>>().To<QueryFlyweightFactory>().InSingletonScope();
            Bind<QueryFlyweightFactory>().ToMock().InSingletonScope();

            Bind<MutatorObserver>().ToSelf().InSingletonScope();

            Bind<QueryDescriptorFactory>().ToSelf().InSingletonScope();

            Bind<IQueryDescriptor>().ToMock().InSingletonScope();
            Bind<IExpressionMutator>().ToMock().InSingletonScope();

            Bind<GenericMutatorTemplate>().ToMock().InSingletonScope();
            Bind<GreaterThanOrEqualMutator>().ToSelf().InSingletonScope();
            Bind<LessThanOrEqualMutator>().ToSelf().InSingletonScope();
            Bind<ThrowingMutator>().ToSelf().InSingletonScope();
        }
    }
}

