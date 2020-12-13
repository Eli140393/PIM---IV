using PIM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Validacao;

namespace PIM.Controller
{
    public class ControllerLogin
    {
        private ModelLogin myModelLogin;
        private Validar myValidar;
        public int ID_Login { get; set; }
        public int ID_Funcionario { get; set; }
        public string DS_NivelAcesso { get; set; }
        public string NM_FuncionarioLogin { get; set; }
        public string DS_Usuario { get; set; }
        public string DS_Senha { get; set; }
        public string DS_Mensagem { get; set; }

        public ControllerLogin()
        {

        }

        public ControllerLogin(string id_funcionario, string ds_nivelAcesso, string ds_usuario, string ds_senha, string connectionString)
        {
            string mDs_Msg = ValidateFields("", id_funcionario, ds_usuario, ds_senha, ds_nivelAcesso, connectionString);

            if (mDs_Msg == "")
            {
                myModelLogin = new ModelLogin(Convert.ToInt32(id_funcionario), ds_nivelAcesso, ds_usuario, ds_senha, connectionString);
                DS_Mensagem = myModelLogin.DS_Mensagem;
            }
            else
            {
                DS_Mensagem = mDs_Msg;
            }
        }
        public ControllerLogin(string id_login, string id_funcionario, string ds_nivelAcesso, string ds_usuario, string ds_senha, string connectionString)
        {
            string mDs_Msg = ValidateFields(id_login, id_funcionario, ds_usuario, ds_senha, ds_nivelAcesso, connectionString);

            if (mDs_Msg == "")
            {
                myModelLogin = new ModelLogin(Convert.ToInt32(id_login), Convert.ToInt32(id_funcionario), ds_nivelAcesso, ds_usuario, ds_senha, connectionString);
                DS_Mensagem = myModelLogin.DS_Mensagem;
            }
            else
            {
                DS_Mensagem = mDs_Msg;
            }

        }
        public ControllerLogin(string id_login, char acao, string connectionString)
        {

            myModelLogin = new ModelLogin(Convert.ToInt32(id_login), acao, connectionString);
            DS_Mensagem = myModelLogin.DS_Mensagem;
        }

        public DataTable Exibir(string status, string connectionString)
        {
            myModelLogin = new ModelLogin();
            return myModelLogin.Exibir(Convert.ToInt32(status), connectionString);

        }

        public DataTable Consultar(string status, string texto, string connectionString)
        {
            myModelLogin = new ModelLogin();
            return myModelLogin.Consultar(Convert.ToInt32(status), texto, connectionString);
        }

        public string VerificarLoginCadastrado(string id_login, string id_funcionario, string ds_usuario, string connectionString)
        {
            myModelLogin = new ModelLogin();
            return myModelLogin.VerificarLoginCadastrado(id_login, id_funcionario, ds_usuario, connectionString);
        }

        public string GetLogin(string id_login, string connectionString)
        {
            myModelLogin = new ModelLogin();
            DS_Mensagem = myModelLogin.GetLogin(id_login, connectionString);

            DS_Usuario = myModelLogin.DS_Usuario;
            DS_Senha = myModelLogin.DS_Senha;

            return DS_Mensagem;
        }

        public string Acessar(string ds_usuario, string connectionString)
        {
            myModelLogin = new ModelLogin();
            DS_Mensagem = myModelLogin.Acessar(ds_usuario, connectionString);

            ID_Login = myModelLogin.ID_Login;
            ID_Funcionario = myModelLogin.ID_Funcionario;
            DS_NivelAcesso = myModelLogin.DS_NivelAcesso;
            NM_FuncionarioLogin = myModelLogin.NM_FuncionarioLogin;
            DS_Senha = myModelLogin.DS_Senha;

            return DS_Mensagem;
        }
        private string ValidateFields(string id_login, string id_funcionario, string ds_usuario, string ds_senha, string ds_nivelAcesso, string connectionString)
        {
            myValidar = new Validar();
            string mDs_Msg = "";

            if (id_funcionario.Equals("Funcionário"))
            {
                mDs_Msg = "É necessário selecionar um funcionário.";
            }
            else
            {
                if (myValidar.CampoPreenchido(ds_usuario))
                {
                    if (!myValidar.TamanhoCampo(ds_usuario, 20))
                    {
                        mDs_Msg = " Limite de caracteres para o nome excedido, " +
                                      "o limite para este campo é: 20 caracteres, " +
                                      "quantidade utilizada: " + ds_usuario.Length + ".";
                    }
                    else
                    {
                        if (ds_usuario.Length < 10)
                        {
                            mDs_Msg = " O nome de usuário deve conter pelo menos 10 caracteres";

                        }
                        else
                        {
                            string verificaLogin = VerificarLoginCadastrado(id_login, id_funcionario, ds_usuario, connectionString);

                            if (verificaLogin.Equals(""))
                            {
                                if (myValidar.CampoPreenchido(ds_senha))
                                {
                                    if (!myValidar.TamanhoCampo(ds_senha, 20))
                                    {
                                        mDs_Msg += " Limite de caracteres para senha excedido, " +
                                                   "o limite para este campo é: 20 caracteres, " +
                                                   "quantidade utilizada: " + ds_senha.Length + ".";
                                    }
                                    else
                                    {
                                        if (ds_nivelAcesso.Equals("Nível de Acesso"))
                                        {
                                            mDs_Msg += "É necessario selecionar um nível de acesso.";

                                        }
                                        if (ds_senha.Length < 10)
                                        {
                                            mDs_Msg += "A senha do usuário deve conter pelo menos 10 caractéres.";
                                        }
                                        else
                                        {
                                            if (ds_usuario == ds_senha)
                                            {
                                                mDs_Msg += "O nome de usuário e senha não podem ser iguais.";

                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    mDs_Msg += " A senha do usuário deve estar preenchida.";
                                }
                            }
                            else
                            {
                                mDs_Msg += " " + verificaLogin + " Verifique nos logins ativos e inativos! ";
                            }
                        }
                    }
                }
                else
                {
                    mDs_Msg = " O nome de usuário deve estar preenchido.";
                }
            }
            return mDs_Msg;
        }
    }
}