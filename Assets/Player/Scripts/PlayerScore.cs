using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int score;
    public int baseScorePerKill = 100;
    public float comboDuration = 2f;
    public int mobsPerRank = 2; //number of mobs per rank increase
    public float rankResetTime = 8.0f; //time until rank resets to F after no kills

    private int currentCombo = 0;
    private float comboTimer = 0f;
    private int currentRank = 0;
    private float timeSinceLastKill = 0f;
    private int scoreFromKills;

    public FPSController fpsController;

    //ui stuff
    public UIController uiController;

    private void Update()
    {
        comboTimer = comboTimer - Time.deltaTime;
        timeSinceLastKill = timeSinceLastKill + Time.deltaTime;
        RankCheck(currentRank);

        if (timeSinceLastKill >= rankResetTime)
        {
            currentRank = 0;
            timeSinceLastKill = 0f;
            ResetCombo();
        }
    }

    //call this method when an enemy is killed
    public void EnemyKilled(int enemyScore)
    {        
        currentCombo++;

        if (currentCombo % mobsPerRank == 0) //increase rank if enough kills
        {
            currentRank++;
        }      

        scoreFromKills = Mathf.RoundToInt(enemyScore * GetMultiplier());
        Debug.Log("yo");

        score =  score + scoreFromKills;

        comboTimer = comboDuration;
        timeSinceLastKill = 0f;

        // TODO: Play combo sound effect or visual effect
        //ui stuff
        uiController.DisplayText("ScoreText", "Score: " + score);

        RankCheck(currentRank);

        Debug.Log(score);
    }

    private void ResetCombo()
    {
        currentCombo = 0;
        RankCheck(currentRank);
        // TODO: Update UI to reset combo
    }

    private void RankCheck(int rank) 
    { 
        if (rank == 0) { uiController.SetFRank(); }

        if (rank == 1) { uiController.SetDRank(); }

        if (rank == 2) { uiController.SetCRank(); }

        if (rank == 3) { uiController.SetBRank(); }

        if (rank == 4) { uiController.SetARank(); }

        if (rank == 5) { uiController.SetSRank(); }
    }

    private float GetMultiplier()
    {
        //return multiplier based on current rank
        switch (currentRank)
        {
            case 0: return 0.5f; //F rank
            case 1: return 1.0f; //D rank
            case 2: return 1.5f; //C rank
            case 3: return 2.0f; //B rank
            case 4: return 2.5f; //A rank
            case 5: return 3.0f; //S rank
            default: return 1.0f; //Default multiplier
        }
    }
}
