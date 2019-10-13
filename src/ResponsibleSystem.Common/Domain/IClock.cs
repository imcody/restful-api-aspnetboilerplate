using System;

namespace ResponsibleSystem.Common.Domain
{
    /// <summary>
    /// Provides an abstraction for determining current date and time.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Gets the current date and time, expressed as the local time.
        /// </summary>
        DateTime Now { get; }

        /// <summary>Gets the current date.</summary>
        DateTime Today { get; }

        /// <summary>
        /// Gets the current date and time, expressed as the Coordinated Universal Time (UTC).
        /// </summary>
        DateTime UtcNow { get; }
    }
}
