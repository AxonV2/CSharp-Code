using System;

namespace Abs4CL
{
    public abstract class Filme : IFilme
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int year;

        public int Year
        {
            get { return year; }
            set { year = value; }
        }


        private float duration;

        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        //METHOD OVERLOADING
        public bool LengthyCheck(float param)
        {
            if (param >= 100)
                return true;

            return false;
        }

        //T derived from abstract Filme
        public bool LengthyCheck<T>(T param) where T : Filme, new()
        {
            if (param.Duration >= 100)
                return true;

            return false;
        }

        public Filme()
        {
        }

        public Filme(string NameValue, int YearValue, float DurationValue)
        {
            Name = NameValue;

            if (YearValue < 1980 || YearValue > 2005)
                YearValue = 0;
            Year = YearValue;

            if (DurationValue <= 0)
                DurationValue = 0;
            Duration = DurationValue;
        }

        public override string ToString()
        {
            return $"Movie Title - {Name}, Release Year - {Year}, Movie Length - {Duration}";
        }

    }
}
