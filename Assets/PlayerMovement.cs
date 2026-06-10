using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private static readonly string MainSceneName = "MainScene";

    public float speed = 8f;
    public float moveX { get; private set; }
    public float moveY { get; private set; }

    [Header("Bounds")]
    public float minX = -8f;
    public float maxX = 8f;
    public float minY = -4.5f;
    public float maxY = 4.5f;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Ativa o movimento apenas na MainScene.
    /// </summary>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        enabled = scene.name == MainSceneName;
        if (!enabled)
        {
            moveX = 0f;
            moveY = 0f;
        }
    }

    void Start()
    {
        enabled = SceneManager.GetActiveScene().name == MainSceneName;
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveX, moveY, 0f) * speed * Time.deltaTime;
        transform.Translate(movement);

        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
