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
        //Establece 1000 euros al jugador
        dineroDisponible = 1000;
        dineroApostado = 0;

        //Deshabilitar botones del Juego
        this.gameObject.GetComponent<Deck>().hitButton.interactable = false;
        this.gameObject.GetComponent<Deck>().stickButton.interactable = false;
    }

    void Update()
    {
        dineroDisponible_text.text = dineroDisponible.ToString();
        dineroApostado_text.text = dineroApostado.ToString();
    }

    //-----------------------------------------------------------// 
    //--------------------- AnyadirApuesta ----------------------//

    public void anyadirApuestaDiez()
    {
        if (dineroDisponible >= dineroApostado + 10) dineroApostado += 10;

    }

    public void anyadirApuestaCien()
    {
        if (dineroDisponible >= dineroApostado + 100) dineroApostado += 100;
    }

    public void anyadirApuestaMil()
    {
        if (dineroDisponible >= dineroApostado + 1000) dineroApostado += 1000;
    }

    public void apostarTodo()
    {
        dineroApostado = dineroDisponible;
    }

    //-----------------------------------------------------------// 
    //-------------------- EliminarApuesta ----------------------//

    public void eliminarApuestaDiez()
    {
        if (dineroApostado >= 10) dineroApostado -= 10;
    }
    public void eliminarApuestaCien()
    {
        if (dineroApostado >= 100) dineroApostado -= 100;
    }
    public void eliminarApuestaMil()
    {
        if (dineroApostado >= 1000) dineroApostado -= 1000;
    }

    public void borrarApuesta()
    {
        dineroApostado = 0;
    }

    //-----------------------------------------------------------// 
    //-------------------- EmpezarApuesta ----------------------//

    public void apostarAndEmpezar()
    {

        //Habilitar botones del Juego
        this.gameObject.GetComponent<Deck>().hitButton.interactable = true;
        this.gameObject.GetComponent<Deck>().stickButton.interactable = true;

        //Barajar y empezar juego
        this.gameObject.GetComponent<Deck>().ShuffleCards();
        this.gameObject.GetComponent<Deck>().StartGame();

        //Deshabilitar botones de Apuestas
        desactivarBotonesApostar();

    }

    //-----------------------------------------------------------// 
    //---------------- Resultados de Apuesta --------------------//

    public void ganarApuesta()
    {
        dineroDisponible += 2 * dineroApostado;
        dineroApostado = 0;
    }

    public void perderApuesta()
    {
        dineroDisponible -= dineroApostado;
        dineroApostado = 0;
    }

    public void empatarApuesta()
    {
        dineroApostado = 0;
    }

    //-----------------------------------------------------------// 
    //------------ Control de botones de Apuesta ----------------//

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

    //-----------------------------------------------------------------------------// 
    //-----------------------------------------------------------------------------// 
}
