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
            Update_DataGrid();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            Update_DataGrid();
        }
        private void Update_DataGrid()
        {
            btnLoad.IsEnabled = false;
            SqlControl st = new SqlControl();
            string StreetSelected = " ";
            string Query = " ";

            StreetSelected = cboxStreet.SelectedItem.ToString();

            if (StreetSelected != " ")
            {
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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            this.txtHouseNum.Text = "";
            this.txtUnit.Text = "";
            this.cboxCode.Text = "";
            this.chkboxTVGuide.IsChecked = false;
            this.txtDelivery.Text = "";
            this.txtUpdated.Text = "";

            TurnsButtonsOff();

        }
        public void TurnsButtonsOff()
        {
            btnAdd.IsEnabled = false;
            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
            btnClose.IsEnabled = false;
            btnRoadList.IsEnabled = false;
        }
        public void ValidateCustomer()
        {
            if (this.cboxStreet.Text != "")
            {
                string Street = cboxStreet.Text;
            }
            else
            {
                MessageBox.Show("Please select a valid Street");
            }
            if (this.txtHouseNum.Text != "")
            {
                int HouseNum = int.Parse(txtHouseNum.Text);
            }
            else
            {
                MessageBox.Show("Please enter a valid House Number");
            }
            if (this.cboxCode.Text != "")
            {
                string Code = cboxCode.Text;
            }
            else
            {
                MessageBox.Show("Please select a valid Code");
            }
            if (this.chkboxTVGuide.IsChecked == false)
            {
                bool TVGuide = true;
            }
            else
            {
                bool TVGuide = false; ;
            }

        }
        public void TurnsButtonsOn()
        {
            btnAdd.IsEnabled = true;
            btnEdit.IsEnabled = true;
            btnDelete.IsEnabled = true;
            btnClose.IsEnabled = true;
            btnRoadList.IsEnabled = true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Edit Button Clicked");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Cancel Button Clicked");
        }

        public void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ValidateCustomer();
            SqlControl sss = new SqlControl();

            string Query = "insert into Route02 (Street, HouseNum, Unit, Code, TVGuide, Delivery, Path, Seq) " +
                "values ('" + cboxStreet.Text + "','"
                + int.Parse(txtHouseNum.Text) + "','"
                + txtUnit.Text + "','"
                + cboxCode.Text + "','"
                + chkboxTVGuide.IsChecked + "','"
                + txtDelivery.Text + "','"
                + 0 + "','"
                + 0 + "')";
            sss.ExecNonQuery(Query);
            MessageBox.Show("Save Button Clicked");
            TurnsButtonsOn();
            Update_DataGrid();


        }
    }

}

