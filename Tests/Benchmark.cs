using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine.AI;

namespace AleVerDes.BinarySerialization.Tests
{
    internal class Benchmark
    {
        private readonly Stopwatch _sw;
        private readonly List<KeyValuePair<string, TimeSpan>> _messages;
        private readonly string _tag;

        private TimeSpan _result;
        
        public Benchmark(string tag)
        {
            _sw = new Stopwatch();
            _messages = new List<KeyValuePair<string, TimeSpan>>();
            _tag = tag;
        }
        
        public void Start()
        {
            _sw.Start();
        }

        public void Round(string message)
        {
            _messages.Add(new KeyValuePair<string, TimeSpan>(message, _sw.Elapsed));
            _result += _sw.Elapsed;
            _sw.Restart();
        }

        public void Stop()
        {
            _sw.Stop();
        }

        public string GetMessage(bool padRight = false)
        {
            var max = 0;
            if (padRight)
            {
                max = _messages.Max(x => x.Key.Length);
            }

            var result = $"[{_tag}]:\n";
            var index = 1;
            foreach (var (message, time) in _messages)
            {
                result += $"#{index} {message.PadRight(max)}: {time}\n";
                index += 1;
            }

            result += "Result: " + _result;

            return result;
        }
    }
}