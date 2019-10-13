// Decompiled with JetBrains decompiler
// Type: ResponsibleSystem.Common.Domain.Operations.OperationMessage
// Assembly: ns4D, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9B44D8D9-B1BD-47D0-B47A-F7256B478C0B
// Assembly location: C:\Repos\sani_nudge_ui\ResponsibleSystem.Api\libs\ns4D.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ResponsibleSystem.Common.Domain.Operations
{
    /// <summary>
    /// A message related to the invocation of an operation which can describe errors, warnings or informational/status messages.
    /// </summary>
    [DataContract]
    [KnownType(typeof(DbConcurrencyMessage))]
    public class OperationMessage : IEquatable<OperationMessage>
    {
        private Dictionary<string, string> mExtendedProperties;

        /// <summary>Message body</summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// Name of property, entity or other, to which this message applies
        /// </summary>
        [DataMember]
        public string AppliesTo { get; set; }

        /// <summary>Type of message</summary>
        [DataMember]
        public OperationMessageType Type { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" />
        /// </summary>
        public OperationMessage()
          : this(OperationMessageType.Error, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" />
        /// </summary>
        /// <param name="type">Type of message</param>
        /// <param name="message">Message body or identifier</param>
        public OperationMessage(OperationMessageType type, string message)
          : this(type, string.Empty, message)
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" />
        /// </summary>
        /// <param name="type">Type of message</param>
        /// <param name="appliesTo">String that specifies to what this message applies, e.g. a property name on an entity</param>
        /// <param name="message">Message body or identifier</param>
        public OperationMessage(OperationMessageType type, string appliesTo, string message)
        {
            this.Type = type;
            this.Message = message;
            this.AppliesTo = appliesTo;
            this.mExtendedProperties = new Dictionary<string, string>((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Implicitly converts an <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> to a <see cref="T:System.String" />.
        /// </summary>
        /// <param name="operationMessage"><see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> to convert</param>
        /// <returns></returns>
        public static implicit operator string(OperationMessage operationMessage)
        {
            if (operationMessage == null)
                throw new ArgumentNullException(nameof(operationMessage));
            return operationMessage.Message;
        }

        /// <summary>A dictionary of extended properties and their values</summary>
        [DataMember]
        [ExcludeFromCodeCoverage]
        public Dictionary<string, string> ExtendedProperties
        {
            get
            {
                return this.mExtendedProperties;
            }
            protected set
            {
            }
        }

        /// <summary>
        /// Gets the value of the specified extended property, or null if property is undefined.
        /// </summary>
        /// <param name="name">Name of extended property</param>
        /// <returns>The string value of the specified property. Returns null if property is not defined.</returns>
        public string GetExtendedProperty(string name)
        {
            string str;
            if (this.mExtendedProperties.TryGetValue(name, out str))
                return str;
            return (string)null;
        }

        /// <summary>
        /// Sets the value of the specified extended property. Property is added if it is not already defined.
        /// </summary>
        /// <param name="name">Name of extended property</param>
        /// <param name="value">Value of extended property</param>
        public void SetExtendedProperty(string name, string value)
        {
            if (this.mExtendedProperties.ContainsKey(name))
                this.mExtendedProperties[name] = value;
            else
                this.mExtendedProperties.Add(name, value);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> is equal to the current
        /// <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" />.
        /// </summary>
        /// <param name="other">The <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> to compare to the current
        /// <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /></param>
        /// <returns></returns>
        public bool Equals(OperationMessage other)
        {
            if (other == null)
                return false;
            int num1 = string.Equals(this.Message, other.Message) ? 1 : 0;
            bool flag1 = string.Equals(this.AppliesTo, other.AppliesTo);
            bool flag2 = object.Equals((object)this.Type, (object)other.Type);
            int num2 = flag1 ? 1 : 0;
            return (num1 & num2 & (flag2 ? 1 : 0)) != 0;
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current
        /// <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" />.
        /// </summary>
        /// <param name="obj">The <see cref="T:System.Object" /> to compare to the current <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" />
        /// </param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as OperationMessage);
        }

        /// <summary>Returns the hash code for the value of this instance.</summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return this.Type.GetHashCode() ^ this.Message.GetHashCode();
        }
    }
}
