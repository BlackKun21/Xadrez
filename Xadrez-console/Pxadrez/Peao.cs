﻿using System;
using tabuleiro;
using Pxadrez;
namespace Pxadrez
{
    class Peao : Peca
    {
        private PartidaXadrez partida;
        public Peao(Cor cor, Tabuleiro tab, PartidaXadrez partida) : base(cor, tab)
        {
            this.partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existePecaInimiga(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos)
        {
            return tab.peca(pos) == null;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.branco)
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qtdMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 1, posicao.coluna -1);
                if (tab.posicaoValida(pos) && existePecaInimiga(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existePecaInimiga(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                //Especial em passant
                if(posicao.linha == 3)
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if(tab.posicaoValida(esquerda) && existePecaInimiga(esquerda) && tab.peca(esquerda) == partida.vulneravelEmPassant)
                    {
                        mat[esquerda.linha, esquerda.coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existePecaInimiga(direita) && tab.peca(direita) == partida.vulneravelEmPassant)
                    {
                        mat[direita.linha, direita.coluna] = true;
                    }
                }

            }
            else
            {
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qtdMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existePecaInimiga(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existePecaInimiga(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }

                //Especial em passant
                if (posicao.linha == 4)
                {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existePecaInimiga(esquerda) && tab.peca(esquerda) == partida.vulneravelEmPassant)
                    {
                        mat[esquerda.linha, esquerda.coluna] = true;
                    }

                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existePecaInimiga(direita) && tab.peca(direita) == partida.vulneravelEmPassant)
                    {
                        mat[direita.linha, direita.coluna] = true;
                    }
                }

            }
            return mat;
        }
    }
}
