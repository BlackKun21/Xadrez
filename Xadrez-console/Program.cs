using System;
using tabuleiro;
using Pxadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Torre(Cor.branco, tab), new Posicao(0, 0));
                tab.colocarPeca(new Torre(Cor.azul, tab), new Posicao(3, 7));
                tab.colocarPeca(new Rei(Cor.azul, tab), new Posicao(2, 4));
                Tela.printTela(tab);
            }
            catch (Xadrez_Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
        }
    }
}
