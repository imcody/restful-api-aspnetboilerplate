// Decompiled with JetBrains decompiler
// Type: ResponsibleSystem.Common.Domain.Operations.OperationResult`1
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
    /// Describes the result of an operation that returns a value of type <typeparamref name="T" />.
    /// </summary>
    /// <typeparam name="T">Type of value returned from operation</typeparam>
    [DataContract(Name = "{0}OperationResult")]
    public class OperationResult<T> : BaseOperationResult
    {
        private T mValue;
        private bool mHasValue;

        /// <summary>
        /// Creates a new, blank instance of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /> returning no value.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public OperationResult()
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /> returning the specified value.
        /// </summary>
        /// <param name="value">Value of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /></param>
        public OperationResult(T value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the result of the operation. Always returns null/default value if operation failed.
        /// </summary>
        [DataMember]
        public T Value
        {
            get
            {
                if (this.HasValue)
                    return this.mValue;
                return default(T);
            }
            set
            {
                this.mValue = value;
                this.mHasValue = true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the result of the operation contains any value.
        /// Always returns false if operation failed.
        /// </summary>
        [DataMember]
        public bool HasValue
        {
            get
            {
                if (this.mHasValue)
                    return this.Success;
                return false;
            }
            protected set
            {
            }
        }

        /// <summary>
        /// Implicitly converts an instance of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /> to <typeparamref name="T" />.
        /// </summary>
        /// <param name="result"><see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /> to convert</param>
        /// <exception cref="T:System.ArgumentNullException">If <paramref name="result" /> is <value>null</value></exception>
        /// <returns>The value of <paramref name="result" /></returns>
        /// <remarks>
        /// This is the recommended method of retrieving the result of an operation, since this will
        /// immediately fail if operation failed or returned no value (see <see cref="M:GetValueOrThrow" />),
        /// thereby avoiding potential <see cref="T:System.NullReferenceException" />s elsewhere.
        /// </remarks>
        public static implicit operator T(OperationResult<T> result)
        {
            if (result == null)
                throw new ArgumentNullException(nameof(result));
            return result.GetValueOrThrow();
        }

        /// <summary>
        /// Implicitly converts any value to an <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /> containing the value
        /// and indicating success.
        /// </summary>
        /// <param name="value">Value returned from an operation</param>
        /// <returns>An <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" /> containing <paramref name="value" />
        /// and indicating succes</returns>
        public static implicit operator OperationResult<T>(T value)
        {
            return OperationResult.Returns<T>(value);
        }

        /// <summary>
        /// Implicitly converts any <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult" /> to an <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationResult`1" />
        /// that has no value.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public static implicit operator OperationResult<T>(OperationResult other)
        {
            OperationResult<T> operationResult = new OperationResult<T>();
            operationResult.Messages.AddRange((IEnumerable<OperationMessage>)other.Messages);
            return operationResult;
        }

        /// <summary>
        /// Returns the value of the operation result. If operation failed or contains no value, an exception is thrown.
        /// </summary>
        /// <exception cref="T:ResponsibleSystem.Common.Domain.Operations.OperationFailedException">Thrown if the operation result indicates failure.</exception>
        /// <exception cref="T:ResponsibleSystem.Common.Domain.Operations.OperationReturnedNoValueException">Thrown if the operation result contains no value.</exception>
        /// <returns>The value of the current instance</returns>
        private T GetValueOrThrow()
        {
            if (!this.Success)
                throw new OperationFailedException((IEnumerable<OperationMessage>)this.Messages);
            if (!this.HasValue)
                throw new OperationReturnedNoValueException();
            return this.Value;
        }
    }
}
