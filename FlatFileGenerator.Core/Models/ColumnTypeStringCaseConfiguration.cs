// <copyright file="ColumnTypeStringCaseConfiguration.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Describes <see cref="ColumnType.StringType"/> column case configuration.
    /// </summary>
    internal enum ColumnTypeStringCaseConfiguration
    {
        /// <summary>
        /// Upper Case
        /// </summary>
        [Display(Name = " ")]
        Upper,

        /// <summary>
        /// Lower case
        /// </summary>
        [Display(Name = " ")]
        Lower,

        /// <summary>
        /// Title case
        /// </summary>
        [Display(Name = " ")]
        Title,

        /// <summary>
        /// Sentence case
        /// </summary>
        [Display(Name = " ")]
        Sentence,

        /// <summary>
        /// Camel case
        /// </summary>
        [Display(Name = "")]
        Camel,

        /// <summary>
        /// Pascal case
        /// </summary>
        [Display(Name = "")]
        Pascal,

        /// <summary>
        /// Snake case
        /// </summary>
        [Display(Name = "_")]
        Snake,

        /// <summary>
        /// Kebab case
        /// </summary>
        [Display(Name = "-")]
        Kebab,
    }
}
