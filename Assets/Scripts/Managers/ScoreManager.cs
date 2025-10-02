using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] SO_Score scoreNum;

    [SerializeField] Text text;

    void Update ()
    {
        text.text = "Score: " + scoreNum.score;
    }

    public void IncreaseScore(int score)
    {
        text.text = "Score: " + score;
    }
}
