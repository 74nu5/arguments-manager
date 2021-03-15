namespace Arguments.Manager.Extensions
{
    #region Usings

    using System;
    using System.Collections.Generic;

    #endregion

    /// <summary>The extensions list. </summary>
    public static class ExtensionsIList
    {
        #region Méthodes publiques

        /// <summary>The index of. </summary>
        /// <param name="tab">The tab. </param>
        /// <param name="value">The value. </param>
        /// <returns>The <see cref="int" />. </returns>
        public static int IndexOf(this IList<string> tab, string value)
        {
            for (var i = 0; i < tab.Count; i++)
            {
                if (string.Equals(tab[i], value, StringComparison.CurrentCulture))
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion
    }
}
