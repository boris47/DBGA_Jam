using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance;

    public float life;
    public float score;
    public float maxScore;
    public float maxLife;
    public HUD hud;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            AddScore(50);
        if (Input.GetKeyDown(KeyCode.O))
            LoseLife();
    }

    public void AddScore(float points)
    {
        this.score += points;
        hud.UpdateScoreBar(this.score / this.maxScore);

    }

    public void LoseLife()
    {
        life--;
        hud.UpdateLifeBar(this.life / this.maxLife);
    }

}
