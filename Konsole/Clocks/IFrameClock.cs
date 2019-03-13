
namespace Konsole.Clocks
{
    public interface IFrameClock : IClock
    {
        /// <summary>
        /// The time between the current and the last frame in milliseconds.
        /// </summary>
        uint ElapsedTime { get; }
    }
}
