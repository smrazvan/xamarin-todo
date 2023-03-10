using Syncfusion.SfChart.XForms;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace routedapp.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public ObservableCollection<ChartDataPoint> HighTemperature { get; set; }

        public AboutViewModel()
        {
            Title = "Home";

            //seed list for chart
            HighTemperature = new ObservableCollection<ChartDataPoint>();

            HighTemperature.Add(new ChartDataPoint("Jan", 15));
            HighTemperature.Add(new ChartDataPoint("Feb", 44));
            HighTemperature.Add(new ChartDataPoint("Mar", 53));
            HighTemperature.Add(new ChartDataPoint("Apr", 64));
            HighTemperature.Add(new ChartDataPoint("May", 75));
            HighTemperature.Add(new ChartDataPoint("Jun", 83));
            HighTemperature.Add(new ChartDataPoint("Jul", 87));
            HighTemperature.Add(new ChartDataPoint("Aug", 84));
            HighTemperature.Add(new ChartDataPoint("Sep", 78));
            HighTemperature.Add(new ChartDataPoint("Oct", 67));
            HighTemperature.Add(new ChartDataPoint("Nov", 55));
            HighTemperature.Add(new ChartDataPoint("Dec", 45));



        }
    }
}