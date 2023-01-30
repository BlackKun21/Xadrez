using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void printTela(Tabuleiro tab)
        {
            for(int i=0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");
                for(int j=0; j < tab.colunas; j++)
                {
                    if(tab.peca(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirTela(tab.peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();                           
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }
        public static void imprimirTela(Peca peca)
        {
            if(peca.cor == Cor.branco)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(peca);
                Console.ForegroundColor = aux;
            }
        }
    }   
}
