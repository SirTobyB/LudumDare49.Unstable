using Assets.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelDecision : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int DecisionID;
    public Decision CurrentDecision;

    private GameObject _camera;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentDecision == null)
            return;

        GameState gameState = _camera.GetComponent(typeof(GameState)) as GameState;

        var description = gameObject.transform.GetChild(0);
        var costs = gameObject.transform.GetChild(1);

        if (gameState.Game.Language == "en")
        {
            description.gameObject.GetComponent<Text>().text = CurrentDecision.DescriptionEN;
            costs.gameObject.GetComponent<Text>().text = "Costs: " + CurrentDecision.Costs.ToStringMoney(gameState.Game.Currency, gameState.Game.Language);
        }
        else
        {
            description.gameObject.GetComponent<Text>().text = CurrentDecision.DescriptionDE;
            costs.gameObject.GetComponent<Text>().text = "Kosten: " + CurrentDecision.Costs.ToStringMoney(gameState.Game.Currency, gameState.Game.Language);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CurrentDecision != null)
        {
            GameState gameState = _camera.GetComponent(typeof(GameState)) as GameState;

            gameState.MakeDecision(gameState.Game, CurrentDecision);

            gameObject.SetActive(false);
            // Debug.Log(gameState.Game.CountryName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // TODO: Change colour
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // TODO: Change colour
    }
}
