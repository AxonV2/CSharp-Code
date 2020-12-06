using System;
using System.Text;

namespace Ex12
{
    internal static class Run
    {
        #region teste da funcao hashtable

        //encontrar VALUE de key
        //Artigo temp = (Artigo)hash[1];
        //temp.Stock = 2;
        //hash[1] = temp;

        ////encontrar key DE VALUE
        //var key = hash.Keys.OfType<int>().FirstOrDefault(key => hash[key] == x1);

        //for (int i = 0; i < xd2lmao.Length; i++)
        //    Console.WriteLine(xd2lmao[i].Key + " --> " + xd2lmao[i].Value);

        //Hashtable htbl = new Hashtable();
        ////key - value
        //htbl.Add("msg", "Welcome");
        //htbl.Add("site", "Tutlane");
        //htbl.Add(1, 20.5f);
        //htbl.Add(2, 10);

        //Console.WriteLine("*********Access Elements with Keys********");
        //string msg = (string)htbl["msg"];
        //Console.WriteLine("Value at Key 'msg': " + msg);
        //float num = (float)htbl[1];
        //Console.WriteLine("Value at Key '1': " + num);

        //Console.WriteLine("*********Access Elements with Foreach Loop********");
        //foreach (DictionaryEntry item in htbl)
        //    Console.WriteLine("Key = {0}, Value = {1}", item.Key, item.Value);

        //Console.WriteLine("*********HashTable Keys********");
        //foreach (var item in htbl.Keys)
        //    Console.WriteLine("Key = {0}", item);

        //Console.WriteLine("*********HashTable Values********");
        //foreach (var item in htbl.Values)
        //    Console.WriteLine("Value = {0}", item);

        //Console.ReadLine();

        #endregion teste da funcao hashtable

        //Aumentado quando estiver a ficar cheio, METEDO ARRAYFILLCHECK em baixo, não utilizado aqui
        private static Artigo[] arr = new Artigo[20];

        private static void ArrayFillCheck()
        {
            int cont = 0;
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] != null)
                    cont++;

            //se array chegar a 80 de capacidade aumentar o limite do mesmo
            if (cont >= (arr.Length * 0.80))
                Array.Resize(ref arr, arr.Length + 10);
        }

        private static bool ArrayFullCheck()
        {
            for (int i = 0; i < arr.Length; i++)
                if (arr[i] == null)
                    return false;

            return true;
        }

        public static void Insert(Artigo art1)
        {
            //hash     key        max do array por causa de limite de array
            int hsh = art1.ID % (arr.Length - 1);
            //ArrayFillCheck();

            if (ArrayFullCheck())
                return;

            if (arr[hsh] == null)
                arr[hsh] = art1;
            else
            {
                //verificar se lugar esta disponivel com linear probing
                while (arr[hsh] != null)
                    hsh++; //linear probing

                arr[hsh] = art1;

                //outras forma de collision checking
                //hsh += (tentativas * tentativas);
                //hsh += 3;
            }
        }

        public static string Artigo(int num)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                //tem que ser igual a hash até o menos 1
                if (arr[i].ID == num)
                    return arr[i].ToString();
            }
            return "";
        }

        //num exist checker
        public static int Exists(int checkdisp)
        {
            int hsh = checkdisp % (arr.Length - 1);
            int numori = hsh;

            if (arr[hsh].ID == checkdisp)
                return hsh;
            else
            {
                while (arr[hsh].ID != checkdisp)
                {
                    hsh = (hsh % (arr.Length - 1)) + 1;

                    if (hsh == numori)
                        return 0;
                }
                return hsh;
            }
        }

        #region alterar

        public static void AlterarPreco(int IDArtigo, double newpreco)
        {
            int check = Exists(IDArtigo);
            arr[check].Preco = newpreco;
        }

        public static void AlterarStock(int IDArtigo, int newstock)
        {
            int check = Exists(IDArtigo);
            arr[check].Stock = newstock;
        }

        public static void AlterarDisp(int IDArtigo, bool newdisp)
        {
            int check = Exists(IDArtigo);
            arr[check].Disp = newdisp;
        }

        #endregion alterar

        public static Artigo[] Stockcheck()
        {
            int cont = 0;
            Artigo[] ret = new Artigo[arr.Length];

            int gen = 5;

            for (int i = 0; i < arr.Length; i++)
            {
                //atualizar hash
                int hsh = gen % (arr.Length - 1);

                while (arr[hsh].ID != gen)
                    hsh++;

                if (arr[hsh].Stock <= 2)
                {
                    ret[cont] = arr[hsh];
                    cont++;
                }
                gen += 4;
            }

            return ret;
        }

        public static string Display()
        {
            int gen = 5;

            StringBuilder st = new StringBuilder();
            st.AppendLine("Inicio");
            for (int i = 0; i < arr.Length; i++)
            {
                //atualizar hash
                int hsh = gen % (arr.Length - 1);

                while (arr[hsh].ID != gen)
                    hsh++;

                st.AppendFormat("Key - {0} || Value -> {1} \n", hsh, arr[hsh]);
                gen += 4;
            }
            st.AppendLine("Fim");
            return st.ToString();
        }
    }
}