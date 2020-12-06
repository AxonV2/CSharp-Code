using System;

namespace Ex12
{
    internal class Program
    {
        private static bool valcheck;
        private static bool inserted;
        private static int escolha;

        private static void Main(string[] args)
        {
            Menu();
        }

        private static void Menu()
        {
            Console.Clear();
            Console.WriteLine(" (1) Inserir Artigos (Automatico)");
            Console.WriteLine(" (2) Alterar Artigo");
            Console.WriteLine(" (3) Listar Artigos stock baixo");
            Console.WriteLine(" (4) Listar Todos Artigos");
            Console.WriteLine(" (5) Fechar Programa");
            do
            {
                Console.Write(" -> ");
                valcheck = int.TryParse(Console.ReadLine(), out escolha);
            } while (escolha <= 0 || escolha > 5 || valcheck == false);

            switch (escolha)
            {
                case 1:
                    Inserir();
                    break;

                case 2:
                    Alterar();
                    break;

                case 3:
                    StockLim();
                    break;

                case 4:
                    Listar();
                    break;

                case 5:
                    Environment.Exit(0);
                    break;
            }
            Menu();
        }

        private static void Inserir()
        {
            inserted = true;
            for (int i = 0; i < 20; i++)
                Run.Insert(new Artigo("art" + i, 10, 10, true, 5));

            Menu();
        }

        private static void StockLim()
        {
            if (inserted == false)
            {
                Console.WriteLine("\nInsire artigos primeiro");
                Console.ReadKey();
                Menu();
            }

            Console.Clear();
            Artigo[] stock = Run.Stockcheck();

            for (int i = 0; i < stock.Length; i++)
            {
                if (stock[i] == null)
                    break;

                Console.WriteLine(stock[i].ToString());
            }

            Console.ReadKey();
            Menu();
        }

        private static void Alterar()
        {
            if (inserted == false)
            {
                Console.WriteLine("\nInsire artigos primeiro");
                Console.ReadKey();
                Menu();
            }

            double preco;
            int quant;
            char charval;
            int escolha2;

            Console.Clear();
            Console.WriteLine("Código do Artigo que deseja alterar (0 Para Cancelar)");
            do
            {
                Console.Write(" -> ");
                valcheck = int.TryParse(Console.ReadLine(), out escolha);

                if (escolha == 0)
                    Menu();

                if (Run.Exists(escolha) == 0)
                    Console.WriteLine("ID Artigo não existe");
            } while (escolha < 1 || valcheck == false || Run.Exists(escolha) == 0);

            Console.WriteLine("\nQue deseja alterar do ARTIGO (0 para cancelar) \n" + Run.Artigo(escolha));
            Console.WriteLine("\nPreço - (1)");
            Console.WriteLine("Quantidade/Stock - (2)");
            Console.WriteLine("Disponibilidade - (3)");

            do
            {
                Console.Write(" -> ");
                valcheck = int.TryParse(Console.ReadLine(), out escolha2);

                if (escolha == 0)
                    Menu();
            } while (escolha2 < 0 || escolha2 > 3 || valcheck == false);

            switch (escolha2)
            {
                case 1:
                    Console.WriteLine("\nInsira o preço novo");
                    do
                    {
                        Console.Write(" -> ");
                        valcheck = double.TryParse(Console.ReadLine(), out preco);
                    } while (preco <= 0 || valcheck == false);
                    Run.AlterarPreco(escolha, preco);
                    break;

                case 2:
                    Console.WriteLine("\nInsira a quantidade nova de stock");
                    do
                    {
                        Console.Write(" -> ");
                        valcheck = int.TryParse(Console.ReadLine(), out quant);
                    } while (quant < 0 || valcheck == false);

                    Run.AlterarStock(escolha, quant);
                    break;

                case 3:
                    Console.WriteLine("\n'D' = Disponível || 'I' = Indisponível");
                    do
                    {
                        charval = Console.ReadKey().KeyChar;
                        charval = char.ToLower(charval);
                    } while (charval != 'd' && charval != 'i');

                    if (charval == 'd')
                        Run.AlterarDisp(escolha, true);
                    else if (charval == 'i')
                        Run.AlterarDisp(escolha, false);
                    break;
            }

            Console.WriteLine("\n\nARTIGO ALTERADO \n" + Run.Artigo(escolha));

            do
            {
                Console.Write("\nDeseja alterar outro artigo? (s / n) ");
                charval = Console.ReadKey().KeyChar;
                charval = char.ToLower(charval);
            } while (charval != 's' && charval != 'n');

            if (charval == 's')
                Alterar();
            else
                Menu();
        }

        private static void Listar()
        {
            if (inserted == false)
            {
                Console.WriteLine("\nInsire artigos primeiro");
                Console.ReadKey();
                Menu();
            }

            Console.Clear();
            Console.WriteLine(Run.Display());
            Console.ReadKey();
            Menu();
        }
    }
}