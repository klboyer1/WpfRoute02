using System.Data;
using System.Windows;
using System.Windows.Controls;

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
            btnLoad.IsEnabled = false;
            SqlControl st = new SqlControl();
            string StreetSelected = " ";
            string Query = " ";

            
            if (StreetSelected != " ")
            {
                StreetSelected = cboxStreet.SelectedItem.ToString();
                Query = "Select Street, HouseNum, Unit, Code, TVGuide, Delivery, Path, Seq from Route02 where Street = " + '"' + cboxStreet.SelectedItem.ToString() + '"';
            }
            else
            {
                Query = "Select Street, HouseNum, Unit, Code, TVGuide, Delivery, Path, Seq from Route02";
            }
            st.ExecQuery(Query);
            DataGrid1.ItemsSource = st.DT.DefaultView;

        }

        private void DataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid)sender;
            DataRowView row_selected = dg.SelectedItem as DataRowView;

            // var txt = row_selected["Updated"].ToString();
            this.txtHouseNum.Text = row_selected["HouseNum"].ToString();
            this.txtUnit.Text = row_selected["Unit"].ToString();
            this.cboxCode.Text = row_selected["Code"].ToString();

            if ((bool)row_selected["TVGuide"])
            { this.chkboxTVGuide.IsChecked = true; }
            else
            { this.chkboxTVGuide.IsChecked = false; }

            this.txtDelivery.Text = row_selected["Delivery"].ToString();
            // this.txtUpdated.Text = row_selected["Updated"].ToString();
        }

        private void RouteButton_Click(object sender, RoutedEventArgs e)
        {
            RouteList02 rt = new RouteList02();
            rt.ShowDialog();
        }
    }
}
