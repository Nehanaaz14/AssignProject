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
    /// Interaction logic for PulseWidthOption.xaml
    /// </summary>
    public partial class PulseWidthOption : UserControl
    {
        private static AssignDatabaseDataContext _adpDC = new AssignDatabaseDataContext();
        private ObservablePulseWidthList _pulsewidthList;
        public PulseWidthOption()
        {
            InitializeComponent();
            LoadPulseWidthList();
        }

        public void LoadPulseWidthList()
        {
            _pulsewidthList = new ObservablePulseWidthList(_adpDC);
            this.PulseWidthListBox.ItemsSource = _pulsewidthList;
        }
    }
}
