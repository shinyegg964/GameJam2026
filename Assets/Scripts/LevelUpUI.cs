using UnityEngine;

public class LevelUpUI : MonoBehaviour
{
    public GameObject panel;
    public UpgradeCard[] cards;

    public Upgrade[] allUpgrades;

    public void ShowLevelUp()
    {
        panel.SetActive(true);
        Time.timeScale = 0f;

        for (int i = 0; i < cards.Length; i++)
        {
            Upgrade randomUpgrade = allUpgrades[Random.Range(0, allUpgrades.Length)];
            cards[i].Setup(randomUpgrade, this);
        }
    }

    public void SelectUpgrade(Upgrade upgrade)
    {
        ApplyUpgrade(upgrade);

        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    void ApplyUpgrade(Upgrade upgrade)
    {
        Player player = FindObjectOfType<Player>();
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();

        switch (upgrade.type)
        {
            case UpgradeType.Damage:
                player.damage += upgrade.value;
                break;

            case UpgradeType.Speed:
                player.moveSpeed += upgrade.value;
                break;

            case UpgradeType.FireRate:
                player.timer -= upgrade.value;
                break;

            case UpgradeType.Health:
                playerHealth.health += upgrade.value;
                break;
        }
    }
}
