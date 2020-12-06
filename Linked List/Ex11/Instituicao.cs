using System;
using System.Text;

namespace Ex11
{
    enum EnumCurso
    {
        C1 = 1,
        C2 = 2,
        C3 = 3,
        C4 = 4,
        C5 = 5,
    }

    class Instituicao
    {
        private ListaSimplesOrd novo;
        public ListaSimplesOrd headORD;

        public Instituicao()
        {
            novo = null;
            headORD = null;
        }

        public ListaSimplesOrd[] ArrayDeAlunos(EnumCurso curso)
        {
            novo = headORD;
            int cont = 0;
            ListaSimplesOrd[] Alunos = new ListaSimplesOrd[100];

            while (novo != null)
            {
                if (novo.Aluno.Curso == curso)
                {
                    Alunos[cont] = novo;
                    cont++;
                }
                novo = novo.Seguinte;
            }
            return Alunos;
        }


        public bool CheckRep(long n)
        {
            if (headORD != null)
            {
                novo = headORD;

                if (novo.Aluno.NumAluno == n)
                    return true;

                while (novo.Seguinte != null)
                {
                    if (novo.Seguinte.Aluno.NumAluno == n)
                        return true;
                    novo = novo.Seguinte;
                }
            }
            return false;
        }


        public void RegistarOrd(Aluno manual)
        {
            ListaSimplesOrd aux = new ListaSimplesOrd(manual);

            if (headORD == null)
                headORD = aux;
            else if (headORD.Aluno.NumAluno > aux.Aluno.NumAluno)
            {
                aux.Seguinte = headORD;
                headORD = aux;
            }
            else
            {
                novo = headORD;

                while (novo.Seguinte != null && novo.Seguinte.Aluno.NumAluno < aux.Aluno.NumAluno)
                    novo = novo.Seguinte;

                aux.Seguinte = novo.Seguinte;
                novo.Seguinte = aux;
            }
        }

        public void EliminarOrd()
        {
            long escolha;
            Console.WriteLine(this.ToString());

            do
            {
                Console.Write(" -> ");
                long.TryParse(Console.ReadLine(), out escolha);

                if (escolha == -1)
                    return;

            } while (CheckRep(escolha) == false);

            Console.WriteLine("\n Removeu o Aluno");

            if (headORD.Aluno.NumAluno == escolha)
            {
                Console.WriteLine(headORD.Aluno.ToString());
                headORD = headORD.Seguinte;
            }
            else
            {
                ListaSimplesOrd prev = null;
                novo = headORD;

                while (novo != null && novo.Aluno.NumAluno != escolha)
                {
                    prev = novo;
                    novo = novo.Seguinte;
                }

                Console.WriteLine(novo.Aluno.ToString());
                prev.Seguinte = novo.Seguinte;
            }
        }
  


        public override string ToString()
        {
            if (headORD == null)
            {
                return "\n De momento não estão registados alunos";
            }

            StringBuilder str = new StringBuilder();
            novo = headORD;
            while (novo != null)
            {
                str.AppendLine(novo.Aluno.ToString());
                novo = novo.Seguinte;
            }


            return str.ToString();
        }
    }
}
