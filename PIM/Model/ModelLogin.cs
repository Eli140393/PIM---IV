using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PIM.Model
{
    public class ModelLogin
    {
        public int ID_Login { get; set; }
        public int ID_Funcionario { get; set; }
        public string DS_NivelAcesso { get; set; }
        public string NM_FuncionarioLogin { get; set; }
        public string DS_Usuario { get; set; }
        public string DS_Senha { get; set; }
        public string DS_Mensagem { get; set; }

        private string ConnectionString = "";


        //Variaveis de conexão

        SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        SqlDataReader sqlDataReader;


        public ModelLogin()
        {

        }

        public ModelLogin(int id_funcionario, string ds_nivelAcesso, string ds_usuario, string ds_senha, string connectionString)
        {
            ID_Funcionario = id_funcionario;
            DS_NivelAcesso = ds_nivelAcesso;
            DS_Usuario = ds_usuario;
            DS_Senha = ds_senha;
            ConnectionString = connectionString;

            Incluir();
        }

        public ModelLogin(int id_login, int id_funcionario, string ds_nivelAcesso, string ds_usuario, string ds_senha, string connectionString)
        {
            ID_Login = id_login;
            ID_Funcionario = id_funcionario;
            DS_NivelAcesso = ds_nivelAcesso;
            DS_Usuario = ds_usuario;
            DS_Senha = ds_senha;
            ConnectionString = connectionString;

            Alterar();
        }

        public ModelLogin(int id_login, char acao, string connectionString)
        {
            ID_Login = id_login;
            ConnectionString = connectionString;

            if (acao.Equals('E'))
            {
                Excluir();
            }
            else if (acao.Equals('A'))
            {
                Ativar();
            }
        }

        private void Incluir()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("INSERT INTO TB_Login (");
                stringSQL.Append("ID_Funcionario, ");
                stringSQL.Append("DS_NivelAcesso,");
                stringSQL.Append("DS_Usuario,");
                stringSQL.Append("DS_Senha,");
                stringSQL.Append("Ativo)");
                stringSQL.Append("VALUES (");
                stringSQL.Append("'" + ID_Funcionario + "', ");
                stringSQL.Append("'" + DS_NivelAcesso + "', ");
                stringSQL.Append("'" + DS_Usuario + "', ");
                stringSQL.Append("'" + DS_Senha + "', ");
                stringSQL.Append("'" + ID_Funcionario + "', ");
                stringSQL.Append("1)");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao cadastrar";
            
            }
            catch(Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        private void Alterar()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Login SET ");
                stringSQL.Append("DS_NivelAcesso = '" + DS_NivelAcesso + "',");
                stringSQL.Append("DS_Usuario = '" + DS_Usuario + "',");
                stringSQL.Append("DS_Senha = '" + DS_Senha + "',");
                stringSQL.Append("WHERE ID_Login = '" + ID_Login + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao alterar";

            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }
        public DataTable Consultar(int status, string texto, string connectionString)
        {
            DataTable dataTable = new DataTable();
            ConnectionString = connectionString;
            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("LOG.ID_Login, ");
                stringSQL.Append("LOG.ID_Funcionario, ");
                stringSQL.Append("FUN.NM_Funcionario, ");
                stringSQL.Append("LOG.DS_NivelAcesso, ");
                stringSQL.Append("LOG.DS_Senha, ");
                stringSQL.Append("LOG.Ativo ");
                stringSQL.Append("FROM TB_Login AS LOG ");
                stringSQL.Append("INNER JOIN TB_Funcionario AS FUN ON LOG.ID_Funcionario = FUN.ID_Funcionario ");
                stringSQL.Append("WHERE LOG.Ativo = '" + status + "'");
                stringSQL.Append("AND FUN.NM_Funcionario LIKE '" + texto + "' + '%' ");
                stringSQL.Append("ORDER BY ID_Login DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                dataTable.Load(sqlDataReader);
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            return dataTable;
        }
        private void Excluir()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Login SET ");
                stringSQL.Append("Ativo = 0 ");
                stringSQL.Append("WHERE ID_Login = '" + ID_Login + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao excluir";
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        private void Ativar()
        {
            DS_Mensagem = "";

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("UPDATE TB_Login SET ");
                stringSQL.Append("Ativo = 1 ");
                stringSQL.Append("WHERE ID_Login = '" + ID_Login + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                int result = sqlCommand.ExecuteNonQuery();

                DS_Mensagem = result > 0 ? "OK" : "Erro ao ativar";
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
        }

        public DataTable Exibir(int status, string connectionString)
        {
            DataTable dataTable = new DataTable();
            ConnectionString = connectionString;
            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("LOG.ID_Login, ");
                stringSQL.Append("LOG.ID_Funcionario, ");
                stringSQL.Append("FUN.NM_Funcionario, ");
                stringSQL.Append("LOG.DS_NivelAcesso, ");
                stringSQL.Append("LOG.DS_Usuario, ");
                stringSQL.Append("LOG.DS_Senha, ");
                stringSQL.Append("LOG.Ativo ");
                stringSQL.Append("FROM TB_Login AS LOG ");
                stringSQL.Append("INNER JOIN TB_Funcionario AS FUN ON LOG.ID_Funcionario = FUN.ID_Funcionario ");
                stringSQL.Append("WHERE LOG.Ativo = " + status + " ");
                stringSQL.Append("ORDER BY ID_Login DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable.Load(sqlDataReader);
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return dataTable;
        }
       
        public string VerificarLoginCadastrado(string id_login, string id_funcionario, string ds_usuario, string connectionString)
        {
            DS_Mensagem = "";
            ConnectionString = connectionString;
            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("1 ");
                stringSQL.Append("FROM TB_Login");
                stringSQL.Append("WHERE ID_Login != '" + id_login + "' ");
                stringSQL.Append("AND ID_Funcionario = '" + id_funcionario + "' ");
                stringSQL.Append("ORDER BY ID_Login DESC ");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem = "Este funcionário ja contém um login. ";
                }

                sqlCommand.Dispose();
                sqlDataReader.Close();
                stringSQL.Clear();


                stringSQL.Append("SELECT ");
                stringSQL.Append("1 ");
                stringSQL.Append("FROM TB_Login ");
                stringSQL.Append("WHERE ID_Login != '" + id_login + "'");
                stringSQL.Append("AND DS_Usuario = '" + ds_usuario + "' ");
                stringSQL.Append("ORDER BY ID_Login DESC");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem += " Este nome de usuário já está cadastrado";
                }

            }

            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }

            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
            }

            return DS_Mensagem;
        }

        public string GetLogin (string id_login, string connectionString)
        {
            DS_Mensagem = "";
            ConnectionString = connectionString;
            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("LOG.DS_Usuario, ");
                stringSQL.Append("LOG.DS_Senha ");
                stringSQL.Append("FROM TB_Login AS LOG ");
                stringSQL.Append("INNER JOIN TB_Funcionario AS FUN  ON LOG.ID_Funcionario = FUN.ID_Funcionario  ");
                stringSQL.Append("WHERE LOG.Ativo = 1 ");
                stringSQL.Append("AND LOG.ID_Login = '" + id_login + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem = "OK";

                    while (sqlDataReader.Read())
                    {
                        DS_Usuario = sqlDataReader["DS_Usuario"].ToString();
                        DS_Senha = sqlDataReader["DS_Senha"].ToString();
                    }

                }
                else
                {
                    DS_Mensagem = "Login não encontrado";
                }
            }

            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlDataReader.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            return DS_Mensagem;
        }

        public string Acessar(string ds_usuario, string connectionString)
        {
            DS_Mensagem = "";
            DS_NivelAcesso = "";
            NM_FuncionarioLogin = "";
            ConnectionString = connectionString;

            try
            {
                sqlConnection = new SqlConnection(ConnectionString);
                sqlConnection.Open();

                StringBuilder stringSQL = new StringBuilder();
                stringSQL.Append("SELECT ");
                stringSQL.Append("LOG.ID_Login, ");
                stringSQL.Append("LOG.ID_Funcionario, ");
                stringSQL.Append("FUN.NM_Funcionario, ");
                stringSQL.Append("LOG.DS_NivelAcesso, ");
                stringSQL.Append("LOG.DS_Senha ");
                stringSQL.Append("FROM TB_Login AS LOG ");
                stringSQL.Append("INNER JOIN TB_Funcionario AS FUN ON LOG.ID_Funcionario = FUN.ID_Funcionario ");
                stringSQL.Append("WHERE LOG.Ativo = 1 ");
                stringSQL.Append("AND DS_Usuario = '" + ds_usuario + "'");

                sqlCommand = new SqlCommand(stringSQL.ToString(), sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    DS_Mensagem = "OK";

                    while (sqlDataReader.Read())
                    {
                        ID_Login = Convert.ToInt32(sqlDataReader["ID_Login"].ToString());
                        ID_Funcionario = Convert.ToInt32(sqlDataReader["ID_Funcionario"].ToString());
                        DS_NivelAcesso = sqlDataReader["DS_NivelAcesso"].ToString();
                        NM_FuncionarioLogin = sqlDataReader["NM_Funcionario"].ToString();
                        DS_Senha = sqlDataReader["DS_Senha"].ToString();
                    }
                }
                else
                {
                    DS_Mensagem = "Login inválido";
                }
            }
            catch (Exception e)
            {
                DS_Mensagem = e.Message;
            }
            finally
            {
                sqlDataReader.Close();
                sqlCommand.Dispose();
                sqlConnection.Close();
            }
            return DS_Mensagem;
        }
    }
}
