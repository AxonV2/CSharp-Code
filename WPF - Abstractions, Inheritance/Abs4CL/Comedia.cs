using System;
using System.Collections.Generic;
using System.Text;

namespace Abs4CL
{
    public class Comedia : Filme, IComedia
    {
        private string director;

        public string Director
        {
            get { return director; }
            set { director = value; }
        }

        //LIST DEFINED HERE, INSIDE CLASS, TO AVOID REFERENCE MISHANDLING 
        public List<string> Actors { get; set; } = new List<string>();


        //Everything is chained upwards to avoid needless repetition
        public Comedia(string DirectorName, string NameValue, int YearValue, float DurationValue) : base(NameValue, YearValue, DurationValue)
        {
            Director = DirectorName;
        }

        public Comedia(string DirectorName, string NameValue, int YearValue, float DurationValue, params string[] actorsParam) : this(DirectorName, NameValue, YearValue, DurationValue)
        {
            //Accounting for the possibility of empty strings being passed
            foreach (string item in actorsParam)
            {
                if (string.IsNullOrEmpty(item))
                    continue;
                else
                    Actors.Add(item);
            }
        }

        public Comedia(string DirectorName, string NameValue, int YearValue, float DurationValue, List<string> Actors) : this(DirectorName, NameValue, YearValue, DurationValue)
        {
            this.Actors.AddRange(Actors);
        }


        public override string ToString()
        {
            StringBuilder st = new StringBuilder();
            st.Append($" Acting Director - {Director}, Chosen Cast - ");
            foreach (string item in Actors)
            {
                st.Append($"{item},");
            }
           
            return base.ToString() + st.ToString();
        }
    }
}
