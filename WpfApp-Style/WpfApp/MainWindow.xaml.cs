using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<dinamic_change> bd = new List<dinamic_change>();

        CLIENT.Program cl = new CLIENT.Program();
        public MainWindow()
        {

            InitializeComponent();

            bd.AddRange(new dinamic_change[] { new dinamic_change(){ LastName = "Иванов", FirstName = "Иван", Photo = "prof.jpg" }, new dinamic_change(){ LastName = "Сидоров", FirstName = "Сидор", Photo = "prof.jpg" }, new dinamic_change(){ LastName = "Петров", FirstName = "Петр", Photo = "prof.jpg" }, new dinamic_change(){ LastName = "Харьков", FirstName = "Антон", Photo = "prof.jpg" },
            new dinamic_change(){ LastName = "Иванов", FirstName = "Иван", Photo = "prof.jpg" }, new dinamic_change(){ LastName = "Сидоров", FirstName = "Сидор", Photo = "prof.jpg" }, new dinamic_change(){ LastName = "Петров", FirstName = "Петр", Photo = "prof.jpg" }, new dinamic_change(){ LastName = "Харьков", FirstName = "Антон", Photo = "prof.jpg" }});
            for (int i = 0; i < bd.Count; i++)
            {
                bd[i].Wall = (i).ToString() + " User_wall";
                bd[i].Message = (i).ToString() + " User_messages";
            }


            for (int i = 0; i < bd.Count; i++)
            {
                Image img = new Image();
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(bd[i].Photo, UriKind.Relative);
                bi3.EndInit();
                img.Stretch = Stretch.Fill;
                img.Height = 55;
                img.Width = 55;
                img.Source = bi3;
                m_list.Items.Add(img);
            }
            
            

        }

        private void sign_in_btn_Click(object sender, RoutedEventArgs e)
        {
            Main_tabcontrol.SelectedIndex = 1;
        }

        private void sign_up_btn_Click(object sender, RoutedEventArgs e)
        {
            Main_tabcontrol.SelectedIndex = 2;
        }

        private void back_btn1_Click(object sender, RoutedEventArgs e)
        {
            Main_tabcontrol.SelectedIndex = 0; 
        }

        private void back_btn2_Click(object sender, RoutedEventArgs e)
        {
            Main_tabcontrol.SelectedIndex = 0;
        }

        private void log_in_btn1_Click(object sender, RoutedEventArgs e)
        {
            Main_tabcontrol.SelectedIndex = 3;
            
        }

        private void create_ac_btn_Click(object sender, RoutedEventArgs e)
        {
            Main_tabcontrol.SelectedIndex = 3;
           
        }

       




       

        private void m_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedIndex == 0)
                main_tab.SelectedIndex = 0;
            else if ((sender as ListBox).SelectedIndex == 1)
                main_tab.SelectedIndex = 1;
            else
            {
                main_tab.SelectedIndex = 2;
                wall_tb.Text = bd[(sender as ListBox).SelectedIndex - 2].Wall;
                read_mes_tb.Text = bd[(sender as ListBox).SelectedIndex - 2].Message;
                friend_name_lb.Text = bd[(sender as ListBox).SelectedIndex - 2].FirstName + "\n" + bd[(sender as ListBox).SelectedIndex - 2].LastName;                
                write_mes_tb.Text = bd[(sender as ListBox).SelectedIndex - 2].Typing;

            }

           
        }

        private void open_dialog_btn_Click(object sender, RoutedEventArgs e)
        {
            if (wall_tb.Visibility != Visibility.Hidden)
            {
                wall_tb.Visibility = Visibility.Hidden;
                wall_2_tb.Visibility = Visibility.Visible;
                open_dialog_btn.Content = "Open profile";
            }
            else
            {
                wall_tb.Visibility = Visibility.Visible;
                wall_2_tb.Visibility = Visibility.Hidden;
                open_dialog_btn.Content = "Open chat";
            }
        }

        private void Upload_btn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                string filename = dlg.FileName;
                MessageBox.Show(filename);

                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(filename, UriKind.Absolute);
                bi3.EndInit();
                user_create_img.Source = bi3;
                user_img.Source = bi3;
                (m_list.Items[0] as Image).Source = bi3;
            }

            
        }

        private void chng_photo_btn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; 
            dlg.DefaultExt = ".txt"; 
            dlg.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                
                string filename = dlg.FileName;
                MessageBox.Show(filename);
                
                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(filename, UriKind.Absolute);
                bi3.EndInit();
                user_img.Source = bi3;
                (m_list.Items[0] as Image).Source = bi3;


            }
        }

        private void chng_name_btn_Click(object sender, RoutedEventArgs e)
        {
            us_lb.Visibility = Visibility.Hidden;
            chng_name_tb.Visibility = Visibility.Visible;
        }

        private void chng_name_tb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                us_child_lb.Text = chng_name_tb.Text;
                us_lb.Visibility = Visibility.Visible;
                chng_name_tb.Visibility = Visibility.Hidden;
                
            }
        }

        private void log_out_btn_Click(object sender, RoutedEventArgs e)
        {
            foreach(var a in bd)
            {
                a.Typing = null;
            }
            write_mes_tb.Clear();
            Main_tabcontrol.SelectedIndex = 0;
        }

        private void write_mes_tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(m_list.SelectedIndex >=2)
            bd[m_list.SelectedIndex - 2].Typing = (sender as TextBox).Text;
            
        }
    }
}


