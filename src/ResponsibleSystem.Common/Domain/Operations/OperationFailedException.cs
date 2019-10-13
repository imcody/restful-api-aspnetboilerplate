// Decompiled with JetBrains decompiler
// Type: ResponsibleSystem.Common.Domain.Operations.OperationFailedException
// Assembly: ns4D, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9B44D8D9-B1BD-47D0-B47A-F7256B478C0B
// Assembly location: C:\Repos\sani_nudge_ui\ResponsibleSystem.Api\libs\ns4D.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace ResponsibleSystem.Common.Domain.Operations
{
    /// <summary>Exception indicating that an operation failed.</summary>
    public class OperationFailedException : Exception
    {
        private readonly List<OperationMessage> mMessages;

        /// <summary>
        /// The original result of the operation which caused the exception to be thrown
        /// </summary>
        public List<OperationMessage> Messages
        {
            get
            {
                return this.mMessages;
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationFailedException" />.
        /// </summary>
        /// <param name="messages">Messages to include in the exception</param>
        public OperationFailedException(IEnumerable<OperationMessage> messages)
          : base(messages.GetMessage())
        {
            this.mMessages = messages.ToList<OperationMessage>();
        }
    }
}
