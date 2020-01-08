using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Image _livesDisplay;

    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Sprite[] _liveSprites;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0.ToString();
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("Game Manager is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onScoreUpdate(int score)
    {
        _scoreText.text = "Score: " + score.ToString();
    }

    public void onLivesUpdate(int currentLives)
    {
        _livesDisplay.sprite = _liveSprites[currentLives];
        if (currentLives == 0)
        {
            StartCoroutine(onGameOver());
        }
    }

    private IEnumerator onGameOver()
    {
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
        }
        
    }
}
