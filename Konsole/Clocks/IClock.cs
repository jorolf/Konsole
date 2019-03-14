
namespace Konsole.Clocks
{
    public interface IClock
    {
        /// <summary>
        /// This Clock's time in milliseconds.
        /// </summary>
        uint TimeMilliseconds { get; }
        /// <summary>
        /// This Clock's time in seconds.
        /// </summary>
        double Time { get; }
    }
}
