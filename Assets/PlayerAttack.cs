using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    private static readonly string MainSceneName = "MainScene";

    public GameObject tiroPrefab;
    public Slider manaSlider;
    private Player player;
    private PlayerMovement playerMovement;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Reavalia o estado do script a cada mudanca de cena.
    /// </summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        enabled = scene.name == MainSceneName;
        if (enabled) InitializeComponents();
    }

    void Start()
    {
        enabled = SceneManager.GetActiveScene().name == MainSceneName;
        if (enabled) InitializeComponents();
    }

    private void InitializeComponents()
    {
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
        manaSlider = FindSlider("ManaBar");
        if (manaSlider != null)
        {
            manaSlider.maxValue = player.maxMana;
            manaSlider.value = player.mana;
        }
    }

    void Update()
    {
        if (Camera.main == null || player == null) return;

        bool clique = Input.GetMouseButtonDown(0);
        if (clique && player.mana >= 20f)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f;
            Vector2 direcao = (mousePos - transform.position).normalized;
            float angulo = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
            GameObject tiro = Instantiate(tiroPrefab, transform.position, Quaternion.Euler(0f, 0f, angulo));
            tiro.GetComponent<Rigidbody2D>().velocity = direcao * player.velocidadeTiro;
            player.mana -= 20;
            if (manaSlider != null) manaSlider.value = player.mana;
        }

        if (player.mana < player.maxMana)
        {
            bool isMoving = playerMovement.moveX != 0 || playerMovement.moveY != 0;
            float regenMultiplier = isMoving ? 1f : 1.4f;
            player.mana += player.manaRegen * regenMultiplier * Time.deltaTime;
            player.mana = Mathf.Min(player.mana, player.maxMana);
            if (manaSlider != null) manaSlider.value = player.mana;
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
