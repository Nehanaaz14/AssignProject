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
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ViewA : UserControl
    {
        private static AssignDatabaseDataContext _adpDC = new AssignDatabaseDataContext();
        private ObservableAmplitudeList _amplitudeList;
        public ViewA()
        {
            InitializeComponent();
            LoadAmplitudeList();
        }

        public void LoadAmplitudeList()
        {
            _amplitudeList = new ObservableAmplitudeList(_adpDC);
            this.AmplitudeListBox.ItemsSource = _amplitudeList;
        }
    }
}
