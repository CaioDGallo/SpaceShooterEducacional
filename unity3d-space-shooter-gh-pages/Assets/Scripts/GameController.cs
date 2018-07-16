using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameController : Singleton<GameController> {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
    public int asteroids = 0;
    public List<GameObject> asteroidsInScene;

    public Text highscore;
    public Button restartButton;
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;
	private int score;
	public bool gameOver;
	private bool restart;
    public bool IsThereAProblem = false;
    public bool spawnProblem = true;
    public bool isProblemSolved = true;

    public TextMesh problemText;
    public GameObject rankingWindow;
    public Button button;

    public List<string> problemas;
    public List<string> respostas;
    public int problemaAtual;
    public string respostaAtual;

    void Start() {
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore ();
		StartCoroutine(SpawnWaves ());
        StartCoroutine(SpawnProblems());
    }

	void Update() {
		if (restart)
        {
            restartButton.gameObject.SetActive(true);
            rankingWindow.gameObject.SetActive(true);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene("Main");
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    IEnumerator SpawnWaves() {
		yield return new WaitForSeconds(startWait);
		while(!restart) {
			for (int i = 0; i < hazardCount; ++i) {
                if(asteroids == 0)
                {
                    AsteroidManager.Instance.acertou = false;
                }
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
                //Asteroid _hazard;
                asteroids++;
                if (IsThereAProblem)
                {
                    Debug.Log("Spawnou com resposta");
                    GameObject ast = Instantiate(hazard, spawnPosition, spawnRotation);
                    ast.GetComponentInChildren<Asteroid>().resposta = respostaAtual;
                    asteroidsInScene.Add(ast);
                    IsThereAProblem = false;
                }
                else
                {
                    asteroidsInScene.Add(Instantiate(hazard, spawnPosition, spawnRotation));
                }
				yield return new WaitForSeconds(spawnWait);
			}
			yield return new WaitForSeconds(waveWait);
			if (gameOver)
            {
                GameOver();
            }
        }
	}

    IEnumerator SpawnProblems()
    {
        if (spawnProblem)
        {
            if (isProblemSolved)
            {
                problemText.text = PickRandomProblem();
                isProblemSolved = false;
            }
        }

        while (!isProblemSolved)
        {
            yield return null;
        }

        StartCoroutine(SpawnProblems());
    }

    string PickRandomProblem()
    {
        Debug.Log(problemas[Random.Range(0, problemas.Count)]);
        problemaAtual = Random.Range(0, problemas.Count);
        string problema = problemas[problemaAtual];
        respostaAtual = respostas[problemaAtual];
        IsThereAProblem = true;
        return problema;
    }

    public void AddScore(int newScoreValue) {
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore() {
		scoreText.text = "Score: " + score;
	}

	public void GameOver() {
		gameOver = false;
		gameOverText.text = "Game Over";
        restartText.text = "Press 'R' for restart";
        restart = true;

        foreach (GameObject g in GameController.Instance.asteroidsInScene)
        {
            Destroy(g);
        }
    }

    public void SetRanking()
    {
        Debug.Log(score.ToString());
        if (score > PlayerPrefs.GetInt("pontos1"))
        {
            PlayerPrefs.SetInt("pontos3", PlayerPrefs.GetInt("pontos2"));
            PlayerPrefs.SetString("nome3", PlayerPrefs.GetString("nome2"));
            PlayerPrefs.SetInt("pontos2", PlayerPrefs.GetInt("pontos1"));
            PlayerPrefs.SetString("nome2", PlayerPrefs.GetString("nome1"));           

            PlayerPrefs.SetInt("Highscore", score);
            PlayerPrefs.SetInt("pontos1", score);
            PlayerPrefs.SetString("nome1", highscore.text);
            Debug.Log("Entrou 1");

        }
        else if (score > PlayerPrefs.GetInt("pontos2"))
        {
            PlayerPrefs.SetInt("pontos3", PlayerPrefs.GetInt("pontos2"));
            PlayerPrefs.SetString("nome3", PlayerPrefs.GetString("nome2"));

            PlayerPrefs.SetInt("pontos2", score);
            PlayerPrefs.SetString("nome2", highscore.text);
            Debug.Log("Entrou 2");
        }
        else if(score > PlayerPrefs.GetInt("pontos3"))
        {
            PlayerPrefs.SetInt("pontos3", score);
            PlayerPrefs.SetString("nome3", highscore.text);
            Debug.Log("Entrou 3");
        }

        button.gameObject.SetActive(false);
        
    }
}
