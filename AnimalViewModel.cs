using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace WpfApp5
{
    public class AnimalViewModel
    {
        public ICommand novoComando { get; set; }

        public ICommand comandoDeletar { get; set; }
        public ICommand comandoEditar { get; set; }
        public ObservableCollection<Animal> Animais {get; set;}

        public Animal _animalSelecionado;

        public event EventHandler? CanExecuteChanged;

        public Animal AnimalSelecionado
        {
            get { return _animalSelecionado; }
            set { _animalSelecionado = value; }
        }

        public AnimalViewModel()
        {
            Animais = new ObservableCollection<Animal>();
            Animais.Add(new Animal()
            {
                ID = 1,
                Nome = "Dakota",
                raça = "Golden Retriever",
                idade = 5
            });
            AnimalSelecionado = Animais.FirstOrDefault();
            IniciaComandos();
        }
        public void IniciaComandos()
        {
            novoComando = new RelayCommand((object param) =>
            {

                Animal animalNovo = new Animal();
                int maxID = 0;
                if (Animais.Any())
                {
                    maxID = Animais.Max(f => f.ID);
                }
                animalNovo.ID = maxID + 1;

                AnimalWindow fw = new AnimalWindow();
                fw.DataContext = animalNovo;
                fw.ShowDialog();

                if (fw.DialogResult.HasValue && fw.DialogResult.Value)
                {
                    Animais.Add(animalNovo);
                    AnimalSelecionado = animalNovo;
                }
            });
            comandoDeletar = new RelayCommand((object param) =>
            {
                Animais.Remove(AnimalSelecionado);
                AnimalSelecionado = Animais.FirstOrDefault();
            });
            comandoEditar = new RelayCommand((object param) =>
            {

                Animal cloneAnimal = (Animal)AnimalSelecionado;
                AnimalWindow fw = new AnimalWindow();
                fw.DataContext = cloneAnimal;
                fw.ShowDialog();

                if (fw.DialogResult.HasValue && fw.DialogResult.Value)
                {
                    AnimalSelecionado.Nome = cloneAnimal.Nome;
                    AnimalSelecionado.ID = cloneAnimal.ID;
                    AnimalSelecionado.raça = cloneAnimal.raça;
                    AnimalSelecionado.idade = cloneAnimal.idade;

                }
            });
        }
    }
}
