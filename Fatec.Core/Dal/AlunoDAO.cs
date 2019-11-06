using Fatec.Core.Connection;
using Fatec.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fatec.Core.Dal
{
    public class AlunoDAO
    {
        public string Mensagem { get; set; }
        private FatecContext Cx { get; set; } = new FatecContext();

        public bool Inserir(Aluno aluno)
        {
            try
            {
                Cx.Aluno.Add(aluno);

                Cx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Mensagem = ex.Message;
                return false;
            }

        }

        public bool Alterar(Aluno aluno)
        {
            try
            {
                var model = Cx.Aluno.FirstOrDefault(a => a.AlunoID == aluno.AlunoID);
                if (model != null)
                {
                    model.NomeCompleto = aluno.NomeCompleto;
                    model.Idade = aluno.Idade;
                    model.Identificacao = aluno.Identificacao;

                    Cx.SaveChanges();
                    return true;
                }
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
                if(id == 0)
                {
                    var alunos = Cx.Aluno.ToList();
                    if(alunos != null)
                    {
                        foreach (var aluno in alunos)
                        {
                            Cx.Aluno.Remove(aluno);
                        }
                    }
                }
                else
                {
                    var aluno = Cx.Aluno.FirstOrDefault(a => a.AlunoID == id);
                    if(aluno != null)
                    {
                        Cx.Aluno.Remove(aluno);
                    }
                }

                Cx.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Mensagem = ex.Message;
            }

            return false;
        }

        public List<Aluno> Listar()
        {
            try
            {
                return Cx.Aluno.ToList();
            }
            catch (Exception ex)
            {
                Mensagem = ex.Message;
            }

            return null;
        }
    }
}
