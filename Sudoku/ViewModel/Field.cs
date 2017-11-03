using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.ViewModel
{
    public class Field : INotifyPropertyChanged
    {
        private Model.Point _selectedPoint;
        public Model.Point SelectedPoint
        {
            get { return _selectedPoint; }
            set
            {
                _selectedPoint = value;
                OnPropertyChanged("SelectedPoint");
            }
        }
        public ObservableCollection<Model.Point> Points { get; set; }
        
        public Model.Point Point(int y, int x)
        {
        	
            return Points.ElementAt(y * 9 + x);
        }

        private void OnPropertyChanged(string property)
        {
        	if(PropertyChanged!=null)
        	{
				PropertyChanged(this, new PropertyChangedEventArgs(property));
        	}
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public Field()
        {
            Points = new ObservableCollection<Model.Point>();
            for (int i = 0; i < 81; i++)
                Points.Add(new Model.Point { Value = 0, IsActive = true });
            BaseFill();
			Transposing();
        }
        private void BaseFill()
        {
            for (int block = 0; block < 9; block++)
            {
                int num = block + 1;
                for (int i = 0; i < 9; i++)
                {
                    int x = i % 3 + (block / 3) * 3;
                    int y = i / 3 + (block % 3) * 3;
                    Point(y, x).Value = (num / 10 == 0) ? num : num % 10 + 1;
                    num++;
                }
            }
        }
        private void Swap(Model.Point p1, Model.Point p2)
        {
			Model.Point tmp = new Model.Point {
				Value = p1.Value,
				IsActive = p1.IsActive
			};
			p1.Value = p2.Value;
			p1.IsActive = p2.IsActive;
			p2.Value = tmp.Value;
			p2.IsActive = tmp.IsActive;
        }
        private void Transposing()
        {
        	for(int x=0;x<9;x++)
        	{
        		for(int y=x;y<9;y++)
        		{
					Swap(Point(x, y), Point(y, x));
        		}
        	}
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
