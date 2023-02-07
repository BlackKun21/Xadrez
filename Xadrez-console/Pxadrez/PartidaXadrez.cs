using tabuleiro;
using System.Collections.Generic;

namespace Pxadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        public  int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.branco;
            colocarPecas();
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();

        }

    public void executarMovimentos(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
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

        public HashSet<Peca> pecaCapturada(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }            
            }
            return aux;
        }

        public HashSet<Peca> PecaemJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecaCapturada(cor));
            return aux;
        }
            public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

    private void colocarPecas()
        {
            colocarNovaPeca('c', 1, new Torre(Cor.branco, tab));
            colocarNovaPeca('c', 2, new Torre(Cor.branco, tab));
            colocarNovaPeca('d', 2, new Torre(Cor.branco, tab));
            colocarNovaPeca('e', 1, new Torre(Cor.branco, tab));
            colocarNovaPeca('e', 2, new Torre(Cor.branco, tab));
            colocarNovaPeca('d', 1, new Torre(Cor.branco, tab));

            colocarNovaPeca('c', 8, new Torre(Cor.branco, tab));
            colocarNovaPeca('c', 7, new Torre(Cor.branco, tab));
            colocarNovaPeca('d', 7, new Torre(Cor.branco, tab));
            colocarNovaPeca('e', 8, new Torre(Cor.branco, tab));
            colocarNovaPeca('e', 7, new Torre(Cor.branco, tab));
            colocarNovaPeca('d', 8, new Torre(Cor.branco, tab));
        }
    }
    
}
