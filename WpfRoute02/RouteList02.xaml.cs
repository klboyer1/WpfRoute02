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
using System.Windows.Shapes;

namespace WpfRoute02
{
    /// <summary>
    /// Interaction logic for RouteList02.xaml
    /// </summary>
    public partial class RouteList02 : Window
    {
        public RouteList02()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            SqlControl st = new SqlControl();

            string Query = " ";
            Query = "Select Street, HouseNum, Unit, Code, TVGuide, Delivery, Path, Seq from Route02";
            st.ExecQuery(Query);
            DataGrid2.ItemsSource = st.DT.DefaultView;


        }
    }
}
