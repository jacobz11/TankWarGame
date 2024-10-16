using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameController gameController;

    public void UpdateScore()
    {
        scoreText.text = "Score: " + gameController.score.ToString() + "/" + EnemySpawner.numOfEnemies;
    }
}
