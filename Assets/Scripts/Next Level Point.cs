
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelPoint : MonoBehaviour
{
    public string lvlName;  // Nome da próxima cena (nível)
    public AudioSource nextLvl;

    // Método que é chamado quando o jogador colide com o ponto de transição
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto que colidiu tem a tag "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameController.instance.totalScore == 550)
            {
                // Salva a pontuação antes de mudar de cena
                GameController.instance.SaveScore();

                // Carrega o lvl secreto
                SceneManager.LoadScene("secret_lvl");

                // Toca o som de próxima cena
                nextLvl.Play();
            } else
            {

                // Salva a pontuação antes de mudar de cena
                GameController.instance.SaveScore();
                GameController.instance.SaveGameTimer();

                // Carrega a próxima cena (nível) usando o nome fornecido
                SceneManager.LoadScene(lvlName);


                // Toca o som de próxima cena
                nextLvl.Play();
            }

        }
    }
}
