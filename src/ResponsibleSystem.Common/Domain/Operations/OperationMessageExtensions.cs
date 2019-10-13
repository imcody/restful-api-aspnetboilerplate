
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace ResponsibleSystem.Common.Domain.Operations
{
    /// <summary>
    /// Provides extension methods for <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> and
    /// <see cref="T:System.Collections.Generic.IEnumerable`1" />.
    /// </summary>
    public static class OperationMessageExtensions
    {
        /// <summary>
        /// Creates a new <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> of type <see cref="F:OperationMessageType.Error" /> with
        /// the specified message and adds it to the current instance.
        /// </summary>
        /// <param name="list">Current list</param>
        /// <param name="message">Error message</param>
        public static void AddError(this IList<OperationMessage> list, string message)
        {
            list.Add(new OperationMessage(OperationMessageType.Error, message));
        }

        /// <summary>
        /// Creates a new <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> of type <see cref="F:OperationMessageType.Error" /> with
        /// the specified message and adds it to the current instance.
        /// </summary>
        /// <param name="list">Current list</param>
        /// <param name="propertyName">Name of property to which the message applies</param>
        /// <param name="message">Error message</param>
        public static void AddError(this IList<OperationMessage> list, string propertyName, string message)
        {
            list.Add(new OperationMessage(OperationMessageType.Error, propertyName, message));
        }

        /// <summary>
        /// Creates a new <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> of type <see cref="F:OperationMessageType.Info" /> with
        /// the specified message and adds it to the current instance.
        /// </summary>
        /// <param name="list">Current list</param>
        /// <param name="message">Informational/status message</param>
        public static void AddInfo(this IList<OperationMessage> list, string message)
        {
            list.Add(new OperationMessage(OperationMessageType.Info, message));
        }

        /// <summary>
        /// Creates a new <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> of type <see cref="F:OperationMessageType.Info" /> with
        /// the specified message and adds it to the current instance.
        /// </summary>
        /// <param name="list">Current list</param>
        /// <param name="propertyName">Name of property to which the message applies</param>
        /// <param name="message">Informational/status message</param>
        public static void AddInfo(this IList<OperationMessage> list, string propertyName, string message)
        {
            list.Add(new OperationMessage(OperationMessageType.Info, propertyName, message));
        }

        /// <summary>
        /// Creates a new <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> of type <see cref="F:OperationMessageType.Warning" /> with
        /// the specified message and adds it to the current instance.
        /// </summary>
        /// <param name="list">Current list</param>
        /// <param name="message">Warning message</param>
        public static void AddWarning(this IList<OperationMessage> list, string message)
        {
            list.Add(new OperationMessage(OperationMessageType.Warning, message));
        }

        /// <summary>
        /// Creates a new <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> of type <see cref="F:OperationMessageType.Warning" /> with
        /// the specified message and adds it to the current instance.
        /// </summary>
        /// <param name="list">Current list</param>
        /// <param name="propertyName">Name of property to which the message applies</param>
        /// <param name="message">Warning message</param>
        public static void AddWarning(this IList<OperationMessage> list, string propertyName, string message)
        {
            list.Add(new OperationMessage(OperationMessageType.Warning, propertyName, message));
        }

        /// <summary>
        /// Determines whether a sequence of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> contains one or more errors.
        /// </summary>
        /// <param name="source">Sequence of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /></param>
        /// <returns></returns>
        public static bool HasErrors(this IEnumerable<OperationMessage> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            return source.Any<OperationMessage>((Func<OperationMessage, bool>)(m => m.Type == OperationMessageType.Error));
        }

        /// <summary>
        /// Combines messages in a sequence of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> into a single <see cref="T:System.String" />.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> from which to generate a message</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public static string GetMessage(this IEnumerable<OperationMessage> source)
        {
            return source.GetMessage((string)null);
        }

        /// <summary>
        /// Combines messages in a sequence of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> into a single <see cref="T:System.String" />.
        /// </summary>
        /// <param name="source">A sequence of <see cref="T:ResponsibleSystem.Common.Domain.Operations.OperationMessage" /> from which to generate a message</param>
        /// <param name="heading">An optional heading to include before the messages in the resulting string</param>
        /// <returns></returns>
        public static string GetMessage(this IEnumerable<OperationMessage> source, string heading)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            OperationMessageType? filterType = new OperationMessageType?();
            IEnumerable<OperationMessage> source1 = source;
            Func<OperationMessage, bool> func = (Func<OperationMessage, bool>)(m => m.Type == OperationMessageType.Error);
            if (source1.Any<OperationMessage>(func))
                filterType = new OperationMessageType?(OperationMessageType.Error);
            IEnumerable<OperationMessage> source2 = source.Distinct<OperationMessage>();
            if (filterType.HasValue)
                source2 = source2.Where<OperationMessage>((Func<OperationMessage, bool>)(m =>
               {
                   int type = (int)m.Type;
                   OperationMessageType? nullable = filterType;
                   int valueOrDefault = (int)nullable.GetValueOrDefault();
                   if (type != valueOrDefault)
                       return false;
                   return nullable.HasValue;
               }));
            StringBuilder stringBuilder = new StringBuilder();
            bool flag = source2.Count<OperationMessage>() > 1;
            foreach (OperationMessage operationMessage in source2)
            {
                if (flag)
                    stringBuilder.Append("- ");
                stringBuilder.AppendLine((string)operationMessage);
            }
            if (stringBuilder.Length > 0 && !string.IsNullOrEmpty(heading))
                stringBuilder.Insert(0, string.Format("{0}{1}{1}", (object)heading, (object)Environment.NewLine));
            return stringBuilder.ToString().Trim(' ', '\n', '\r');
        }
    }
}
