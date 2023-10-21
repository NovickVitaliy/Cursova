using CursovaProject.Rooms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace CursovaProject
{
  /// <summary>
  /// Interaction logic for MakeDatabaseBasedOnKeyWindow.xaml
  /// </summary>
  public partial class MakeDatabaseBasedOnKeyWindow : Window
  {
    Hotel _chosenHotel;
    public MakeDatabaseBasedOnKeyWindow(Hotel chosenHotel)
    {
      InitializeComponent();
      _chosenHotel = chosenHotel;
    }

    private void OptionsToCreateDatabaseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

      if (OptionsToCreateDatabaseComboBox.SelectedItem != null)
      {
        var option = (OptionsToCreateDatabaseComboBox.SelectedItem as ComboBoxItem).Name;
        RoomTypesComboBox.Visibility = Visibility.Collapsed;
        PriceOptionStackPanel.Visibility = Visibility.Collapsed;

        switch (option)
        {
          case "CreateByRoomType":
            RoomTypesComboBox.Visibility = Visibility.Visible;
            break;
          case "CreateByPricePerNight":
            PriceOptionStackPanel.Visibility = Visibility.Visible;
            break;
          case "CreateByAllResidents":

            break;
        }
      }
    }

    private void CreateDatabseBasedOnOption_Click(object sender, RoutedEventArgs e)
    {
      System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
      saveFileDialog.InitialDirectory = "c:\\";
      saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
      saveFileDialog.FilterIndex = 2;
      saveFileDialog.RestoreDirectory = true;
      string path;
      Stream stream;

      if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        try
        {
          if ((stream = saveFileDialog.OpenFile()) != null)
          {
            using (stream)
            {
              PathTextBox.Text = saveFileDialog.FileName;
            }
          }
        }
        catch (Exception)
        {
          System.Windows.MessageBox.Show("Файл використовується іншим процесом");
          throw;
        }
      }
    }

    private void CreateDatabaseButton_Click(object sender, RoutedEventArgs e)
    {
      if (PathTextBox.Text == string.Empty)
      {
        MessageBox.Show("Шлях до файлу не було вказано", "Відсутній шлях файлу",MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }
      if (OptionsToCreateDatabaseComboBox.SelectedItem != null)
      {
        try
        {
          string path = PathTextBox.Text;
          string option = (OptionsToCreateDatabaseComboBox.SelectedItem as ComboBoxItem).Name;
          IEnumerable<HotelRoom> hotelRooms = Enumerable.Empty<HotelRoom>();

          switch (option)
          {
            case "CreateByRoomType":
              if (RoomTypesComboBox.SelectedItem != null)
              {
                string type = (RoomTypesComboBox.SelectedItem as ComboBoxItem).Name;

                hotelRooms = _chosenHotel.HotelRooms
                        .Where(r => r.RoomType == type && r.Residents.Count > 0);

              }
              else
              {
                System.Windows.MessageBox.Show("Будь ласка виберіть тип кімнати на основі якої буде створено базу даних про мешканців");
                return;
              }
              break;
            case "CreateByPricePerNight":
              if (int.TryParse(PriceTextBoxOption.Text, out var price))
              {
                if (MoreOrLessOptionCBItem.SelectedItem != null)
                {
                  string value = (MoreOrLessOptionCBItem.SelectedItem as ComboBoxItem).Name;

                  switch (value)
                  {
                    case "MoreThanOption":
                      hotelRooms = _chosenHotel.HotelRooms
                          .Where(r => r.GetTotalPrice >= price);
                      break;
                    case "LessThanOption":
                      hotelRooms = _chosenHotel.HotelRooms
                          .Where(r => r.GetTotalPrice <= price);
                      break;
                    default:
                      return;
                  }
                }
                else
                {
                  System.Windows.Forms.MessageBox.Show("Будь ласка, виберіть більше чи менше вказаної суми за якою буде створена база даних");
                  return;
                }
              }
              else
              {
                System.Windows.MessageBox.Show("Будь ласка, введіть ціну по якій ви хочете створити базу даних");
                return;
              }
              break;
            case "CreateByAllResidents":
              hotelRooms = _chosenHotel.HotelRooms
                      .Where(r => r.Residents.Count > 0);
              break;
          }
          using (StreamWriter file = new StreamWriter(path))
          {
            for (int i = 0; i < hotelRooms.Count(); i++)
            {
              for (int j = 0; j < hotelRooms.ElementAt(i).Residents.Count; j++)
              {
                file.WriteLine("Номер кімнати: " + hotelRooms.ElementAt(i).RoomNumber + "\n" +
                    hotelRooms.ElementAt(i).Residents[j].GetInfo());
              }
            }
            System.Windows.MessageBox.Show("Файл з даними був успішно створений");
          }

        }
        catch (Exception)
        {

        }
      }
      else
      {
        System.Windows.MessageBox.Show("Будь ласка виберіть ключ по якому потрібно створити базу даних");
      }
    }
  }
}
