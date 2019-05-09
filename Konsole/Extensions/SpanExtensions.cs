using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

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
    }
}
