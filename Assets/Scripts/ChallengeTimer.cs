using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ChallengeTimer : MonoBehaviour
{
    private enum ChallengeState { Waiting, Running, Won, Lost }

    [SerializeField, Min(1f)] private float duration = 60f;

    private float _remaining;
    private ChallengeState _state;

    public float Remaining => _state == ChallengeState.Waiting ? duration : _remaining;
    public float Duration => duration;
    public bool IsRunning => _state == ChallengeState.Running;
    public bool IsCompleted => _state == ChallengeState.Won;
    public bool HasStarted => _state != ChallengeState.Waiting;

    public event System.Action Started;
    public event System.Action<float> TimeChanged;
    public event System.Action<bool> Finished;

    public void Begin()
    {
        if (HasStarted)
            return;

        _remaining = duration;
        _state = ChallengeState.Running;
        Started?.Invoke();
        TimeChanged?.Invoke(_remaining);
    }

    public void Complete()
    {
        Finish(true);
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
            Restart();

        Tick(Time.deltaTime);
    }

    public void Tick(float deltaTime)
    {
        if (!IsRunning || deltaTime <= 0f)
            return;

        _remaining = Mathf.Max(0f, _remaining - deltaTime);
        TimeChanged?.Invoke(_remaining);

        if (_remaining == 0f)
            Finish(false);
    }

    private void Finish(bool won)
    {
        if (!IsRunning)
            return;

        _state = won ? ChallengeState.Won : ChallengeState.Lost;
        Finished?.Invoke(won);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
