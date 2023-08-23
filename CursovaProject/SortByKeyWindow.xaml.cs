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
using System.Windows.Shapes;

namespace CursovaProject
{
    /// <summary>
    /// Interaction logic for SortByKeyWindow.xaml
    /// </summary>
    public partial class SortByKeyWindow : Window
    {
        Hotel _chosenHotel;
        public SortByKeyWindow(Hotel chosenHotel)
        {
            InitializeComponent();
            _chosenHotel = chosenHotel;
        }

        private void SortByKey_Click(object sender, RoutedEventArgs e)
        {
            if(SortKeys.SelectedItem != null) 
            {
                var item = SortKeys.SelectedItem as ComboBoxItem;

                string key = item.Name;

                _chosenHotel.SortByKey(key);

            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть ключ для сортування", "Помилка при виборі ключа", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
