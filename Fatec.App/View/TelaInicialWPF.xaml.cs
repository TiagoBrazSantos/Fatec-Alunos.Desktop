using Fatec.Core.Business;
using Fatec.Core.Connection;
using Fatec.Core.Model;
using Fatec.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fatec.App.View
{
    public partial class TelaInicialWPF : Window
    {
        private TelaInicialVm TelaInicialVm { get; set; } = new TelaInicialVm();

        public TelaInicialWPF()
        {
            InitializeComponent();
        }

        private void TelaInicial_Loaded(object sender, RoutedEventArgs e)
        {
            Inicializar();
        }

        private void BtnNovo_Click(object sender, RoutedEventArgs e)
        {
            Novo();
        }

        private void BtnExcluirTodos_Click(object sender, RoutedEventArgs e)
        {
            Excluir();
        }

        private void BtnEnviarWs_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn && btn.Tag is AlunoVm alunoVm)
            {
                EnviarWebService(alunoVm);
            }            
        }

        private void BtnAlterar_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is AlunoVm alunoVm)
            {
                Alterar(alunoVm);
            }
        }

        private void BtnExcluir_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn && btn.Tag is AlunoVm alunoVm)
            {
                Excluir(alunoVm.IdAluno);
            }
        }

        private void Inicializar()
        {
            try
            {
                using (var cx = new FatecContext())
                {
                    cx.CreateDatabase();
                }

                DataContext = TelaInicialVm;

                ConsultarTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha para inicializar a tela. {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ConsultarTodos()
        {
            try
            {
                var alunoBusiness = new AlunoBusiness();

                var listaAlunosVm = alunoBusiness.Listar();
                if (listaAlunosVm == null)
                {
                    MessageBox.Show($"Falha para consultar os alunos. {alunoBusiness.Mensagem}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    TelaInicialVm.ListaAlunos = new System.Collections.ObjectModel.ObservableCollection<AlunoVm>(listaAlunosVm);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha para consultar os alunos. {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Novo()
        {
            try
            {
                new CadastroWPF().ShowDialog();

                ConsultarTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha para exibir a tela de cadastro. {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Excluir(int id = 0)
        {
            try
            {
                var result = MessageBox.Show($"Deseja excluir {(id == 0 ? "todos os registros?" : "o registro?")}", "Atenção", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(result == MessageBoxResult.Yes)
                {
                    var alunoBusiness = new AlunoBusiness();
                    if (alunoBusiness.Excluir(id))
                    {
                        MessageBox.Show(alunoBusiness.Mensagem, "Sucesso", MessageBoxButton.OK);
                        ConsultarTodos();
                    }
                    else
                    {
                        MessageBox.Show($"Falha para excluir os alunos. {alunoBusiness.Mensagem}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha para excluir os alunos. {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Alterar(AlunoVm alunoVm)
        {
            try
            {
                new CadastroWPF(alunoVm).ShowDialog();

                ConsultarTodos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha para alterar o cadastro. {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EnviarWebService(AlunoVm alunoVm)
        {
            try
            {
                var alunoBusiness = new AlunoBusiness();
                if(alunoBusiness.EnviarWebService(alunoVm))
                {
                    MessageBox.Show(alunoBusiness.Mensagem, "Sucesso", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show($"Falha ao enviar o cadastro para o webservice. {alunoBusiness.Mensagem}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha ao enviar o cadastro para o webservice. {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
