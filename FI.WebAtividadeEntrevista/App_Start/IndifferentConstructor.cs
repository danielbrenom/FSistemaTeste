using System;
using System.Linq;
using System.Reflection;
using SimpleInjector.Advanced;

namespace WebAtividadeEntrevista
{
    public class IndifferentConstructor : IConstructorResolutionBehavior
    {
        public ConstructorInfo GetConstructor(Type implementationType) =>
            implementationType.GetConstructors().OrderByDescending(ctor => ctor.GetParameters().Length).Last();
    }
}