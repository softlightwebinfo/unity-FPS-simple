using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int _itemsCollected = 0;
    public int maxItems = 0;
    private float _playerHP = 100f;
    public string labelText = "Recolecta los 4 items y ganate lla libertad";
    public bool showWinScreen = false;

    public int items
    {
        get { return _itemsCollected; }
        set
        {
            if (value >= maxItems)
            {
                labelText = "Has encontrado todos los items";
                showWinScreen = true;
                Time.timeScale = 0;
            }
            else
            {
                _itemsCollected = value;
                labelText = $"Item encontrado, te faltan {maxItems - _itemsCollected}";
            }
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(25, 25, 180, 25), $"Vida: {_playerHP}");
        GUI.Box(new Rect(25, 25 + 25 + 15, 180, 25), $"Items recogidos: {items}");
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 400, 50), labelText);
        if (showWinScreen)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200), "HAS GANADO!"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
            }
        }
    }
}
