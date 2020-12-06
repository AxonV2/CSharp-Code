using System.Text.RegularExpressions;

namespace Projeto.Model
{
	class Paragem
	{

		private string codparagem;

		public string CodParagem
		{
			get { return codparagem; }
			set { codparagem = value.ToUpper(); }
		}


		private string nome;
		public string Nome
		{
			get { return nome; }
			set
			{
				Regex stringcheck = new Regex(@"\s\s+", RegexOptions.Compiled); //	procupara e agrupa os espaços 
				value = stringcheck.Replace(value, " ");                        //	substitui esses espaços por um espaço
				value = (char.ToUpper(value[0]) + value.Substring(1).ToLower()).Trim();
				nome = value;
			}
		}

		private string concelho;
		public string Concelho { get { return concelho; } set { concelho = value; } }


		private string zona;
		public string Zona { get { return zona; } set { zona = value; } }

		//				Construtor vazio
		public Paragem()
		{
			codparagem = "";
			nome = "";
			concelho = "";
			zona = "";
		}

		//				Construtor
		public Paragem(string codparagemValue, string nomeValue, string conselhoValue, string zonaValue)
		{
			CodParagem = codparagemValue;
			Nome = nomeValue;
			Concelho = conselhoValue;
			Zona = zonaValue;
		}


		public override string ToString()
		{
			return codparagem + " - " + nome + ", " + concelho + " - " + zona;
		}

	}
}
