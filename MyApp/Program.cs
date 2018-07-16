using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            program.Start();
            Console.ReadKey();
            Console.WriteLine("Stopping");
            program.Stop();
        }

        private static readonly AutoResetEvent _closeRequested = new AutoResetEvent(false);
        private long _last = 0;
        private long _current = 0;
        private Task _work;

        public bool IsRunning => !(_work is null || _work.IsCompleted);

        public void Start()
        {
            _work = Task.Run(() => DoWorkLoop());
        }

        public void Stop()
        {
            _closeRequested.Set();
            if (_work != null)
            {
                _work.Wait();
                _work = null;
            }
        }

        public void DoWorkLoop()
        {
            while (!_closeRequested.WaitOne(1000))
            {
                var last = _current;
                var next = _last + _current;
                if (next == 0)
                {
                    next = 1;
                }
                _last = _current;
                _current = next;
                Console.WriteLine(next);
            }
        }
    }
}