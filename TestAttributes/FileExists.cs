namespace Arguments.Manager.TestAttributes
{
    #region Usings

    using System;
    using System.IO;

    using JetBrains.Annotations;

    #endregion

    [PublicAPI]
    public sealed class FileExists : Attribute, ITestAttribute
    {
        #region Méthodes publiques

        /// <inheritdoc />
        public bool Test<T>([NotNull] T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }


            switch (value)
            {
                case FileInfo fi:
                    return fi.Exists;
                case string filePath:
                    if (!File.Exists(filePath))
                    {
                        throw new FileNotFoundException("Le fichier spécifié est introuvable");
                    }

                    return new FileInfo(filePath).Exists;
                default:
                    return false;
            }
        }

        #endregion
    }
}
