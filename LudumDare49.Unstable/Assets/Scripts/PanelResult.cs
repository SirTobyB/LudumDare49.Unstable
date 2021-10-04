using System;
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
                description.gameObject.GetComponent<Text>().text = "Hurrey! You did it! People are happily embracing themeselves, bees are buzzing, and the sun is sparkling in the clear water. (Of course, with only pleasant temperature which is harmless for climate). You brought stability back to environment without sacrificing the satisfaction of the four players. Congratulations!";
                effects.gameObject.GetComponent<Text>().text = "Thanks for playing!" + Environment.NewLine + Environment.NewLine + 
                                                               "programming and design" + Environment.NewLine + "Tobi (SirTobyB)" + Environment.NewLine + Environment.NewLine +
                                                               "idea and writing" + Environment.NewLine + "Kathi";
                pollution.gameObject.GetComponent<Text>().text = "Asset Credits" + Environment.NewLine + Environment.NewLine +
                                                                 "Intro Music: Beyond The Star - Steve Oxen (https://www.FesliyanStudios.com Background Music)" + Environment.NewLine +
                                                                 "Background Music: Moonshine - Ketsa" + Environment.NewLine +
                                                                 "Background Image: mihacreative (AdobeStock #403313708)";
            }
            else
            {
                headline.gameObject.GetComponent<Text>().text = "Spiel gewonnen!";
                description.gameObject.GetComponent<Text>().text = "Hurra! Du hast es geschafft! Die Menschen liegen sich freudestrahlend in den Armen, die Bienchen summen und die Sonne spiegelt sich im glitzernd klaren Wasser. (Natürlich nur mit angenehmen, klimaunschädlichen Temperaturen). Dir ist es gelungen die Stabilität der Umwelt wieder herzustellen, ohne die Zufriedenheit der vier Akteure zu opfern. Glückwunsch!";
                effects.gameObject.GetComponent<Text>().text = "Vielen Dank fürs Spielen!" + Environment.NewLine + Environment.NewLine +
                                                               "Programmierung und Design" + Environment.NewLine + "Tobi (SirTobyB)" + Environment.NewLine + Environment.NewLine +
                                                               "Idee und Texte" + Environment.NewLine + "Kathi";
                pollution.gameObject.GetComponent<Text>().text = "Asset Credits" + Environment.NewLine + Environment.NewLine +
                                                                 "Intro Musik: Beyond The Star - Steve Oxen (https://www.FesliyanStudios.com Background Music)" + Environment.NewLine +
                                                                 "Hintergrund Musik: Moonshine - Ketsa" + Environment.NewLine +
                                                                 "Hintergrund Bild: mihacreative (AdobeStock #403313708)";
            }

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
                effects.gameObject.GetComponent<Text>().text = "Thanks for playing!" + Environment.NewLine + Environment.NewLine +
                                                               "programming and design" + Environment.NewLine + "Tobi (SirTobyB)" + Environment.NewLine + Environment.NewLine +
                                                               "idea and writing" + Environment.NewLine + "Kathi";
                pollution.gameObject.GetComponent<Text>().text = "Asset Credits" + Environment.NewLine + Environment.NewLine +
                                                                 "Intro Music: Beyond The Star - Steve Oxen (https://www.FesliyanStudios.com Background Music)" + Environment.NewLine +
                                                                 "Background Music: Moonshine - Ketsa" + Environment.NewLine +
                                                                 "Background Image: mihacreative (AdobeStock #403313708)";
            }
            else
            {
                headline.gameObject.GetComponent<Text>().text = "Spiel verloren!";
                description.gameObject.GetComponent<Text>().text = cause;
                effects.gameObject.GetComponent<Text>().text = "Vielen Dank fürs Spielen!" + Environment.NewLine + Environment.NewLine +
                                                               "Programmierung und Design" + Environment.NewLine + "Tobi (SirTobyB)" + Environment.NewLine + Environment.NewLine +
                                                               "Idee und Texte" + Environment.NewLine + "Kathi";
                pollution.gameObject.GetComponent<Text>().text = "Asset Credits" + Environment.NewLine + Environment.NewLine +
                                                                 "Intro Musik: Beyond The Star - Steve Oxen (https://www.FesliyanStudios.com Background Music)" + Environment.NewLine +
                                                                 "Hintergrund Musik: Moonshine - Ketsa" + Environment.NewLine +
                                                                 "Hintergrund Bild: mihacreative (AdobeStock #403313708)";
            }

            gameObject.SetActive(true);
            return;
        }

        gameState.StartRound(gameState.Game, false);

        gameObject.SetActive(false);
    }
}
