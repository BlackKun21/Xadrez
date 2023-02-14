using tabuleiro;
using System.Collections.Generic;

namespace Pxadrez
{
    class PartidaXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }
        public Peca vulneravelEmPassant { get; private set; }

        public PartidaXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            xeque = false;
            jogadorAtual = Cor.branco;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executarMovimentos(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarMovimentos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }

            // # jogada especial roque pequeno
            if(p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // # jogada especial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(origemT);
                T.incrementarMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            return pecaCapturada;
        }

        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executarMovimentos(origem, destino);
            if (estaemXeque(jogadorAtual))
            {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new Xadrez_Exception("Você não pode se colocar em xeque");
            }            

            if (estaemXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequeMate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }

            Peca p = tab.peca(destino);
            //Jogada especial em Passant
            if(p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2))
            {
                vulneravelEmPassant = p;
            }
            else
            {
                vulneravelEmPassant = null;
            }
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }

            // # jogada especial roque pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // # jogada especial roque grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoT);
                T.decrementarMovimentos();
                tab.colocarPeca(T, origemT);
            }
        }
        public void validarPosicaodeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new Xadrez_Exception("Não existe peça na posição selecionada");
            }
            if (jogadorAtual != tab.peca(pos).cor)
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
            if (!tab.peca(origem).movimentoPossivel(destino))
            {
                throw new Xadrez_Exception("Destino incorreto");
            }
        }

        public void mudaJogador()
        {
            if (jogadorAtual == Cor.branco)
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
            foreach (Peca x in capturadas)
            {
                if (x.cor == cor)
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

        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.branco)
            {
                return Cor.azul;
            }
            else
            {
                return Cor.branco;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in PecaemJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool estaemXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new Xadrez_Exception("Não existe peça rei da cor " + cor);
            }
            foreach (Peca x in PecaemJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequeMate(Cor cor)
        {
            if (!estaemXeque(cor))
            {
                return false;
            }
            foreach(Peca x in PecaemJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for(int i = 0; i<tab.linhas; i++)
                {
                    for(int j = 0; j<tab.colunas; j++)
                    {
                        if(mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executarMovimentos(origem, destino);
                            bool testeXeque = estaemXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }                
                }            
            }
            return true;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas()
        {
            colocarNovaPeca('a', 1, new Torre(Cor.branco, tab));
            colocarNovaPeca('b', 1, new Cavalo(Cor.branco, tab));
            colocarNovaPeca('c', 1, new Bispo(Cor.branco, tab));
            colocarNovaPeca('d', 1, new Dama(Cor.branco, tab));
            colocarNovaPeca('e', 1, new Rei(Cor.branco, tab, this));
            colocarNovaPeca('f', 1, new Bispo(Cor.branco, tab));
            colocarNovaPeca('g', 1, new Cavalo(Cor.branco, tab));
            colocarNovaPeca('h', 1, new Torre(Cor.branco, tab));
            colocarNovaPeca('a', 2, new Peao(Cor.branco, tab, this));
            colocarNovaPeca('b', 2, new Peao(Cor.branco, tab, this));
            colocarNovaPeca('c', 2, new Peao(Cor.branco, tab, this));
            colocarNovaPeca('d', 2, new Peao(Cor.branco, tab, this));
            colocarNovaPeca('e', 2, new Peao(Cor.branco, tab, this));
            colocarNovaPeca('f', 2, new Peao(Cor.branco, tab, this));
            colocarNovaPeca('g', 2, new Peao(Cor.branco, tab, this));
            colocarNovaPeca('h', 2, new Peao(Cor.branco, tab, this));



            colocarNovaPeca('a', 8, new Torre(Cor.azul, tab));
            colocarNovaPeca('b', 8, new Cavalo(Cor.azul, tab));
            colocarNovaPeca('c', 8, new Bispo(Cor.azul, tab));
            colocarNovaPeca('d', 8, new Dama(Cor.azul, tab));
            colocarNovaPeca('e', 8, new Rei(Cor.azul, tab, this));
            colocarNovaPeca('f', 8, new Bispo(Cor.azul, tab));
            colocarNovaPeca('g', 8, new Cavalo(Cor.azul, tab));
            colocarNovaPeca('h', 8, new Torre(Cor.azul, tab));
            colocarNovaPeca('a', 7, new Peao(Cor.azul, tab, this));
            colocarNovaPeca('b', 7, new Peao(Cor.azul, tab, this));
            colocarNovaPeca('c', 7, new Peao(Cor.azul, tab, this));
            colocarNovaPeca('d', 7, new Peao(Cor.azul, tab, this));
            colocarNovaPeca('e', 7, new Peao(Cor.azul, tab, this));
            colocarNovaPeca('f', 7, new Peao(Cor.azul, tab, this));
            colocarNovaPeca('g', 7, new Peao(Cor.azul, tab, this));
            colocarNovaPeca('h', 7, new Peao(Cor.azul, tab, this));
        }
    }

}
