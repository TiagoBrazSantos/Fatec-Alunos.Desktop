using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace Fatec.Core.ViewModel
{
    public class TelaInicialVm : INotifyPropertyChanged
    {
        private ObservableCollection<AlunoVm> _listaAlunos;
        public ObservableCollection<AlunoVm> ListaAlunos
        {
            get => _listaAlunos;
            set
            {
                _listaAlunos = value;
                OnPropertyChanged(nameof(ListaAlunos));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
