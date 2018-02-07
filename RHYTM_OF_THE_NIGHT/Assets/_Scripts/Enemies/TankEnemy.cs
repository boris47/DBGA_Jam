
using UnityEngine;
using UnityEngine.UI;

// deve avere tot click da fare, fatti tutti crepa male
// ha un tempo massimo in beat

public class TankEnemy : MonoBehaviour
{
    // vita espressa in beat
    [Header("in beat")]
    public int lifeTime;
    // vita espressa in click
    [Header("in click")]
    public int lifeClick;

    private int currentLifeTime = 0;
    private int currentLifeClick = 0;


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



    public  void    OnKill()
    {

        // MALE

    }


    public void OnClick()
    {
        currentLifeClick++;
        if (currentLifeClick > lifeClick)
            OnKill();
    }


}
