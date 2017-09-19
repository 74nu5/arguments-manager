namespace Arguments.Manager.TestAttributes
{
    public interface ITestAttribute
    {
        #region Méthodes publiques

        bool Test<T>(T value);

        #endregion
    }
}
