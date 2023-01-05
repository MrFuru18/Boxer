using ApiLibrary;
using ApiLibrary.Model;
using ApiLibrary.Repo;
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

namespace Boxer
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ClientHttp.InitializeClient();
        }

        private async void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> employeesList = await EmployeeProcessor.getAllEmployees();

            if (employeesList.Count != 0)
            {
                Console.WriteLine(employeesList[0].name);
            }
        }
    }
}
