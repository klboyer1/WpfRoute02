using System;
using System.Collections.Generic;
using System.Data;
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

namespace WpfRoute02
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FillStreetBox();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void FillStreetBox()
        {
            SqlControl st = new SqlControl();
            string Query = "Select Street from Streets";
            st.ExecQuery(Query);

            foreach (DataRow dr in st.DT.Rows)
            {
                cboxStreet.Items.Add(dr["Street"].ToString());
            }

        }
        private void cboxStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            SqlControl st = new SqlControl();
            string Query = "Select Street, HouseNum, Unit, Code, TVGuide, Path, Seq from Route02 where Street = " +'"'+ cboxStreet.SelectedItem.ToString() + '"';
            st.ExecQuery(Query);
            DataGrid1.ItemsSource = st.DT.DefaultView;
           
        }
    }
}
