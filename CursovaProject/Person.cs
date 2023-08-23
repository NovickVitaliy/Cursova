using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursovaProject
{
    public class Person
    {
        private string _name;
        private string _secondName;
        private string _surname;
        private int _passportSeries;
        private int _passportNumber;


        public Person(string name, string secondName, string surname, int passportSeries, int passportNumber)
        {
            _name = name;
            _secondName = secondName;
            _surname = surname;
            _passportSeries = passportSeries;
            _passportNumber = passportNumber;
        }




        public string Name { get { return _name; } }
        public string SecondName { get { return _secondName; } }
        public string Surname { get { return _surname; } }
        public int PassortSeries { get => _passportSeries; }
        public int PassportNumber { get => _passportNumber; }

        public string GetInfo()
        {
            return $"Ім'я: {Name}\n" +
                    $"Прізвище: {Surname}\n" +
                    $"По-батькові: {SecondName}\n" +
                    $"Серія паспорта: {PassortSeries}\n" +
                    $"Номер паспорта: {PassportNumber}\n";
        }

    }
}
