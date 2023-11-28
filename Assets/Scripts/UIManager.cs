using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _ScoreText;

    [SerializeField]
    private Image _lives;

    [SerializeField]
    private Sprite[] _LiveSprite;

    [SerializeField]
    private Text _GameOverText;

    [SerializeField]
    private Text _RestartText;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {

        _ScoreText.text = "Score: " + 0;
        _GameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.Log("Game Manager is null");
        }
    }

    public void Score(int player_Score)
    {
          _ScoreText.text = "Score: " + player_Score.ToString();
    }

    public void Lives(int lives_curr)
    {
        _lives.sprite = _LiveSprite[lives_curr];
       
        if(lives_curr == 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        _gameManager.GameOver();
        _GameOverText.gameObject.SetActive(true);
        _RestartText.gameObject.SetActive(true);
    }

}
