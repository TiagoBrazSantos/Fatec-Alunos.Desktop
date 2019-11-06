using Fatec.Core.Dal;
using Fatec.Core.Model;
using Fatec.Core.ViewModel;
using Fatec.Core.WebService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace Fatec.Core.Business
{
    public class AlunoBusiness
    {
        public string Mensagem { get; set; } = "";

        public bool InserirAlterar(AlunoVm alunoVm)
        {
            try
            {
                var aluno = new Aluno()
                {
                    AlunoID = alunoVm.IdAluno,
                    NomeCompleto = alunoVm.NomeCompleto,
                    Identificacao = alunoVm.Identificacao
                };

                if (int.TryParse(alunoVm.Idade, out int idade))
                {
                    aluno.Idade = idade;
                }
                else
                {
                    Mensagem = "O campo idade não está em um formato válido.";
                    return false;
                }
                  
                var alunoDao = new AlunoDAO();
                if (aluno.AlunoID == 0)
                {
                    if (alunoDao.Inserir(aluno))
                    {
                        Mensagem = "O cadastro foi realizado com sucesso!";
                        return true;
                    }
                }
                else
                {
                    if (alunoDao.Alterar(aluno))
                    {
                        Mensagem = "A alteração foi realizada com sucesso!";
                        return true;
                    }
                }

                Mensagem = alunoDao.Mensagem;
            }
            catch (Exception ex)
            {
                Mensagem = ex.Message;
            }

            return false;
        }

        public bool Excluir(int id)
        {
            try
            {
                var alunoDao = new AlunoDAO();
                if (alunoDao.Excluir(id))
                {
                    Mensagem = "Registro(s) excluído(s) com sucesso!";
                    return true;
                }
                else
                {
                    Mensagem = alunoDao.Mensagem;
                }
            }
            catch (Exception ex)
            {
                Mensagem = ex.Message;
            }

            return false;
        }

        public List<AlunoVm> Listar()
        {
            try
            {
                var listaAlunosVm = new List<AlunoVm>();
                var alunoDao = new AlunoDAO();
                var alunos = alunoDao.Listar();

                if (alunos == null)
                {
                    Mensagem = alunoDao.Mensagem;
                }
                else
                {
                    foreach (var aluno in alunos)
                    {
                        listaAlunosVm.Add(new AlunoVm
                        {
                            IdAluno = aluno.AlunoID,
                            NomeCompleto = aluno.NomeCompleto,
                            Idade = aluno.Idade.ToString(),
                            DescricaoIdade = aluno.Idade.ToString(),
                            Identificacao = aluno.Identificacao
                        });
                    }

                    return listaAlunosVm;
                }
            }
            catch (Exception ex)
            {
                Mensagem = ex.Message;
            }

            return null;
        }

        public bool EnviarWebService(AlunoVm alunoVm)
        {
            try
            {
                var ip = "";
                var host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (var address in host.AddressList)
                {
                    if (address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ip = address.ToString();
                        break;
                    }
                }

                var alunoJson = new AlunoJson
                {
                    Nome = alunoVm.NomeCompleto,
                    Documento = alunoVm.Identificacao,
                    Ip = ip,
                    NomeComputador = Environment.MachineName
                };

                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErros) => true;

                using (var httpClient = new HttpClient { BaseAddress = new Uri("http://fatecaluno.azurewebsites.net") })
                {
                    HttpContent content = null;
                    if (alunoJson != null)
                    {
                        var json = JsonConvert.SerializeObject(alunoJson);
                        content = new StringContent(json, Encoding.Default, "application/json");
                    }

                    var response = httpClient.PostAsync("Alunos/Inserir", content).Result;
                    var retorno = response.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(retorno))
                    {
                        var retornoJson = (RetornoJson)JsonConvert.DeserializeObject(retorno, typeof(RetornoJson));
                        if(retornoJson != null)
                        {
                            Mensagem = retornoJson.Mensagem;

                            if(retornoJson.Codigo == 100)
                            {
                                return true;
                            }                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Mensagem = ex.Message;
            }

            return false;
        }
    }
}
