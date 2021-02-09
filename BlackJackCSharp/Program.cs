using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*************************************************************************
 * 
 *  Daniel Moreno
 *  daniel.moreno.garcia88@gmail.com
 *  https://github.com/danielmorenog88
 * 
 * 
 * 
 * ************************************************************************/

namespace BlackjackCSharp
{
    class Program
    {

        static void Main(string[] args)
        {


            int winScore = 0;
            int loseScore = 0;
            bool keepPlaying = true;
  
          

            do
            {

                 List<Card> deck = CreateDeck();


                // Test case
                

                //string[] symbolArray = { "H", "D", "S", "C" };
                //string[] numberArray = { "8", "A"};

                //foreach (string symbol in symbolArray)
                //{
                //    foreach (string number in numberArray)
                //    {
                //        Card newCard = new Card();
                //        newCard.symbol = symbol;
                //        newCard.number = number;
                         
                //        newCard.value =  (number == "A") ? 1 : int.Parse(number);

                //        deck.Insert(0,newCard);
                //    }  
                //}
                 
                // 

                List<Card> dealerHand = new List<Card>();
                List<Card> playerHand = new List<Card>();

                char playerOption;
                int playerScore = 0;
                int dealerScore = 0;
                bool endGame = false;

                PrintGuide();
                Console.WriteLine("\nWINS: " + winScore.ToString() + "  |  LOSES: " + loseScore.ToString());
                Console.WriteLine("\n-----------------------------------------------" + "\n");

                //Draw player's first two cards.
                DrawCard(deck, playerHand, 2);
                playerScore = Score(playerHand, false, false);

                //Blackjack at first hand
                if (playerScore == 21)
                {
                    winScore += 1;
                    endGame = true;
                }

                //Draw dealer's first two cards. Hiding the second card
                DrawCard(deck, dealerHand, 2);
                Print(dealerHand, true, false);


                //Rounds iterations
                while (!endGame)
                {
                    Console.WriteLine("-----------------------------------------------" + "\n");
                    Console.WriteLine("\nWould you like to hit?\n\n - Press 'Y' to Hit\n - Press 'N' to stay\n Press ESC key to exit.");

                    playerOption = Console.ReadKey().KeyChar;


                    switch (playerOption.ToString().ToUpper())
                    {
                        //Hit card
                        case "Y":

                            Console.Clear();
                            PrintGuide();
                            Console.WriteLine("\nWINS: " + winScore.ToString() + "  |  LOSES: " + loseScore.ToString());
                            Console.WriteLine("\n-----------------------------------------------" + "\n");
                            DrawCard(deck, playerHand, 1);
                            playerScore = Score(playerHand, false, false);

                            Print(dealerHand, true, false);

                            if (21 == playerScore)
                            {
                                endGame = true;
                                winScore += 1;

                                Console.WriteLine("\n************* Player wins round. *************\n");
                            }
                            else if (playerScore >= 21)
                            {
                                endGame = true;
                                loseScore += 1;

                                Console.WriteLine("\n\n************* Bust! You lose this round. *************\n");
                            }
                            break;

                        // Stay
                        case "N":

                            dealerScore = Score(dealerHand, true, true);


                            while (dealerScore < 17)
                            {
                                Console.WriteLine("\n Dealer hits...\n");
                                DrawCard(deck, dealerHand, 1);

                                dealerScore = Score(dealerHand, true, true);

                                if (dealerScore > 21)
                                {
                                    endGame = true;
                                    winScore += 1;
                                    Console.WriteLine("\n\n*************Dealer busted! You win this round.*************\n");

                                }

                            }

                            //Check result if dealer didn't bust.
                            if (!endGame)
                            {
                                endGame = true;

                                if (dealerScore > playerScore)
                                {
                                    loseScore += 1;
                                    Console.WriteLine("\n\n************* Dealer wins this round. *************\n");
                                }
                                else if (dealerScore == playerScore)
                                {
                                    Console.WriteLine("\n\n************* Push. No winners this round....*************\n");
                                }
                                else if (dealerScore < playerScore)
                                {
                                    winScore += 1;
                                    Console.WriteLine("\n\n************* You win this round....*************\n");
                                }
                            }

                            break;
 

                        default:
                            Console.WriteLine("\nNot a valid option. Please enter 'y' to Hit or 'n' to Stay. Press ESC key to exit.\n");
                            break;
                    }
                }


                //Ask for a next round.
                Console.WriteLine("-----------------------------------------------" + "\n");
                Console.WriteLine("\nNext round?\n\n - Press 'Y' to keep playing.\n - Press any other key to exit.");

                playerOption = Console.ReadKey().KeyChar;

                keepPlaying = playerOption.ToString().ToUpper() == "Y" ? true : false;
                Console.Clear();
            } while (keepPlaying);

            Environment.Exit(-1);
        }

