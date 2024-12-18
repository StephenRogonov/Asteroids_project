using System.Collections.Generic;

namespace _Project.Scripts.Common
{
    public class PauseHandler
    {
        private List<IPause> _pausableObjects = new();

        public void Add(IPause pausable) => _pausableObjects.Add(pausable);
        public void Remove(IPause pausable) => _pausableObjects.Remove(pausable);

        public void Pause()
        {
            foreach (IPause pausable in _pausableObjects)
            {
                pausable.Pause();
            }
        }

        public void Unpause()
        {
            foreach (IPause pausable in _pausableObjects)
            {
                pausable.Unpause();
            }
        }
    }
}