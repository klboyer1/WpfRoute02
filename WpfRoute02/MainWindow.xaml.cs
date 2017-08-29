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
            SqlControl st = new SqlControl();
            string StreetSelected = " ";
            string Query = " ";

            StreetSelected = cboxStreet.SelectedItem.ToString();
            if (StreetSelected != " ")
            {
                Query = "Select Street, HouseNum, Unit, Code, TVGuide, Path, Seq from Route02 where Street = " + '"' + cboxStreet.SelectedItem.ToString() + '"';
            }
            else
            {
                Query = "Select Street, HouseNum, Unit, Code, TVGuide, Path, Seq from Route02";
            }
            st.ExecQuery(Query);
            DataGrid1.ItemsSource = st.DT.DefaultView;

        }
    }
}
