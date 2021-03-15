namespace Arguments.Manager.TestAttributes
{
    #region Usings

    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using JetBrains.Annotations;

    #endregion

    public abstract class ArgumentTestable
    {
        #region Méthodes publiques

        [PublicAPI]
        public Dictionary<PropertyInfo, bool> CheckArguments()
        {
            var result = new Dictionary<PropertyInfo, bool>();
            foreach (var propertyInfo in this.GetType().GetProperties())
            {
                var attributes = propertyInfo.GetCustomAttributes(false);

                if (!attributes.Any(o => o is ArgumentAttribute))
                {
                    continue;
                }

                foreach (var testAttribute in attributes.Where(attribute => !(attribute is ArgumentAttribute) && attribute is ITestAttribute).Cast<ITestAttribute>())
                {
                    result.Add(propertyInfo, testAttribute.Test(propertyInfo.GetValue(this, null)));
                }
            }

            return result;
        }

        #endregion
    }
}
