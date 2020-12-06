using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTest
{
    public static class Tools
    {
        public static Random rand = new Random();

        public class NumeroBIException : Exception
        {
            public NumeroBIException(string message) : base(message)
            {
            }

            public NumeroBIException(string message, Exception inEx) : base(message, inEx)
            {
            }
        }

        public static void Swap<T>(ref T swp1, ref T swp2)
        {
            T temp;
            temp = swp1;
            swp1 = swp2;
            swp2 = temp;
        }


        public static T Test<T>(T var)
        {
            T lol = var;
            return lol;
        }


        public static bool PrimeChecker(int Value)
        {
            bool Prime = true;
            for (int i = 2; i <= Value / 2; i++)
                if (Value % i == 0)
                    Prime = false;

            return Prime;
        }

        public static bool PrimeChecker(double Value)
        {
            bool Prime = true;
            for (int i = 2; i <= Value / 2; i++)
                if (Value % i == 0)
                    Prime = false;

            return Prime;
        }

        public static async Task<bool> PrimeChecker_Async(int Value)
        {
            bool Prime = true;
            return await Task.Run(() =>
            {
                for (int i = 2; i <= Value / 2; i++)
                    if (Value % i == 0)
                        Prime = false;

                return Prime;
            });
        }

        public static async Task<bool> PrimeChecker_Async(double Value)
        {
            bool Prime = true;
            return await Task.Run(() =>
            {
                for (double i = 2; i <= Value / 2; i++)
                    if (Value % i == 0)
                        Prime = false;

                return Prime;
            });
        }

        public static bool EmptyCheck(string a)
        {
            return string.IsNullOrWhiteSpace(a);
        }

        public static string GenerateRandomID(int max)
        {
            // double alea.nextDouble()*(LS - LI) + li |||| int alea.next(LI, LS+1)
            StringBuilder st = new StringBuilder();
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            //starting Letter
            for (int i = 0; i < 3; i++)
                st.Append(chars[rand.Next(0, chars.Length - 1)]);


            //numbers
            for (int i = 0; i < max; i++)
                st.Append(rand.Next(10));

            return st.ToString();
        }

        private static string FizzBuzz(int n)
        {
            //THIS IS CODE SMELL, THE POINT OF GENERIC IS TO BE GENERIC
            //YOU'RE BETTER OFF OVERLOADING IF THIS IS THE CASE
            //if(typeof(T) == typeof(int) || typeof(T) == typeof(double))


            //MOST UNCOMMON ONE FIRST FOR CHECKING PURPOSES
            //if (n % 3 == 0 && n % 5 == 0)
            //    return "FizzBuzz";

            //if (n % 3 == 0)
            //    return "Fizz";

            //if(n% 5 == 0)
            //    return "Buzz";

            switch (n)
            {
                case int n1 when (n1 % 3 == 0):
                    return "Fizz";
                case int n1 when (n1 % 5 == 0):
                    return "Buzz";
            }

            return "";
        }

        //METHOD OVERLOADING SAME METHOD NAME DIFFERENT TYPE
        private static string FizzBuzz(double n)
        {

            //MOST UNCOMMON ONE FIRST FOR CHECKING PURPOSES
            //if (n % 3 == 0 && n % 5 == 0)
            //    return "FizzBuzz";

            //if (n % 3 == 0)
            //    return "Fizz";

            //if(n% 5 == 0)
            //    return "Buzz";

            switch (n)
            {
                case double n1 when (n1 % 3 == 0):
                    return "Fizz";
                case double n1 when (n1 % 5 == 0):
                    return "Buzz";
            }

            return "";
        }



        public static List<T> LoadFromTextFile<T>(string filePath) where T : class, new()
        {
            var lines = System.IO.File.ReadAllLines(filePath).ToList();
            List<T> output = new List<T>();
            T entry = new T();

            //Gets the type of the class recieved and then its properties
            //If we pass in Person, Type = Person, Properties = FirstName, LastName, IsAlive
            var cols = entry.GetType().GetProperties();

            // Checks to be sure we have at least one header row and one data row
            if (lines.Count < 2)
                throw new IndexOutOfRangeException("The file was either empty or missing.");

            // Splits the header into one column header per entry
            var headers = lines[0].Split(',');

            // Removes the header row from the lines so we don't
            // have to worry about skipping over that first row.
            lines.RemoveAt(0);

            foreach (var row in lines)
            {
                entry = new T();

                // Splits the row into individual columns. Now the index
                // of this row matches the index of the header so the
                // FirstName column header lines up with the FirstName
                // value in this row.
                var vals = row.Split(',');

                // Loops through each header entry so we can compare that
                // against the list of columns from reflection. Once we get
                // the matching column, we can do the "SetValue" method to 
                // set the column value for our entry variable to the vals
                // item at the same index as this particular header.
                for (var i = 0; i < headers.Length; i++)
                {
                    foreach (var col in cols)
                    {
                        if (col.Name == headers[i])
                            //Converting type, cols is properties, so if col property is boolean, vals will be boolean too
                            col.SetValue(entry, Convert.ChangeType(vals[i], col.PropertyType));
                    }
                }

                output.Add(entry);
            }

            return output;
        }


        public static void SaveToTextFile<T>(List<T> data, string filePath) where T : class
        {
            List<string> lines = new List<string>();
            StringBuilder line = new StringBuilder();

            if (data == null || data.Count == 0)
                throw new ArgumentNullException("data", "You must populate the data parameter with at least one value.");


            var cols = data[0].GetType().GetProperties();

            // Loops through each column and gets the name so it can comma 
            // separate it into the header row.
            foreach (var col in cols)
            {
                line.Append(col.Name);
                line.Append(",");
            }

            // Adds the column header entries to the first line (removing
            // the last comma from the end first).
            lines.Add(line.ToString().Substring(0, line.Length - 1));

            foreach (var row in data)
            {
                line = new StringBuilder();

                foreach (var col in cols)
                {
                    line.Append(col.GetValue(row));
                    line.Append(",");
                }

                // Adds the row to the set of lines (removing
                // the last comma from the end first).
                lines.Add(line.ToString().Substring(0, line.Length - 1));
            }

            System.IO.File.WriteAllLines(filePath, lines);
        }

    }
}
