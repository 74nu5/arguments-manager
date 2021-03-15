namespace Arguments.Manager
{
    #region Usings

    using System.Collections.Generic;
    using System.Reflection;

    using JetBrains.Annotations;

    #endregion

    [PublicAPI]
    public interface IArguments
    {
        #region Méthodes publiques

        Dictionary<PropertyInfo, bool> CheckArguments();

        #endregion
    }
}
