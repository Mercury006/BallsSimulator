using UnityEngine;
using System.Collections.Generic;

public class ResetAllBalls : MonoBehaviour
{
    private List<Transform> ballTransforms = new List<Transform>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private bool isResetting = false;
    public float resetSpeed = 2f;

    void Start()
    {
        GameObject[] initialBalls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (var ball in initialBalls)
        {
            RegisterBall(ball.transform);
        }
    }

    public void RegisterBall(Transform ball)
    {
        if (!ballTransforms.Contains(ball))
        {
            ballTransforms.Add(ball);
            initialPositions.Add(ball.position);
            Debug.Log($"Bola {ball.name} registrada para reset.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isResetting = !isResetting;
            Debug.Log(isResetting ? "Reset suave ativado!" : "Reset suave desativado!");
        }

        if (isResetting)
        {
            for (int i = 0; i < ballTransforms.Count; i++)
            {
                if (ballTransforms[i] == null) continue;

                ballTransforms[i].position = Vector3.Lerp(
                    ballTransforms[i].position,
                    initialPositions[i],
                    resetSpeed * Time.deltaTime
                );
            }
        }
    }
}
