namespace Arguments.Manager.TestAttributes
{
    #region Usings

    using System;

    #endregion

    public sealed class NotNullAttribute : Attribute, ITestAttribute
    {
        #region Méthodes publiques

        public bool Test<T>(T value)
            => value != null;

        #endregion
    }
}
