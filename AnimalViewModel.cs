using System;
using System.Linq;
using System.Windows.Input;

namespace WpfApp5
{
    public class AnimalViewModel : BaseNotifyPropertyChanged, ICommand
    {
        public ICommand novoComando { get; set; }

        public ICommand comandoDeletar { get; set; }
        public ICommand comandoEditar { get; set; }
        public System.Collections.ObjectModel.ObservableCollection<Animal> Animais { get; set; }

        public Animal _animalSelecionado;

        public event EventHandler? CanExecuteChanged;

        public Animal AnimalSelecionado
        {
            get { return _animalSelecionado; }
            set { SetField(ref _animalSelecionado, value); }
        }

        public AnimalViewModel()
        {
            Animais = new System.Collections.ObjectModel.ObservableCollection<Animal>();
            Animais.Add(new Animal()
            {
                ID = 1,
                Nome = "Dakota",
                raça = "Golden Retriever",
                idade = 5
            });
            AnimalSelecionado = Animais.FirstOrDefault();
            novoComando = new RelayCommand((object param) =>
            {
                var viewModel = (AnimalViewModel)param;
                Animal animalNovo = new Animal();
                int maxID = 0;
                if (viewModel.Animais.Any())
                {
                    maxID = viewModel.Animais.Max(f => f.ID);
                }
                animalNovo.ID = maxID + 1;

                AnimalWindow fw = new AnimalWindow();
                fw.DataContext = animalNovo;
                fw.ShowDialog();

                if (fw.DialogResult.HasValue && fw.DialogResult.Value)
                {
                    viewModel.Animais.Add(animalNovo);
                    viewModel.AnimalSelecionado = animalNovo;
                }
            });
            comandoDeletar = new RelayCommand((object param) =>
            {

                AnimalViewModel viewModel = (AnimalViewModel)param;
                viewModel.Animais.Remove(viewModel.AnimalSelecionado);
                viewModel.AnimalSelecionado = viewModel.Animais.FirstOrDefault();
            });
            comandoEditar = new RelayCommand((object param) =>
            {
                AnimalViewModel viewModel = (AnimalViewModel)param;
                Animal cloneAnimal = (Animal)viewModel.AnimalSelecionado;
                AnimalWindow fw = new AnimalWindow();
                fw.DataContext = cloneAnimal;
                fw.ShowDialog();

                if (fw.DialogResult.HasValue && fw.DialogResult.Value)
                {
                    viewModel.AnimalSelecionado.Nome = cloneAnimal.Nome;
                    viewModel.AnimalSelecionado.ID = cloneAnimal.ID;
                    viewModel.AnimalSelecionado.raça = cloneAnimal.raça;
                    viewModel.AnimalSelecionado.idade = cloneAnimal.idade;

                }
            });
        }

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
