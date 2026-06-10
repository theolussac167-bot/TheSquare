using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class DeathButtons : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    private Player player;
    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
        player.health = player.maxHealth;
    }
    public void OpenUpgrades()
    {
        SceneManager.LoadScene("UpgradeScene");
    }
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
