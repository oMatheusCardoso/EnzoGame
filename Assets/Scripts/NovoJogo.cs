using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // Necessário para trabalhar com o sistema de UI (legado)

public class StartGameButton : MonoBehaviour
{
    public Button startButton;  // O botão que irá iniciar a fase
    public string levelName = "lvl_1";  // Nome da cena que você quer carregar

    public GameController gameController;  // Referência ao GameController para resetar a pontuação

    // Start é chamado quando o script é carregado
    void Start()
    {
        // Verifica se o botão foi atribuído
        if (startButton != null)
        {
            // Adiciona a função de carregamento de cena ao evento de clique do botão
            startButton.onClick.AddListener(StartLevel);
        }
        else
        {
            Debug.LogError("O botão não foi atribuído no Inspector!");
        }

        // Verifica se o GameController foi atribuído
        if (gameController == null)
        {
            Debug.LogError("O GameController não foi atribuído no Inspector!");
        }
    }

    // Função que será chamada quando o botão for clicado
    void StartLevel()
    {
        // Reseta todos os dados.
        if (gameController != null)
        {
            gameController.ResetGameState();
            gameController.StartTimer();
        }

        // Carrega a cena especificada no levelName
        SceneManager.LoadScene(levelName);
    }
}
