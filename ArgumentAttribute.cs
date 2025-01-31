﻿namespace Arguments.Manager
{
    #region Usings

    using JetBrains.Annotations;

    #region Usings

    using System;

    #endregion

    #endregion

    /// <summary>The argument attribute. </summary>
    [PublicAPI]
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ArgumentAttribute : Attribute
    {
        #region Constructeurs et destructeurs

        /// <summary>Initializes a new instance of the <see cref="ArgumentAttribute" /> class.</summary>
        /// <param name="key">The key.</param>
        /// <param name="isRequired">The is Required.</param>
        public ArgumentAttribute(string key, bool isRequired = false)
        {
            this.Key = key;
            this.IsRequired = isRequired;
        }

        #endregion

        #region Propriétés et indexeurs

        /// <summary>Gets a value indicating whether is required.</summary>
        /// <value>The is required.</value>
        public bool IsRequired { get; }

        /// <summary>Gets the key.</summary>
        /// <value>The key.</value>
        public string Key { get; }

        #endregion
    }
}
