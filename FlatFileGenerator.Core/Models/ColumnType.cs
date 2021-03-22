// <copyright file="ColumnType.cs" company="KJDS Srinivasa Rao">
// Copyright (c) KJDS Srinivasa Rao. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace FlatFileGenerator.Core.Models
{
    /// <summary>
    /// Describes Column Types.
    /// </summary>
    internal enum ColumnType
    {
        /// <summary>
        /// String type
        /// </summary>
        [Display(Name = "string")]
        StringType,

        /// <summary>
        /// Date type
        /// </summary>
        [Display(Name = "date")]
        DateType,

        /// <summary>
        /// Integer type
        /// </summary>
        [Display(Name = "int")]
        IntergerType,

        /// <summary>
        /// Decimal type
        /// </summary>
        [Display(Name = "decimal")]
        DecimalType,

        /// <summary>
        /// Boolean type
        /// </summary>
        [Display(Name = "bool")]
        BooleanType,

        /// <summary>
        /// Default type
        /// </summary>
        [Display(Name = "default")]
        DefaultType,

        /// <summary>
        /// Email type
        /// </summary>
        [Display(Name = "email")]
        EmailType,

        /// <summary>
        /// List type
        /// </summary>
        [Display(Name = "list")]
        ListType,

        /// <summary>
        /// Guid type
        /// </summary>
        [Display(Name = "guid")]
        GuidType,
    }
}
