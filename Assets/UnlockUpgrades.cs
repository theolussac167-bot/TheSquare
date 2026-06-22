using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockUpgrades : MonoBehaviour
{
    public GameObject orb;
    public GameObject perfuracao;
    public GameObject vida;
    public GameObject elementos;

    private Upgrade upgrade;                  // ✅ declaração do campo
    private bool hasUnlocked = false;

    void Start()
    {
        GameObject damageUpgradeObj = GameObject.Find("DamageUpgrade1"); // ✅ atribuição dentro do método
        if (damageUpgradeObj != null)
            upgrade = damageUpgradeObj.GetComponent<Upgrade>();
    }

    void Update()
    {
        if (hasUnlocked) return;
        Debug.Log(upgrade.UpgradesBought);
        if (upgrade != null && upgrade.UpgradesBought >= 3)
        {
            Debug.Log("upgrades desbloqueados");
            orb.SetActive(true);
            perfuracao.SetActive(true);
            vida.SetActive(true);
            elementos.SetActive(true);
            hasUnlocked = true;
        }
    }
}
