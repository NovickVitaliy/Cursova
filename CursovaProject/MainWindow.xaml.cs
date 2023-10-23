using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using CursovaProject.Rooms;
namespace CursovaProject
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private ObservableCollection<Hotel> hotelList;
    DatabaseManager _databaseManager;
    string[] paths;
    string[] names;
    HotelManager hotelManager;
    Hotel _chosenHotel;
    public MainWindow()
    {
      InitializeComponent();
      hotelList = new ObservableCollection<Hotel>();
      _databaseManager = new DatabaseManager(ref hotelList);
      paths = _databaseManager.GetDatabasesPaths();
      names = _databaseManager.GetNamesOfDatabases(paths);
      hotelManager = new HotelManager();
      hotelList = hotelManager.CreateHotelsObjectsBasedOnSavedData(paths, names);
      hotelControl.ItemsSource = hotelList;
    }
    private void AddHotelButton_Click(object sender, RoutedEventArgs e)
    {
      AddHotelsWindow addHotelsWindow = new AddHotelsWindow(this, _databaseManager, hotelList);
      addHotelsWindow.Show();
    }
    private void DeleteHotelButton_Click(object sender, RoutedEventArgs e)
    {
      var deleteHotelWindow = new DeleteHotelButton(hotelList, _databaseManager);
      deleteHotelWindow.Show();
    }
    private void ChooseHotelButton_OnClick(object sender, RoutedEventArgs e)
    {
      MainScrollViewer.Visibility = Visibility.Visible;
      ChooseHotelPrompt.Visibility = Visibility.Collapsed;
      var btn = (sender as Button);
      _chosenHotel = hotelList.Where((hotel) => hotel.Name == btn.Content.ToString()).FirstOrDefault();
      HotelRoomsItemsControl.DataContext = _chosenHotel;
      HotelRoomsItemsControl.ItemsSource = _chosenHotel.HotelRooms;
      HotelDetailsItemsControl.DataContext = _chosenHotel;
    }
    private void RegisterResidentButton_Click(object sender, RoutedEventArgs e)
    {
      if (_chosenHotel != null)
      {
        RegisterResidentWindow registerResidentWindow = new RegisterResidentWindow(_chosenHotel, HotelDetailsItemsControl, _databaseManager);
        registerResidentWindow.Show();
      }
    }
    private void GetInfoButton_Click(object sender, RoutedEventArgs e)
    {
      if (_chosenHotel != null)
      {
        MessageBox.Show(_chosenHotel.GetData());
      }
    }
    private void DeleteResidenButton_Click(object sender, RoutedEventArgs e)
    {
      FreeHotelRoom deleteResidentWindow = new FreeHotelRoom(_chosenHotel, _databaseManager);
      deleteResidentWindow.Show();
    }
    private void ChangeDataAboutHotelButton_Click(object sender, RoutedEventArgs e)
    {
      EditDataAboutHotel edit = new EditDataAboutHotel(_databaseManager, _chosenHotel, this);
      edit.Show();
    }
    private void FindInfoByRoomNumber_Click(object sender, RoutedEventArgs e)
    {
      FindInfoByRoomNumber findInfo = new FindInfoByRoomNumber(_chosenHotel);
      findInfo.Show();
    }
    private void SortByKeyButton_Click(object sender, RoutedEventArgs e)
    {
      SortByKeyWindow sortByKeyWindow = new SortByKeyWindow(_chosenHotel, HotelRoomsItemsControl);
      sortByKeyWindow.Show();
    }
    private void AddResidentButton_Click(object sender, RoutedEventArgs e)
    {
      var btn = sender as Button;
      int roomNumber = Convert.ToInt32(btn.Tag.ToString()) - 1000;
      if (_chosenHotel.HotelRooms.FirstOrDefault(room => room.RoomNumber == roomNumber)?.NumberOfResidents >= HotelRoom.MAX_AMOUNT_OF_RESIDENTS)
      {
        MessageBox.Show("У кімнаті більше немає місця для поселення", "Недостатньо місць", MessageBoxButton.OK, MessageBoxImage.Warning);
        return;
      }
      AddResidentWindow addResidentWindow = new AddResidentWindow(_chosenHotel, roomNumber, _databaseManager, this);
      addResidentWindow.Show();
    }
    private void TotalNumberOfResidentsInRoomOfSpecificType_Click(object sender, RoutedEventArgs e)
    {
      TotalNumbersOfResidentsInSpecificRoomType total = new TotalNumbersOfResidentsInSpecificRoomType(_chosenHotel);
      total.Show();
    }
    private void Expander_Expanded(object sender, RoutedEventArgs e)
    {
      var expander = sender as Expander;
      DataGrid dataGrid = expander.FindName("RoomDataGrid") as DataGrid;
      dataGrid.ItemsSource = _chosenHotel.GetHotelRoom(Convert.ToInt32(expander.Tag)).Residents;
    }
    private void FilterRoomsButton_Click(object sender, RoutedEventArgs e)
    {
      FilterRoomsWindow filter = new FilterRoomsWindow(_chosenHotel, this);
      filter.Show();
    }
    private void MakeDatabaseBasedOnKey_Click(object sender, RoutedEventArgs e)
    {
      MakeDatabaseBasedOnKeyWindow makeDatabaseWindow = new MakeDatabaseBasedOnKeyWindow(_chosenHotel);
      makeDatabaseWindow.Show();
    }
    private void DeleteConcreteResidentButton_Click(object sender, RoutedEventArgs e)
    {
      Button button = (Button)sender;
      int roomNumber = Convert.ToInt32(button.Tag.ToString()) - 1000;
      RemoveConcreteResident remove = new RemoveConcreteResident(_chosenHotel, roomNumber, _databaseManager, this);
      remove.Show();
    }
    private void EditDatesButton_Click(object sender, RoutedEventArgs e)
    {
      Button button = (Button)sender;
      int roomNumber = Convert.ToInt32(button.Tag);
      EditDatesWindow edit = new EditDatesWindow(_chosenHotel, _databaseManager, roomNumber);
      edit.ShowDialog();
      this.HotelDetailsItemsControl.Items.Refresh();
      this.HotelRoomsItemsControl.Items.Refresh();
    }
    private void SumPerDateButton_Click(object sender, RoutedEventArgs e)
    {
      Button button = (Button)sender;
      int roomNumber = Convert.ToInt32(button.Tag);

      HotelRoom hotelRoom = _chosenHotel.GetHotelRoom(roomNumber);
      float totalPrice = hotelRoom.GetTotalPriceInTheEnd();

      MessageBox.Show($"За весь період проживання буде оплачено: {totalPrice} грн.");
    }
    private void GuideBookButton_Click(object sender, RoutedEventArgs e)
    {
      GuideBookWindow guideBook = new GuideBookWindow();
      guideBook.Show();
    }
    private void CleanRoomButton_Click(object sender, RoutedEventArgs e)
    {
      Button button = (Button)sender;
      int roomNumber = (int)button.Tag;

      _chosenHotel.CleanRoom(roomNumber);
      _databaseManager.ChangeRoomCleanedStatus(_chosenHotel, roomNumber, true);
    }
    private void GetTotalIncomeButton_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show($"Загальний прибуток за весь час становить: {_chosenHotel.TotalIncome}", "Загальний прибуток", MessageBoxButton.OK, MessageBoxImage.Information);
    }
  }
}
