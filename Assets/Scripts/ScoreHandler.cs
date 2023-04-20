using UnityEngine;

public class ScoreHandler 
{
    private static string key = "Score";

    private static int score;
    
    public static int Score
    {
        set {
            score = value > PlayerPrefs.GetInt(key) ? value : PlayerPrefs.GetInt(key);
            PlayerPrefs.SetInt(key, score);
        }
        get { return PlayerPrefs.GetInt(key); }
    }
}
