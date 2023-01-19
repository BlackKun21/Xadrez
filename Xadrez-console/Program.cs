using System;
using tabuleiro;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Posicao p = new Posicao(2, 3);
            Tabuleiro tab = new Tabuleiro(8, 8);
            Console.WriteLine(p);
        }
    }
}
