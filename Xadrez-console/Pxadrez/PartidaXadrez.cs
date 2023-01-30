using tabuleiro;

namespace Pxadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool terminada { get; private set; }

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.branco;
            colocarPecas();
            terminada = false;
        }

    public void executarMovimentos(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }

    private void colocarPecas()
        {
            tab.colocarPeca(new Torre(Cor.branco, tab), new PosicaoXadrez('c', 1).toPosicao());
            tab.colocarPeca(new Torre(Cor.branco, tab), new PosicaoXadrez('c', 2).toPosicao());
            tab.colocarPeca(new Torre(Cor.branco, tab), new PosicaoXadrez('d', 2).toPosicao());
            tab.colocarPeca(new Torre(Cor.branco, tab), new PosicaoXadrez('e', 1).toPosicao());
            tab.colocarPeca(new Torre(Cor.branco, tab), new PosicaoXadrez('e', 2).toPosicao());
            tab.colocarPeca(new Rei(Cor.branco, tab), new PosicaoXadrez('d', 1).toPosicao());

            tab.colocarPeca(new Torre(Cor.azul, tab), new PosicaoXadrez('c', 8).toPosicao());
            tab.colocarPeca(new Torre(Cor.azul, tab), new PosicaoXadrez('c', 7).toPosicao());
            tab.colocarPeca(new Torre(Cor.azul, tab), new PosicaoXadrez('d', 7).toPosicao());
            tab.colocarPeca(new Torre(Cor.azul, tab), new PosicaoXadrez('e', 8).toPosicao());
            tab.colocarPeca(new Torre(Cor.azul, tab), new PosicaoXadrez('e', 7).toPosicao());
            tab.colocarPeca(new Rei(Cor.azul, tab), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
    
}
