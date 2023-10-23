using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace CursovaProject
{
  public class Person : INotifyPropertyChanged
  {
    private string _name;
    private string _secondName;
    private string _surname;
    private int _passportSeries;
    private int _passportNumber;
    private int _age;
    public Person(string name, string secondName, string surname, int age, int passportSeries, int passportNumber)
    {
      _name = name;
      _secondName = secondName;
      _surname = surname;
      _age = age;
      _passportSeries = passportSeries;
      _passportNumber = passportNumber;
    }
    public string Name
    {
      get { return _name; }
      set { _name = value; OnPropertyChanged(); }
    }
    public int Age
    {
      get { return _age;}
      set { _age = value; OnPropertyChanged();}
    }
    public string SecondName 
    { 
      get { return _secondName; } 
      set { _secondName = value; OnPropertyChanged(); }
    }
    public string Surname 
    { 
      get { return _surname; } 
      set { _surname = value; OnPropertyChanged(); }
    }
    public int PassortSeries 
    {
      get { return _passportSeries; }
      set { _passportSeries = value; OnPropertyChanged(); }
    }
    public int PassportNumber
    { 
      get => _passportNumber;
      set { _passportNumber = value; OnPropertyChanged(); }
    }
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    public string GetInfo()
    {
      return $"Ім'я: {Name}\n" + $"Прізвище: {Surname}\n" + $"По-батькові: {SecondName}\n" + $"Серія паспорта: {PassortSeries}\n" + $"Номер паспорта: {PassportNumber}\n";
    }
    public override bool Equals(object obj)
    {
      if(obj == null) return false;
      if (!(obj is Person)) return false;
      Person personToCompare = (Person)obj;
      return this._name == personToCompare._name
        && this._secondName == personToCompare._secondName
        && this._passportNumber == personToCompare._passportNumber
        && this._passportSeries == personToCompare._passportSeries
        && this._surname == personToCompare._surname;
    }
  }
}
