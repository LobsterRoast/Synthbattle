using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI singleton;
    private float _health = 100;
    private float _score = 0;
    private static float _high_score = 0;
    private bool high_score_initialized = false;
    private TextMeshProUGUI health_text;
    private TextMeshProUGUI score_text;
    private TextMeshProUGUI high_score_text;
    public float health
    {
        get
        {
            return _health;
        }

        set 
        { 
            _health = value;
            health_text.SetText("Health: " + _health);
        }
    }
    public float high_score
    {
        get
        {
            return _high_score;
        }
        set
        {
            _high_score = value;
            high_score_text.SetText("High Score: " + _high_score);
        }
    }
    public float score
    {
        get
        {
            return _score;
        }

        set 
        {
            _score = value;
            score_text.SetText("Score: " + _score);
            if (_score > _high_score)
            {
                high_score = _score;
            }
        }
    }


    public void LazyInit()
    {
        health_text = GameObject.FindWithTag("Health").GetComponent<TextMeshProUGUI>();
        score_text = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
        high_score_text = GameObject.FindWithTag("HighScore").GetComponent<TextMeshProUGUI>();
        high_score_text.SetText("High Score: " + high_score);
        health = 100;
        score = 0;
    }

    void Start()
    {
        singleton = this;
    }
}