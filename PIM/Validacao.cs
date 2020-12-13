using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Validacao
{

        public class Validar
        {
            public Validar() { }

            public bool CampoPreenchido(string campo)
            {
                if (string.IsNullOrWhiteSpace(campo))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public bool TamanhoCampo(string campo, int tamanho)
            {
                if (campo.Length > tamanho)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public bool Valor(string campo)
            {
                if (!Regex.IsMatch(campo, @"^(0|[1-9]\d{0,2}(\.\d{3})*),\d{2}$"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public bool Numero(string campo)
            {
                if (!Regex.IsMatch(campo, @"^[\d]+$"))
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }

            public bool EAN(string campo)
            {
                bool result = (campo.Length == 13);

                if (result)
                {
                    const string checkSum = "131313131313";

                    int digito = int.Parse(campo[campo.Length - 1].ToString());

                    string ean = campo.Substring(0, campo.Length - 1);

                    int sum = 0;

                    for (int i = 0; i <= ean.Length - 1; i++)
                    {
                        sum += int.Parse(ean[i].ToString()) * int.Parse(checkSum[i].ToString());
                    }

                    int calculo = 10 - (sum % 10);

                    result = (digito == calculo);
                }

                return result;
            }

            public bool ValidaCPF(string cpf)
            {
                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                string tempCpf;
                string digito;
                int soma;
                int resto;

                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");

                if (cpf.Length != 11 ||
                    cpf == "00000000000" ||
                    cpf == "11111111111" ||
                    cpf == "22222222222" ||
                    cpf == "33333333333" ||
                    cpf == "44444444444" ||
                    cpf == "55555555555" ||
                    cpf == "66666666666" ||
                    cpf == "77777777777" ||
                    cpf == "88888888888" ||
                    cpf == "99999999999")
                    return false;

                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);
            }
        }
    }
