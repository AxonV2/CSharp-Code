using System;
using System.Collections.Generic;
using System.Text;

namespace Ex11
{
    internal class ListaSimplesOrd
    {
        private Aluno aluno;
        public Aluno Aluno { get { return aluno; } set { aluno = value; } }

        private ListaSimplesOrd seguinte;
        public ListaSimplesOrd Seguinte { get { return seguinte; } set { seguinte = value; }}

        public ListaSimplesOrd(Aluno alu)
        {
            Aluno = alu;
        }


    }
}
