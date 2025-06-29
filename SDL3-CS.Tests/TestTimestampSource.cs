// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System.Diagnostics;
using NUnit.Framework;

namespace SDL.Tests
{
    /// <summary>
    /// These tests check that <see cref="Stopwatch.GetTimestamp"/> and <see cref="SDL3.SDL_GetPerformanceCounter"/> use the same source of time.
    /// </summary>
    /// <remarks>
    /// You also need to check that the clocks remain consistent even after the system is put to sleep. To do so:
    /// <list type="number">
    ///     <item>Run both tests normally.</item>
    ///     <item>Run the tests one at a time, putting your computer to sleep while the test is running.
    ///         Adjust <see cref="ITERATIONS"/> and <see cref="SLEEP_TIMEOUT"/> as needed.</item>
    ///     <item>Run both tests normally. (Checks consistency after a sleep cycle.)</item>
    /// </list>
    /// </remarks>
    public class TestTimestampSource
    {
        public const int ITERATIONS = 5;
        public const int SLEEP_TIMEOUT = 200;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            SDL3.SDL_Init(0);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            SDL3.SDL_Quit();
        }

        [Test]
        public void TestSDLBoundedByStopwatch()
        {
            Assert.That(SDL3.SDL_GetPerformanceFrequency(), Is.EqualTo(Stopwatch.Frequency));

            DateTime? wallClockLastEnd = null;
            long? lastEnd = null;

            for (int i = 0; i < ITERATIONS; i++)
            {
                var wallClockStart = DateTime.UtcNow;

                long stopwatchStart = Stopwatch.GetTimestamp();
                ulong sdlTimestamp = SDL3.SDL_GetPerformanceCounter();
                long stopwatchEnd = Stopwatch.GetTimestamp();

                var wallClockEnd = DateTime.UtcNow;

                Assert.That(sdlTimestamp, Is.GreaterThanOrEqualTo(stopwatchStart).And.LessThanOrEqualTo(stopwatchEnd));

                if (lastEnd.HasValue && wallClockLastEnd.HasValue)
                {
                    long diff = stopwatchStart - lastEnd.Value;
                    double seconds = (double)diff / Stopwatch.Frequency;
                    double wallClockSeconds = (wallClockStart - wallClockLastEnd.Value).TotalSeconds;
                    Console.WriteLine($"Elapsed inter-iteration: {diff} ({seconds} s), wall clock: {wallClockSeconds} s");
                    Console.WriteLine();
                }

                Console.WriteLine($"Stw: {stopwatchStart}");
                Console.WriteLine($"SDL: {sdlTimestamp}");
                Console.WriteLine($"Stw: {stopwatchEnd}");
                Console.WriteLine($"Elapsed this interation: {stopwatchEnd - stopwatchStart}");
                Console.WriteLine();

                lastEnd = stopwatchEnd;
                wallClockLastEnd = wallClockEnd;

                Thread.Sleep(SLEEP_TIMEOUT);
            }
        }

        [Test]
        public void TestStopwatchBoundedBySDL()
        {
            Assert.That(Stopwatch.Frequency, Is.EqualTo(SDL3.SDL_GetPerformanceFrequency()));

            DateTime? wallClockLastEnd = null;
            ulong? lastEnd = null;

            for (int i = 0; i < ITERATIONS; i++)
            {
                var wallClockStart = DateTime.UtcNow;

                ulong sdlStart = SDL3.SDL_GetPerformanceCounter();
                long stopwatchTimestamp = Stopwatch.GetTimestamp();
                ulong sdlEnd = SDL3.SDL_GetPerformanceCounter();

                var wallClockEnd = DateTime.UtcNow;

                Assert.That(stopwatchTimestamp, Is.GreaterThanOrEqualTo(sdlStart).And.LessThanOrEqualTo(sdlEnd));

                if (lastEnd.HasValue && wallClockLastEnd.HasValue)
                {
                    ulong diff = sdlStart - lastEnd.Value;
                    double seconds = (double)diff / SDL3.SDL_GetPerformanceFrequency();
                    double wallClockSeconds = (wallClockStart - wallClockLastEnd.Value).TotalSeconds;
                    Console.WriteLine($"Elapsed inter-iteration: {diff} ({seconds} s), wall clock: {wallClockSeconds} s");
                    Console.WriteLine();
                }

                Console.WriteLine($"SDL: {sdlStart}");
                Console.WriteLine($"Stw: {stopwatchTimestamp}");
                Console.WriteLine($"SDL: {sdlEnd}");
                Console.WriteLine($"Elapsed: {sdlEnd - sdlStart}");
                Console.WriteLine();

                lastEnd = sdlEnd;
                wallClockLastEnd = wallClockEnd;

                Thread.Sleep(SLEEP_TIMEOUT);
            }
        }
    }
}
