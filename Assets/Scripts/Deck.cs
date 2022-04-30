using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    //-----------------------------------------------------------------------------// 
    //------------------------------ ATRIBUTOS ------------------------------------// 

    //-------------------------------------// 
    //-------------ModeloCarta-------------// 

    [Header("Modelo Carta")]

    public GameObject defaultCarta;
    public Sprite[] faces;
    public int[] values = new int[52];
    public string[] nombres = new string[52];

    int cardIndex = 0;

    //-------------------------------------// 
    //-----------Player & Dealer-----------// 

    [Header("Player & Dealer")]

    public GameObject dealer;
    public GameObject player;

    //-------------------------------------// 
    //--------------Botones----------------// 

    [Header("Botones")]

    public Button hitButton;
    public Button stickButton;
    public Button playAgainButton;

    //----------------------------------// 
    //--------------HUD----------------// 

    [Header("Mensajes")]

    public Text finalMessage;
    public Text probMessage;
    public Text pointPlayer;
    public Text pointDealer;

    //----------------------------------// 
    //--------------Baraja--------------//

    [Header("Baraja")]
    public List<GameObject> BarajaInicial = new List<GameObject>();
    public List<GameObject> BarajaAleatoria = new List<GameObject>();
    public Stack<GameObject> BarajaAux = new Stack<GameObject>();

    //-----------------------------------------------------------------------------// 
    //------------------------------ MÉTODOS --------------------------------------// 

    private void Awake()
    {    
        InitCardValues();  
    }

    private void Start()
    {
        ShuffleCards();
        StartGame();        
    }

    private void Update()
    {
        pointPlayer.text = player.GetComponent<CardHand>().points.ToString();
    }

    //-----------------------------------------------------------// 
    //--------------------- InitCardValues ----------------------//

    private void InitCardValues()
    {
        //Asignar los valores de un palo
        int[] valoresPalo = new int[13];
        int valoresPaloIndex = 0;

        for (int i=0; i<=valoresPalo.Length-1 ; i++) 
        {
            if(i < 10) valoresPalo[i] = i + 1;
            else valoresPalo[i] = 10;
        }

        //Asgnar un valor a los elementos de values
        for (int i=0; i<=values.Length-1; i++)
        {
            values[i] = valoresPalo[valoresPaloIndex];

            valoresPaloIndex++;
            if (valoresPaloIndex == 13) valoresPaloIndex = 0;
        }

        //Baraja inicial como GameObject
        for (int i = 0; i <= faces.Length - 1; i++)
        {
            GameObject carta = Instantiate(defaultCarta);
            carta.name = nombres[i];
            carta.GetComponent<CardModel>().value = values[i];
            carta.GetComponent<CardModel>().front = faces[i];

            BarajaInicial.Add(carta);
        }

    }

    //-----------------------------------------------------------// 
    //--------------------- ShuffleCards ------------------------//

    private void ShuffleCards()
    {
        BarajaAleatoria.Clear();

        //Creación y copia de baraja en una baraja Auxiliar
        List<GameObject> BarajaAux = new List<GameObject>();
        foreach (GameObject carta in BarajaInicial)
        {
            BarajaAux.Add(carta);
        }

        //Baraja aleatoria
        for(int i=0; i<=51; i++)
        {
            int indiceRandom = Random.Range(0, BarajaAux.Count - 1);
            GameObject carta = BarajaAux[indiceRandom];
            BarajaAux.RemoveAt(indiceRandom);
            BarajaAleatoria.Add(carta);
        }
    }

    //-----------------------------------------------------------// 
    //----------------------- StartGame -------------------------//

    void StartGame()
    {
        for (int i = 0; i < 2; i++)
        {
            PushPlayer();
            PushDealer();
            //dealer.GetComponent<CardHand>().points = 17;
            //player.GetComponent<CardHand>().points = 18;

            /*TODO:
             * Si alguno de los dos obtiene Blackjack, termina el juego y mostramos mensaje
             */
            if (dealer.GetComponent<CardHand>().points == 21 && player.GetComponent<CardHand>().points == 21)
            {
                finalMessage.text = "Empate";
                finalMessage.color = Color.yellow;
                hitButton.interactable = false;
                stickButton.interactable = false;
                pointDealer.enabled = true;
                pointDealer.text = dealer.GetComponent<CardHand>().points.ToString();
                dealer.GetComponent<CardHand>().cards[0].GetComponent<CardModel>().ToggleFace(true);

            }
            else if (player.GetComponent<CardHand>().points==21 || dealer.GetComponent<CardHand>().points > 21)
            {
                finalMessage.text = "Has ganado";
                finalMessage.color = Color.green;
                hitButton.interactable = false;
                stickButton.interactable = false;
                pointDealer.enabled = true;
                pointDealer.text = dealer.GetComponent<CardHand>().points.ToString();
                dealer.GetComponent<CardHand>().cards[0].GetComponent<CardModel>().ToggleFace(true);
            }
            else if(dealer.GetComponent<CardHand>().points == 21 || player.GetComponent<CardHand>().points > 21)
            {
                finalMessage.text = "Has perdido";
                finalMessage.color = Color.red;
                hitButton.interactable = false;
                stickButton.interactable = false;
                pointDealer.enabled = true;
                pointDealer.text = dealer.GetComponent<CardHand>().points.ToString();
                dealer.GetComponent<CardHand>().cards[0].GetComponent<CardModel>().ToggleFace(true);
            }
            
        }
    }

    //-----------------------------------------------------------// 
    //---------------- CalculateProbabilities -------------------//

    private void CalculateProbabilities()
    {
        /*TODO:
         * Calcular las probabilidades de:
         * - Teniendo la carta oculta, probabilidad de que el dealer tenga más puntuación que el jugador
         * - Probabilidad de que el jugador obtenga entre un 17 y un 21 si pide una carta
         * - Probabilidad de que el jugador obtenga más de 21 si pide una carta          
         */
    }

    //-----------------------------------------------------------// 
    //----------------------- PushDealer -----------------------//

    void PushDealer()
    {
        /*TODO:
         * Dependiendo de cómo se implemente ShuffleCards, es posible que haya que cambiar el índice.
         */
        //dealer.GetComponent<CardHand>().Push(faces[cardIndex],values[cardIndex]);

        dealer.GetComponent<CardHand>().Push(BarajaAleatoria[cardIndex].GetComponent<CardModel>().front, 
            BarajaAleatoria[cardIndex].GetComponent<CardModel>().value);
        cardIndex++;        
    }

    //-----------------------------------------------------------// 
    //----------------------- PushPlayer ------------------------//

    void PushPlayer()
    {
        /*TODO:
         * Dependiendo de cómo se implemente ShuffleCards, es posible que haya que cambiar el índice.
         */
        //player.GetComponent<CardHand>().Push(faces[cardIndex], values[cardIndex]/*,cardCopy*/);

        player.GetComponent<CardHand>().Push(BarajaAleatoria[cardIndex].GetComponent<CardModel>().front,
           BarajaAleatoria[cardIndex].GetComponent<CardModel>().value);

        cardIndex++;
        CalculateProbabilities();
    }

    //-----------------------------------------------------------// 
    //--------------------------- Hit ---------------------------//

    public void Hit()
    {
        
        //Repartimos carta al jugador
        PushPlayer();

        if(player.GetComponent<CardHand>().points > 21)
        {
            finalMessage.text = "Has perdido";
            finalMessage.color = Color.red;
            hitButton.interactable = false;
            stickButton.interactable = false;
            pointDealer.enabled = true;
            pointDealer.text = dealer.GetComponent<CardHand>().points.ToString();
            dealer.GetComponent<CardHand>().cards[0].GetComponent<CardModel>().ToggleFace(true);
        }
        else if(player.GetComponent<CardHand>().points == 21)
        {
            finalMessage.text = "Has ganado";
            finalMessage.color = Color.green;
            hitButton.interactable = false;
            stickButton.interactable = false;
            pointDealer.enabled = true;
            pointDealer.text = dealer.GetComponent<CardHand>().points.ToString();
            dealer.GetComponent<CardHand>().cards[0].GetComponent<CardModel>().ToggleFace(true);
        }

        /*TODO:
         * Comprobamos si el jugador ya ha perdido y mostramos mensaje
         */      
    }

    //-----------------------------------------------------------// 
    //------------------------- Stand ---------------------------//

    public void Stand()
    {

        hitButton.interactable = false;
        stickButton.interactable = false;

        dealer.GetComponent<CardHand>().cards[0].GetComponent<CardModel>().ToggleFace(true);

        do
        {
            PushDealer();
        } while (dealer.GetComponent<CardHand>().points < 17);

        if (dealer.GetComponent<CardHand>().points == player.GetComponent<CardHand>().points)
        {
            finalMessage.text = "Empate";
            finalMessage.color = Color.yellow;
            hitButton.interactable = false;
            stickButton.interactable = false;
            pointDealer.enabled = true;
            pointDealer.text = dealer.GetComponent<CardHand>().points.ToString();

        }
        else if (dealer.GetComponent<CardHand>().points > 21)
        {
            finalMessage.text = "Has ganado";
            finalMessage.color = Color.green;
            hitButton.interactable = false;
            stickButton.interactable = false;
            pointDealer.enabled = true;
            pointDealer.text = dealer.GetComponent<CardHand>().points.ToString();
        }
        else if (player.GetComponent<CardHand>().points > dealer.GetComponent<CardHand>().points)
        {
            finalMessage.text = "Has ganado";
            finalMessage.color = Color.green;
            hitButton.interactable = false;
            stickButton.interactable = false;
            pointDealer.enabled = true;
            pointDealer.text = dealer.GetComponent<CardHand>().points.ToString();
        }
        else if (player.GetComponent<CardHand>().points < dealer.GetComponent<CardHand>().points)
        {
            finalMessage.text = "Has perdido";
            finalMessage.color = Color.red;
            hitButton.interactable = false;
            stickButton.interactable = false;
            pointDealer.enabled = true;
            pointDealer.text = dealer.GetComponent<CardHand>().points.ToString();
        }
        

        /*TODO:
         * Repartimos cartas al dealer si tiene 16 puntos o menos
         * El dealer se planta al obtener 17 puntos o más
         * Mostramos el mensaje del que ha ganado
         */

    }

    //-----------------------------------------------------------// 
    //---------------------- PlayAgain --------------------------//

    public void PlayAgain()
    {
        
        hitButton.interactable = true;
        stickButton.interactable = true;
        finalMessage.text = "";
        player.GetComponent<CardHand>().Clear();
        dealer.GetComponent<CardHand>().Clear();          
        cardIndex = 0;
        pointDealer.enabled = false;
        ShuffleCards();
        StartGame();
    }
    
}
