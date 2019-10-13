// Decompiled with JetBrains decompiler
// Type: ResponsibleSystem.Common.Domain.Operations.BaseOperationResult
// Assembly: ns4D, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9B44D8D9-B1BD-47D0-B47A-F7256B478C0B
// Assembly location: C:\Repos\sani_nudge_ui\ResponsibleSystem.Api\libs\ns4D.dll

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.Serialization;

namespace ResponsibleSystem.Common.Domain.Operations
{
    /// <summary>
    /// Base class for <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult" /> and <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" />.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [DataContract]
    public abstract class BaseOperationResult
    {
        private List<OperationMessage> mMessages;

        /// <summary>
        /// Creates a new, empty instance of <see cref="T:ResponsibleSystem.Common.Domain.Operations.BaseOperationResult" /> indicating success.
        /// </summary>
        public BaseOperationResult()
          : this(Enumerable.Empty<OperationMessage>())
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="T:ResponsibleSystem.Common.Domain.Operations.BaseOperationResult" /> with the specified messages.
        /// </summary>
        public BaseOperationResult(IEnumerable<OperationMessage> messages)
        {
            this.mMessages = new List<OperationMessage>(messages);
        }

        /// <summary>
        /// Gets all messages associated with the result of the operation
        /// </summary>
        [DataMember]
        public List<OperationMessage> Messages
        {
            get
            {
                return this.mMessages;
            }
            protected set
            {
            }
        }

        /// <summary>
        /// Gets a value indicating whether operation was a success, i.e. the result does not hold any errors.
        /// </summary>
        [DataMember]
        public bool Success
        {
            get
            {
                return !this.Messages.HasErrors();
            }
            protected set
            {
            }
        }
    }
}
