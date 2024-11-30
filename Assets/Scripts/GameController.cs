using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    public int totalScore;  // A pontuação total que será mantida
    public int scoreDead;
    public float timeRemaining = 0;
    public bool timeIsRunning = true;
    public Text scoreText;
    public Text scoreDeadText;  // Referência ao texto que exibe a pontuação
    public GameObject gameOver;  // Referência ao painel de "game over"
    public GameObject Restart_Game;
    public AudioSource gameOverSound;
    public TMP_Text timeText;

    public static GameController instance;

    // Start é chamado uma vez, no início da execução do script
    void Start()
    {
        // Garantir que o GameController seja uma instância única
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);  // Garantir que só haja uma instância
        }

        // Carregar a pontuação salva ao iniciar a cena
        totalScore = PlayerPrefs.GetInt("TotalScore", 0);
        scoreDead = PlayerPrefs.GetInt("ScoreDead", 0);
        timeRemaining = PlayerPrefs.GetFloat("TimeRemaining", 0f);
        UpdateScoreDeadText();
        UpdateScoreText();  // Atualiza o texto da pontuação
    }

    // Update é chamado uma vez por frame, mas não estamos usando nesse caso
    void Update()
    {
        if (timeIsRunning)
        {
            if (timeRemaining >= 0)
            {
                timeRemaining += Time.deltaTime;
                DisplayTime(timeRemaining);
            }

            if (SceneManager.GetActiveScene().name == "final_lvl")
            {
                StopTimer();
                SaveGameTimer();
            }
        }
    }

    void DisplayTime (float timeToDisplay)
    {
        timeToDisplay +=1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    // Atualiza o texto da pontuação na tela
    public void UpdateScoreText()
    {
        scoreText.text = totalScore.ToString();  // Exibe a pontuação total no UI
    }
    public void UpdateScoreDeadText()
    {
        scoreDeadText.text = scoreDead.ToString();  // Exibe a pontuação total no UI
    }

    // Exibe o painel de "game over"
    public void ShowGameOver()
    {
        if (!gameOver.activeInHierarchy)
        {

        gameOver.SetActive(true);
        Restart_Game.SetActive(false);
        gameOverSound.Play();
        IncrementScoreDead();
        

        }
        
    }

    public void StartTimer() 
    { 
        timeIsRunning = true;
        timeRemaining = 0;
    } 
    public void StopTimer() 
    { 
        timeIsRunning = false; 
    }
    // Método para reiniciar o jogo e carregar a próxima fase
    public void RestartGame(string lvlName)
    {
        // Salvar a pontuação atual antes de reiniciar ou carregar a próxima fase
        //SaveScore();

        // Carregar a cena da próxima fase
        SceneManager.LoadScene(lvlName);
        SaveGameTimer();
    }

    //------------------------------------------------Contador Pontos----------------------------------------------------
    // Método para salvar a pontuação no PlayerPrefs
    public void SaveScore()
    {
        PlayerPrefs.SetInt("TotalScore", totalScore);  // Salva a pontuação
        PlayerPrefs.Save();  // Garantir que os dados sejam salvos imediatamente
    }

    // Método para adicionar pontos à pontuação
    public void IncrementScore(int amount)
    {
        totalScore += amount;  // Incrementa a pontuação
        UpdateScoreText();  // Atualiza o texto da pontuação na tela
        SaveScore();  // Salva a nova pontuação
    }

    public void ResetScore()
    {
        totalScore = 0;  // Reseta a pontuação
        UpdateScoreText();  // Atualiza o UI
        PlayerPrefs.SetInt("TotalScore", 0);  // Salva a pontuação como 0 no PlayerPrefs
        PlayerPrefs.Save();  // Garantir que os dados sejam salvos imediatamente
    }

    //------------------------------------------------Contador de mortes----------------------------------------------------
    public void SaveScoreDead()
    {
        PlayerPrefs.SetInt("ScoreDead", scoreDead); 
        PlayerPrefs.Save();  
    }
    // Método para adicionar pontos à pontuação
    public void IncrementScoreDead()
    {
        scoreDead++;
        UpdateScoreDeadText(); 
        SaveScoreDead();
    }

    public void ResetScoreDead()
    {
        scoreDead = 0;  
        UpdateScoreDeadText();
        PlayerPrefs.SetInt("ScoreDead", 0);  
        PlayerPrefs.Save();
    }

    public void SaveGameTimer() 
    { 
        PlayerPrefs.SetFloat("TimeRemaining", timeRemaining); 
        PlayerPrefs.Save();
    }
    public void ResetGameState() 
    { 
    ResetScore();
    ResetScoreDead(); 
    PlayerPrefs.SetFloat("TimeRemaining", 0); 
    PlayerPrefs.Save(); timeRemaining = 0; 
    DisplayTime(timeRemaining); 
    }
}
