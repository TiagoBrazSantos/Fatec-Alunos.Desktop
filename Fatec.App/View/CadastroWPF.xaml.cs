using Fatec.Core.Business;
using Fatec.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Fatec.App.View
{
    public partial class CadastroWPF : Window
    {
        private AlunoVm AlunoVm { get; set; }
        public CadastroWPF(AlunoVm aluno = null)
        {
            InitializeComponent();

            AlunoVm = aluno;            
        }

        private void Cadastro_Loaded(object sender, RoutedEventArgs e)
        {
            Inicializar();
        }

        private void BtnConfirmar_Click(object sender, RoutedEventArgs e)
        {
            Confirmar();
        }

        private void Inicializar()
        {
            try
            {
                if (AlunoVm == null)
                {
                    AlunoVm = new AlunoVm();
                }

                TxtNome.Focus();

                DataContext = AlunoVm;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha para inicializar a tela de cadastro. {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Confirmar()
        {
            try
            {
                var alunoBusiness = new AlunoBusiness();
                if(alunoBusiness.InserirAlterar(AlunoVm))
                {
                    MessageBox.Show(alunoBusiness.Mensagem, "Sucesso", MessageBoxButton.OK);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(alunoBusiness.Mensagem, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falha ao confirmar o cadastro. {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
