using System.Windows;

namespace WpfApp5
{
    /// <summary>
    /// Lógica interna para AnimalWindow.xaml
    /// </summary>
    public partial class AnimalWindow : Window
    {
        public AnimalWindow()
        {
            InitializeComponent();
            DataContext = new AnimalViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
