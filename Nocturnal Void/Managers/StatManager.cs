﻿namespace Nocturnal_Void.Managers
{
    /// <summary>
    /// Used to manage stats for an entity.
    /// </summary>
    public class StatManager
    {
        private int _HP;
        private int maxHP;
        public int MaxHP { get => maxHP; protected set => maxHP = value; }
        public int HP { get => _HP; set => _HP = value; }

        /// <summary>
        /// This will be called whenever we want the owner of this StatMan to cease existing.
        /// </summary>
        public Action OnDeath = () => { }; // Lambda so no error.

        public StatManager(int maxHP)
        {
            MaxHP = maxHP;
            HP = maxHP;
        }

        /// <summary>
        /// Damages the owner, calling OnDeath if hp hits 0.
        /// The caller should calculate defense, because this method takes final damage.
        /// </summary>
        /// <param name="fd">The final damage value to be applied.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Damage(int fd)
        {
            if (fd < 0) { throw new ArgumentOutOfRangeException("final damage was less than 0."); }
            else if (fd == 0) return; // This isn't wrong, we just don't need to do anything.
            HP -= fd;

            // Check if the owner should be dead.
            if (HP <= 0) { HP = 0; OnDeath(); }
        }

        /// <summary>
        /// Heals the owner, but cannot exceed maxhp.
        /// </summary>
        /// <param name="value">The value to heal by.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public void Heal(int value)
        {
            if (value < 0) { throw new ArgumentOutOfRangeException("final healing was less than 0."); }
            else if (value == 0) return; // Again, this isn't wrong, and we don't need to do anything.
            HP += value;

            // Check if hp is valid, and if not, set it back to a valid value.
            if (HP <= MaxHP) { HP = MaxHP; }
        }

        /// <summary>
        /// Forcibly set the value of HP without range checks.
        /// </summary>
        /// <param name="value">The new value.</param>
        public void SetHP(int value) { HP = value; }

        public StatManager Clone() { return (StatManager)MemberwiseClone(); }
    }
}
