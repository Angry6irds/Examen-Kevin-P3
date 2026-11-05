using UnityEngine;

public class ChallengeTrigger : MonoBehaviour
{
    [SerializeField] private ChallengeTimer timer;
    [SerializeField] private bool finish;

    private void OnTriggerEnter(Collider other)
    {
        if (timer == null || other.GetComponentInParent<Movement>() == null)
            return;
        if (finish)
            timer.Complete();
        else
            timer.Begin();
    }
}
