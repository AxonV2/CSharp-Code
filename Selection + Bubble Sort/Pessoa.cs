using System.Collections.Generic;
using System.Text;

namespace Semana3
{
	class Pessoa
	{
		private string nome;
		public string Nome { get { return nome; } }

		private int dependentes;

		public int Dependentes
		{
			get { return dependentes; }
			set
			{
				if (value < 0)
					value = 0;
				dependentes = value;
			}
		}

		private int titulares;

		public int Titulares
		{
			get { return titulares; }
			set { titulares = value; }
		}


		private double salario;
		public double Salario
		{
			get { return salario; }
			set
			{
				if (value < 0)
					salario = 0;
				else
					salario = value;
			}
		}


		private float deficiencia;
		public float Deficiencia
		{
			get { return deficiencia; }
			set
			{
				if (value < 0)
					value = 0;
				else if (value > 100)
					value = 100;
				deficiencia = value;
			}
		}


		private Estado casado;
		public Estado Casado
		{
			get { return casado; }
			set { casado = value; }
		}


		private bool trabalha;
		public bool Trabalha
		{
			get { return trabalha; }
			set { trabalha = value; }
		}


		public Pessoa()
		{
			nome = "";
			salario = 0;
			deficiencia = 0;
			casado = Estado.Solteiro;
			trabalha = false;
			dependentes = 0;
			titulares = 0;
		}

		public Pessoa(string nomeval, double salarioval, float defi, Estado cas, bool traba, int dependentesval, int titularesval)
		{
			nome = nomeval;
			Salario = salarioval;
			Deficiencia = defi;
			Casado = cas;
			trabalha = traba;
			Dependentes = dependentesval;
			Titulares = titularesval;
		}

		public override string ToString()
		{
			string nomecor = char.ToUpper(Nome[0]) + Nome.Substring(1).ToLower();
			return "Nome - " + nomecor + "\nDeficiencia - " + deficiencia + "%\nEstado - " + casado + "\nTrabalha - " + trabalha + "\nSalário - " + salario + "$\nTitulares - " + titulares + "\nDependentes " + dependentes;
		}

	}
}
