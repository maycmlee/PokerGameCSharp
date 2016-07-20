using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    class Program
    {
        enum HandType
        {
            HighCard,
            Pair,
            TwoPair,
            ThreeofAKind,
            Straight,
            Flush,
            FullHouse,
            FourofAKind,
            StraightFlush
        }

        static void Main(string[] args)
        {
            string[] handOne = GetHand("One");
            string[] handTwo= GetHand("Two");

            Card[] sortedCards = SortHand(handOne);
            Card[] sortedCardsTwo = SortHand(handTwo);

            /*
            Test logic of all the hands: 
            Console.WriteLine("");
            Console.WriteLine("StraightFlush: {0}", isStraightFlush(sortedCards));
            Console.WriteLine("Straight: {0}", isStraight(sortedCards));
            Console.WriteLine("Flush: {0}", isFlush(sortedCards));
            Console.WriteLine("Four of a Kind: {0}", isFourofAKind(sortedCards));
            Console.WriteLine("Three of a Kind: {0}", isThreeofAKind(sortedCards));
            Console.WriteLine("2 Pairs: {0}", isTwoPairs(sortedCards));
            Console.WriteLine("Pair: {0}", isPair(sortedCards));
            Console.WriteLine("Full House: {0}", isFullHouse(sortedCards));
            Console.WriteLine("HighCard: {0}", isHighCard(sortedCards));

            Console.WriteLine("");
            Console.WriteLine("StraightFlush: {0}", isStraightFlush(sortedCardsTwo));
            Console.WriteLine("Straight: {0}", isStraight(sortedCardsTwo));
            Console.WriteLine("Flush: {0}", isFlush(sortedCardsTwo));
            Console.WriteLine("Four of a Kind: {0}", isFourofAKind(sortedCardsTwo));
            Console.WriteLine("Three of a Kind: {0}", isThreeofAKind(sortedCardsTwo));
            Console.WriteLine("2 Pairs: {0}", isTwoPairs(sortedCardsTwo));
            Console.WriteLine("Pair: {0}", isPair(sortedCardsTwo));
            Console.WriteLine("Full House: {0}", isFullHouse(sortedCardsTwo));
            Console.WriteLine("HighCard: {0}", isHighCard(sortedCardsTwo));
            Console.WriteLine("");
            */

            HandType handTypeOne = EvaluateHand(sortedCards);
            HandType handTypeTwo = EvaluateHand(sortedCardsTwo);
            Console.WriteLine(handTypeOne);
            Console.WriteLine(handTypeTwo);
            Console.WriteLine(WinnerIs(handTypeOne, handTypeTwo));
            Console.ReadLine();
        }


        static string[] GetHand(string playerNum)
        {
            Console.WriteLine("Please enter Player {0}'s hand with the rank and suit (eg. 4H 5D KH JS 9H):", playerNum);
            string cards = Console.ReadLine();
            return cards.Split(' ');
        }

        static Card[] SortHand(string[] cards)
        {
            //Convert to Card instances
            Rank[] ranks = GetRank(cards);
            Suit[] suits = GetSuit(cards);

            Card[] handOfCards = new Card[5];
            for(int i = 0; i < 5; i++)
            {
                handOfCards[i] = new Card(ranks[i], suits[i]);
            }

            Array.Sort(handOfCards);
            return handOfCards;
        }

        #region Methods
        /// <summary>
        /// Evaluate Hand
        /// </summary>
        /// <param name="cards">The sorted cards of type Card</param>
        /// <returns></returns>
        static HandType EvaluateHand(Card[] cards)
        {
            if (isStraightFlush(cards) == true)
                return HandType.StraightFlush;
            else if (isFourofAKind(cards) == true)
                return HandType.FourofAKind;
            else if (isFullHouse(cards) == true)
                return HandType.FullHouse;
            else if (isFlush(cards) == true)
                return HandType.Flush;
            else if (isStraight(cards) == true)
                return HandType.Straight;
            else if (isThreeofAKind(cards) == true)
                return HandType.ThreeofAKind;
            else if (isTwoPairs(cards) == true)
                return HandType.TwoPair;
            else if (isPair(cards) == true)
                return HandType.Pair;
            else
                return HandType.HighCard;
        }

        static string WinnerIs(HandType handOne, HandType handTwo)
        {
            if (handOne > handTwo)
                return "The winner is PLAYER ONE!!";
            else if (handOne < handTwo)
                return "The winner is PLAYER TWO!!";
            else
                return "It's a tie, deal with it later";
        }

        #endregion

        #region Helper Methods
        static Rank[] GetRank(string[] cards)
        {
            Rank[] rankHand = new Rank[5];
            int count = 0;

            foreach (string card in cards)
            {
                if (card[0] == 'T')
                    rankHand[count] = Rank.ten;
                else if (card[0] == '2')
                    rankHand[count] = Rank.two;
                else if (card[0] == '3')
                    rankHand[count] = Rank.three;
                else if (card[0] == '4')
                    rankHand[count] = Rank.four;
                else if (card[0] == '5')
                    rankHand[count] = Rank.five;
                else if (card[0] == '6')
                    rankHand[count] = Rank.six;
                else if (card[0] == '7')
                    rankHand[count] = Rank.seven;
                else if (card[0] == '8')
                    rankHand[count] = Rank.eight;
                else if (card[0] == '9')
                    rankHand[count] = Rank.nine;
                else if (card[0] == 'J')
                    rankHand[count] = Rank.J;
                else if (card[0] == 'Q')
                    rankHand[count] = Rank.Q;
                else if (card[0] == 'K')
                    rankHand[count] = Rank.K;
                else if (card[0] == 'A')
                    rankHand[count] = Rank.A;
                else
                    Console.WriteLine("Something went wrong with getting rank");

                count++;
            }
            return rankHand;
        }

        static Suit[] GetSuit(string[] cards)
        {
            Suit[] suitHand = new Suit[5];
            int count = 0;

            foreach(string card in cards)
            {
                if (card[card.Length - 1] == 'H')
                    suitHand[count] = Suit.H;
                else if (card[card.Length - 1] == 'S')
                    suitHand[count] = Suit.S;
                else if (card[card.Length - 1] == 'D')
                    suitHand[count] = Suit.D;
                else if (card[card.Length - 1] == 'C')
                    suitHand[count] = Suit.C;
                else
                    Console.WriteLine("Something went wrong getting suits");

                count++;
            }

            return suitHand;
        }
        
        static bool isStraight(Card[] cards)
        {
            for(int i = 1; i <5; i++)
            {
                if (cards[i].Rank - cards[i - 1].Rank != 1)
                    return false;
            }

            return true;
        }

        static bool isFlush(Card[] cards)
        {
            for(int i = 1; i < 5; i++)
            {
                if (cards[i].Suit != cards[i - 1].Suit)
                    return false;
            }

            return true;
        }

        static bool isStraightFlush(Card[] cards)
        {
            if (isStraight(cards) == true && isFlush(cards) == true)
            {
                return true;
            }
            return false;
        }
        
        static bool isFourofAKind(Card[] cards)
        {
            if (cards[0].Rank == cards[3].Rank || cards[1].Rank == cards[4].Rank)
                return true;

            return false;
        }

        static bool isThreeofAKind(Card[] cards)
        {
            if (cards[0].Rank == cards[1].Rank && cards[1].Rank == cards[2].Rank)
                return true;
            else if (cards[1].Rank == cards[2].Rank && cards[2].Rank == cards[3].Rank)
                return true;
            else if (cards[2].Rank == cards[3].Rank && cards[3].Rank == cards[4].Rank)
                return true;
            else
                return false;
        }

        static bool isTwoPairs(Card[] cards)
        {
            if (findPairs(cards) == 2)
                return true;
            else
                return false;
        }

        static bool isPair(Card[] cards)
        {
            //If there is ONLY one pair. 
            if (findPairs(cards) == 1)
                return true;
            else
                return false;
        }

        static int findPairs(Card[] cards)
        {
            Dictionary<Rank, int> dict = new Dictionary<Rank, int>();

            foreach (Card card in cards)
            {
                if (dict.ContainsKey(card.Rank))
                {
                    dict[card.Rank] += 1;
                }
                else
                {
                    dict.Add(card.Rank, 1);
                }
            }

            var keys = from entry in dict
                       where entry.Value == 2
                       select entry.Key;
            int count = 0;
            foreach (var key in keys)
            {
                count++;
            }

            if (count == 2)
                return 2;
            else if (count == 1)
                return 1;
            else
                return 0;

        }

        static bool isFullHouse(Card[] cards)
        {
            if (isThreeofAKind(cards) == true && isPair(cards) == true)
                return true;
            else
                return false;
        }

        static bool isHighCard(Card[] cards)
        {
            return true;
        }

        static void PrintArray(string[] arr)
        {
            foreach (string element in arr)
            {
                Console.WriteLine(element);
            }
        }
        #endregion
    }
}
