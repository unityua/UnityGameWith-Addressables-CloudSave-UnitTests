using System;
using UnityEngine;

namespace PesPatron.Characters
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private bool _invincible;

        public int HealthAmount => _health;

        public bool Invincible { get => _invincible; set => _invincible = value; }

        public event Action<Health, int> HealthChanged;
        public event Action<Health> Died;

        private void OnDestroy()
        {
            HealthChanged = null;
            Died = null;
        }

        public void SetHealth(int amount)
        {
            _health = amount;

            HealthChanged?.Invoke(this, amount);
        }

        public void ApplyDamage(int damage)
        {
            if (_health <= 0 || _invincible || damage < 0)
                return;

            _health -= damage;

            if (_health < 0)
                _health = 0;

            HealthChanged?.Invoke(this, _health);

            if (_health == 0)
                Died?.Invoke(this);
        }
    }
}