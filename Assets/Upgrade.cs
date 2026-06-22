using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Upgrade : MonoBehaviour
{
    [Header("Dados")]
    public UpgradeData data;

    [Header("Evento especial (opcional)")]
    public UnityEvent onPurchased;

    [Header("Som")]
    public AudioClip purchaseSound;
    private AudioSource audioSource;

    [Header("UI")]
    public Button upgradeButton;
    public TextMeshProUGUI titleLabel;       // Text (TMP) [0] — título
    public TextMeshProUGUI descriptionLabel; // Text (TMP) [1] — descrição
    public TextMeshProUGUI progressLabel;    // Text (TMP) [2] — "0/10"
    public TextMeshProUGUI costLabel;        // Text (TMP) (1) — custo (amarelo)

    private int upgradesBought = 0;
    public int UpgradesBought => upgradesBought;
    private int currentCostCoins;
    private Player player;

    void Start()
    {
        player = Player.instance;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        currentCostCoins = data.costCoins;
        LoadState();
        upgradeButton.onClick.AddListener(Buy);
        titleLabel.text = data.upgradeName;
        descriptionLabel.text = data.description;
        RefreshUI();
    }

    void Update() => RefreshUI();

    /// <summary>
    /// Tenta comprar o upgrade: aplica todos os modificadores de stat e dispara o evento especial.
    /// </summary>
    public void Buy()
    {
        if (upgradesBought >= data.maxUpgrades) return;
        if (player.coins < currentCostCoins || player.orbs < data.costOrbs) return;

        player.coins -= currentCostCoins;
        player.orbs -= data.costOrbs;
        upgradesBought++;
        currentCostCoins = Mathf.RoundToInt(currentCostCoins * 1.5f);

        if (purchaseSound != null) audioSource.PlayOneShot(purchaseSound);

        foreach (StatModifier mod in data.statModifiers)
            ApplyModifier(mod);

        SaveState();
        player.Salvar();
        onPurchased?.Invoke();
        RefreshUI();
    }

    private void SaveState()
    {
        PlayerPrefs.SetInt($"Upgrade_{data.upgradeName}_bought", upgradesBought);
        PlayerPrefs.SetInt($"Upgrade_{data.upgradeName}_cost", currentCostCoins);
        PlayerPrefs.Save();
    }

    private void LoadState()
    {
        upgradesBought = PlayerPrefs.GetInt($"Upgrade_{data.upgradeName}_bought", 0);
        currentCostCoins = PlayerPrefs.GetInt($"Upgrade_{data.upgradeName}_cost", data.costCoins);
    }

    private void ApplyModifier(StatModifier mod)
    {
        switch (mod.stat)
        {
            case StatModifier.StatType.Damage:
                player.damage = Calculate(player.damage, mod); break;
            case StatModifier.StatType.MaxHealth:
                player.maxHealth = (int)Calculate(player.maxHealth, mod); break;
            case StatModifier.StatType.MaxMana:
                player.maxMana = (int)Calculate(player.maxMana, mod); break;
            case StatModifier.StatType.Speed:
                player.speed = (int)Calculate(player.speed, mod); break;
            case StatModifier.StatType.ManaRegen:
                player.manaRegen = Calculate(player.manaRegen, mod); break;
            case StatModifier.StatType.VelocidadeTiro:
                player.velocidadeTiro = (int)Calculate(player.velocidadeTiro, mod); break;
            case StatModifier.StatType.Coins:
                player.coins = (int)Calculate(player.coins, mod); break;
            case StatModifier.StatType.Orbs:
                player.orbs = (int)Calculate(player.orbs, mod); break;
        }
    }

    private float Calculate(float current, StatModifier mod)
    {
        return mod.modifier == StatModifier.ModifierType.Flat
            ? current + mod.value
            : current * mod.value;
    }

    private void RefreshUI()
    {
        bool maxed = upgradesBought >= data.maxUpgrades;
        bool canAfford = player.coins >= currentCostCoins && player.orbs >= data.costOrbs;

        upgradeButton.interactable = !maxed && canAfford;
        progressLabel.text = maxed ? "MAX" : $"{upgradesBought}/{data.maxUpgrades}";
        costLabel.text = maxed ? "-" : $"{currentCostCoins}";
    }
}
