using System;
using System.Collections.Generic;
using System.Text;

namespace Fatec.Core.Model
{
    public class Aluno
    {
        public Aluno() { }

        public int AlunoID { get; set; }
        public string NomeCompleto { get; set; }
        public int Idade { get; set; }
        public string Identificacao { get; set; }
    }
}
