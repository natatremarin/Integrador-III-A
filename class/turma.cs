using System;
using System.Collections.Generic;

namespace SistemaEscolar
{
    public class Turma
    {
        public string Codigo { get; set; }
        public EtapaEnsino Etapa { get; set; }
        public int Ano { get; set; }
        public int LimiteVagas { get; set; }
        public int NumeroMatriculados { get; private set; }
        private List<Aluno> AlunosMatriculados { get; set; }

        public Turma(string codigo, EtapaEnsino etapa, int ano, int limiteVagas)
        {
            Codigo = codigo;
            Etapa = etapa;
            Ano = ano;
            LimiteVagas = limiteVagas;
            NumeroMatriculados = 0;
            AlunosMatriculados = new List<Aluno>();
        }

        public bool MatricularAluno(Aluno aluno)
        {
            if (NumeroMatriculados >= LimiteVagas)
                return false;

            AlunosMatriculados.Add(aluno);
            NumeroMatriculados++;
            return true;
        }

        public List<Aluno> GetAlunosMatriculados()
        {
            return AlunosMatriculados;
        }

        public List<Aluno> GetAlunosForaFaixaEtaria()
        {
            List<Aluno> alunosForaFaixa = new List<Aluno>();
            
            foreach (var aluno in AlunosMatriculados)
            {
                int idade = aluno.CalcularIdade();
                bool foraFaixa = false;
                
                switch (Etapa)
                {
                    case EtapaEnsino.Infantil:
                        foraFaixa = idade < 3 || idade > 5;
                        break;
                    case EtapaEnsino.FundamentalAnosIniciais:
                        foraFaixa = idade < 6 || idade > 10;
                        break;
                    case EtapaEnsino.FundamentalAnosFinais:
                        foraFaixa = idade < 11 || idade > 14;
                        break;
                    case EtapaEnsino.Medio:
                        foraFaixa = idade < 15 || idade > 17;
                        break;
                }
                
                if (foraFaixa)
                    alunosForaFaixa.Add(aluno);
            }
            
            return alunosForaFaixa;
        }

        public override string ToString()
        {
            return $"Código: {Codigo}, Etapa: {Etapa}, Ano: {Ano}, Vagas: {NumeroMatriculados}/{LimiteVagas}";
        }
    }
}