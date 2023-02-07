using tabuleiro;

namespace Pxadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        public  int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
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

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            executarMovimentos(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaodeOrigem(Posicao pos)
        {
            if(tab.peca(pos) == null)
            {
                throw new Xadrez_Exception("Não existe peça na posição selecionada");
            }
            if(jogadorAtual != tab.peca(pos).cor)
            {
                throw new Xadrez_Exception("A peça de origem escolhida não é sua");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new Xadrez_Exception("Não há movimentos possiveis para a peça de origem");
            }
        }

        public void validarPosicaoDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new Xadrez_Exception("Destino incorreto");
            }
        }
        
        public void mudaJogador()
        {
            if(jogadorAtual == Cor.branco)
            {
                jogadorAtual = Cor.azul;
            }
            else
            {
                jogadorAtual = Cor.branco;
            }
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
