using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BD_Funcionario
{
    public class Campos //Inner Class (Classe Interna)
    {
        public int id;
        public string nome;
        public string ender;
        public decimal sal;
    }

    class ClassDao
    {
        public Campos campos = new Campos();

        public MySqlConnection minhaConexao;

        public string usuarioBD = "root";
        public string senhaBD = "root";
        public string servidor = "localhost";
        string bancoDados;
        string tabela;

        public void Conecte(string BancoDados, string Tabela)
        {
            bancoDados = BancoDados;
            tabela = Tabela;
            minhaConexao = new MySqlConnection("server=" + servidor +
                "; database=" + bancoDados + "; uid=" + usuarioBD +
                "; password=" + senhaBD);
        }

        void Abrir()
        {
            minhaConexao.Open();
        }

        void Fechar()
        {
            minhaConexao.Close();
        }

        public void PreencheTabela(System.Windows.Forms.DataGridView dataGridView)
        {
            Abrir();

            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * From " + tabela, minhaConexao);

            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela;

            Fechar();
        }

        public void PreencheTabela(System.Windows.Forms.DataGridView dataGridView, string busca)
        {
            MySqlDataAdapter meuAdapter = new MySqlDataAdapter("Select * From " + tabela + " where nome like "+"'"+ busca +"%';", minhaConexao);
            System.Data.DataSet dataSet = new System.Data.DataSet();
            dataSet.Clear();
            meuAdapter.Fill(dataSet, tabela);
            dataGridView.DataSource = dataSet;
            dataGridView.DataMember = tabela;

            Fechar();
        }

        public void Insere(string campoNome, string campoEnder, decimal campoSalario)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("insert into " + tabela
                + "(nome, endereco, salario) " +
                "values(@nome,@endereco,@salario)", minhaConexao);
            comando.Parameters.AddWithValue("@nome", campoNome);
            comando.Parameters.AddWithValue("@endereco", campoEnder);
            comando.Parameters.AddWithValue("@salario", campoSalario);
            comando.ExecuteNonQuery();

            Fechar();
        }

        public void Atualiza(string campoNome, string campoEnder, decimal campoSalario, int id)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("update " + tabela
                                                    + " set nome=@nome, endereco=@endereco , "
                                                    + "salario=@salario where id=@id", minhaConexao);

            comando.Parameters.AddWithValue("@id", id);
            comando.Parameters.AddWithValue("@nome", campoNome);
            comando.Parameters.AddWithValue("@endereco", campoEnder);
            comando.Parameters.AddWithValue("@salario", campoSalario);
            comando.ExecuteNonQuery();

            Fechar();
        }

        public void Consulta(string campoNome)
        {
            //consulta por nome
            Abrir();

            MySqlCommand comando = new MySqlCommand("select * from " + tabela
                                                    + " where nome = '" + campoNome + "'", minhaConexao);
            MySqlDataReader dtReader = comando.ExecuteReader();
            if (dtReader.Read())
            {
                campos.id = int.Parse(dtReader["id"].ToString());
                campos.nome = dtReader["nome"].ToString();
                campos.ender = dtReader["endereco"].ToString();
                campos.sal = decimal.Parse(dtReader["salario"].ToString());
            }

            Fechar();
        }

        public void Consulta(int id)
        {
            //consulta por id - sobrecarga de metodo (dois metodos podem ser usados ao mesmo tempo)
            Abrir();

            MySqlCommand comando = new MySqlCommand("select * from " + tabela
                                                    + " where id = '" + id + "'", minhaConexao);
            MySqlDataReader dtReader = comando.ExecuteReader();
            if (dtReader.Read())
            {
                campos.id = int.Parse(dtReader["id"].ToString());
                campos.nome = dtReader["nome"].ToString();
                campos.ender = dtReader["endereco"].ToString();
                campos.sal = decimal.Parse(dtReader["salario"].ToString());
            }

            Fechar();
        }


        public void Deletar(int id)
        {
            Abrir();

            MySqlCommand comando = new MySqlCommand("delete from " + tabela + " where id= '" + id + "'", minhaConexao);
            comando.ExecuteNonQuery();

            Fechar();
        }
    }
}
