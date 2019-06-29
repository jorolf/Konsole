using System;
using System.Runtime.InteropServices;

namespace Konsole.Extensions
{
    public static class SpanExtensions
    {
        public static Span<TOut> Cast<TIn, TOut>(this Span<TIn> span) where TIn : struct where TOut : struct
        {
            return MemoryMarshal.Cast<TIn, TOut>(span);
        }
        public static Span<TOut> ReverseCast<TIn, TOut>(this Span<TIn> span) where TIn : struct where TOut : struct
        {
            span.Reverse();
            return MemoryMarshal.Cast<TIn, TOut>(span);
        }
        public static TOut[] Cast<TIn, TOut>(this TIn[] array) where TIn : struct where TOut : struct
        {
            return MemoryMarshal.Cast<TIn, TOut>(array.AsSpan()).ToArray();
        }
        public static TOut[] ReverseCast<TIn, TOut>(this TIn[] array) where TIn : struct where TOut : struct
        {
            Span<TIn> span = array.AsSpan();
            span.Reverse();
            return MemoryMarshal.Cast<TIn, TOut>(span).ToArray();
        }
    }
}
