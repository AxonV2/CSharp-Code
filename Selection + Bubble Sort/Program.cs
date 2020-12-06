using System;
using System.Text;

namespace Semana3
{
    enum Estado
    {
        Casado,
        Solteiro,
        Divorciado,
        Viuvo
    }

    class Program
    {
        static Pessoa[] lista = new Pessoa[100];
        static Empresa empre = new Empresa(lista);
        static int cont = 0;
        static bool valcheck;

        static bool TudoLetras(string s)
        {
            foreach (char c in s)
            {
                if (!Char.IsLetter(c) && c != ' ')
                    return false;
            }
            return true;
        }

        static char Keycheck()
        {
            int whilecheck = 0;
            char charval;
            Console.WriteLine("(s / n) - (E maiuscula) fecha o programa");
            do
            {
                charval = Console.ReadKey().KeyChar;

                if (charval == 'E')
                    Environment.Exit(0);

                if (charval == 's' || charval == 'n' || charval == 'S' || charval == 'N')
                    whilecheck = 1;
            } while (whilecheck == 0);

            return charval;
        }

        static void Inserir()
        {
            Pessoa ins;
            int limite, cont2 = 0;

            Console.WriteLine("Quantas pessoas deseja inserir (-1 para teste de sort)");
            do
            {
                valcheck = int.TryParse(Console.ReadLine(), out limite);
            } while (valcheck == false || limite == 0 || limite < -1);

            if (limite != -1)
            {
                while (cont2 < limite)
                {
                    double sal = 0;
                    int chk; float defi; char d;
                    bool trab = false;
                    int titulares; int dependentes;
                    string nome;
                    bool check;
                    Estado cas;

                    Console.WriteLine("\n{0,-9}-------------" + (cont + 1) + "-------------", "");
                    Console.WriteLine("Numero de Titulares 1 ou 2");
                    do
                    {
                        valcheck = int.TryParse(Console.ReadLine(), out titulares);
                    } while (titulares < 1 || titulares > 2 || valcheck == false);

                    Console.WriteLine("Quantos dependentes? (Insira numeros, numero abaixo de 0 será contado como 0)");

                    do
                    {
                        valcheck = int.TryParse(Console.ReadLine(), out dependentes);
                    } while (valcheck == false);

                    Console.WriteLine("Insira o nome da {0} pessoa, não pode conter numeros ", (cont + 1));
                    do
                    {
                        nome = Console.ReadLine();
                        check = TudoLetras(nome);
                    } while (check == false);

                    Console.WriteLine("Trabalha?");
                    d = Keycheck();

                    if (d == 's')
                    {
                        trab = true;
                        Console.WriteLine("\nSalario (Valor Positivo)");
                        do
                        {
                            valcheck = double.TryParse(Console.ReadLine(), out sal);
                        } while (valcheck == false || sal <= 0);

                    }
                    else
                        Console.WriteLine("");

                    Console.WriteLine("Casado (1) // Viuvo (2) // Solteiro(3) // Divorciado(4)");
                    do
                    {
                        valcheck = int.TryParse(Console.ReadLine(), out chk);
                    } while (chk < 1 || valcheck == false || chk > 4);

                    switch (chk)
                    {
                        case 1:
                            cas = Estado.Casado;
                            break;

                        case 2:
                            cas = Estado.Viuvo;
                            break;

                        default:
                        case 3:
                            cas = Estado.Solteiro;
                            break;

                        case 4:
                            cas = Estado.Divorciado;
                            break;
                    }

                    Console.WriteLine("Deficiencia? (%)");
                    do
                    {
                        valcheck = float.TryParse(Console.ReadLine(), out defi);
                    } while (valcheck == false);

                    ins = new Pessoa(nome, sal, defi, cas, trab, dependentes, titulares);
                    lista[cont] = ins;
                    cont++; cont2++;
                }
            }
            else
            {
                lista[0] = new Pessoa("Esqueleto", 715, 0, Estado.Casado, true, 2, 2);
                lista[1] = new Pessoa("Ja Fumega", 1020, 0, Estado.Casado, true, 0, 2);
                lista[2] = new Pessoa("Sulfato", 1200, 0, Estado.Casado, true, 1, 2);
                lista[3] = new Pessoa("Agostinho", 2300, 0, Estado.Casado, true, 2, 2);
                lista[4] = new Pessoa("Zé Pequeno", 1200, 0, Estado.Casado, true, 4, 2);
                lista[5] = new Pessoa("Moelas", 1200, 0, Estado.Casado, true, 4, 2);
                lista[6] = new Pessoa("Colmeia", 3200, 0, Estado.Casado, true, 1, 2);

                if (cont < 7)
                    cont = 7;
            }
            Menu();
        }

