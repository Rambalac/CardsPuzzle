using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace ConsoleApplication1
{
    enum Mast
    {
        Cher, Bubn, Pika
    }

    enum Rank
    {
        King,
        Quee
    }
    class Card
    {
        public Rank rank;
        public Mast mast;
        public override string ToString()
        {
            return mast.ToString() + " " + rank.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var cards = new List<Card>();
            foreach (var m in (Mast[])Enum.GetValues(typeof(Mast)))
                foreach (var r in (Rank[])Enum.GetValues(typeof(Rank)))
                    cards.Add(new Card { mast = m, rank = r });

            const int total = 1000000000;
            var bingo = 0;
            for (var i = 0; i < total; i++)
            {
                var rand = new Random();
                cards.Sort((x, y) => rand.Next(2) == 0 ? -1 : 1);
                //WriteLine(string.Join(", ", cards));
                var mastlookup = cards.ToLookup(c => c.mast);

                var list1 = new List<Card>();
                var list2 = new List<Card>();
                foreach (var m in (Mast[])Enum.GetValues(typeof(Mast)))
                {
                    var ce = mastlookup[m].ToArray();
                    if (ce.Length != 2) throw new Exception("Not 2");
                    list1.Add(ce[0]);
                    list2.Add(ce[1]);
                }
                //WriteLine(string.Join(", ", list1));
                //WriteLine(string.Join(", ", list2));

                var c1 = list1[rand.Next(3)];
                var c2 = list2[rand.Next(3)];
                //WriteLine($"{c1} : {c2}");

                if (c1.rank == c2.rank)
                {
                    bingo++;
                    //WriteLine("Bingo!");
                }
                //ReadKey();
            }

            WriteLine($"{(double)bingo / total} {bingo}/{total}");
            ReadKey();
        }

    }
}
