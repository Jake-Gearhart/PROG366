using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Program
    {
        static void Main()
        {
            Deck deck = new();
            deck.Populate();
            deck.Shuffle();

            Hand playerHand = new();
            Hand dealerHand = new();

            playerHand.AddCard(deck.Draw());
            playerHand.AddCard(deck.Draw());
            dealerHand.AddCard(deck.Draw());
            dealerHand.AddCard(deck.Draw());

            Console.WriteLine("Blackjack");
            Console.WriteLine("————————————————————————");
            Console.WriteLine($"Dealer's visible card: {dealerHand.cards[0]} ({dealerHand.cards[0].GetValue()})");

            void PlayerTurn()
            {
                Console.WriteLine($"Your hand: {playerHand}");
                Console.WriteLine("Would you like to Hit (H) or Stand (S)?");
                string choice = Console.ReadLine().ToUpper();

                if (choice == "H")
                {
                    playerHand.AddCard(deck.Draw());
                    Console.WriteLine($"You drew: {playerHand.cards[^1]}");

                    if (playerHand.GetValue() < 21)
                    {
                        PlayerTurn();
                    }
                }
            }
            PlayerTurn();

            if (playerHand.GetValue() > 21)
            {
                Console.WriteLine($"Your hand: {playerHand}");
                Console.WriteLine("Your cards total more than 21. You lose.");
                return;
            }

            void DealerTurn()
            {
                Console.WriteLine($"Dealer's hand: {dealerHand}");

                if (dealerHand.GetValue() < 17)
                {
                    dealerHand.AddCard(deck.Draw());
                    DealerTurn();
                }
            }
            DealerTurn();

            int playerValue = playerHand.GetValue();
            int dealerValue = dealerHand.GetValue();

            Console.WriteLine($"Your final hand: {playerHand}");
            Console.WriteLine($"Dealer's final hand: {dealerHand}");

            if (dealerValue == playerValue || (dealerValue > 21 && playerValue > 21))
            {
                Console.WriteLine("It's a tie!");
            }
            else if (dealerValue > 21 || playerValue > dealerValue)
            {
                Console.WriteLine("You win!");
            }
            else if (playerValue < dealerValue)
            {
                Console.WriteLine("Dealer wins!");
            }
        }
    }

    class Deck
    {
        readonly Random random = new();
        public List<Card> cards = new();

        public void Populate()
        {
            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
                {
                    cards.Add(new Card(suit, rank));
                }
            }
        }

        public void Shuffle()
        {
            List<Card> oldCards = new(cards);

            for (int i = 0; i < cards.Count; i++)
            {
                int index = random.Next(0, oldCards.Count);
                cards[i] = oldCards[index];
                oldCards.RemoveAt(index);
            }
        }

        public Card Draw()
        {
            Card card = cards[^1];
            cards.RemoveAt(cards.Count - 1);
            return card;
        }
    }

    class Card(Card.Suit suit, Card.Rank rank)
    {
        public Suit suit = suit;
        public Rank rank = rank;
        
        public enum Suit
        {
            Diamond,
            Heart,
            Spade,
            Club
        }

        public enum Rank
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }

        public int GetValue()
        {
            return this.rank switch
            {
                Rank.Two => 2,
                Rank.Three => 3,
                Rank.Four => 4,
                Rank.Five => 5,
                Rank.Six => 6,
                Rank.Seven => 7,
                Rank.Eight => 8,
                Rank.Nine => 9,
                Rank.Ten => 10,
                Rank.Jack => 10,
                Rank.Queen => 10,
                Rank.King => 10,
                Rank.Ace => 11,
                _ => 0,
            };
        }

        public override string ToString()
        {
            return $"{rank} of {suit}s";
        }
    }

    class Hand
    {
        public List<Card> cards { get; } = new();

        public void AddCard(Card card)
        {
            cards.Add(card);
        }

        public int GetValue()
        {
            int value = 0;
            int aceCount = 0;

            foreach (Card card in cards)
            {
                value += card.GetValue();
                if (card.rank == Card.Rank.Ace)
                {
                    aceCount++;
                }
            }

            // If the value exceeds 21 and there are Aces, count them as 1
            while (value > 21 && aceCount > 0)
            {
                value -= 10;
                aceCount--;
            }

            return value;
        }

        public override string ToString()
        {
            return $"{string.Join(", ", cards)} ({GetValue()})";
        }
    }
}