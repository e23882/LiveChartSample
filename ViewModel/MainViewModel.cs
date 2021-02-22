using LiveCharts;
using LiveCharts.Geared;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TestLiveChart.Command;

namespace TestLiveChart.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        #region Declarations
        private SeriesCollection _seriesCollection;
        #endregion

        #region Property
        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged();
            }
        }
        
        public ICommand AddPointButtonClickCommand
        {
            get { return new RelayCommand(AddPointButtonClickCommandAction); }
        }

        private void AddPointButtonClickCommandAction(object obj)
        {
            var parameter = int.Parse(obj.ToString());
            
            List<object> temp = new List<object>();
            IEnumerable<object> list = new List<object>();
            Random rd = new Random();
            for (int i = 1; i < parameter; i++)
            {
                temp.Add(rd.NextDouble());
            }
            list = temp.ToList<object>();
            SeriesCollection[0].Values.AddRange(list);
        }
        #endregion

        #region Memberfunction
        /// <summary>
        /// 
        /// </summary>
        public MainViewModel()
        {
            Random rd = new Random();
            double[] array = new double[1000];
            for (int i = 0; i < 1000; i++)
            {
                array[i] = rd.NextDouble();
            }

            var series = new GLineSeries()
            {
                Values = array.AsGearedValues().WithQuality(Quality.Low)
            };

            SeriesCollection = new SeriesCollection
            {
                series
            };

            //Thread th = new Thread(AddPoint);
            //th.Start();
            //Task.Run(() => AddPoint());

        }

        /// <summary>
        /// 
        /// </summary>
        public void AddPoint()
        {
            ////case1
            //List<double> list = new List<double>();
            //for(int i = 0; i < 20000000; i++)
            //    list.Add(i);

            //Parallel.ForEach(list, (item, loopState) =>
            //{
            //    SeriesCollection[0].Values.Add(item);
            //});

            //case2
            //Random rd = new Random();
            //for (double i = 0.1; i < 20000000; i++)
            //    SeriesCollection[0].Values.Add(rd.NextDouble());

            //best practice
            while (true)
            {
                List<object> temp = new List<object>();
                IEnumerable<object> list = new List<object>();
                Random rd = new Random();
                for (int i = 1; i < 100000; i++)
                {
                    temp.Add(rd.NextDouble());
                }
                list = temp.ToList<object>();
                SeriesCollection[0].Values.AddRange(list);
                Thread.Sleep(1000);
            }

        }
        #endregion
    }
}
