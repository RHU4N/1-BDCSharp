using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BD_Funcionario
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ClassDao dao = new ClassDao();
        void LimparCampos()
        {
            txtNome.Clear();
            txtEndereco.Clear();
            txtSalario.Clear();
            lblNumCli.Text="";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dao.Conecte("bd_teste", "table_teste");
            dao.PreencheTabela(dataGridView1);
            LimparCampos();
        }

        void ExibirDados()
        {
            lblNumCli.Text = dao.campos.id.ToString();
            txtNome.Text = dao.campos.nome;
            txtEndereco.Text = dao.campos.ender;
            txtSalario.Text = dao.campos.sal.ToString();
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            LimparCampos();

        }

        private void BtnInserir_Click_1(object sender, EventArgs e)
        {
            // insere
            if (txtNome.Text == "" || txtEndereco.Text == "" || txtSalario.Text == "")
            {
                MessageBox.Show("Campos em branco", "AVISO!");
            }
            else
            {
                dao.Insere(txtNome.Text, txtEndereco.Text, decimal.Parse(txtSalario.Text));
                MessageBox.Show("Registro gravado com sucesso!", "Informação do Sistema");
                LimparCampos();
                dao.PreencheTabela(dataGridView1);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int numLinha = e.RowIndex; //retorna o numero da linha selecionada

            if (numLinha >= 0)
            {
                int idCliente = int.Parse(dataGridView1.Rows[numLinha].Cells[0].Value.ToString());
                dao.Consulta(idCliente);
                ExibirDados();
            }
        }

        private void Btnpesquisar_Click(object sender, EventArgs e)
        {
            if(txtNome.Text != "")
            {
                dao.PreencheTabela(dataGridView1, txtNome.Text);
                txtEndereco.Clear();
                txtSalario.Clear();
            }
            else
            {
                dao.PreencheTabela(dataGridView1);
                ExibirDados();
                LimparCampos();
            }
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            dao.Atualiza(txtNome.Text, txtEndereco.Text, decimal.Parse(txtSalario.Text), int.Parse(lblNumCli.Text));
            ExibirDados();
            dao.PreencheTabela(dataGridView1);
        }

        private void BtnDeletar_Click(object sender, EventArgs e)
        {
            dao.Deletar(int.Parse(lblNumCli.Text));
            ExibirDados();
            dao.PreencheTabela(dataGridView1);
            LimparCampos();
        }
    }

}
