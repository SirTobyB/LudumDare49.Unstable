using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        gameState.StartRound(gameState.Game, false);

        gameObject.SetActive(false);
    }
}
