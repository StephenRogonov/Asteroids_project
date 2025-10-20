using System.Collections.Generic;

namespace _Project.Scripts.GameFlow
{
    public class PauseSwitcher
    {
        private List<IPause> _pausableObjects = new();

        public void Add(IPause pausable) => _pausableObjects.Add(pausable);
        public void Remove(IPause pausable) => _pausableObjects.Remove(pausable);

        public void PauseAll()
        {
            foreach (IPause pausable in _pausableObjects)
            {
                pausable.Pause();
            }
        }

        public void UnpauseAll()
        {
            foreach (IPause pausable in _pausableObjects)
            {
                pausable.Unpause();
            }
        }
    }
}