using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSceneTexts : MonoBehaviour
{
    private Player player;
    public TextMeshProUGUI coinsText;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        coinsText.text = player.coins.ToString();
    }
}
