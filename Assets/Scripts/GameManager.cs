using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Score { get; private set; } = 0;
    public bool IsGameOver { get; private set; } = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddScore(int amount)
    {
        if (IsGameOver) return;
        Score += amount;
        Debug.Log($"[GameManager] 점수 획득! 현재 점수: {Score}");
    }

    public void GameOver()
    {
        IsGameOver = true;
        Debug.Log("[GameManager] 게임 오버! 더 이상 조작할 수 없습니다.");
    }
}