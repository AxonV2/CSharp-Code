using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Semana3
{
	class Empresa
	{

		private Pessoa[] pessoas;

		public Pessoa[] Pessoas
		{
			get { return pessoas; }
			set { pessoas = value; }
		}

		public Empresa(Pessoa[] lista)
		{
			Pessoas = lista;
		}

		public void Alteracoes()
		{
			int escolha; int alteracao;
			int chk; bool valcheck; bool check = false;
			double salval; Estado cas;
			List<int> indexcheck = new List<int>();
		
			for (int i = 0; i < pessoas.Count(); i++)
			{
				if (pessoas[i] != null && pessoas[i].Trabalha == true)
					check = true;
			}
			if (check)
			{
				Console.WriteLine("\nSELECIONE PELO NUMERO MOSTRADO, aqui tem a opção de alterar ou a situação matrimonial ou o salario do membro desejado\n");
				for (int i = 0; i < pessoas.Count(); i++)
				{
					if (pessoas[i] != null && pessoas[i].Trabalha == true)
					{
						indexcheck.Add(i);
						Console.WriteLine("NUMERO - " + i + "\n------------------------------------\n" + pessoas[i] + "\n------------------------------------\n");
					}
				}
			}
			else
			{
				Console.WriteLine("\nNão existem funcionários para alterar\n");
				Console.ReadKey();
				return;
			}

			do
			{
				valcheck = int.TryParse(Console.ReadLine(), out escolha);
			} while (!indexcheck.Contains(escolha) || pessoas[escolha] == null || valcheck == false);

			Console.Clear();
			Console.WriteLine("Que deseja alterar do funcionário " + pessoas[escolha].Nome);
			Console.WriteLine("(1) para Salário, (2) para Situação Matrimonial , (3) para os dois, (4) para cancelar");
			do
			{
				valcheck = int.TryParse(Console.ReadLine(), out alteracao);
			} while (alteracao <= 0 || alteracao > 4 || valcheck == false);

			if (alteracao == 1)
			{
				Console.WriteLine("Insira um novo valor salarial para " + pessoas[escolha].Nome + " salario atual é " + pessoas[escolha].Salario + "$");

				do
				{
					valcheck = double.TryParse(Console.ReadLine(), out salval);
				} while (valcheck == false || salval <= 0);

				pessoas[escolha].Salario = Math.Round(salval, 2);
			}
			else if (alteracao == 2)
			{
				Console.WriteLine("Alteracao de situacao matrimonial de " + pessoas[escolha].Nome + " situacao atual é " + pessoas[escolha].Casado);
				do
				{
					Console.WriteLine("Casado (1) // Viuvo (2) // Solteiro(3) // Divorciado(4)");
					valcheck = int.TryParse(Console.ReadLine(), out chk);
				} while (chk < 1 || valcheck == false || chk > 4);

				switch (chk)
				{
					case 1:
						cas = Estado.Casado;
						break;

					case 2:
						cas = Estado.Viuvo;
						break;

					default:
					case 3:
						cas = Estado.Solteiro;
						break;

					case 4:
						cas = Estado.Divorciado;
						break;
				}
				pessoas[escolha].Casado = cas;
			}
			else if (alteracao == 3)
			{
				Console.WriteLine("Insira um novo valor salarial para " + pessoas[escolha].Nome + " salario atual é " + pessoas[escolha].Salario + "$");
				do
				{
					valcheck = double.TryParse(Console.ReadLine(), out salval);
				} while (valcheck == false || salval <= 0);

				pessoas[escolha].Salario = Math.Round(salval, 2);

				Console.WriteLine("Alteracao de situacao matrimonial de " + pessoas[escolha].Nome + " situacao atual é " + pessoas[escolha].Casado);
				do
				{
					Console.WriteLine("Casado (1) // Viuvo (2) // Solteiro(3) // Divorciado(4)");
					valcheck = int.TryParse(Console.ReadLine(), out chk);
				} while (chk < 1 || valcheck == false || chk > 4);

				switch (chk)
				{
					case 1:
						cas = Estado.Casado;
						break;

					case 2:
						cas = Estado.Viuvo;
						break;

					default:
					case 3:
						cas = Estado.Solteiro;
						break;

					case 4:
						cas = Estado.Divorciado;
						break;
				}
				pessoas[escolha].Casado = cas;
			}
		}

		public void InserirFuncionario(int cont)
		{
			#region novotrabalhador
			double sal;
			int chk; bool valcheck;
			bool trab = true; int dependentes;
			float defi; int titulares;
			Estado cas;

			Console.WriteLine("Detalhes de novo funcionário");
			Console.WriteLine("Numero de Titulares 1 ou 2");
			do
			{
				valcheck = int.TryParse(Console.ReadLine(), out titulares);
			} while (titulares < 1 || titulares > 2 || valcheck == false);


			Console.WriteLine("Quantos dependentes? (Insira numeros, numero abaixo de 0 será contado como 0)");
			do
			{
				valcheck = int.TryParse(Console.ReadLine(), out dependentes);
			} while (valcheck == false);

			Console.WriteLine("Insira o nome do funcionário novo");
			string nome = Console.ReadLine();

			Console.WriteLine("Salario (Valor Positivo)");
			do
			{
				valcheck = double.TryParse(Console.ReadLine(), out sal);
			} while (valcheck == false || sal <= 0);

			do
			{
				Console.WriteLine("Casado (1) // Viuvo (2) // Solteiro(3) // Divorciado(4)");
				valcheck = int.TryParse(Console.ReadLine(), out chk);
			} while (chk < 1 || valcheck == false || chk > 4);

			switch (chk)
			{
				case 1:
					cas = Estado.Casado;
					break;

				case 2:
					cas = Estado.Viuvo;
					break;

				default:
				case 3:
					cas = Estado.Solteiro;
					break;

				case 4:
					cas = Estado.Divorciado;
					break;
			}

			Console.WriteLine("Deficiencia? (%)");

			do
			{
				valcheck = float.TryParse(Console.ReadLine(), out defi);
			} while (valcheck == false);

			Pessoa extra = new Pessoa(nome, sal, defi, cas, trab, dependentes, titulares);
			pessoas[cont] = extra;
			#endregion novotrabalhador

			Console.Clear();
		}

		public void RemoverFuncionario()
		{
			bool check = false;
			for (int i = 0; i < pessoas.Count(); i++)
			{
				if (pessoas[i] != null && pessoas[i].Trabalha == true)
					check = true;
			}
			List<int> indexcheck = new List<int>();
			if (check)
			{
				Console.WriteLine("\nSELECIONE PELO NUMERO MOSTRADO, aqui tem a opção de remover um membro desejado\n");
				for (int i = 0; i < pessoas.Count(); i++)
				{

					if (pessoas[i] != null && pessoas[i].Trabalha == true)
					{
						indexcheck.Add(i);
						Console.WriteLine("NUMERO - " + i + "\n------------------------------------\n" + pessoas[i] + "\n------------------------------------\n");
					}
				}
			}
			else
			{
				Console.WriteLine("\nNão existem funcionários para Remover\n");
				Console.ReadKey();
				return;
			}

			bool valcheck;
			int escolha;
			Console.WriteLine("Escolha um funcionário para remover (Insira -1 para cancelar)");
			do
			{
				valcheck = int.TryParse(Console.ReadLine(), out escolha);
				if (escolha == -1)
					return;
			} while (!indexcheck.Contains(escolha) || valcheck == false);

			pessoas[escolha].Trabalha = false;
			pessoas[escolha].Salario = 0;
		}

		private double Calculos(Pessoa a)
		{
			#region tabelanova
			if (a.Salario <= 659)
			{
				return a.Dependentes switch
				{
					0 => 0,
					1 => 0,
					2 => 0,
					3 => 0,
					4 => 0,
					_ => 0,
				};
			}
			if (a.Salario > 659 && a.Salario <= 686)
			{
				return a.Dependentes switch
				{
					0 => 0.01,
					1 => 0,
					2 => 0,
					3 => 0,
					4 => 0,
					_ => 0,
				};
			}
			if (a.Salario > 686 && a.Salario <= 718)
			{

				return a.Dependentes switch
				{
					0 => 0.042,
					1 => 0.013,
					2 => 0.009,
					3 => 0.004,
					4 => 0,
					_ => 0,
				};
			}
			if (a.Salario > 718 && a.Salario <= 739)
			{
				return a.Dependentes switch
				{
					0 => 0.073,
					1 => 0.044,
					2 => 0.026,
					3 => 0.007,
					4 => 0,
					_ => 0,
				};
			}
			if (a.Salario > 739 && a.Salario <= 814)
			{
				return a.Dependentes switch
				{
					0 => 0.082,
					1 => 0.053,
					2 => 0.035,
					3 => 0.026,
					4 => 0.007,
					_ => 0,
				};
			}
			if (a.Salario > 814 && a.Salario <= 922)
			{
				return a.Dependentes switch
				{
					0 => 0.104,
					1 => 0.076,
					2 => 0.067,
					3 => 0.039,
					4 => 0.032,
					_ => 0.013,
				};
			}
			if (a.Salario > 922 && a.Salario <= 1005)
			{
				return a.Dependentes switch
				{
					0 => 0.116,
					1 => 0.089,
					2 => 0.081,
					3 => 0.053,
					4 => 0.045,
					_ => 0.032,
				};
			}
			if (a.Salario > 1005 && a.Salario <= 1065)
			{
				return a.Dependentes switch
				{
					0 => 0.124,
					1 => 0.098,
					2 => 0.089,
					3 => 0.062,
					4 => 0.049,
					_ => 0.04,
				};
			}
			if (a.Salario > 1065 && a.Salario <= 1143)
			{
				return a.Dependentes switch
				{
					0 => 0.135,
					1 => 0.117,
					2 => 0.109,
					3 => 0.082,
					4 => 0.073,
					_ => 0.055,
				};
			}
			if (a.Salario > 1143 && a.Salario <= 1225)
			{
				return a.Dependentes switch
				{
					0 => 0.145,
					1 => 0.128,
					2 => 0.118,
					3 => 0.092,
					4 => 0.083,
					_ => 0.065,
				};
			}
			if (a.Salario > 1225 && a.Salario <= 1321)
			{
				return a.Dependentes switch
				{
					0 => 0.156,
					1 => 0.148,
					2 => 0.13,
					3 => 0.11,
					4 => 0.093,
					_ => 0.084,
				};
			}
			if (a.Salario > 1321 && a.Salario <= 1424)
			{
				return a.Dependentes switch
				{
					0 => 0.166,
					1 => 0.158,
					2 => 0.14,
					3 => 0.122,
					4 => 0.103,
					_ => 0.095,
				};
			}
			if (a.Salario > 1424 && a.Salario <= 1562)
			{
				return a.Dependentes switch
				{
					0 => 0.177,
					1 => 0.169,
					2 => 0.15,
					3 => 0.132,
					4 => 0.114,
					_ => 0.105,
				};
			}
			if (a.Salario > 1562 && a.Salario <= 1711)
			{
				return a.Dependentes switch
				{
					0 => 0.191,
					1 => 0.183,
					2 => 0.166,
					3 => 0.147,
					4 => 0.138,
					_ => 0.12,
				};
			}
			if (a.Salario > 1711 && a.Salario <= 1870)
			{
				return a.Dependentes switch
				{
					0 => 0.205,
					1 => 0.199,
					2 => 0.182,
					3 => 0.165,
					4 => 0.157,
					_ => 0.139,
				};
			}
			if (a.Salario > 1870 && a.Salario <= 1977)
			{
				return a.Dependentes switch
				{
					0 => 0.215,
					1 => 0.21,
					2 => 0.191,
					3 => 0.174,
					4 => 0.166,
					_ => 0.149,
				};
			}
			if (a.Salario > 1977 && a.Salario <= 2090)
			{
				return a.Dependentes switch
				{
					0 => 0.225,
					1 => 0.22,
					2 => 0.202,
					3 => 0.183,
					4 => 0.176,
					_ => 0.168,
				};
			}
			if (a.Salario > 2090 && a.Salario <= 2218)
			{
				return a.Dependentes switch
				{
					0 => 0.235,
					1 => 0.23,
					2 => 0.213,
					3 => 0.195,
					4 => 0.185,
					_ => 0.179,
				};
			}
			if (a.Salario > 2218 && a.Salario <= 2367)
			{
				return a.Dependentes switch
				{
					0 => 0.245,
					1 => 0.241,
					2 => 0.233,
					3 => 0.205,
					4 => 0.197,
					_ => 0.188,
				};
			}
			if (a.Salario > 2367 && a.Salario <= 2535)
			{
				return a.Dependentes switch
				{
					0 => 0.255,
					1 => 0.251,
					2 => 0.243,
					3 => 0.216,
					4 => 0.208,
					_ => 0.20,
				};
			}
			if (a.Salario > 2535 && a.Salario <= 2767)
			{
				return a.Dependentes switch
				{
					0 => 0.265,
					1 => 0.26,
					2 => 0.253,
					3 => 0.226,
					4 => 0.218,
					_ => 0.21,
				};
			}
			if (a.Salario > 2767 && a.Salario <= 3104)
			{
				return a.Dependentes switch
				{
					0 => 0.278,
					1 => 0.273,
					2 => 0.265,
					3 => 0.238,
					4 => 0.23,
					_ => 0.222,
				};
			}
			if (a.Salario > 3104 && a.Salario <= 3534)
			{
				return a.Dependentes switch
				{
					0 => 0.294,
					1 => 0.293,
					2 => 0.289,
					3 => 0.265,
					4 => 0.261,
					_ => 0.257,
				};
			}
			if (a.Salario > 3534 && a.Salario <= 4118)
			{
				return a.Dependentes switch
				{
					0 => 0.305,
					1 => 0.305,
					2 => 0.299,
					3 => 0.285,
					4 => 0.271,
					_ => 0.267,
				};
			}
			if (a.Salario > 4181 && a.Salario <= 4650)
			{
				return a.Dependentes switch
				{
					0 => 0.323,
					1 => 0.32,
					2 => 0.316,
					3 => 0.299,
					4 => 0.286,
					_ => 0.282,
				};
			}
			if (a.Salario > 4650 && a.Salario <= 5194)
			{
				return a.Dependentes switch
				{
					0 => 0.333,
					1 => 0.33,
					2 => 0.326,
					3 => 0.312,
					4 => 0.305,
					_ => 0.292,
				};
			}
			if (a.Salario > 5194 && a.Salario <= 5880)
			{
				return a.Dependentes switch
				{
					0 => 0.343,
					1 => 0.34,
					2 => 0.336,
					3 => 0.322,
					4 => 0.318,
					_ => 0.301,
				};
			}
			if (a.Salario > 5880 && a.Salario <= 6727)
			{
				return a.Dependentes switch
				{
					0 => 0.363,
					1 => 0.361,
					2 => 0.355,
					3 => 0.348,
					4 => 0.346,
					_ => 0.344,
				};
			}
			if (a.Salario > 6727 && a.Salario <= 7939)
			{
				return a.Dependentes switch
				{
					0 => 0.373,
					1 => 0.371,
					2 => 0.369,
					3 => 0.358,
					4 => 0.356,
					_ => 0.354,
				};
			}
			if (a.Salario > 7939 && a.Salario <= 9560)
			{
				return a.Dependentes switch
				{
					0 => 0.393,
					1 => 0.391,
					2 => 0.389,
					3 => 0.378,
					4 => 0.376,
					_ => 0.374,
				};
			}
			if (a.Salario > 9560 && a.Salario <= 11282)
			{
				return a.Dependentes switch
				{
					0 => 0.403,
					1 => 0.401,
					2 => 0.399,
					3 => 0.392,
					4 => 0.386,
					_ => 0.384,
				};
			}
			if (a.Salario > 11282 && a.Salario <= 18854)
			{
				return a.Dependentes switch
				{
					0 => 0.413,
					1 => 0.411,
					2 => 0.409,
					3 => 0.402,
					4 => 0.40,
					_ => 0.394,
				};
			}
			if (a.Salario > 18854 && a.Salario <= 20221)
			{
				return a.Dependentes switch
				{
					0 => 0.423,
					1 => 0.421,
					2 => 0.419,
					3 => 0.412,
					4 => 0.41,
					_ => 0.404,
				};
			}
			if (a.Salario > 20221 && a.Salario <= 22749)
			{
				return a.Dependentes switch
				{
					0 => 0.431,
					1 => 0.431,
					2 => 0.429,
					3 => 0.422,
					4 => 0.42,
					_ => 0.416,
				};
			}
			if (a.Salario > 22749 && a.Salario <= 25276)
			{
				return a.Dependentes switch
				{
					0 => 0.441,
					1 => 0.441,
					2 => 0.439,
					3 => 0.432,
					4 => 0.43,
					_ => 0.428,
				};
			}
			if (a.Salario > 25276)
			{
				return a.Dependentes switch
				{
					0 => 0.451,
					1 => 0.451,
					2 => 0.449,
					3 => 0.442,
					4 => 0.44,
					_ => 0.438,
				};
			}

			#endregion tabelanova
			return 0;
		}

		public void Switch(ref Pessoa i, ref Pessoa b)
		{
			Pessoa temp = i;
			i = b;
			b = temp;
		}

		public Pessoa[] BubbleSortList(int cont)
		{
			Pessoa[] ordered = new Pessoa[cont];
			Array.Copy(pessoas, ordered, cont);

			for (int i = 0; i < cont; i++)
			{
				for (int l = 0; l <= cont - 2; l++)
				{
					if (ordered[l].Dependentes > ordered[l + 1].Dependentes)
						Switch(ref ordered[l], ref ordered[l + 1]);

					else if (ordered[l].Dependentes == ordered[l + 1].Dependentes)
						if (string.Compare(ordered[l].Nome, ordered[l + 1].Nome, true) > 0)
							Switch(ref ordered[l], ref ordered[l + 1]);
				}
			}
			return ordered;
		}

		public Pessoa[] SelectionSortList(int cont)
		{

			Pessoa[] ordered = new Pessoa[cont];
			Array.Copy(pessoas, ordered, cont);

			for (int i = 0; i < cont; i++)
			{
				int min = i;
				for (int l = i + 1; l < cont; l++)
					if (string.Compare(ordered[l].Nome, ordered[min].Nome, true) < 0)
						min = l;

				if (min != i)
				{
					Switch(ref ordered[i], ref ordered[min]);
				}
			}
			return ordered;
		}

		public override string ToString()
		{
			string l = "";
			double retencao = 0;
			double despesastotais = 0;

			//https://www.cgd.pt/Site/Saldo-Positivo/negocios/Pages/custo-trabalhador-empresa.aspx
			//Subsidio alimentacao
			double SA = 6.83 * 21 * 11 / 12;
			double TSU, ST;

			for (int i = 0; i < pessoas.Count(); i++)
			{
				if (pessoas[i] != null && pessoas[i].Trabalha == true)
				{
					//taxa social unica SS
					TSU = pessoas[i].Salario * 0.2375 * 14 / 12;
					//seguro trabalho
					ST = pessoas[i].Salario * 0.01 * 14 / 12;
					despesastotais += TSU + ST + SA;
					retencao += Math.Round((Calculos(pessoas[i]) * pessoas[i].Salario), 2);

					string nome = char.ToUpper(pessoas[i].Nome[0]) + pessoas[i].Nome.Substring(1).ToLower();

					l += "Nome Funcionário - " + nome + "\nSalario - " + pessoas[i].Salario + "$\nDesconto Percentagem - " + Math.Round((Calculos(pessoas[i]) * 100), 2) + "%\nRetenção(Desconto em valor) - " + Math.Round((Calculos(pessoas[i]) * pessoas[i].Salario), 2)
						+ "$\nSalario Liquido (apos desconto) - " + Math.Round(pessoas[i].Salario - (pessoas[i].Salario * Calculos(pessoas[i])), 2) + "$\nTaxa Social Unica - " + Math.Round(TSU, 2) + "$\nSeguro Trabalho - " + Math.Round(ST, 2) + "$\n\n-----------------------------------------------\n";
				}
			}
			return l + "        CUSTOS FUNCIONÁRIOS(MENSAIS)\n-----------------------------------------------\nSubsidio Alimentacao(131,48$ por Func) - " + Math.Round(SA * pessoas.Count(), 2) + "$\nRetenção Total - " + retencao + "$\nDespesas Totais(SA + TSU + ST) - " + Math.Round(despesastotais, 2) + "$\nDespesas Anuais - " + Math.Round((despesastotais * 12), 2) + "$\n\n";
		}
	}
}