using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelResult : MonoBehaviour, IPointerClickHandler
{
    private GameObject _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameState gameState = _camera.GetComponent(typeof(GameState)) as GameState;

        bool won = gameState.IsGameWon(gameState.Game);

        if (won)
        {
            gameObject.SetActive(false);

            var headline = gameObject.transform.GetChild(0);
            var description = gameObject.transform.GetChild(1);
            var effects = gameObject.transform.GetChild(2);
            var pollution = gameObject.transform.GetChild(3);

            // TODO: Show Screen: You've won! With calculated points
            // TODO: Show credits

            if (gameState.Game.Language == "en")
            {
                headline.gameObject.GetComponent<Text>().text = "Game won!";
                description.gameObject.GetComponent<Text>().text = "You've archived the climate goal!";
            }
            else
            {
                headline.gameObject.GetComponent<Text>().text = "Spiel gewonnen!";
                description.gameObject.GetComponent<Text>().text = "Du hast das Klimaziel erreicht!";
            }

            effects.gameObject.GetComponent<Text>().text = "";
            pollution.gameObject.GetComponent<Text>().text = "";

            gameObject.SetActive(true);
            return;
        }

        string cause = "";
        bool lost = gameState.IsGameOver(gameState.Game, ref cause);

        if (lost)
        {
            gameObject.SetActive(false);
            
            var headline = gameObject.transform.GetChild(0);
            var description = gameObject.transform.GetChild(1);
            var effects = gameObject.transform.GetChild(2);
            var pollution = gameObject.transform.GetChild(3);

            // TODO: Show Screen: Game Over with stats and "try again" button

            if (gameState.Game.Language == "en")
            {
                headline.gameObject.GetComponent<Text>().text = "Game over!";
                description.gameObject.GetComponent<Text>().text = cause;
            }
            else
            {
                headline.gameObject.GetComponent<Text>().text = "Spiel verloren!";
                description.gameObject.GetComponent<Text>().text = cause;
            }

            effects.gameObject.GetComponent<Text>().text = "";
            pollution.gameObject.GetComponent<Text>().text = "";

            gameObject.SetActive(true);
            return;
        }

        gameState.StartRound(gameState.Game, false);

        gameObject.SetActive(false);
    }
}
