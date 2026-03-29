using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeCard : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Image icon;

    private Upgrade upgrade;
    private LevelUpUI manager;

    public void Setup(Upgrade newUpgrade, LevelUpUI ui)
    {
        upgrade = newUpgrade;
        manager = ui;

        title.text = upgrade.name;
        description.text = upgrade.description;
        icon.sprite = upgrade.icon;
    }

    public void OnClick()
    {
        manager.SelectUpgrade(upgrade);
    }
}
