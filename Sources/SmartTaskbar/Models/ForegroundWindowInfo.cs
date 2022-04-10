namespace SmartTaskbar
{
    public readonly struct ForegroundWindowInfo
    {
        public static readonly ForegroundWindowInfo Empty = new();

        /// <summary>
        ///     ctor
        /// </summary>
        public ForegroundWindowInfo(IntPtr foregroundHandle, IntPtr monitor, TagRect rect)
        {
            Handle = foregroundHandle;
            Monitor = monitor;
            Rect = rect;
        }

        public readonly IntPtr Handle;

        public readonly IntPtr Monitor;

        public readonly TagRect Rect;

        public static bool operator ==(ForegroundWindowInfo left, ForegroundWindowInfo right)
            => left.Handle == right.Handle
               && left.Monitor == right.Monitor
               && left.Rect == right.Rect;

        public static bool operator !=(ForegroundWindowInfo left, ForegroundWindowInfo right)
            => !(left == right);

        public bool Equals(ForegroundWindowInfo other)
            => Handle == other.Handle
               && Monitor == other.Monitor
               && Rect == other.Rect;

        public override bool Equals(object obj)
            => obj is ForegroundWindowInfo other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Handle.GetHashCode();
                hashCode = hashCode * 397 ^ Monitor.GetHashCode();
                hashCode = hashCode * 397 ^ Rect.GetHashCode();
                return hashCode;
            }
        }
    }
}
