using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Model
{
	class Passagem
	{

		private Carreira linha;
		public Carreira Linha { get { return linha; } set { linha = value; } }

		private TimeSpan hora;
		public TimeSpan Hora
		{
			get { return hora; }
			set { hora = value; }
		}

		private Paragem local;
		public Paragem Local { get { return local; } set { local = value; } }

		private string dia;
		public string Dia { get { return dia; } set { dia = value; } }

		public Passagem(Carreira IdCarreira, Paragem localValue, TimeSpan horaValue, string diaValue)
		{
			Linha = IdCarreira;
			Local = localValue;
			Hora = horaValue;
			Dia = diaValue;
		}

        public Passagem()
        {
		}

		public override string ToString()
		{
			//ToString("HH:mm")
			return "Paragem - " + local + " ás - " + hora.ToString() + " horas aos " + dia + "\n";
		}
	}
}
