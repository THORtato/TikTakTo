using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text;
    public Button button;

    
}

[System.Serializable]
public class PlayerColor{
    public Color PanelColor;
    public Color TextColor;
}

public class GameController : MonoBehaviour
{
    public Text[] buttonList;
    private string playerSide;
    public GameObject gameOverPanel;
    public Text gameOverText;
    private int moveCount;
    public GameObject RestartButton;
    public GameObject StartInfo;

    public Player PlayerX;
    public Player PlayerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;

    void Awake()
    {
        gameOverPanel.SetActive(false);
        RestartButton.SetActive(false);
        moveCount = 0;
        SetGameControllerReferenceOnButtons();
    }

    void SetGameControllerReferenceOnButtons()
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().setGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        /*equivalent to
        if(playerSide == "X")
        {
            playerSide = "O";
        }
        else
        {
            playerSide = "X";
        }*/
        if(playerSide == "X")
        {
            SetPlayerColors(PlayerX, PlayerO);
        }else if(playerSide == "O")
        {
            SetPlayerColors(PlayerO, PlayerX);
        }
        else if(playerSide == null)
        {
            Debug.Log("Playerside Null");
        }
    }
    
    public void EndTurn()
    {
        moveCount++;
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if(buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if(buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if(buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if(buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if(buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if(buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if(moveCount >=9)
        {
            GameOver("draw");
            
        }
        else
        {
            ChangeSides();
        }
        
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        if (winningPlayer == "draw")
        {
            Debug.Log("DRAW");
            setGameOverText("It's a Draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            setGameOverText(winningPlayer + " Wins!");
        }
        gameOverPanel.SetActive(true);
        RestartButton.SetActive(true);
    }

    void setGameOverText(string value)
    {
        gameOverPanel.SetActive(true);  
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        StartInfo.SetActive(true);
        setPlayerButton(true);
        moveCount = 0;
        gameOverPanel.SetActive(false);
        RestartButton.SetActive(false);
        SetPlayerColorsInactive();
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
        

    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
            
        }
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.PanelColor; 
        newPlayer.text.color = activePlayerColor.TextColor; 
        oldPlayer.panel.color = inactivePlayerColor.PanelColor;
        oldPlayer.text.color = inactivePlayerColor.TextColor;
    }

    public void setStartingSide(string StartingSide)
    {
        playerSide = StartingSide;
        if(playerSide == "X")
        {
            SetPlayerColors(PlayerX, PlayerO);
        }else if(playerSide == "O")
        {
            SetPlayerColors(PlayerO, PlayerX);
        }
        startGame();
    }

    void startGame()
    {
        StartInfo.SetActive(false);
        setPlayerButton(false);
        SetBoardInteractable(true);
    }

    void setPlayerButton(bool toggle)
    {
        PlayerX.button.interactable = toggle;
        PlayerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive() { 
        PlayerX.panel.color = inactivePlayerColor.PanelColor; 
        PlayerX.text.color = inactivePlayerColor.TextColor; 
        PlayerO.panel.color = inactivePlayerColor.PanelColor;
        PlayerO.text.color = inactivePlayerColor.TextColor; 
    }


}
