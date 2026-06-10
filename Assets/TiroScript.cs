using UnityEngine;

public class TiroScript : MonoBehaviour
{
    private Player playerScript;

    void Start()
    {
        playerScript = GameObject.FindWithTag("Player").GetComponent<Player>();
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemie"))
        {
            other.GetComponent<EnemyScript>().TakeDamage(playerScript.damage);
            Destroy(gameObject);
        }
    }
}