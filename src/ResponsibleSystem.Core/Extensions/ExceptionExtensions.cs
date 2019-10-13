using System;
using System.Collections.Generic;
using System.Text;

namespace ResponsibleSystem.Extensions
{
    public static class ExceptionExtensions
    {
        public static string GetFullErrorMessage(this Exception e)
        {
            var sb = new StringBuilder();
            var curr = e;
            while (curr != null)
            {
                sb.AppendLine(curr.Message);
                curr = curr.InnerException;
            }
            return sb.ToString();
        }
    }
}
