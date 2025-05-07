using System;

namespace SistemaEscolar
{
    public class ExcecaoDeAlunoJaExistente : Exception
    {
        public ExcecaoDeAlunoJaExistente(string mensagem) : base(mensagem) { }
    }
}
