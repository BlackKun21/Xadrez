using System;
using tabuleiro;
using Pxadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)        {
            
            Tabuleiro tab = new Tabuleiro(8, 8);            
        
            tab.colocarPeca(new Torre(Cor.preto, tab), new Posicao(0, 0));
            tab.colocarPeca(new Torre(Cor.preto, tab), new Posicao(1, 3));
            tab.colocarPeca(new Rei(Cor.preto, tab), new Posicao(2, 4));
            Tela.printTela(tab);

        }
    }
}