        static void PessoasDisplay()
        {
            StringBuilder str = new StringBuilder();
            Console.Clear();
            str.AppendLine("-----------------------------------------------");
            str.AppendFormat("{0,-11} ", "");
            str.AppendLine("PESSOAS / AGREGADOS");

            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i] != null)
                {
                    str.AppendFormat("{0,-7}", "");
                    str.AppendLine("-------------" + (i + 1) + "-------------");
                    str.AppendLine(lista[i].ToString());
                }
            }

            str.AppendLine("\n-----------------------------------------------");
            str.AppendFormat("{0,-7} ", "");
            str.AppendLine("LISTA PESSOAS ORDENADA POR NOME\n");

            Pessoa[] ordered = empre.SelectionSortList(cont);

            foreach (Pessoa item in ordered)
                if (item != null)
                    str.AppendLine("Nome - " + item.Nome.ToUpper());

            str.AppendLine("\n-----------------------------------------------");
            str.AppendFormat("{0,-5} ", "");
            str.AppendLine("LISTA PESSOAS ORDENADA POR DEPENDENTES\n");

            ordered = empre.BubbleSortList(cont);
            foreach (Pessoa item in ordered)
                if (item != null)
                    str.AppendLine("Nome - " + item.Nome.ToUpper() + "\nDependentes - " + item.Dependentes + "\n");

            Console.WriteLine(str.ToString());
            str.Clear();
            Console.ReadKey();
            Menu();
        }

        static void EmpresaDisplay()
        {
            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine("{0,-9} EMPRESA - TRABALHADORES", "");
            Console.WriteLine("-----------------------------------------------");
            bool trabcheck = false;
            for (int i = 0; i < lista.Length; i++)
            {
                if (lista[i] != null && lista[i].Trabalha == true)
                    trabcheck = true;
            }
            if (trabcheck)
                Console.WriteLine(empre);
            else
                Console.WriteLine("Das pessoas inseridas ninguem trabalha\n");
            Console.ReadKey();
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("(1) Detalhes Pessoas");
            Console.WriteLine("(2) Detalhes Empresa/Funcionários");
            Console.WriteLine("(3) Correr o programa outra vez");
            Console.WriteLine("(4) Alterar dados de Funcionário");
            Console.WriteLine("(5) Inserir Funcionário");
            Console.WriteLine("(6) Remover Funcionário");
            Console.WriteLine("(7) Fechar Programa");
            int escolha;
            do
            {
                valcheck = int.TryParse(Console.ReadLine(), out escolha);
            } while (escolha <= 0 || escolha > 7 || valcheck == false);

            Console.Clear();
            switch (escolha)
            {
                case 1:
                   
                    PessoasDisplay();
                    break;
                case 2:
                    EmpresaDisplay();
                    break;
                case 3:
                    Inserir();
                    break;
                case 4:
                    empre.Alteracoes();
                    break;
                case 5:
                    empre.InserirFuncionario(cont);
                    cont++;
                    break;
                case 6:
                    empre.RemoverFuncionario();
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
            }
            Menu();
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Inserir();
        }
    }
}
