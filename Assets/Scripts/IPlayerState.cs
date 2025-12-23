using UnityEngine;

// 1. 상태 인터페이스
public interface IPlayerState
{
    void EnterState(PlayerController player);
    void UpdateState(PlayerController player);
    void ExitState(PlayerController player);
}

// [1] 대기 상태 (Idle)
public class IdleState : IPlayerState
{
    public void EnterState(PlayerController player) => Debug.Log(">> [Idle] 대기 중...");
    public void ExitState(PlayerController player) { }

    public void UpdateState(PlayerController player)
    {
        if (Input.GetKey(KeyCode.W)) player.ChangeState(new WalkState());
        else if (Input.GetKeyDown(KeyCode.Space)) player.ChangeState(new JumpState());
        else if (Input.GetKeyDown(KeyCode.Z)) player.ChangeState(new AttackState());
        else if (Input.GetKeyDown(KeyCode.X)) player.ChangeState(new DieState());
    }
}

// [2] 걷기 상태 (Walk)
public class WalkState : IPlayerState
{
    public void EnterState(PlayerController player) => Debug.Log(">> [Walk] 걷는 중 (속도: 5)");
    public void ExitState(PlayerController player) { }

    public void UpdateState(PlayerController player)
    {
        if (Input.GetKeyUp(KeyCode.W)) 
        {
            player.ChangeState(new IdleState());
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            player.ChangeState(new RunState());
        }
        else if (Input.GetKeyDown(KeyCode.Space)) player.ChangeState(new JumpState());
        else if (Input.GetKeyDown(KeyCode.Z)) player.ChangeState(new AttackState());
        else if (Input.GetKeyDown(KeyCode.X)) player.ChangeState(new DieState());
    }
}

// [3] 달리기 상태 (Run)
public class RunState : IPlayerState
{
    public void EnterState(PlayerController player) => Debug.Log(">> [Run] 달리는 중! (속도: 10)");
    public void ExitState(PlayerController player) { }

    public void UpdateState(PlayerController player)
    {

        if (Input.GetKeyUp(KeyCode.W)) player.ChangeState(new IdleState());

        else if (Input.GetKeyUp(KeyCode.LeftShift)) player.ChangeState(new WalkState());
        
        else if (Input.GetKeyDown(KeyCode.Space)) player.ChangeState(new JumpState());
        else if (Input.GetKeyDown(KeyCode.X)) player.ChangeState(new DieState());
    }
}

// [4] 점프 상태 (Jump)
public class JumpState : IPlayerState
{
    private float jumpTimer = 0f;

    public void EnterState(PlayerController player)
    {
        Debug.Log(">> [Jump] 점프! (공중에 뜸)");
        jumpTimer = 0f;
    }
    public void ExitState(PlayerController player) => Debug.Log("<< [Jump] 착지함");

    public void UpdateState(PlayerController player)
    {
        jumpTimer += Time.deltaTime;
        if (jumpTimer >= 0.5f)
        {
            player.ChangeState(new IdleState());
        }
    }
}

// [5] 공격 상태 (Attack)
public class AttackState : IPlayerState
{
    private float attackDelay = 0f;

    public void EnterState(PlayerController player)
    {
        Debug.Log(">> [Attack] 공격!! (챙!)");
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(100);
        }
        attackDelay = 0f;
    }
    public void ExitState(PlayerController player) { }

    public void UpdateState(PlayerController player)
    {
        attackDelay += Time.deltaTime;
        if (attackDelay >= 0.2f)
        {
            player.ChangeState(new IdleState());
        }
    }
}

// [6] 사망 상태 (Die)
public class DieState : IPlayerState
{
    public void EnterState(PlayerController player)
    {
        Debug.Log(">> [Die] 으악! 사망했습니다.");
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
    public void ExitState(PlayerController player) { } 

    public void UpdateState(PlayerController player)
    {
        // 사망 상태에서는 아무 입력도 받지 않음 (로직 차단)
    }
}