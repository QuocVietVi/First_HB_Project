using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    //public static UIManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = FindObjectOfType<UIManager>();
    //        }
    //        return instance;
    //    }
    //}

    private void Awake()
    {
        instance=this;
    }

    [SerializeField] Text scoreText;
    [SerializeField] Text highScoreText;


    public void SetScore(int score)
    {
        scoreText.text = score.ToString(); 
    }

    public void SetHighScore(int hiscore)
    {
        highScoreText.text = hiscore.ToString();
    }
}
