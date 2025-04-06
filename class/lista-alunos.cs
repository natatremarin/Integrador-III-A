using System;
using System.Collections.Generic;

namespace SistemaEscolar
{
    public class ListaDeAlunos
    {
        private class No
        {
            public Aluno Aluno { get; set; }
            public No Proximo { get; set; }

            public No(Aluno aluno)
            {
                Aluno = aluno;
                Proximo = null;
            }
        }

        private No primeiro;
        private No ultimo;
        private int quantidade;

        public ListaDeAlunos()
        {
            primeiro = null;
            ultimo = null;
            quantidade = 0;
        }

        public void incluirNoInicio(Aluno aluno)
        {
            No novoNo = new No(aluno);
            
            if (primeiro == null)
            {
                primeiro = novoNo;
                ultimo = novoNo;
            }
            else
            {
                novoNo.Proximo = primeiro;
                primeiro = novoNo;
            }
            
            quantidade++;
        }

        public void incluirNoFim(Aluno aluno)
        {
            No novoNo = new No(aluno);
            
            if (primeiro == null)
            {
                primeiro = novoNo;
                ultimo = novoNo;
            }
            else
            {
                ultimo.Proximo = novoNo;
                ultimo = novoNo;
            }
            
            quantidade++;
        }

        public void ordenar()
        {
            if (quantidade <= 1)
                return;

            // Convertendo a lista encadeada para um array para facilitar a ordenação
            Aluno[] alunos = new Aluno[quantidade];
            No atual = primeiro;
            int index = 0;
            
            while (atual != null)
            {
                alunos[index++] = atual.Aluno;
                atual = atual.Proximo;
            }
            
            // Ordenando o array pelo nome
            Array.Sort(alunos, (a1, a2) => string.Compare(a1.Nome, a2.Nome, StringComparison.OrdinalIgnoreCase));
            
            // Reconstruindo a lista encadeada
            primeiro = null;
            ultimo = null;
            quantidade = 0;
            
            foreach (var aluno in alunos)
            {
                incluirNoFim(aluno);
            }
        }

        public Aluno removerDoFim()
        {
            if (primeiro == null)
                return null;
                
            Aluno alunoRemovido;
            
            if (primeiro == ultimo)
            {
                alunoRemovido = primeiro.Aluno;
                primeiro = null;
                ultimo = null;
            }
            else
            {
                No atual = primeiro;
                while (atual.Proximo != ultimo)
                {
                    atual = atual.Proximo;
                }
                
                alunoRemovido = ultimo.Aluno;
                ultimo = atual;
                ultimo.Proximo = null;
            }
            
            quantidade--;
            return alunoRemovido;
        }

        public int tamanho()
        {
            return quantidade;
        }

        public Aluno get(int posicao)
        {
            if (posicao < 0 || posicao >= quantidade)
                throw new IndexOutOfRangeException("Posição inválida");
                
            No atual = primeiro;
            for (int i = 0; i < posicao; i++)
            {
                atual = atual.Proximo;
            }
            
            return atual.Aluno;
        }
        
        public List<Aluno> ToList()
        {
            List<Aluno> lista = new List<Aluno>();
            No atual = primeiro;
            
            while (atual != null)
            {
                lista.Add(atual.Aluno);
                atual = atual.Proximo;
            }
            
            return lista;
        }
    }
}