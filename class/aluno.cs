using System;

namespace SistemaEscolar
{
    public class Aluno
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }

        public Aluno(string nome, string cpf, string endereco, DateTime dataNascimento)
        {
            Nome = nome;
            CPF = cpf;
            Endereco = endereco;
            DataNascimento = dataNascimento;
        }

        public int CalcularIdade()
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - DataNascimento.Year;
            if (DataNascimento.Date > hoje.AddYears(-idade)) idade--;
            return idade;
        }

        public override string ToString()
        {
            return $"Nome: {Nome}, CPF: {CPF}, Endereço: {Endereco}, Data de Nascimento: {DataNascimento.ToShortDateString()}, Idade: {CalcularIdade()} anos";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Aluno outro)
            {
                return this.CPF == outro.CPF;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return CPF.GetHashCode();
        }
    }
}
