using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Projeto.Model
{

	class Carreira
	{

		private int codCarreira;
		public int CodCarreira { get { return codCarreira; } set { codCarreira = value; } }

		public List<Passagem> Horario { get; set; } = new List<Passagem>();

        public List<Paragem> Circuito { get; set; } = new List<Paragem>();


		//				Construtor
		public Carreira(int codigoValue, List<Paragem> circuitoValue, List<Passagem> horarioValue)
		{
			CodCarreira = codigoValue;

			//precisa de guardar numa var aqui na classe por causa do clear dado a seguir ao form
			//se retirar este var circuitoC, depois é dado o clear da lista e esta instancia da carreira perde o seu circuito
			Circuito = circuitoValue;
			Horario = horarioValue;
		}

        public Carreira()
        {
			CodCarreira = 0;
        }
		public override string ToString()
		{
			StringBuilder strbld = new StringBuilder();

			strbld.Append("CodCarreira - " + CodCarreira + "\nParagens(");

			return strbld.ToString();
		}

	}
}
