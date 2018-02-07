using UnityEngine;
using System.Collections;

public class EnemyRunner : MonoBehaviour
{

    public int lifeTime = 0;

    private int currentLifeTime = 0;

    private void OnEnable()
    {
        FMOD_BeatListener.Instance.OnBeat += OnBeat;
    }


    private void OnDisable()
    {
        FMOD_BeatListener.Instance.OnBeat -= OnBeat;
    }


    public void OnBeat(int i)
    {
        currentLifeTime++;

        if (currentLifeTime > lifeTime)
            OnKill();
    }


    public void OnKill()
    {

    }

}
