using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    public void Setup(int score)
    {
        gameObject.SetActive(true);    
        scoreText.text = score.ToString();
    }

    
}
