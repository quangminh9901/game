using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Tốc độ di chuyển của người chơi

    private bool isMoving = false; // Biến xác định xem player đang di chuyển hay không
    private Vector3 targetPosition; // Vị trí đích của player

    private ScoreManager scoreManager; // Tham chiếu tới ScoreManager

    public Color color1 = new Color(241f / 255f, 17f / 255f, 94f / 255f);
    public Color color2 = new Color(1f, 1f, 1f);
    
    public bool isGameOver = false;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>(); // Tìm đối tượng ScoreManager trong Scene
    }

    private void Update()
{
    

    if (isGameOver)
    {
        return;
    }
    
    if (Input.GetMouseButtonDown(0) && !isMoving)
    {
        MoveToSquare();
    }

    if (isMoving)
    {   
        
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

        if (transform.position == targetPosition)
        {
            isMoving = false;
            RandomSquareSpawner squareSpawner = FindObjectOfType<RandomSquareSpawner>();
            if (squareSpawner != null && squareSpawner.currentSquare != null)
            {
                bool isGameOver = CheckGameOver(squareSpawner.currentSquare);
                Destroy(squareSpawner.currentSquare);

                if (isGameOver)
                {
                    scoreManager.GameOver(); // Gọi phương thức game over từ ScoreManager
                }
                else
                {
                    scoreManager.IncreaseScore(1); // Gọi phương thức tăng điểm số từ ScoreManager
                }
            }
            squareSpawner.SpawnSquare();
        }
    }
}

private bool CheckGameOver(GameObject square)
{
    SpriteRenderer squareRenderer = square.GetComponent<SpriteRenderer>();
    float squareY = square.transform.position.y;

    if ((squareRenderer.color == color1 && (squareY == 1f || squareY == -3f)) ||
        (squareRenderer.color == color2 && (squareY == 3f || squareY == -1f)))
    {
        return true;
    }

    return false;
}


    private void MoveToSquare()
    {
        RandomSquareSpawner squareSpawner = FindObjectOfType<RandomSquareSpawner>();
        if (squareSpawner != null && squareSpawner.currentSquare != null)
        {
            Vector3 squarePosition = squareSpawner.currentSquare.transform.position;
            targetPosition = new Vector3(squarePosition.x, transform.position.y, transform.position.z);

            if (targetPosition.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); // Đảo chiều player khi di chuyển về bên trái
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f); // Khôi phục chiều player khi di chuyển về bên phải
            }

            isMoving = true;
            AudioManager.instance.PlaySound(AudioManager.instance.player, 1f);

        }
    }
}
