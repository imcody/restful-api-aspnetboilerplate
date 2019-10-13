// Decompiled with JetBrains decompiler
// Type: ResponsibleSystem.Common.Domain.Operations.OperationResult
// Assembly: ns4D, Version=1.0.2.0, Culture=neutral, PublicKeyToken=null
// MVID: 9B44D8D9-B1BD-47D0-B47A-F7256B478C0B
// Assembly location: C:\Repos\sani_nudge_ui\ResponsibleSystem.Api\libs\ns4D.dll

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace ResponsibleSystem.Common.Domain.Operations
{
    /// <summary>
    /// Describes the result of an operation that returns no value
    /// </summary>
    [DataContract]
    public class OperationResult : BaseOperationResult
    {
        /// <summary>
        /// Creates a new, empty <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult" /> indicating success.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public OperationResult()
        {
        }

        /// <summary>
        /// Creates a new <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult" /> with the specified messages.
        /// </summary>
        /// <param name="messages"></param>
        public OperationResult(IEnumerable<OperationMessage> messages)
          : base(messages)
        {
        }

        /// <summary>
        /// Creates an <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /> with the specified value.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="value">Value returned by <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /></param>
        /// <returns></returns>
        public static OperationResult<T> Returns<T>(T value)
        {
            return new OperationResult<T>(value);
        }

        /// <summary>
        /// Creates an <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /> with the specified value.
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="value">Value returned by <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /></param>
        /// <param name="messages">Messages to include in result</param>
        /// <returns></returns>
        public static OperationResult<T> Returns<T>(T value, IEnumerable<OperationMessage> messages)
        {
            OperationResult<T> operationResult = new OperationResult<T>(value);
            operationResult.Messages.AddRange(messages);
            return operationResult;
        }

        /// <summary>
        /// Creates an <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult" /> returning no value, indicating failure.
        /// </summary>
        /// <param name="errorMessage">Error message contained within <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult" /></param>
        /// <returns></returns>
        public static OperationResult Error(string errorMessage)
        {
            OperationResult operationResult = new OperationResult();
            operationResult.Messages.AddError(errorMessage);
            return operationResult;
        }
    }
}
