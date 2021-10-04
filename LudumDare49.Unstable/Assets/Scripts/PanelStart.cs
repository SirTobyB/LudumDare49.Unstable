using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelStart : MonoBehaviour, IPointerClickHandler
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
        var headline = gameObject.transform.GetChild(0);
        var description = gameObject.transform.GetChild(1);

        var inputName = gameObject.transform.GetChild(2);
        var inputCountry = gameObject.transform.GetChild(3);
        var inputCurrency = gameObject.transform.GetChild(4);

        var nameLabel = gameObject.transform.GetChild(5);
        var countryLabel = gameObject.transform.GetChild(6);
        var currencyLabel = gameObject.transform.GetChild(7);

        GameState gameState = _camera.GetComponent(typeof(GameState)) as GameState;

        if (gameState.Game.Language == "en")
        {
            headline.gameObject.GetComponent<Text>().text = "Welcome";
            description.gameObject.GetComponent<Text>().text = "Goal of this game is to reduce CO2 emission about 40 % until 2030 without neglecting the satisfaction of the four players society, industry, agriculture and energy sector. Every year you have to make a desicion, which can have a positve or negative effect on the environment and the four players. Please consider, that every decision lower your budget. Of course, you are getting income tax, which is depending on the satisfaction rates of the four players. If you are running out of money or the satisfaction of one player sinks to 0, game is over immediately. If you are not able to reduce CO2 emission until 2030 about 40 %, you are losing the game, too." +
                                                               Environment.NewLine + Environment.NewLine + "Click on this grey window to continue and then select your decision.";

            nameLabel.gameObject.GetComponent<Text>().text = "Your name";
            countryLabel.gameObject.GetComponent<Text>().text = "Your country";
            currencyLabel.gameObject.GetComponent<Text>().text = "Currency symbol";
        }
        else
        {
            headline.gameObject.GetComponent<Text>().text = "Willkommen";
            description.gameObject.GetComponent<Text>().text = "Ziel des Spiels ist es die CO2-Emission bis 2030 um mindestens 40 % zu senken, ohne dabei die Zufriedenheit der vier Akteure Gesellschaft, Industrie, Landwirtschaft und Energiesektor aus den Augen zu verlieren. Jedes Jahr musst du über eine Maßnahme entscheiden, die sich sowohl positiv als auch negativ auf die Umwelt und auf die vier Akteure auswirken kann. Beachte darüber hinaus, dass jede Maßnahme deinen Haushalt belastet. Natürlich erhältst du aber auch Steuereinnahmen, die von den Zufriedenheitswerten der vier Akteure abhängen. Geht dir das Geld aus oder sinkt die Zufriedenheit eines Akteurs auf 0, ist das Spiel sofort vorbei. Gelingt es dir nicht bis 2030 die CO2-Emission um 40 % zu senken, hast du ebenfalls verloren." +
                                                               Environment.NewLine + Environment.NewLine + "Klicke auf dieses graue Fenster zum Fortfahren und wähle dann deine Maßnahme.";

            nameLabel.gameObject.GetComponent<Text>().text = "Dein Name";
            countryLabel.gameObject.GetComponent<Text>().text = "Dein Land";
            currencyLabel.gameObject.GetComponent<Text>().text = "Währung Symbol";
        }

        gameState.Game.PlayerName = inputName.gameObject.GetComponent<InputField>().text;
        gameState.Game.CountryName = inputCountry.gameObject.GetComponent<InputField>().text;
        gameState.Game.Currency = inputCurrency.gameObject.GetComponent<InputField>().text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);

        GameState gameState = _camera.GetComponent(typeof(GameState)) as GameState;

        gameState.StartRound(gameState.Game, true);
    }
}
