using System;
using System.Collections.Generic;

namespace SistemaEscolar
{
    class Program
    {
        private static ListaDeAlunos listaAlunos = new ListaDeAlunos();
        private static List<Turma> turmas = new List<Turma>();

        static void Main(string[] args)
        {
            bool continuar = true;
            
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE GESTÃO ESCOLAR ===");
                Console.WriteLine("1. Cadastrar Aluno");
                Console.WriteLine("2. Cadastrar Turma");
                Console.WriteLine("3. Listar Alunos");
                Console.WriteLine("4. Listar Turmas");
                Console.WriteLine("5. Matricular Aluno em Turma");
                Console.WriteLine("6. Listar Alunos de uma Turma");
                Console.WriteLine("7. Listar Alunos Fora da Faixa Etária");
                Console.WriteLine("8. Ordenar Alunos por Nome");
                Console.WriteLine("9. Remover Último Aluno da Lista");
                Console.WriteLine("0. Sair");
                Console.Write("Escolha uma opção: ");
                
                string opcao = Console.ReadLine();
                
                switch (opcao)
                {
                    case "1":
                        CadastrarAluno();
                        break;
                    case "2":
                        CadastrarTurma();
                        break;
                    case "3":
                        ListarAlunos();
                        break;
                    case "4":
                        ListarTurmas();
                        break;
                    case "5":
                        MatricularAluno();
                        break;
                    case "6":
                        ListarAlunosTurma();
                        break;
                    case "7":
                        ListarAlunosForaFaixaEtaria();
                        break;
                    case "8":
                        OrdenarAlunos();
                        break;
                    case "9":
                        RemoverUltimoAluno();
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
                
                if (continuar)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private static void CadastrarAluno()
        {
            Console.WriteLine("\n=== CADASTRO DE ALUNO ===");
            
            Console.Write("Nome: ");
            string nome = Console.ReadLine();
            
            Console.Write("CPF: ");
            string cpf = Console.ReadLine();
            
            Console.Write("Endereço: ");
            string endereco = Console.ReadLine();
            
            DateTime dataNascimento;
            while (true)
            {
                Console.Write("Data de Nascimento (dd/mm/aaaa): ");
                if (DateTime.TryParse(Console.ReadLine(), out dataNascimento))
                    break;
                Console.WriteLine("Data inválida! Tente novamente.");
            }
            
            Aluno aluno = new Aluno(nome, cpf, endereco, dataNascimento);
            listaAlunos.incluirNoFim(aluno);
            
            Console.WriteLine("\nAluno cadastrado com sucesso!");
        }

        private static void CadastrarTurma()
        {
            Console.WriteLine("\n=== CADASTRO DE TURMA ===");
            
            Console.Write("Código da Turma: ");
            string codigo = Console.ReadLine();
            
            Console.WriteLine("Etapa de Ensino:");
            Console.WriteLine("1. Infantil");
            Console.WriteLine("2. Fundamental Anos Iniciais");
            Console.WriteLine("3. Fundamental Anos Finais");
            Console.WriteLine("4. Médio");
            
            EtapaEnsino etapa;
            while (true)
            {
                Console.Write("Escolha a etapa: ");
                string opcaoEtapa = Console.ReadLine();
                
                switch (opcaoEtapa)
                {
                    case "1":
                        etapa = EtapaEnsino.Infantil;
                        break;
                    case "2":
                        etapa = EtapaEnsino.FundamentalAnosIniciais;
                        break;
                    case "3":
                        etapa = EtapaEnsino.FundamentalAnosFinais;
                        break;
                    case "4":
                        etapa = EtapaEnsino.Medio;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        continue;
                }
                break;
            }
            
            int ano;
            while (true)
            {
                Console.Write("Ano: ");
                if (int.TryParse(Console.ReadLine(), out ano))
                    break;
                Console.WriteLine("Valor inválido! Tente novamente.");
            }
            
            int limiteVagas;
            while (true)
            {
                Console.Write("Limite de Vagas: ");
                if (int.TryParse(Console.ReadLine(), out limiteVagas) && limiteVagas > 0)
                    break;
                Console.WriteLine("Valor inválido! Tente novamente.");
            }
            
            Turma turma = new Turma(codigo, etapa, ano, limiteVagas);
            turmas.Add(turma);
            
            Console.WriteLine("\nTurma cadastrada com sucesso!");
        }

        private static void ListarAlunos()
        {
            Console.WriteLine("\n=== LISTA DE ALUNOS ===");
            
            if (listaAlunos.tamanho() == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado.");
                return;
            }
            
            for (int i = 0; i < listaAlunos.tamanho(); i++)
            {
                Console.WriteLine($"{i+1}. {listaAlunos.get(i)}");
            }
        }

        private static void ListarTurmas()
        {
            Console.WriteLine("\n=== LISTA DE TURMAS ===");
            
            if (turmas.Count == 0)
            {
                Console.WriteLine("Nenhuma turma cadastrada.");
                return;
            }
            
            for (int i = 0; i < turmas.Count; i++)
            {
                Console.WriteLine($"{i+1}. {turmas[i]}");
            }
        }

        private static void MatricularAluno()
        {
            Console.WriteLine("\n=== MATRICULAR ALUNO EM TURMA ===");
            
            if (listaAlunos.tamanho() == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado.");
                return;
            }
            
            if (turmas.Count == 0)
            {
                Console.WriteLine("Nenhuma turma cadastrada.");
                return;
            }
            
            // Listar alunos
            ListarAlunos();
            
            int indiceAluno;
            while (true)
            {
                Console.Write("\nEscolha o número do aluno: ");
                if (int.TryParse(Console.ReadLine(), out indiceAluno) && 
                    indiceAluno > 0 && indiceAluno <= listaAlunos.tamanho())
                    break;
                Console.WriteLine("Opção inválida! Tente novamente.");
            }
            
            Aluno aluno = listaAlunos.get(indiceAluno - 1);
            
            // Listar turmas
            ListarTurmas();
            
            int indiceTurma;
            while (true)
            {
                Console.Write("\nEscolha o número da turma: ");
                if (int.TryParse(Console.ReadLine(), out indiceTurma) && 
                    indiceTurma > 0 && indiceTurma <= turmas.Count)
                    break;
                Console.WriteLine("Opção inválida! Tente novamente.");
            }
            
            Turma turma = turmas[indiceTurma - 1];
            
            if (turma.MatricularAluno(aluno))
            {
                Console.WriteLine($"\nAluno {aluno.Nome} matriculado com sucesso na turma {turma.Codigo}!");
            }
            else
            {
                Console.WriteLine("\nNão foi possível realizar a matrícula. Turma sem vagas disponíveis.");
            }
        }

        private static void ListarAlunosTurma()
        {
            Console.WriteLine("\n=== LISTAR ALUNOS DE UMA TURMA ===");
            
            if (turmas.Count == 0)
            {
                Console.WriteLine("Nenhuma turma cadastrada.");
                return;
            }
            
            // Listar turmas
            ListarTurmas();
            
            int indiceTurma;
            while (true)
            {
                Console.Write("\nEscolha o número da turma: ");
                if (int.TryParse(Console.ReadLine(), out indiceTurma) && 
                    indiceTurma > 0 && indiceTurma <= turmas.Count)
                    break;
                Console.WriteLine("Opção inválida! Tente novamente.");
            }
            
            Turma turma = turmas[indiceTurma - 1];
            List<Aluno> alunosTurma = turma.GetAlunosMatriculados();
            
            Console.WriteLine($"\nAlunos matriculados na turma {turma.Codigo}:");
            
            if (alunosTurma.Count == 0)
            {
                Console.WriteLine("Nenhum aluno matriculado nesta turma.");
                return;
            }
            
            for (int i = 0; i < alunosTurma.Count; i++)
            {
                Console.WriteLine($"{i+1}. {alunosTurma[i]}");
            }
        }

        private static void ListarAlunosForaFaixaEtaria()
        {
            Console.WriteLine("\n=== LISTAR ALUNOS FORA DA FAIXA ETÁRIA ===");
            
            if (turmas.Count == 0)
            {
                Console.WriteLine("Nenhuma turma cadastrada.");
                return;
            }
            
            // Listar turmas
            ListarTurmas();
            
            int indiceTurma;
            while (true)
            {
                Console.Write("\nEscolha o número da turma: ");
                if (int.TryParse(Console.ReadLine(), out indiceTurma) && 
                    indiceTurma > 0 && indiceTurma <= turmas.Count)
                    break;
                Console.WriteLine("Opção inválida! Tente novamente.");
            }
            
            Turma turma = turmas[indiceTurma - 1];
            List<Aluno> alunosForaFaixa = turma.GetAlunosForaFaixaEtaria();
            
            Console.WriteLine($"\nAlunos fora da faixa etária na turma {turma.Codigo}:");
            
            if (alunosForaFaixa.Count == 0)
            {
                Console.WriteLine("Não há alunos fora da faixa etária nesta turma.");
                return;
            }
            
            for (int i = 0; i < alunosForaFaixa.Count; i++)
            {
                Console.WriteLine($"{i+1}. {alunosForaFaixa[i]}");
            }
        }

        private static void OrdenarAlunos()
        {
            Console.WriteLine("\n=== ORDENAR ALUNOS POR NOME ===");
            
            if (listaAlunos.tamanho() == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado.");
                return;
            }
            
            listaAlunos.ordenar();
            Console.WriteLine("Alunos ordenados com sucesso!");
            
            // Mostrar a lista ordenada
            ListarAlunos();
        }

        private static void RemoverUltimoAluno()
        {
            Console.WriteLine("\n=== REMOVER ÚLTIMO ALUNO DA LISTA ===");
            
            if (listaAlunos.tamanho() == 0)
            {
                Console.WriteLine("Nenhum aluno cadastrado.");
                return;
            }
            
            Aluno alunoRemovido = listaAlunos.removerDoFim();
            Console.WriteLine($"Aluno removido: {alunoRemovido}");
        }
    }
}
