using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Model
{
    public class FieldOld : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int[,] _table;
        public int[,] Table
        {
            set
            {
                _table = value;
                OnPropertyChanged("Table");
            }
            get { return _table; }
        }

        private void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public FieldOld()
        {
            Table = new int[9, 9];
            BaseFill();

        }
        /// <summary>
        /// Заполнение таблицы базовой сеткой значений
        /// </summary>
        private void BaseFill()
        {
            for (int block = 0; block < 9; block++)
            {
                int num = block + 1;
                for (int i = 0; i < 9; i++)
                {
                    int x = i % 3 + (block / 3) * 3;
                    int y = i / 3 + (block % 3) * 3;
                    Table[y, x] = (num / 10 == 0) ? num : num % 10 + 1;
                    num++;
                }
            }
        }
    }
}
