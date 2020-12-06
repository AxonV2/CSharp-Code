using System;
using System.Collections.Generic;
using System.Text;

namespace Ex11
{
    class Aluno
    {
        private long numaluno;
        public long NumAluno { get { return numaluno; } set { numaluno = value; } }

        private string nome;
        public string Nome { get { return nome; } set { nome = value; } }

        private DateTime datanasc;
        public DateTime DataNasc { get { return datanasc; } set { datanasc = value; } }

        private EnumCurso curso;
        public EnumCurso Curso { get { return curso; } set { curso = value; } }

        public Aluno(long numValue, string nomeValue, DateTime datavalue, EnumCurso cursoValue)
        {
            NumAluno = numValue;
            Nome = nomeValue;
            DataNasc = datavalue;
            Curso = cursoValue;
        }
        public override string ToString()
        {
            return string.Format("\n | {4,-20} {0,20} |\n | {5,-20} {1,20} |\n | {6,-20} {2,20} |\n | {7,-20} {3,20} |",
                 numaluno, nome, datanasc.ToShortDateString(), curso, "Número do Aluno", "Nome do Aluno", "Data de Nascimento", "Curso");
        }
    }
}
