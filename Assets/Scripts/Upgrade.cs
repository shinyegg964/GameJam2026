using UnityEngine;

[System.Serializable]
public class Upgrade
{
    public string name;
    public string description;
    public Sprite icon;

    public UpgradeType type;
    public float value;
}
public enum UpgradeType
{
    Damage,
    Speed,
    FireRate,
    Health
}
