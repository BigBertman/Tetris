using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    private string scoreDisplay = "";
    public Text scoreLabel;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.NewGame();
        scoreLabel.text = "Score: " + GameManager.Instance.scoreValue.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore()
    {
        if (Playfield.rows <= 1)
        {
            GameManager.Instance.scoreValue = GameManager.Instance.scoreValue + 40;
        }
        else if (Playfield.rows == 2)
        {
            GameManager.Instance.scoreValue = GameManager.Instance.scoreValue + 100;
        }
        else if (Playfield.rows == 3)
        {
            GameManager.Instance.scoreValue = GameManager.Instance.scoreValue + 300;
        }
        else if (Playfield.rows == 4)
        {
            GameManager.Instance.scoreValue = GameManager.Instance.scoreValue + 1200;
        }

        scoreDisplay = "Score: " + GameManager.Instance.scoreValue.ToString();
        scoreLabel.text = scoreDisplay;
    }
}
