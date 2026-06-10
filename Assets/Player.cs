using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private static readonly string MainSceneName = "MainScene";
    private static readonly float AutoSaveIntervalSeconds = 30f;

    public int coins = 0;
    public int orbs = 0;
    public float mana = 100f;
    public float health = 100f;
    public int sharpness = 1;
    public int maxHealth = 100;
    public int maxMana = 100;
    public int velocidadeTiro = 16;
    public int maxWave = 0;
    public int speed = 8;
    public float damage = 20f;
    public float manaRegen = 0.18f;

    private float contador;
    private Text coinsText;

    public static Player instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        Carregar();
        mana = maxMana;
        RefreshCoinsText();
    }

    /// <summary>
    /// Rebusca referencias de UI ao carregar uma nova cena.
    /// </summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == MainSceneName)
        {
            RefreshCoinsText();
            // Resetar posicao ao voltar para MainScene
            transform.position = Vector3.zero;
        }
    }

    /// <summary>
    /// Atualiza a referencia ao CoinsText na cena atual.
    /// </summary>
    public void RefreshCoinsText()
    {
        GameObject go = GameObject.Find("CoinsText");
        coinsText = go != null ? go.GetComponent<Text>() : null;
        if (coinsText != null) coinsText.text = "Coins: " + coins;
    }

    /// <summary>
    /// Atualiza o texto de coins na UI (chamado externamente).
    /// </summary>
    public void UpdateCoinsUI()
    {
        if (coinsText != null) coinsText.text = "Coins: " + coins;
    }

    public void Salvar()
    {
        PlayerData data = new PlayerData();
        data.coins = coins;
        data.orbs = orbs;
        data.sharpness = sharpness;
        data.maxHealth = maxHealth;
        data.maxMana = maxMana;
        data.speed = speed;
        data.maxWave = maxWave;
        data.velocidadeTiro = velocidadeTiro;
        data.damage = damage;
        data.manaRegen = manaRegen;

        SaveSystem.Salvar(data);
    }

    public void Carregar()
    {
        PlayerData data = SaveSystem.Carregar();
        if (data != null)
        {
            coins = data.coins;
            orbs = data.orbs;
            if (data.maxMana > 0) maxMana = data.maxMana;
            if (data.speed > 0) speed = data.speed;
            if (data.maxHealth > 0) maxHealth = data.maxHealth;
            if (data.velocidadeTiro > 0) velocidadeTiro = data.velocidadeTiro;
            sharpness = data.sharpness > 0 ? data.sharpness : sharpness;
            maxWave = data.maxWave;
            if (data.damage > 0) damage = data.damage;
            if (data.manaRegen > 0) manaRegen = data.manaRegen;
        }
    }

    void Update()
    {
        contador += Time.deltaTime;
        if (contador >= AutoSaveIntervalSeconds)
        {
            Salvar();
            contador = 0f;
        }
    }

    void OnApplicationQuit()
    {
        Salvar();
    }
}
