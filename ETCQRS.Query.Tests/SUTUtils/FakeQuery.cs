using System;

using ETCQRS.Query.Abstractions.Base;


namespace ETCQRS.Query.Tests.SUTUtils
{
    public class FakeQuery:IQuery
    {
        public Type ParameterType
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string PropertyName
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
