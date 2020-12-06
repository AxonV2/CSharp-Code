using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Projeto.Controller
{
    class CController
    {

        private string con;

        public string Con
        {
            get { return con; }
            set { con = value; }
        }


        public CController()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["ConString"];
            Con = settings.ToString();
        }


        public DataTable Fill(string query)
        {
            using (SqlConnection conn = new SqlConnection(con))
            using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
            {
                //conn.Open();
                DataTable Table = new DataTable();
                adapter.Fill(Table);
                return Table;
            }
        }

        public string Zona(ComboBox CB)
        {
            string zonatext = "";
            DataTable table = Fill("Select Freguesia, ZonaREF From Enum_Freguesia WHERE Freguesia = '" + CB.Text + "'");
            try
            {
                var tableRow = table.AsEnumerable().First();
                zonatext = tableRow.Field<string>("ZonaREF");
            }
            catch (Exception)
            {
            }
        
            return zonatext;
        }

        #region inserir
        public void InsertCarr(Carreira car)
        {
            string query = "INSERT INTO Carreiras VALUES (@cod)";
            using (SqlConnection conn = new SqlConnection(con))
            using (SqlCommand command = new SqlCommand(query, conn))
            {
                conn.Open();
                command.Parameters.AddWithValue("@cod", car.CodCarreira);
                command.ExecuteNonQuery();
            }
        }

        public void InsertPara(Paragem p1)
        {
            using (SqlConnection conn = new SqlConnection(con))
            using (SqlCommand command = new SqlCommand("INSERT INTO Paragem VALUES (@cod, @nome, @conc)", conn))
            {
                conn.Open();
                #region insertrestriction
                //SqlCommand cmd2 = new SqlCommand("SELECT count(*) FROM Paragem WHERE CodigoPar= @cod", conn);
                //cmd2.Parameters.AddWithValue("@cod", CodParagem);
                //var chk = cmd2.ExecuteScalar();
                //if ((int)chk != 0)
                //{
                //    MessageBox.Show("Cod Paragem já inserido");
                //    return
                //}
                //else
                #endregion insertrestriction
                command.Parameters.AddWithValue("@cod", p1.CodParagem);
                command.Parameters.AddWithValue("@nome", p1.Nome);
                command.Parameters.AddWithValue("@conc", p1.Concelho);
                command.ExecuteNonQuery();
            }
        }

        public void InsertRel(Carreira car)
        {
            SqlConnection conn = new SqlConnection(con);
            for (int i = 0; i < car.Circuito.Count; i++)
            {
                //No caso de ser para atualizar se linha já existir não adicionar outra vez repetida
                SqlCommand check = new SqlCommand("SELECT COUNT(*) FROM [Rel.CarPas] WHERE IDParagem = @par AND IDCarreira = @car", conn);
                check.Parameters.AddWithValue("@par", car.Circuito[i].CodParagem);
                check.Parameters.AddWithValue("@car", car.CodCarreira);
                conn.Open();
                int existe = (int)check.ExecuteScalar();
                conn.Close();

                if (existe <= 0)
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO [Rel.CarPas] VALUES (@Idcar, @Idlocal)", conn))
                    {
                        SqlCommand command2 = new SqlCommand("INSERT INTO Passagem(Linha, Local) VALUES (@Idcar, @Idlocal)", conn);
                        conn.Open();
                        command.Parameters.AddWithValue("@Idcar", car.CodCarreira);
                        command.Parameters.AddWithValue("@Idlocal", car.Circuito[i].CodParagem);
                        command.ExecuteNonQuery();
                        command2.Parameters.AddWithValue("@Idcar", car.CodCarreira);
                        command2.Parameters.AddWithValue("@Idlocal", car.Circuito[i].CodParagem);
                        command2.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
        }

        public void InsertPass(Passagem ps)
        {
            using (SqlConnection conn = new SqlConnection(con))
            using (SqlCommand command = new SqlCommand("UPDATE Passagem SET Hora = @hora, Dia = @dia WHERE Linha = @linha AND Local = @local", conn))
            {
                conn.Open();
                command.Parameters.AddWithValue("@hora", ps.Hora);
                command.Parameters.AddWithValue("@dia", ps.Dia);
                command.Parameters.AddWithValue("@linha", ps.Linha.CodCarreira);
                command.Parameters.AddWithValue("@local", ps.Local.CodParagem);
                command.ExecuteNonQuery();
            }
        }

        #endregion inserir

        #region extract
        public List<Paragem> ExtractPara()
        {
            List<Paragem> ext = new List<Paragem>();

            using (SqlConnection conn = new SqlConnection(con))
            using (SqlDataAdapter adapter = new SqlDataAdapter("Select P.CodigoPar, P.Nome, E.Freguesia, E.ZonaREF From Paragem as P JOIN Enum_Freguesia as E ON P.Concelho = E.Freguesia", conn))
            {
                DataTable Table = new DataTable();
                adapter.Fill(Table);

                ext = (from DataRow dr in Table.Rows
                       select new Paragem()
                       {
                           CodParagem = dr["CodigoPar"].ToString(),
                           Nome = dr["Nome"].ToString(),
                           Concelho = dr["Freguesia"].ToString(),
                           Zona = dr["ZonaREF"].ToString()
                       }).ToList();
            }
            return ext;
        }


        public List<Passagem> ExtractPass(List<Paragem> ListaPar, List<Carreira> ListaCar)
        {
            List<Passagem> ext = new List<Passagem>();

            using (SqlConnection conn = new SqlConnection(con))
            using (SqlDataAdapter adapter = new SqlDataAdapter("Select * From Passagem", conn))
            {
                DataTable Table = new DataTable();
                adapter.Fill(Table);

                Passagem novo = new Passagem();

                foreach (DataRow item in Table.Rows)
                {
                    if (!item.IsNull(0) && !item.IsNull(1) && !item.IsNull(2) && !item.IsNull(3))
                    {
                        ext.Add(novo = new Passagem(ListaCar.Find(x => x.CodCarreira == (int)item["Linha"]), ListaPar.Find(x => x.CodParagem == item["Local"].ToString()), TimeSpan.Parse(item["Hora"].ToString()), item["Dia"].ToString()));
                    }
                }

            }
            return ext;
        }


        public List<Carreira> ExtractCarr(List<Paragem> ListaPar)
        {
            List<Carreira> ext = new List<Carreira>();
            using (SqlConnection conn = new SqlConnection(con))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("Select * From Carreiras", conn))
                {
                    DataTable Table = new DataTable();
                    adapter.Fill(Table);

                    ext = (from DataRow dr in Table.Rows
                           select new Carreira()
                           {
                               CodCarreira = (int)dr["CodigoCar"],
                               Circuito = ExtractSup(ListaPar, (int)dr["CodigoCar"])
                           }).ToList();
                }
            }
            return ext;
        }

        public List<Paragem> ExtractSup(List<Paragem> ListaPar, int Cod)
        {
            List<Paragem> ext = new List<Paragem>();

            using (SqlConnection conn = new SqlConnection(con))
            using (SqlCommand command = new SqlCommand("SELECT * FROM [Rel.CarPas] WHERE IDCarreira = @cod", conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@cod", Cod);
                DataTable Table = new DataTable();
                adapter.Fill(Table);

                foreach (DataRow item in Table.Rows)
                {
                    ext.Add(ListaPar.Find(x => x.CodParagem == item["IDParagem"].ToString()));
                }
            }
            return ext;
        }

        #endregion extract
    }
}
