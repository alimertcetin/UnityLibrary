using UnityEngine;
using XIV.UpgradeSystem.Integration;
using XIV.UpgradeSystem.Utils;

namespace XIV.UpgradeSystem.Examples
{
    public class UpgradeUser : MonoBehaviour
    {
        public float damageAmount;
        public UpgradeContainer<PlayerUpgrade> playerUpgrades;

        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CollectableUpgrade>(out var collectableUpgrade))
            {
                playerUpgrades.TryAdd(collectableUpgrade.upgrade);
                Destroy(other.gameObject);
            }
            else if (other.TryGetComponent<Enemy>(out var enemy))
            {
                var damageToDeal = damageAmount;
                if (playerUpgrades.ContainsType(PlayerUpgrade.Damage, out var damageUpgrade))
                {
                    damageToDeal = UpgradeUtils.BoostUsingPercentage(damageToDeal, damageUpgrade);
                }
                enemy.TakeDamage(damageToDeal, null);
            }
            Debug.Log("Trigger enter");
        }
    }
}
