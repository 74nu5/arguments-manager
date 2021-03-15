namespace Arguments.Manager
{
    #region Usings

    using System;
    using System.Linq;
    using System.Text;

    using Arguments.Manager.Extensions;
    using Arguments.Manager.TestAttributes;

    using JetBrains.Annotations;

    #endregion

    /// <summary>The arguments manager. </summary>
    [PublicAPI]
    public static class ArgumentsManager
    {
        #region Champs et constantes statiques

        /// <summary>The no m_ method e_ parse. </summary>
        private const string NomMethodeParse = "Parse";

        #endregion

        #region Méthodes publiques

        /// <summary>The check.</summary>
        /// <param name="str">The str.</param>
        /// <returns>The <see cref="Checker" />.</returns>
        public static Checker Check(string str)
            => new Checker(str);

        /// <summary>The init. </summary>
        /// <typeparam name="T">The t </typeparam>
        /// <param name="args">The args. </param>
        /// <returns>The <see cref="T" />. </returns>
        public static T Init<T>(string[] args)
            where T : ArgumentTestable, new()
            => Init<T>(args, false);

        /// <summary>The init. </summary>
        /// <typeparam name="T">The t </typeparam>
        /// <param name="args">The args. </param>
        /// <param name="showSummary">The afficher Resume. </param>
        /// <returns>The <see cref="T" />. </returns>
        public static T Init<T>(string[] args, bool showSummary)
            where T : ArgumentTestable, new()
        {
            var retour = new T();
            var atts =
                typeof(T).GetProperties()
                         .Select(prop => new { Property = prop, Attribute = (ArgumentAttribute)Attribute.GetCustomAttribute(prop, typeof(ArgumentAttribute)) })
                         .Where(a => a.Attribute != null);

            var summary = new StringBuilder();
            summary.Append("************************************************************\n");

            foreach (var arguments in atts)
            {
                summary.AppendFormat("\t ~ {0} ({1}) : ", arguments.Property.Name, arguments.Attribute.Key);
                var index = args.IndexOf(arguments.Attribute.Key);
                if (index < 0)
                {
                    if (arguments.Attribute.IsRequired)
                    {
                        throw new ArgumentMissingException($"L'argument {arguments.Property.Name.ToLower()} ({arguments.Attribute.Key}) obligatoire, est manquant.");
                    }

                    return retour;
                }

                var methodInfo = arguments.Property.PropertyType.GetMethod(NomMethodeParse, new[] { typeof(string) });
                object valeur = null;

                var arg = args.ElementAt(index + 1);
                if (arguments.Property.PropertyType.IsArray)
                {
                    if (arguments.Property.PropertyType == typeof(string[]))
                    {
                        valeur = arg.Split(',');
                    }
                    else
                    {
                        var strings = arg.Split(',');
                        var construct = arguments.Property.PropertyType.GetConstructor(new[] { typeof(int) });
                        if (construct != null)
                        {
                            var tab = construct.Invoke(new object[] { strings.Length }) as Array;
                            for (var i = 0; i < strings.Length; i++)
                            {
                                tab?.SetValue(strings[i], i);
                            }
                        }
                    }
                }
                else if (methodInfo != null)
                {
                    try
                    {
                        valeur = methodInfo.Invoke(null, new object[] { arg });
                    }
                    catch (Exception e)
                    {
                        if (e.InnerException != null)
                        {
                            throw e.InnerException;
                        }

                        throw;
                    }
                }
                else if (arguments.Property.PropertyType == typeof(string))
                {
                    valeur = arg;
                }
                else
                {
                    var construct = arguments.Property.PropertyType.GetConstructor(new[] { typeof(string) });
                    if (construct != null)
                    {
                        try
                        {
                            valeur = construct.Invoke(new object[] { arg });
                        }
                        catch (Exception e)
                        {
                            if (e.InnerException != null)
                            {
                                throw e.InnerException;
                            }

                            throw;
                        }
                    }
                }

                summary.AppendFormat("{0}\n", valeur);
                arguments.Property.SetValue(retour, valeur, null);
            }

            summary.Append("************************************************************\n");

            if (showSummary)
            {
                Console.Write(summary.ToString());
            }

            return retour;
        }

        #endregion
    }
}
