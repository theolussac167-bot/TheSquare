using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public float life = 50f;
    public float speed = 3f;
    public int damage = 10;
    public int coinDrop = 5;
    public Slider healthBar;
    public Transform player;
    public EnemieSpawn enemySpawn;

    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("EnemyScript: Player nao encontrado na cena!");
        }

        Canvas canvas = GetComponentInChildren<Canvas>();
        if (canvas != null)
        {
            canvas.worldCamera = Camera.main;
        }

        healthBar.maxValue = life;
        healthBar.value = life;
        enemySpawn = FindObjectOfType<EnemieSpawn>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direcao = (player.position - transform.position).normalized;
            transform.Translate(direcao * speed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Aplica dano ao inimigo e verifica se morreu.
    /// </summary>
    public void TakeDamage(float dano)
    {
        life -= dano;
        healthBar.value = life;
        if (life <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        if (player != null)
        {
            Player playerComponent = player.GetComponent<Player>();
            if (playerComponent != null)
            {
                playerComponent.coins += coinDrop;
                playerComponent.UpdateCoinsUI();
            }
        }

        if (enemySpawn != null)
        {
            enemySpawn.waveEnemiesKilled += 1;
        }

        Destroy(gameObject);
    }
}
