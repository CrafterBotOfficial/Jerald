using System;

namespace Jerald
{
    [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AutoRegisterAttribute : Attribute;
}
