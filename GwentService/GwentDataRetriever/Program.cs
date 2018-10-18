using System;
using System.Collections.Generic;
using System.Text;

namespace GwentDataRetriever
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CardsInfoRetriever retriever = new CardsInfoRetriever();
            retriever.GetCardsInfo();
        }
    }
}
