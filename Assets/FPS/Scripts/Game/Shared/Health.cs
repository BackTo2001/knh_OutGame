using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("Maximum amount of health")] public float MaxHealth = 10f;

        [Tooltip("Health ratio at which the critical health vignette starts appearing")]
        public float CriticalHealthRatio = 0.3f;

        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction<float> OnHealed;
        public UnityAction OnDie;

        public float CurrentHealth { get; set; }
        public bool Invincible { get; set; }
        public bool CanPickup() => CurrentHealth < MaxHealth;

        public float GetRatio() => CurrentHealth / MaxHealth;
        public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

        bool m_IsDead;

        [Header("Pooling (Optional)")]
        public bool UsePooling = false;
        public EPoolType PoolType;

        void Start()
        {
            CurrentHealth = MaxHealth;
        }

        public void Heal(float healAmount)
        {
            float healthBefore = CurrentHealth;
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            float trueHealAmount = CurrentHealth - healthBefore;
            if (trueHealAmount > 0f)
            {
                OnHealed?.Invoke(trueHealAmount);
            }
        }

        public void TakeDamage(float damage, GameObject damageSource)
        {
            if (Invincible)
                return;

            float healthBefore = CurrentHealth;
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            float trueDamageAmount = healthBefore - CurrentHealth;
            if (trueDamageAmount > 0f)
            {
                OnDamaged?.Invoke(trueDamageAmount, damageSource);
            }

            HandleDeath();
        }

        public void Kill()
        {
            CurrentHealth = 0f;
            OnDamaged?.Invoke(MaxHealth, null);
            HandleDeath();
        }

        void HandleDeath()
        {
            if (m_IsDead)
                return;

            if (CurrentHealth <= 0f)
            {
                m_IsDead = true;
                OnDie?.Invoke();

                // 풀로 복귀 처리
                if (UsePooling && ObjectPoolManager.Instance != null)
                {
                    // 애니메이션 등을 고려해서 약간의 딜레이 주고 싶다면 Invoke나 코루틴 사용
                    ObjectPoolManager.Instance.ReturnToPool(PoolType, gameObject);
                }
                else
                {
                    Destroy(gameObject); // 풀링 사용 안 할 경우 Destroy
                }
            }
        }
    }
}
