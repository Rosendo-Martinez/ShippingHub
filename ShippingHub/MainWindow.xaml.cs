using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShippingHub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PackagesTracker packageTracker;
        
        public MainWindow()
        {
            InitializeComponent();
            packageTracker = new PackagesTracker();
            
        }

        private void ScanNewClick(object sender, RoutedEventArgs e) {
            ToggleButtons();
            ToggleButtons2();
            Clear();
            ArrivedAt.Text = DateTime.Now.ToString();
            PackageID.Text = packageTracker.CurrentID().ToString();
        }

        private void BackClick(object sender, RoutedEventArgs e) {
            Package prev = packageTracker.GetPrev();
            if (prev != null) {
                ArrivedAt.Text = prev.ArrivalDate.ToString();
                PackageID.Text = prev.ID.ToString();
                Address.Text = prev.Address;
                Zip.Text = prev.Zip;
                State.Text = prev.StateInitials;
                City.Text = prev.City;
            }
        }

        private void AddClick(object sender, RoutedEventArgs e) {

            if (packageTracker.Contains((uint)Int32.Parse(PackageID.Text))) {
                packageTracker.Edit((uint)Int32.Parse(PackageID.Text), Address.Text, City.Text, State.Text, Zip.Text);
            } else {
                packageTracker.ScanNew(City.Text, State.Text, Zip.Text, Address.Text, DateTime.Parse(ArrivedAt.Text));
            }
            packageTracker.ResetPointer();
            ToggleButtons();
            ToggleButtons2();
            Clear();
        }

        private void RemoveClick(object sender, RoutedEventArgs e) {
            if (PackageID.Text.Length != 0) {
                packageTracker.RemovePackage((uint)Int32.Parse(PackageID.Text));
                packageTracker.ResetPointer();
                Clear();
            }
        }

        private void EditClick(object sender, RoutedEventArgs e) {
            if (PackageID.Text.Length != 0) {
                ToggleButtons();
                ToggleButtons2();
            }
        }

        private void NextClick(object sender, RoutedEventArgs e) {
            Package next = packageTracker.GetNext();
            if (next != null) { 
                ArrivedAt.Text = next.ArrivalDate.ToString();
                PackageID.Text = next.ID.ToString();
                Address.Text = next.Address;
                Zip.Text = next.Zip;
                State.Text = next.StateInitials;
                City.Text = next.City;
            }
        }

        private void Clear() {
            ArrivedAt.Text = "";
            PackageID.Text = "";
            ArrivedAt.Text = "";
            Zip.Text = "";
            PackageID.Text = "";
            Address.Text = "";
            State.Text = "";
            City.Text = "";
        }

        private void PackagesByState_DropDownClosed(object sender, EventArgs e) {
            PackagesByStateList.Text = packageTracker.GetPackageIDsByState(PackagesByState.Text);
        }

        private void ToggleButtons() {
            Address.IsEnabled = !Address.IsEnabled;
            City.IsEnabled = !City.IsEnabled;
            Zip.IsEnabled = !Zip.IsEnabled;
            State.IsEnabled = !State.IsEnabled;
            ScanNew.IsEnabled = !ScanNew.IsEnabled;
            Add.IsEnabled = !Add.IsEnabled;
        }

        private void ToggleButtons2() { 
            Next.IsEnabled = !Next.IsEnabled;
            Back.IsEnabled = !Back.IsEnabled;
            Edit.IsEnabled = !Edit.IsEnabled;
            Remove.IsEnabled = !Remove.IsEnabled;
        }
    }
}
