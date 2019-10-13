using System;

namespace ResponsibleSystem.Common.Domain
{
    /// <summary>Determines current system date and time</summary>
    public class SystemClock : IClock
    {
        /// <summary>
        /// Gets the current date and time on this computer, expressed as the local time.
        /// </summary>
        public DateTime Now
        {
            get
            {
                return DateTime.Now;
            }
        }

        /// <summary>Gets the current date on this computer.</summary>
        public DateTime Today
        {
            get
            {
                return DateTime.Today;
            }
        }

        /// <summary>
        /// Gets the current date and time on this computer, expressed as the Universal Coordinated Time (UTC)
        /// </summary>
        public DateTime UtcNow
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}
