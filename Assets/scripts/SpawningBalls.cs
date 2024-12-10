using UnityEngine;

public class SpawningBalls : MonoBehaviour
{
    [Header("Configurações de Spawn")]
    public GameObject ballPrefab;
    public int numberOfBalls = 100;
    public Vector3 spawnAreaSize = new Vector3(10, 10, 10);
    public Vector2 ballSizeRange = new Vector2(0.5f, 2f);

    [Header("Referências")]
    public ForceFieldManager forceFieldManager;
    public ResetAllBalls resetAllBalls;

    void Start()
    {
        if (forceFieldManager == null)
        {
            forceFieldManager = FindObjectOfType<ForceFieldManager>();
        }

        if (resetAllBalls == null)
        {
            resetAllBalls = FindObjectOfType<ResetAllBalls>();
        }

        SpawnBalls();
    }

    void SpawnBalls()
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            Vector3 randomPosition = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            GameObject newBall = Instantiate(ballPrefab, randomPosition, Quaternion.identity);

            float randomScale = Random.Range(ballSizeRange.x, ballSizeRange.y);
            newBall.transform.localScale = Vector3.one * randomScale;

            if (newBall.GetComponent<Rigidbody>() == null)
            {
                newBall.AddComponent<Rigidbody>().useGravity = false;
            }

            if (newBall.GetComponent<SphereCollider>() == null)
            {
                newBall.AddComponent<SphereCollider>();
            }

            if (newBall.GetComponent<MouseDragForceField>() == null)
            {
                newBall.AddComponent<MouseDragForceField>();
            }

            // Registra a bola nos sistemas
            forceFieldManager.RegisterBall(newBall);
            resetAllBalls.RegisterBall(newBall.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
