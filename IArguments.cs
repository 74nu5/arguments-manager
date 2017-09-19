namespace Arguments.Manager
{
    using System.Collections.Generic;
    using System.Reflection;

    public interface IArguments
    {
        Dictionary<PropertyInfo, bool> CheckArguments();
    }
}
