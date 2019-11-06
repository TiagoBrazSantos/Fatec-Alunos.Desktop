using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Fatec.Core.ViewModel
{
    public class AlunoVm : INotifyPropertyChanged
    {
        public int IdAluno { get; set; }

        private string _nomeCompleto;
        public string NomeCompleto
        {
            get => _nomeCompleto;
            set
            {
                _nomeCompleto = value;
                OnPropertyChanged(nameof(NomeCompleto));
            }
        }

        private string _descricaoIdade;
        public string DescricaoIdade
        {
            get => $"{_descricaoIdade} anos";
            set
            {
                _descricaoIdade = value;
                OnPropertyChanged(nameof(DescricaoIdade));
            }
        }

        private string _idade;
        public string Idade
        {
            get => _idade;
            set
            {
                _idade = value;
                OnPropertyChanged(nameof(Idade));
            }
        }

        private string _identificacao;
        public string Identificacao
        {
            get => _identificacao;
            set
            {
                _identificacao = value;
                OnPropertyChanged(nameof(Identificacao));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
