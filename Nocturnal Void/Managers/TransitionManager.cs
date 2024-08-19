namespace Nocturnal_Void.Managers
{
    /// <summary>
    /// An overrideable manager to provide utilities for transitions.
    /// </summary>
    internal class TransitionManager
    {
        public static TransitionManager Instance { get; private set; }

        public TransitionManager() { Instance = this; }

        public virtual void ProcessCollision(int collisionIndex)
        {
            switch (collisionIndex)
            {
                // 0 should represent empty or nonfunctioning.
                case 0: break;
            }
        }
    }
}
