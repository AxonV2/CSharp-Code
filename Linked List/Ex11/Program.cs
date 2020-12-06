using System;
using System.Globalization;
using System.Text;

namespace Ex11
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Menu();
        }

        static Instituicao Inst = new Instituicao();
        static int escolha;
        static bool check;

        static void Menu()
        {
            bool valcheck;
            Console.Clear();
            Console.WriteLine(" (1) Inserir Registo");
            Console.WriteLine(" (2) Array de Curso");
            Console.WriteLine(" (3) Remover Registo");
            Console.WriteLine(" (4) Listar todos os elementos");
            Console.WriteLine(" (5) Fechar Programa\n");
            int escolha;
            do
            {
                Console.Write(" -> ");
                valcheck = int.TryParse(Console.ReadLine(), out escolha);
            } while (escolha <= 0 || escolha > 5 || valcheck == false);

            if (escolha == 5)
                Environment.Exit(0);

            Console.Clear();
            switch (escolha)
            {
                case 1:
                    Registo();
                    break;
                case 2:
                    Consulta();
                    break;
                case 3:
                    if (Inst.headORD == null)
                    {
                        Console.WriteLine("\n Não existem alunos inseridos\n");
                        Console.ReadKey();
                        break;
                    }
                    Console.WriteLine(" Escolha um aluno a remover através do seu número (-1 para cancelar)\n");
                    Inst.EliminarOrd();
                    Console.ReadKey();
                    break;
                case 4:
                    Console.WriteLine(Inst);
                    Console.ReadKey();
                    break;
            }

            Menu();
        }


        static void Registo()
        {
            Console.Clear();
            Console.Write(" Inserção Manual (1) ou Automática (2) ");
            do
            {
                check = int.TryParse(Console.ReadLine(), out escolha);
            } while (check == false || (escolha > 2 || escolha < 1));

            if (escolha == 1)
            {
                #region Manual

                EnumCurso curso;
                long numeroaluno;
                string nomealuno;
                DateTime dataNasc;

                Console.WriteLine("");
                do
                {
                    Console.Write(" {0,-5} {1,30}", "Número do aluno", " -> ");
                    check = long.TryParse(Console.ReadLine(), out numeroaluno);
                    if (Inst.CheckRep(numeroaluno) == true)
                        Console.WriteLine(" Insira um número não repetido");
                } while (check == false || Inst.CheckRep(numeroaluno) == true || numeroaluno <= 0);

                do
                {
                    Console.Write(" {0,-5} {1,32}", "Nome do aluno", " -> ");
                    nomealuno = Console.ReadLine();
                } while (string.IsNullOrEmpty(nomealuno));

                do
                {
                    Console.Write(" {0,-5} {1,3}", "Data de Nascimento do aluno (ano-mês-ano)", " -> ");
                    check = DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dataNasc);
                    if (dataNasc >= DateTime.Now)
                        Console.WriteLine(" Insira uma data anterior à corrente");
                }
                while (check == false || dataNasc >= DateTime.Now);

                StringBuilder str = new StringBuilder();
                foreach (int i in Enum.GetValues(typeof(EnumCurso)))
                {
                    str.AppendFormat("         -------- {1} {0} --------\n", (EnumCurso)i, i);
                }
                Console.Write(str.ToString());
                do
                {
                    Console.Write(" {0} {1,41}", "Curso", " -> ");
                    int.TryParse(Console.ReadLine(), out escolha); ;
                    curso = (EnumCurso)escolha;
                } while (!Enum.IsDefined(typeof(EnumCurso), escolha));

                Aluno manual = new Aluno(numeroaluno, nomealuno, dataNasc, curso);

                Inst.RegistarOrd(manual);

                char charval;
                do
                {
                    Console.Write("\n Deseja inserir outro registo? (s / n) ");
                    charval = Console.ReadKey().KeyChar;
                    charval = char.ToLower(charval);
                } while (charval != 's' && charval != 'n');

                if (charval == 's')
                    Registo();
                else
                {
                    Console.WriteLine("\n\n Dados inseridos com sucesso!\n Verifique as inserções na opção de listagem no menu principal"); Console.ReadKey();
                    Menu();
                }

                #endregion Manual
            }
            else
            {

                #region Auto


                Aluno[] teste = new Aluno[10];
                teste[0] = new Aluno(1, "A", new DateTime(1990, 02, 02), EnumCurso.C1);
                teste[1] = new Aluno(8, "B", new DateTime(1990, 02, 02), EnumCurso.C1);
                teste[2] = new Aluno(9, "C", new DateTime(1990, 02, 02), EnumCurso.C2);
                teste[3] = new Aluno(60, "D", new DateTime(1990, 02, 02), EnumCurso.C2);
                teste[4] = new Aluno(10, "E", new DateTime(1990, 02, 02), EnumCurso.C3);
                teste[5] = new Aluno(7, "F", new DateTime(1990, 02, 02), EnumCurso.C3);
                teste[6] = new Aluno(4, "G", new DateTime(1990, 02, 02), EnumCurso.C4);
                teste[7] = new Aluno(2, "H", new DateTime(1990, 02, 02), EnumCurso.C4);
                teste[8] = new Aluno(3, "I", new DateTime(1990, 02, 02), EnumCurso.C5);
                teste[9] = new Aluno(5, "J", new DateTime(1990, 02, 02), EnumCurso.C5);


                Inst = new Instituicao();

                for (int i = 0; i < teste.Length; i++)
                    Inst.RegistarOrd(teste[i]);

                Console.WriteLine("\n Dados inseridos com sucesso!\n Verifique as inserções na opção de listagem no menu principal");
                Console.ReadKey();
                #endregion Auto
            }
        }


        static void Consulta()
        {
            StringBuilder str = new StringBuilder();
            int escolha;
            EnumCurso curso;

            if (Inst.headORD == null)
            {
                Console.WriteLine("\n Não existem alunos inseridos\n");
                Console.ReadKey();
                return;
            }

            Console.WriteLine(" Selecione um curso (-1 para sair)");

            foreach (int i in Enum.GetValues(typeof(EnumCurso)))
            {
                str.AppendFormat("         -------- {1} {0} --------\n", (EnumCurso)i, i);
            }
            Console.WriteLine(str.ToString());

            do
            {
                do
                {
                    Console.Write(" -> ");
                    int.TryParse(Console.ReadLine(), out escolha);
                    curso = (EnumCurso)escolha;
                } while (!Enum.IsDefined(typeof(EnumCurso), escolha) && escolha != -1);


                ListaSimplesOrd[] array = Inst.ArrayDeAlunos(curso);

                if (array[0] == null && escolha != -1)
                    Console.WriteLine("\n Não existem registos de alunos neste curso");
                else
                {
                    foreach (ListaSimplesOrd item in array)
                    {
                        if (item == null)
                            break;

                        Console.WriteLine(item.Aluno.ToString()+"\n");
                    }
                }
            } while (escolha != -1);
        }
    }
}
