using System.Globalization;

namespace ResponsibleSystem.Extensions
{
    public static class DecimalExtensions
    {
        /// <summary>
        ///   Checks if a decimal contains a proper currency value.
        /// </summary>
        /// <param name="value">Value to be checked</param>
        /// <returns>Pass or fail.</returns>
        public static bool IsMoney(this decimal value)
        {
            if (value < 0.0M) return false;
            return true;
        }

        /// <summary>
        ///   Checks if a nullable decimal contains a proper currency value.
        /// </summary>
        /// <param name="value">Value to be checked</param>
        /// <returns>Pass or fail.</returns>
        public static bool IsMoney(this decimal? value)
        {
            if (value.HasValue && value < 0.0M) return false;
            return true;
        }

        /// <summary>
        /// check if provided value fits scale and precision 
        /// </summary>
        /// <param name="dNumber"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static bool WillItTruncate(this decimal dNumber, int precision, int scale)
        {
            var dString = dNumber.ToString(CultureInfo.InvariantCulture).Split('.');
            return dString[0].Length <= (precision - scale) && (dString.Length <= 1 || dString[1].Length <= scale);
        }
    }
}