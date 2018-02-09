using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance;

    public int life;
    public int score;
    public int maxScore;
    public HUD hud;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int points)
    {
        this.score += points;

    }

    public void LooseLife()
    {
        life--;
    }

}
