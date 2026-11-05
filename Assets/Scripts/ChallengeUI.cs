using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChallengeUI : MonoBehaviour
{
    [SerializeField] private ChallengeTimer timer;
    [SerializeField] private UnityEngine.UI.Slider timeSlider;
    [SerializeField] private TMP_Text timeLabel;
    [SerializeField] private TMP_Text objectiveLabel;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private UnityEngine.UI.Button winRestart;
    [SerializeField] private UnityEngine.UI.Button loseRestart;
    [SerializeField] private Movement movement;
    [SerializeField] private Shoot shoot;
    private int _displayedSeconds = -1;

    private void OnEnable()
    {
        timer.Started += ShowObjective;
        timer.TimeChanged += UpdateTime;
        timer.Finished += ShowResult;
        UpdateTime(timer.Remaining);
        ShowObjective();
    }

    private void OnDisable()
    {
        timer.Started -= ShowObjective;
        timer.TimeChanged -= UpdateTime;
        timer.Finished -= ShowResult;
    }

    private void UpdateTime(float remaining)
    {
        timeSlider.SetValueWithoutNotify(remaining / timer.Duration);
        int seconds = Mathf.CeilToInt(remaining);

        if (seconds == _displayedSeconds)
            return;

        _displayedSeconds = seconds;
        timeLabel.text = $"{seconds} s";
    }

    private void ShowObjective()
    {
        objectiveLabel.text = timer.HasStarted ? "Move the pillars. Reach the golden platform." : "Cross the green line to begin.";
    }

    private void ShowResult(bool won)
    {
        hud.SetActive(false);
        winPanel.SetActive(won);
        losePanel.SetActive(!won);
        movement.enabled = false;
        shoot.enabled = false;
        EventSystem.current.SetSelectedGameObject(won ? winRestart.gameObject : loseRestart.gameObject);
    }
}
