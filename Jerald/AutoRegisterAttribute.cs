using System;

namespace Jerald
{
    [System.AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class AutoRegisterAttribute : Attribute
    { }
}
