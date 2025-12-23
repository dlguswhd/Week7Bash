using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerState _currentState;

    void Start()
    {
        // 게임 시작 시 대기 상태로 설정
        ChangeState(new IdleState());
    }

    void Update()
    {
        // 게임 오버가 아닐 때만 상태 업데이트 실행
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    public void ChangeState(IPlayerState newState)
    {
        // 상태 교체 로직
        if (_currentState != null)
            _currentState.ExitState(this);

        _currentState = newState;
        _currentState.EnterState(this);
    }
}