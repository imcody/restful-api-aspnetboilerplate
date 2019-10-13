// Decompiled with JetBrains decompiler
// Type: ResponsibleSystem.Common.Domain.Operations.OperationReturnedNoValueException
// Assembly: ns4D, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9B44D8D9-B1BD-47D0-B47A-F7256B478C0B
// Assembly location: C:\Repos\sani_nudge_ui\ResponsibleSystem.Api\libs\ns4D.dll

using System;

namespace ResponsibleSystem.Common.Domain.Operations
{
    /// <summary>
    /// Exception indicating that an attempt was made to implicitly retrieve a value from
    /// an <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /> that contains no value.
    /// </summary>
    public class OperationReturnedNoValueException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationReturnedNoValueException" />.
        /// </summary>
        public OperationReturnedNoValueException()
          : base("Operation did not return a value")
        {
        }
    }
}
