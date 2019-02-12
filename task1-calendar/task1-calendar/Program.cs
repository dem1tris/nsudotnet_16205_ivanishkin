using System;
using System.Text;

namespace task1_calendar {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Enter the date:");
            if (DateTime.TryParse(Console.ReadLine(), out var date)) {
                Calendar calendar = new Calendar(date);
                calendar.Print();
            } else {
                Console.WriteLine("Unrecognized date format");
            }

            Console.ReadKey();
            return;
        }
    }

    class Calendar {
        private const int DaysInWeek = 7;
        private const int CalendarHeight = 6;
        private const int StringAligner = -4;
        private readonly DateTime _askedDate;
        private readonly int _today;
        private readonly string[,] _representation = new string[CalendarHeight + 1, DaysInWeek];
        private readonly int _workingDays = 0;

        public Calendar(DateTime date) {
            this._askedDate = date;
            DateTime currentDay = _askedDate;
            var placeholder = $".   ";

            while (currentDay.DayOfWeek != DayOfWeek.Monday) {
                currentDay = currentDay.AddDays(1);
            }

            for (int i = 0; i < DaysInWeek; i++, currentDay = currentDay.AddDays(1)) {
                _representation[0, i] = $"{currentDay,StringAligner:ddd}";
            }

            currentDay = new DateTime(_askedDate.Year, _askedDate.Month, 1);

            DayOfWeek weekDay = DayOfWeek.Monday;
            int place;
            for (place = 0; place < CalendarHeight * DaysInWeek; place++) {
                if (weekDay == currentDay.DayOfWeek && currentDay.Month == _askedDate.Month) {
                    _representation[1 + place / DaysInWeek, place % DaysInWeek] = $"{currentDay.Day,StringAligner}";
                    if (currentDay == _askedDate) {
                        _today = place;
                    }

                    if (weekDay != DayOfWeek.Saturday && weekDay != DayOfWeek.Sunday) {
                        _workingDays++;
                    }

                    currentDay = currentDay.AddDays(1);
                } else {
                    _representation[1 + place / DaysInWeek, place % DaysInWeek] = placeholder;
                }

                weekDay = weekDay == DayOfWeek.Saturday ? DayOfWeek.Sunday : weekDay + 1;
            }
        }

        public void Print() {
            Console.WriteLine($"\nAsked date is {_askedDate:D}");
            for (int i = 0; i < DaysInWeek; i++) {
                Console.Write(_representation[0, i]);
            }

            for (int i = 0; i < CalendarHeight * DaysInWeek; i++) {
                if (i % 7 == 0) {
                    Console.WriteLine();
                }

                if (_today == i) {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }

                if (i % DaysInWeek == 5 || i % DaysInWeek == 6) {
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                }

                Console.Write(_representation[1 + i / DaysInWeek, i % DaysInWeek]);
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.WriteLine($"Total working days: {_workingDays}");
        }
    }
}