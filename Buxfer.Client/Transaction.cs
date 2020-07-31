using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using RestSharp.Deserializers;

namespace Buxfer.Client
{
    #region Enums

    #endregion

    /// <summary>
    ///     Represents a transaction.
    /// </summary>
    [DebuggerDisplay("{Description}. {Type}: {Amount} in {Date} ({Tags})")]
    public class Transaction //: EntityBase
    {
        #region Properties
        public int Id { get; set; }
        /// <summary>
        ///     Gets or sets the account identifier.
        /// </summary>
        /// <value>
        ///     The account identifier.
        /// </value>
        public int AccountId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the account.
        /// </summary>
        /// <value>
        ///     The name of the account.
        /// </value>
        public string AccountName { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the date.
        /// </summary>
        /// <value>
        ///     The date.
        /// </value>
        [DeserializeAs(Name = "NormalizedDate")]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        public TransactionType Type { get; set; }

        /// <summary>
        ///     Gets or sets the amount.
        /// </summary>
        /// <value>
        ///     The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        ///     Gets or sets the tag names.
        /// </summary>
        /// <value>
        ///     The tag names.
        /// </value>
        public List<string> TagNames { get; set; }=new List<string>();

        public string Tags => String.Join(",", TagNames);

        /// <summary>
        ///     Gets or sets the extra information.
        /// </summary>
        /// <value>
        ///     The extra information.
        /// </value>
        //public string ExtraInfo { get; set; }

        /// <summary>
        ///     Gets or sets the status.
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        public TransactionStatus Status { get; set; }
        
        public bool IsPending { get; set; }

        #endregion
    }
}