        //Print game guide
        private static void PrintGuide()
        {
            Console.WriteLine("\n************* Let's play Blackjack! *************" + "\n\n");
            Console.WriteLine("Suits symbols: " + "\n");
            Console.WriteLine("C: Clubs\nD: Diamonds\nH: Hearts\nS: Spades" + "\n");
            Console.WriteLine("-----------------------------------------------" + "\n");
        }

        //Creates deck as 4 suits, A,2-10,J,Q,K 
        private static List<Card> CreateDeck()
        {

            List<Card> deck = new List<Card>();


            string[] symbolArray = { "H", "D", "S", "C" };

            foreach (string symbol in symbolArray)
            {
                string cardNumber = "";
                int cardValue = 0;

                for (int i = 1; i < 14; i++)
                {
                    Card newCard = new Card();

                    newCard.symbol = symbol;

                    switch (i)
                    {
                        case 1:
                            cardNumber = "A";
                            cardValue = 1;
                            break;
                        case 11:
                            cardNumber = "J";
                            cardValue = 10;
                            break;
                        case 12:
                            cardNumber = "Q";
                            cardValue = 10;
                            break;
                        case 13:
                            cardNumber = "K";
                            cardValue = 10;
                            break;
                        default:
                            cardNumber = i.ToString();
                            cardValue = i;
                            break;
                    }

                    newCard.number = cardNumber;
                    newCard.value = cardValue;

                    deck.Add(newCard);

                    //   Console.WriteLine(newCard.symbol + newCard.number.ToString() + " " + newCard.value) ; 
                }
            }

            Shuffle(deck);

            return deck;

        }

        private static void DrawCard(List<Card> deck, List<Card> playerHand, int number)
        {
            for (int i = 0; i < number; i++)
            {
                playerHand.Add(deck.First());
                deck.Remove(deck.First());
            }
        }

        //Shuffles the deck before playing 
        public static void Shuffle(List<Card> list)
        {
            Random rnd = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int i = rnd.Next(n + 1);
                Card value = list[i];
                list[i] = list[n];
                list[n] = value;
            }
        }

        //Display cards within hands
        public static void Print(List<Card> hand, bool dealerHand, bool endGame)
        {

            int totalcardsValue = 0;
            bool hideCards = false;
            string handOwner = dealerHand ? "Dealer" : "Player";
            string cards = "";

            Console.WriteLine("\n\n**************** " + handOwner + " 's hand" + "****************");

            foreach (Card item in hand)
            {

                totalcardsValue += item.value;

                cards = cards + (!hideCards ? item.symbol + item.number : "XX") + "  ";

                if (dealerHand && (!endGame))
                {
                    hideCards = true;
                }

            }

            Console.WriteLine(cards);

        }

        //Calculates score by hand
        public static int Score(List<Card> hand, bool dealerHand, bool endGame)
        {
            int score = 0;
            bool gotAce = false;
            string handOwner = dealerHand ? "Dealer" : "Player";

            foreach (Card item in hand)
            {

                score += item.value;
                if (item.number == "A")
                {
                    gotAce = true;
                }

            }

            if (gotAce)
            {
                if ((score + 10) <= 21)
                {
                    score += 10;
                }
            }

            // Print hand
            Print(hand, dealerHand, endGame);

            // Print score   
            if ((dealerHand && endGame) || !dealerHand)
            {
                Console.WriteLine("\n" + handOwner + " score: " + score.ToString());
            }

            //Blackjack at first deal
            if (!dealerHand)
            {
                if (score == 21 && hand.Count == 2)
                {
                    Console.WriteLine("\n************* B l a c k j a c k *************\n");
                    Console.WriteLine("\n\n************* You win this round....*************\n");
                }

            }

            return score;
        }

    }

    //Card structure
    public class Card
    {
        public string symbol;
        public string number;
        public int value;

    }
}
