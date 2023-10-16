using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameUIController : MonoBehaviour
{
    [SerializeField] Button increasePlayers;
    [SerializeField] Button decreasePlayers;
    [SerializeField] TMP_Text numberOfPlayers;
    [SerializeField] Button startGame;

    private int playersCount = 4;
    public int PlayersCount
    {
        get => playersCount;
        set
        {
            playersCount = value;
            if (playersCount > 4)
                playersCount = 1;
            if (playersCount < 1)
                playersCount = 4;
            numberOfPlayers.text = playersCount.ToString();
            PlayerPrefs.SetInt("PlayersCount", PlayersCount);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        numberOfPlayers.text = playersCount.ToString();

        increasePlayers.onClick.AddListener(() =>
        {
            PlayersCount++;
            
        });

        decreasePlayers.onClick.AddListener(() =>
        {
            PlayersCount--;
        });

        startGame.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("PlayersCount", PlayersCount);
            SceneManager.LoadScene("GameScene");
        });
    }
}
