using Projeto.Controller;
using Projeto.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Projeto
{
    public partial class Form1 : Form
    {
        CController control = new CController();

        public Form1()
        {
            InitializeComponent();
        }

        //codigo para arrastar a janela so na parte de cima
        #region arrastar
        private const int cGrip = 16;      // esta a segurar (grip size)
        private const int cCaption = 32;   // barra altura


        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);
                if (pos.Y < cCaption)
                {
                    m.Result = (IntPtr)2;
                    return;
                }
                if (pos.X >= this.ClientSize.Width - cGrip && pos.Y >= this.ClientSize.Height - cGrip)
                {
                    m.Result = (IntPtr)17;
                    return;
                }
            }
            base.WndProc(ref m);
        }
        #endregion arrastar

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaPara.AddRange(control.ExtractPara());
            ListaCarreira.AddRange(control.ExtractCarr(ListaPara));
            ListaPassagens.AddRange(control.ExtractPass(ListaPara, ListaCarreira));

            foreach (Carreira item in ListaCarreira)
            {
                item.Horario.AddRange(ListaPassagens.FindAll(x => x.Linha.CodCarreira == item.CodCarreira));
            }

            comboBox13.DataSource = control.Fill("Select Freguesia From Enum_Freguesia");
            comboBox13.DisplayMember = "Freguesia";
            comboBox13.ValueMember = "Freguesia";

            comboBox14.DataSource = control.Fill("Select Zona From Enum_Zona");
            comboBox14.DisplayMember = "Zona";
            comboBox14.ValueMember = "Zona";
            comboBox14.Text = "PV";


        }

        //---------------------------------------------------------------------------------------
        //                                      Variáveis
        //---------------------------------------------------------------------------------------
        List<Paragem> ListaPara = new List<Paragem>();              //  lista de paragens
        List<Paragem> ListaCircuito = new List<Paragem>();          //  lista de circuito de paragens 
        List<Carreira> ListaCarreira = new List<Carreira>();        //  lista de carreiras     
        List<Passagem> ListaPassagens = new List<Passagem>();       //  lista de horario

        //---------------------------------------------------------------------------------------
        //                                      Menuu
        //---------------------------------------------------------------------------------------
        #region navmenus

        private void button1_Click(object sender, EventArgs e)      //  BOTAO de adicionar carreira 
        {
            panel6.Visible = false;
            panel4.Visible = false;                             //  desaparece o painel de passagem
            panel3.Visible = true;                              //  mostra o painel de carreira
            panel2.Visible = false;                             //  desaparece o painel de paragem

            dataGridView3.Visible = false;
            dataGridView2.Visible = true;
            dataGridView1.DataSource = control.Fill("Select * From Carreiras ORDER BY CodigoCar");
            dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia ORDER BY Pas.Linha , Pas.Hora, Pas.Dia, Pas.Local");

            checkedListBox1.Items.Clear();
            foreach (Paragem item in ListaPara)                 //  procura e imprime todas as variáveis do tipo Paragem que estão 
                checkedListBox1.Items.Add(item);                //      dentro da ListPara
        }
        private void button2_Click(object sender, EventArgs e)      //  BOTAO de adicionar paragens
        {
            dataGridView3.DataSource = control.Fill("Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', E.Freguesia as 'Concelho', E.ZonaREF as 'Zona' From Paragem as P JOIN Enum_Freguesia as E ON P.Concelho = E.Freguesia ORDER BY P.CodigoPar");
            comboBox3.DataSource = control.Fill("Select Freguesia From Enum_Freguesia");
            comboBox3.DisplayMember = "Freguesia";
            comboBox3.ValueMember = "Freguesia";
            textBox5.Text = "PV";

            dataGridView3.Visible = true;
            dataGridView2.Visible = false;

            richTextBox1.Text = "";
            foreach (Paragem item in ListaPara)
            {
                richTextBox1.Text += item.ToString() + "\n";
            }

            panel6.Visible = false;
            panel2.Visible = true;                                  //  mostra apenas o painel de paragem
            panel3.Visible = false;
            panel4.Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)      //  BOTAO de adicionar passagem 
        {
            comboBox4.SelectedIndex = 0;
            comboBox5.Items.Clear();
            dataGridView2.DataSource = "";

            foreach (Carreira item in ListaCarreira)
                comboBox5.Items.Add(item.CodCarreira);

            dataGridView3.Visible = false;
            dataGridView2.Visible = true;

            dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia ORDER BY Pas.Linha, Pas.Hora, Pas.Dia, Pas.Local");

            panel6.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = true;
        }

        public void resetpesq()
        {
            comboBox2.ResetText();
            comboBox9.ResetText();
            comboBox10.ResetText();
            comboBox12.ResetText();
            comboBox13.ResetText();
            comboBox14.ResetText();
            textBox4.Text = "";
            textBox6.Text = "";
            maskedTextBox3.ResetText();
            maskedTextBox1.ResetText();

            checkedListBox3.Items.Clear();
            for (int i = 0; i < checkedListBox3.Items.Count; i++)
                checkedListBox3.SetItemChecked(i, false);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;

            comboBox2.Items.Clear();
            comboBox9.Items.Clear();
            comboBox12.Items.Clear();
            resetpesq();

            foreach (Carreira item in ListaCarreira)
                comboBox2.Items.Add(item.CodCarreira);

            foreach (Paragem item in ListaPara)
            {
                comboBox12.Items.Add(item.Nome);
                comboBox9.Items.Add(item.CodParagem);
                checkedListBox3.Items.Add(item);
            }


            dataGridView2.DataSource = "";
            dataGridView2.DataSource = control.Fill("Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Linha as Carreira, Pas.Dia as Horário, Pas.Hora as 'Hora de Passagem' From Paragem as P LEFT JOIN Passagem as Pas ON P.CodigoPar = Pas.Local JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia ORDER BY P.CodigoPar");

            dataGridView3.Visible = false;
            dataGridView2.Visible = true;
            comboBox1.SelectedIndex = 0;



        }

        #endregion navmenus


        //---------------------------------------------------------------------------------------
        //                                      Saídas
        //---------------------------------------------------------------------------------------
        #region saidas
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox5_Click_1(object sender, EventArgs e)
        {
            panel4.Visible = false;
            dataGridView3.DataSource = "";
        }

        private void pictureBox3_Click_1(object sender, EventArgs e)
        {
            panel2.Visible = false;
            dataGridView3.DataSource = "";
        }

        private void pictureBox4_Click_1(object sender, EventArgs e)
        {
            panel3.Visible = false;
            dataGridView3.DataSource = "";
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            panel6.Visible = false;
            dataGridView3.DataSource = "";
        }




        #endregion saidas



        //---------------------------------------------------------------------------------------
        //                                      Paragem
        //---------------------------------------------------------------------------------------
        #region paragem
        private void button3_Click(object sender, EventArgs e)
        {
            //  verificação se ocuparam
            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Insira um nome e código de paragem", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //  verificação se existe um código igual
            if (ListaPara.Exists(x => x.CodParagem == textBox1.Text))
            {
                MessageBox.Show("Insira um código de paragem não repetido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //  verificação se existe um nome igual no concelho
            if (ListaPara.Exists(x => x.Nome == textBox2.Text) && ListaPara.Exists(y => y.Concelho == comboBox3.Text))
            {
                MessageBox.Show("Insira um nome não repetido no concelho", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string zona = textBox5.Text;
            string conc = comboBox3.Text;

            Paragem p1 = new Paragem();
            try
            {
                p1 = new Paragem(textBox1.Text, textBox2.Text, conc, zona);
                ListaPara.Add(p1);

                control.InsertPara(p1);
                richTextBox1.Text += p1 + "\n";
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            textBox1.Text = "";
            textBox2.Text = "";
            //AllDataGridView.DataSource = control.Fill("Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', E.Freguesia as 'Concelho', E.ZonaREF as 'Zona' From Paragem as P JOIN Enum_Freguesia as E ON P.Concelho = E.Freguesia ORDER BY P.CodigoPar + 0");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) // zona muda consoante o concelho escolhid 
        {
            ///////////////////////////// Zonation
            textBox5.Text = control.Zona(comboBox3);
        }


        #endregion paragem




        //---------------------------------------------------------------------------------------
        //                                      Carreira
        //---------------------------------------------------------------------------------------

        #region carreira
        private void button7_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox3.Text, out int CodCarr))
            {
                MessageBox.Show("Insira um inteiro como código carreira", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (ListaCarreira.Exists(x => x.CodCarreira == CodCarr))
            {
                DialogResult r = MessageBox.Show("Carreira já existe, deseja atualizar o circuito da mesma?", "Erro", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (r == DialogResult.Yes)
                {
                    foreach (Paragem item in checkedListBox1.CheckedItems)
                    {
                        if (!ListaCarreira.Find(x => x.CodCarreira == CodCarr).Circuito.Contains(item))
                            ListaCarreira.Find(x => x.CodCarreira == CodCarr).Circuito.Add(item);
                    }

                    for (int i = 0; i < checkedListBox1.Items.Count; i++) // verifica se estao checked
                        checkedListBox1.SetItemChecked(i, false); //  quando se carrega no botao retira-os de checked

                    control.InsertRel(ListaCarreira.Find(x => x.CodCarreira == CodCarr));
                    textBox3.Text = "";
                    dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Linha = '" + CodCarr + "' ORDER BY Pas.Linha, Pas.Hora, Pas.Dia, Pas.Local");
                }
                else
                    return;
            }
            else
            {
                try // envia para a class Carreira
                {
                    if (checkedListBox1.CheckedItems.Count <= 1) // tem de selecionar mais de uma paragem para que um circuito seja circuito
                        throw new Exception("Crie um circuito(mais do que uma paragem) para a nova carreira");

                    foreach (Paragem item in checkedListBox1.CheckedItems)
                        ListaCircuito.Add(item);

                    for (int i = 0; i < checkedListBox1.Items.Count; i++) // verifica se estao checked
                        checkedListBox1.SetItemChecked(i, false);   //  quando se carrega no botao retira-os de checked


                    Carreira car = new Carreira(CodCarr, ListaCircuito, ListaPassagens);
                    control.InsertCarr(car);
                    control.InsertRel(car);
                    ListaCarreira.Add(car); // adiciona a carreira à lista
                    ListaCircuito.Clear(); // limpa o circuito
                    textBox3.Text = "";
                    dataGridView1.DataSource = control.Fill("Select * From Carreiras ORDER BY CodigoCar");

                }
                catch (Exception er) // ou dá erro
                {
                    MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {                                       //(e) do CellEvent(mouse click)
                //MessageBox.Show(AllDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString());
                var cellval = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                textBox3.Text = cellval.ToString();
                //dataGridView2.DataSource = control.Fill("Select * From Passagem WHERE Linha = '" + cellval + "' ORDER BY Linha");
                dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Linha = '" + cellval + "' ORDER BY Pas.Linha, Pas.Hora, Pas.Dia, Pas.Local");
            }
            catch (Exception)
            {
                //counter erros
            }
        }
        #endregion carreira



        //---------------------------------------------------------------------------------------
        //                                      Passagens
        //---------------------------------------------------------------------------------------

        #region passagem
        private void button5_Click(object sender, EventArgs e)
        {
            if (!TimeSpan.TryParse(maskedTextBox2.Text, out TimeSpan hora))     //  hora
            {
                MessageBox.Show("Insira uma hora válida (00:00 - 23:59)", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (Passagem item in ListaPassagens)
            {
                if (item.Hora == hora && item.Dia == comboBox4.Text && item.Local == ListaCarreira[comboBox5.SelectedIndex].Circuito[comboBox6.SelectedIndex])
                {
                    MessageBox.Show("Hora de Passagem, no horário escolhido, já se encontra preenchida", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            try
            {
                Passagem ps = new Passagem(ListaCarreira[comboBox5.SelectedIndex], ListaCarreira[comboBox5.SelectedIndex].Circuito[comboBox6.SelectedIndex], hora, comboBox4.Text);
                ListaPassagens.Add(ps);   //  lista de passagens
                control.InsertPass(ps);
                maskedTextBox2.ResetText();
                button5.Enabled = false;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            comboBox6.ResetText();
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Linha = '" + comboBox5.SelectedItem + "' ORDER BY Pas.Linha,P.Nome, Pas.Hora, Pas.Dia, Pas.Local");
        }

        #endregion passagem


        //---------------------------------------------------------------------------------------
        //                                      Passagens FILL
        //---------------------------------------------------------------------------------------

        #region fills
        // PASSAGENS DA CARREIRA CROSS CHECK
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            button5.Enabled = false;
            comboBox6.Items.Clear();
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            comboBox6.Text = "";

            dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Linha = '" + comboBox5.SelectedItem + "' ORDER BY Pas.Linha, P.Nome, Pas.Hora, Pas.Dia, Pas.Local");

            foreach (Paragem item in ListaCarreira[comboBox5.SelectedIndex].Circuito)//  mostra as paragens da carreira selecionada
            {
                comboBox6.Items.Add(item);
            }
            comboBox6.Enabled = true;

            List<Passagem> check = new List<Passagem>();                                        // lista para verificações
            for (int i = 0; i < ListaCarreira[comboBox5.SelectedIndex].Circuito.Count(); i++)   // adiciona a lista check
                check.AddRange(ListaPassagens.FindAll(x => x.Local == ListaCarreira[comboBox5.SelectedIndex].Circuito[i]));

            foreach (Passagem item in check)                                            //  mostra para aquela carreira
            {
                if (item.Linha.CodCarreira == ListaCarreira[comboBox5.SelectedIndex].CodCarreira)
                    richTextBox3.Text += item;
            }
        }

        // PASSAGENS NA PARAGEM ESPECIFICA CROSS CHECK PASSADO PARA CARREIRA DEPOIS COMO METODO?
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Passagem> check = new List<Passagem>(); // lista para verificações
            richTextBox2.Text = "";

            check.AddRange(ListaPassagens.FindAll(x => x.Local == comboBox6.SelectedItem));

            foreach (Passagem item in check)
            {
                richTextBox2.Text += "CódCarreira - " + item.Linha.CodCarreira + " às " + item.Hora.ToString() + " horas aos - " + item.Dia + "\n";
            }

            button5.Enabled = true;
        }
        #endregion fills



        //---------------------------------------------------------------------------------------
        //                                      Pesquisas
        //---------------------------------------------------------------------------------------

        #region pesquisas
        private void comboBox13_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox14.Text = control.Zona(comboBox13);
            if (comboBox1.SelectedIndex == 0) //Paragem
                dataGridView2.DataSource = control.Fill("Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Linha as Carreira, Pas.Dia as Horário, Pas.Hora as 'Hora de Passagem' From Paragem as P LEFT JOIN Passagem as Pas ON P.CodigoPar = Pas.Local JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE P.Concelho = '" + comboBox13.Text + "' ORDER BY P.CodigoPar");
            else //Passagem
                dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Hora is NOT NULL AND P.Concelho = '" + comboBox13.Text + "' ORDER BY Pas.Linha, Pas.Hora, Pas.Dia, Pas.Local, F.ZonaREF");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            resetpesq();

            if (comboBox1.SelectedIndex == 0) //Paragem
            {
                dataGridView2.DataSource = "";
                dataGridView2.DataSource = control.Fill("Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Linha as Carreira, Pas.Dia as Horário, Pas.Hora as 'Hora de Passagem' From Paragem as P LEFT JOIN Passagem as Pas ON P.CodigoPar = Pas.Local JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia ORDER BY P.CodigoPar");

            }
            else //Passagem  
            {
                dataGridView2.DataSource = "";
                dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Hora is NOT NULL ORDER BY Pas.Linha, Pas.Hora, Pas.Dia, Pas.Local, F.ZonaREF");

            }

        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //dataGridView2.DataSource = control.Fill("Select * From Carreiras WHERE CodigoCar = '" + ListaCarreira[comboBox2.SelectedIndex].CodCarreira + "'");
            if (comboBox1.SelectedIndex == 0) //Paragem
                dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário, Pas.Hora as 'Hora de Passagem' From Paragem as P LEFT JOIN Passagem as Pas ON P.CodigoPar = Pas.Local JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Linha = '" + ListaCarreira[comboBox2.SelectedIndex].CodCarreira + "' ORDER BY Carreira, P.CodigoPar");
            else //Passagem
                dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Hora is NOT NULL AND Pas.Linha = '" + ListaCarreira[comboBox2.SelectedIndex].CodCarreira + "' ORDER BY Pas.Linha, Pas.Hora, Pas.Dia, Pas.Local, F.ZonaREF");
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox4.Text = ListaPara[comboBox9.SelectedIndex].Nome;

            if (comboBox1.SelectedIndex == 0) //Paragem
                dataGridView2.DataSource = control.Fill("Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Linha as Carreira, Pas.Dia as Horário, Pas.Hora as 'Hora de Passagem' From Paragem as P LEFT JOIN Passagem as Pas ON P.CodigoPar = Pas.Local JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE P.CodigoPar = '" + ListaPara[comboBox9.SelectedIndex].CodParagem + "' ORDER BY P.CodigoPar");
            else //Passagem
                dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Hora is NOT NULL AND P.CodigoPar = '" + ListaPara[comboBox9.SelectedIndex].CodParagem + "' ORDER BY Pas.Linha, Pas.Hora, Pas.Dia, Pas.Local, F.ZonaREF");

        }

        private void comboBox12_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox6.Text = ListaPara[comboBox12.SelectedIndex].CodParagem;

            if (comboBox1.SelectedIndex == 0) //Paragem
                dataGridView2.DataSource = control.Fill("Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Linha as Carreira, Pas.Dia as Horário, Pas.Hora as 'Hora de Passagem' From Paragem as P LEFT JOIN Passagem as Pas ON P.CodigoPar = Pas.Local JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE P.Nome = '" + ListaPara[comboBox12.SelectedIndex].Nome + "' ORDER BY P.CodigoPar");
            else //Passagem
                dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Hora is NOT NULL AND P.Nome = '" + ListaPara[comboBox12.SelectedIndex].Nome + "' ORDER BY Pas.Linha , Pas.Hora, Pas.Dia, Pas.Local, F.ZonaREF");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string query;
            if (comboBox1.SelectedIndex == 0) //Paragem
                query = "Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Linha as Carreira, Pas.Dia as Horário, Pas.Hora as 'Hora de Passagem' From Paragem as P LEFT JOIN Passagem as Pas ON P.CodigoPar = Pas.Local JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE P.CodigoPar ";
            else //Passagem
                query = "Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE (P.CodigoPar ";


            foreach (Paragem item in checkedListBox3.CheckedItems)
                query += "LIKE '" + item.CodParagem + "' or P.CodigoPar ";


            if (comboBox1.SelectedIndex == 0)
            {
                query = query.Substring(0, query.Length - 15);
                query += "ORDER BY P.CodigoPar + 0";
            }
            else
            {
            query = query.Substring(0, query.Length - 16);
            query += ") AND Pas.Hora is NOT NULL ORDER BY Pas.Linha , Pas.Hora, Pas.Dia, Pas.Local, F.ZonaREF";
            }

            dataGridView2.DataSource = control.Fill(query);
            //MessageBox.Show(query);
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (maskedTextBox1.MaskCompleted == false || maskedTextBox3.MaskCompleted == false)
            {
                MessageBox.Show("Preencha ambas caixas de hora");
                return;
            }

            if (comboBox1.SelectedIndex == 0) //Paragem
                dataGridView2.DataSource = control.Fill("Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Linha as Carreira, Pas.Dia as Horário, Pas.Hora as 'Hora de Passagem' From Paragem as P LEFT JOIN Passagem as Pas ON P.CodigoPar = Pas.Local JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Hora >= '" + maskedTextBox3.Text + "' AND Pas.Hora <= '" + maskedTextBox1.Text + "' ORDER BY Pas.Hora, P.CodigoPar ");
            else //Passagem
                dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Hora is NOT NULL AND Pas.Hora >= '" + maskedTextBox3.Text + "' AND Pas.Hora <= '" + maskedTextBox1.Text + "' ORDER BY Pas.Hora, Pas.Linha , Pas.Dia, Pas.Local, F.ZonaREF");

        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) //Paragem
                dataGridView2.DataSource = control.Fill("Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Linha as Carreira, Pas.Dia as Horário, Pas.Hora as 'Hora de Passagem' From Paragem as P LEFT JOIN Passagem as Pas ON P.CodigoPar = Pas.Local JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.dia = '" + comboBox10.Text + "' ORDER BY P.CodigoPar ");
            else //Passagem
                dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Hora is NOT NULL AND Pas.dia = '" + comboBox10.Text + "' ORDER BY Pas.Linha , Pas.Hora, Pas.Dia, Pas.Local, F.ZonaREF");

        }

        private void comboBox14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) //Paragem
                dataGridView2.DataSource = control.Fill("Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Linha as Carreira, Pas.Dia as Horário, Pas.Hora as 'Hora de Passagem' From Paragem as P LEFT JOIN Passagem as Pas ON P.CodigoPar = Pas.Local JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE F.ZonaREF = '" + comboBox14.Text + "' ORDER BY P.CodigoPar ");
            else //Passagem
                dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Hora is NOT NULL AND F.ZonaRef = '" + comboBox14.Text + "' ORDER BY Pas.Linha , Pas.Hora, Pas.Dia, Pas.Local, F.ZonaREF");

        }

        private void button10_Click(object sender, EventArgs e)
        {
            resetpesq();

            if (comboBox1.SelectedIndex == 0) //Paragem
            {
                dataGridView2.DataSource = "";
                dataGridView2.DataSource = control.Fill("Select P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Linha as Carreira, Pas.Dia as Horário, Pas.Hora as 'Hora de Passagem' From Paragem as P LEFT JOIN Passagem as Pas ON P.CodigoPar = Pas.Local JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia ORDER BY P.CodigoPar ");

            }
            else //Passagem  
            {
                dataGridView2.DataSource = "";
                dataGridView2.DataSource = control.Fill("Select Pas.Linha as Carreira, P.CodigoPar as 'Código Paragem', P.Nome as 'Nome Paragem', P.Concelho as 'Concelho Paragem', F.ZonaREF as 'Zona', Pas.Dia as Horário,Pas.Hora as 'Hora de Passagem' From Passagem as Pas JOIN Paragem as P ON Pas.Local = P.CodigoPar JOIN Enum_Freguesia as F on P.Concelho = F.Freguesia WHERE Pas.Hora is NOT NULL ORDER BY Pas.Linha , Pas.Hora, Pas.Dia, Pas.Local, F.ZonaREF");

            }
        }
        #endregion pesquisas
    }
}
