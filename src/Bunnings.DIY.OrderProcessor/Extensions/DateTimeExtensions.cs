using System;

namespace Bunnings.DIY.OrderProcessor.Extensions;

public static class DateTimeExtensions
{
    public static TimeSpan ToTimeSpan(this int seconds) => TimeSpan.FromSeconds(seconds);
}