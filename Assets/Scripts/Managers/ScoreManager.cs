using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    SO_Score objectScore;

    [SerializeField] ScriptableObject scoreNum;


    [SerializeField] Text text;


    void Awake ()
    {
        score = 0;
    }


    void Update ()
    {
        text.text = "Score: " + score;
    }

    public void IncreaseScore(int score)
    {
        text.text = "Score: " + score;
    }
}
