// Decompiled with JetBrains decompiler
// Type: ResponsibleSystem.Common.Domain.Operations.DbConcurrencyEntityInfo
// Assembly: ns4D, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9B44D8D9-B1BD-47D0-B47A-F7256B478C0B
// Assembly location: C:\Repos\sani_nudge_ui\ResponsibleSystem.Api\libs\ns4D.dll

using System.Runtime.Serialization;

namespace ResponsibleSystem.Common.Domain.Operations
{
    /// <summary>
    /// Provides information about an entity that caused concurrency issues during a data store operation.
    /// </summary>
    [DataContract]
    public class DbConcurrencyEntityInfo
    {
        /// <summary>Gets the entity type name.</summary>
        [DataMember]
        public string EntityType { get; set; }

        /// <summary>
        /// Gets a description of the attempted action on this entity.
        /// </summary>
        [DataMember]
        public string AttemptedAction { get; set; }

        /// <summary>Gets the entity's primary key values.</summary>
        [DataMember]
        public object[] EntityKeys { get; set; }
    }
}
