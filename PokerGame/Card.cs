using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class Card : IComparable<Card>
    {
        public Rank Rank { get; set; }
        public Suit Suit { get; set; }

        public Card(Rank rank, Suit suit)
        {
            this.Rank = rank;
            this.Suit = suit;
        }

        public int CompareTo(Card other)
        {
            if (this.Rank > other.Rank) return 1;
            if (this.Rank < other.Rank)
                return -1;
            else
                //this.Rank == other.Rank
                return 0;
        }
    }
}
