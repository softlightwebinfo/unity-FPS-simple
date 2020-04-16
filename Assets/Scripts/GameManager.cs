using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour, IManager
{
    [SerializeField]
    private int _itemsCollected = 0;
    public int maxItems = 0;
    private float _playerHP = 100f;
    public string labelText = "Recolecta los 4 items y ganate lla libertad";
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public string _state;

    private void Start()
    {
        this.Initialize();
    }

    public int items
    {
        get { return _itemsCollected; }
        set
        {
            if (value >= maxItems)
            {
                this.GameOver(true);
            }
            else
            {
                _itemsCollected = value;
                labelText = $"Item encontrado, te faltan {maxItems - _itemsCollected}";
            }
        }
    }

    public float playerHP
    {
        get { return _playerHP; }
        set
        {
            _playerHP = value;

            if (value <= 0)
            {
                this.GameOver(false);
            }
            else
            {
                labelText = "Ouch, me han dado...";
            }
        }
    }

    public string State { get => _state; set => _state = value; }

    public void Initialize()
    {
        State = "Manager inicializado";
        Debug.Log(State);
    }

    private void GameOver(bool gameWon)
    {
        labelText = gameWon ? "Has encontrado todos los items" : "Has muerto... Prueba otra vez!";
        showWinScreen = gameWon;
        showLossScreen = !gameWon;
        Time.timeScale = 0;
    }

    public string GetPlayerHpString()
    {
        return playerHP.ToString("N2");
    }

    private void ShowEndLevel(string text)
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 200), text))
        {
            Utilities.RestartLevel();
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(25, 25, 180, 25), $"Vida: {this.GetPlayerHpString()}");
        GUI.Box(new Rect(25, 25 + 25 + 15, 180, 25), $"Items recogidos: {items}");
        GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height - 50, 400, 50), labelText);
        if (showWinScreen)
        {
            this.ShowEndLevel("Enhorabuena, Has ganado!");
        }

        if (showLossScreen)
        {
            this.ShowEndLevel("GAME OVER!");
        }
    }
}
