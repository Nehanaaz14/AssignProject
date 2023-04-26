using AssignProject.Modules.Amplitude.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssignProject.Modules.Amplitude.Views
{
    /// <summary>
    /// Interaction logic for RateSelectView.xaml
    /// </summary>
    public partial class RateSelectView : UserControl
    {
        private static AssignDatabaseDataContext _adpDC = new AssignDatabaseDataContext();
        private ObservableRateList _rateList;
        public int SelectedRateValue;

        public RateSelectView()
        {
            InitializeComponent();
            LoadRateList();
            RateSelectViewModel rateSelectViewModel = new RateSelectViewModel();
        }

        public void LoadRateList()
        {
            _rateList = new ObservableRateList(_adpDC);
            this.RateListBox.ItemsSource = _rateList;
        }
    }
}
