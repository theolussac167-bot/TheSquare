using UnityEngine;
using UnityEngine.UI;

public class EnemieSpawn : MonoBehaviour
{
    public GameObject InimigoNormalPrefab;
    public GameObject InimigoGrandePrefab;

    public int currentWave = 1;
    public int nextWave = 2;
    public int waveEnemiesKilled;
    public int waveEnemies;
    public Text waveText;

    private bool isSpawning = false;

    void Start()
    {
        SpawnWave();
    }

    void Update()
    {
        if (!isSpawning && waveEnemiesKilled >= waveEnemies && waveEnemies > 0)
        {
            ProximaWave();
        }
    }

    void SpawnWave()
    {
        isSpawning = true;
        waveEnemiesKilled = 0;
        waveEnemies = nextWave / 2;

        for (int i = 0; i < waveEnemies; i++)
        {
            float spawnRangeX = Random.Range(-8.65f, 8.65f);
            float spawnRangeY = Random.Range(-4.75f, 4.75f);

            float chance = Random.value;
            bool canSpawnBig = currentWave >= 5;
            GameObject prefab = (canSpawnBig && chance >= 0.8f) ? InimigoGrandePrefab : InimigoNormalPrefab;

            if (prefab == null)
            {
                Debug.LogError($"Prefab do inimigo nao esta atribuido no Inspector! (chance: {chance})");
                continue;
            }

            Instantiate(prefab, new Vector2(spawnRangeX, spawnRangeY), Quaternion.identity);
        }

        isSpawning = false;
    }

    /// <summary>
    /// Avanca para a proxima wave e atualiza o maxWave do jogador.
    /// </summary>
    public void ProximaWave()
    {
        currentWave++;
        nextWave++;

        Player player = Player.instance;
        if (player != null && currentWave > player.maxWave)
        {
            player.maxWave = currentWave;
        }

        if (waveText == null)
        {
            GameObject go = GameObject.Find("WaveText");
            if (go != null) waveText = go.GetComponent<Text>();
        }

        if (waveText != null)
        {
            waveText.text = "Wave: " + currentWave;
        }

        SpawnWave();
    }
}
