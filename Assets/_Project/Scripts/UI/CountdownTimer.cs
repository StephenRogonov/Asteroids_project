namespace _Project.Scripts.UI
{
    public class CountdownTimer
    {
        private float _remainingTime;
        public float RemainingTime => _remainingTime;

        public void Tick(float deltaTime)
        {
            _remainingTime -= deltaTime;
        }

        public void Reset(float time)
        {
            _remainingTime = time;
        }
    }
}