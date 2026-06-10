using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    private static readonly string MainSceneName = "MainScene";

    public Slider healthBar;
    private Player playerScript;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Reinicializa ou desativa o script a cada mudança de cena.
    /// </summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        enabled = scene.name == MainSceneName;
        if (enabled) InitializeComponents();
    }

    void Start()
    {
        enabled = SceneManager.GetActiveScene().name == MainSceneName;
        if (!enabled) return;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        playerScript = GetComponent<Player>();
        healthBar = FindSlider("HealthBar");
        if (healthBar != null)
        {
            healthBar.maxValue = playerScript.maxHealth;
            healthBar.value = playerScript.health;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (!enabled) return;
        if (other.CompareTag("Enemie"))
        {
            playerScript.health -= 0.3f;
            if (healthBar != null) healthBar.value = playerScript.health;
        }
    }

    void Update()
    {
        if (playerScript == null) return;
        if (playerScript.health <= 0)
        {
            playerScript.health = playerScript.maxHealth;
            SceneManager.LoadScene("DeathScene");
        }
    }

    /// <summary>
    /// Busca um Slider pelo nome do GameObject na cena.
    /// </summary>
    private static Slider FindSlider(string gameObjectName)
    {
        GameObject go = GameObject.Find(gameObjectName);
        return go != null ? go.GetComponent<Slider>() : null;
    }
}
