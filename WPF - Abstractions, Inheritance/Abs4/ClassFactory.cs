using System;
using System.Collections.Generic;
using System.Text;
using Abs4CL;

namespace Abs4
{
    public static class ClassFactory
    {
        public static IComedia ComediaFactory(string DirectorName, string NameValue, int YearValue, float DurationValue, List<string> Actors)
        {
            return new Comedia(DirectorName, NameValue, YearValue, DurationValue, Actors);
        }

        public static IComedia ComediaFactory(string DirectorName, string NameValue, int YearValue, float DurationValue, params string[] Actors)
        {
            return new Comedia(DirectorName, NameValue, YearValue, DurationValue, Actors);
        }
    }
}
