// Decompiled with JetBrains decompiler
// Type: ResponsibleSystem.Common.Domain.Operations.DbConcurrencyMessage
// Assembly: ns4D, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9B44D8D9-B1BD-47D0-B47A-F7256B478C0B
// Assembly location: C:\Repos\sani_nudge_ui\ResponsibleSystem.Api\libs\ns4D.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ResponsibleSystem.Common.Domain.Operations
{
    /// <summary>
    /// Special operation message describing a concurrency issue.
    /// </summary>
    [DataContract]
    public class DbConcurrencyMessage : OperationMessage
    {
        /// <summary>
        /// Gets information about entities that caused the concurrency issues.
        /// </summary>
        [DataMember]
        public DbConcurrencyEntityInfo[] EntityInfo { get; set; }

        /// <summary>
        /// Creates a new instance of <see cref="T:ResponsibleSystem.Common.Domain.Operations.DbConcurrencyMessage" />.
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="entityInfo">Information about entities that caused the concurrency issues</param>
        public DbConcurrencyMessage(IEnumerable<DbConcurrencyEntityInfo> entityInfo, string message)
          : base(OperationMessageType.Error, message)
        {
            if (entityInfo == null)
                throw new ArgumentNullException(nameof(entityInfo));
            this.EntityInfo = entityInfo.ToArray<DbConcurrencyEntityInfo>();
        }
    }
}
