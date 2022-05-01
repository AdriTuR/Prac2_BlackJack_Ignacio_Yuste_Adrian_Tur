using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bet : MonoBehaviour
{
    //-----------------------------------------------------------------------------// 
    //------------------------------ ATRIBUTOS ------------------------------------// 

    [Header("Dinero")]
    public double dineroDisponible;
    public double dineroApostado;

    [Header("Botones apuestas")]
    public Button Plus10Button;
    public Button Plus100Button;
    public Button Plus1000Button;
    public Button Minus10Button;
    public Button Minus100Button;
    public Button Minus1000Button;
    public Button All_InButton;
    public Button ClearButton;
    public Button BetButton;

    [Header("Apuestas Texto")]
    public Text dineroDisponible_text;
    public Text dineroApostado_text;

    //-----------------------------------------------------------------------------// 
    //------------------------------ MÉTODOS --------------------------------------// 
    void Start()
    {
        dineroDisponible = 1000;
        dineroApostado = 0;

        this.gameObject.GetComponent<Deck>().hitButton.interactable = false;
        this.gameObject.GetComponent<Deck>().stickButton.interactable = false;
    }

    void Update()
    {
        dineroDisponible_text.text = dineroDisponible.ToString();
        dineroApostado_text.text = dineroApostado.ToString();
    }

    public void anyadir10()
    {
        if (dineroDisponible >= dineroApostado + 10) dineroApostado += 10;

    }

    public void anyadir100()
    {
        if (dineroDisponible >= dineroApostado + 100) dineroApostado += 100;
    }
    public void anyadir1000()
    {
        if (dineroDisponible >= dineroApostado + 1000) dineroApostado += 1000;
    }
    public void quitar10()
    {
        if (dineroApostado >= 10) dineroApostado -= 10;
    }
    public void quitar100()
    {
        if (dineroApostado >= 100) dineroApostado -= 100;
    }
    public void quitar1000()
    {
        if (dineroApostado >= 1000) dineroApostado -= 1000;
    }

    public void clearBet()
    {
        dineroApostado = 0;
    }

    public void AllIn()
    {
        dineroApostado = dineroDisponible;
    }

    public void apostar()
    {
        this.gameObject.GetComponent<Deck>().hitButton.interactable = true;
        this.gameObject.GetComponent<Deck>().stickButton.interactable = true;

        this.gameObject.GetComponent<Deck>().ShuffleCards();
        this.gameObject.GetComponent<Deck>().StartGame();

        desactivarBotonesApostar();

    }

    public void winBet()
    {
        dineroDisponible += 2 * dineroApostado;
        dineroApostado = 0;
    }

    public void loseBet()
    {
        dineroDisponible -= dineroApostado;
        dineroApostado = 0;
    }

    public void tieBet()
    {
        dineroApostado = 0;
    }

    public void desactivarBotonesApostar()
    {
        Plus10Button.interactable = false;
        Plus100Button.interactable = false;
        Plus1000Button.interactable = false;
        Minus10Button.interactable = false;
        Minus100Button.interactable = false;
        Minus1000Button.interactable = false;
        ClearButton.interactable = false;
        All_InButton.interactable = false;
    }

    public void activarBotonesApostar()
    {
        Plus10Button.interactable = true;
        Plus100Button.interactable = true;
        Plus1000Button.interactable = true;
        Minus10Button.interactable = true;
        Minus100Button.interactable = true;
        Minus1000Button.interactable = true;
        ClearButton.interactable = true;
        All_InButton.interactable = true;
    }
}
