using System.Collections.Generic;
using System.Threading;
using DirectoryScan.Abstract;
using DirectoryScan.Models;

namespace DirectoryScan.Concrete
{
    public class MyDirectoryBlockingQueue : IDirectoryBlockingQueue
    {
        private Queue<DirectoryViewModel> _queue = new Queue<DirectoryViewModel>(); 
        private readonly object _lock = new object();

        public bool Ended { get; private set; }
        public int LastPickCount = 0;

        public int Count
        {
            get { return _queue.Count; }
        }

        public void End()
        {
            lock (_lock)
            {
                Ended = true;
                Monitor.PulseAll(_lock);
            }
        }

        public MyDirectoryBlockingQueue()
        {
            Ended = false;
        }

        public void Enqueue(DirectoryViewModel model)
        {
            lock (_lock)
            {
                
                    _queue.Enqueue(model);
                
                Monitor.PulseAll(_lock);
            }
        }

        public bool TryDequeue(out DirectoryViewModel model)
        {
            model = default(DirectoryViewModel);
            lock (_lock)
            {
                while (_queue.Count == 0)
                {
                    if (Ended)
                        return false;
                    Monitor.Wait(_lock);
                }
                if (LastPickCount == 1)
                {
                    model = _queue.Dequeue();
                    LastPickCount = 0;
                }
                else
                {
                    model = _queue.Peek();
                    LastPickCount++;
                }
                Monitor.PulseAll(_lock);
            }
            return true;
        }
    }
}
