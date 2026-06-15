using UnityEngine;
using TMPro;

public class UpgradeSceneTexts : MonoBehaviour
{
    private Player player;
    public TextMeshProUGUI coinsText;

    void Start()
    {
        player = Player.instance;
    }

    void Update()
    {
        if (player == null) return; // safety check
        coinsText.text = player.coins.ToString();
    }
}
