using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using System.Text;

namespace Ex12
{
    class Artigo
    {
        private static int idgen = 1;

        //Always the same no matter the instance
        int generateId()
        {
            return idgen+= 4;
        }

        private int id;
        public int ID { get { return id; }  set { id = value; } }

        private string desig;
        public string Desig { get { return desig; } set { desig = value; } }

        private double preco;
        public double Preco { get { return preco; } set { preco = value; } }

        private float peso;
        public float Peso { get { return peso; } set { peso = value; } }

        private bool disp;
        public bool Disp { get { return disp; } set { disp = value; } }

        private int stock;
        public int Stock { get { return stock; } set { stock = value; } }

        public Artigo(string desigval, double precoval, float pesoval, bool dispval, int stockval )
        {
            ID = generateId();
            Desig = desigval;
            Preco = precoval;
            Peso = pesoval;
            Disp = dispval;
            Stock = stockval;
        }

        public override string ToString()
        {
                return "ID - " + id + " || Des - " + Desig + " || Preço - " + preco + "$ || Peso - " + peso + "Kg || Disp - verdade || Stock - " + stock + (disp? "": " - Descontinuado").ToString();       
        }
    }
}
