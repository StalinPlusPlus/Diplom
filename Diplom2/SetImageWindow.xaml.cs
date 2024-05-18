using Microsoft.Win32;
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

namespace Diplom2
{
    /// <summary>
    /// Логика взаимодействия для SetImageWindow.xaml
    /// </summary>
    public partial class SetImageWindow : Window
    {
        private FrameworkElement _element;
        private Action<FrameworkElement> _deleteAction;
        private string imagePath;

        public SetImageWindow(FrameworkElement element, Action<FrameworkElement> deleteAction)
        {
            InitializeComponent();

            _element = element;
            _deleteAction = deleteAction;
        }

        public double ImageWidth
        {
            set { textBoxWidth.Text = value.ToString(); }
            get { return Convert.ToInt32(textBoxWidth.Text); }
        }

        public double ImageHeight
        {
            set { textBoxHeight.Text = value.ToString(); }
            get { return Convert.ToInt32(textBoxHeight.Text); }
        }

        public string ImagePath
        {
            set 
            { 
                imagePath = value;
                textBoxPath.Text = imagePath;
            }
            get { return imagePath; }
        }

        private void ChoosePath(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Все файлы (*.*)|*.*|Изображения (*.jpg; *.jpeg; *.png)|*.jpg;*.jpeg;*.png";
            dialog.FilterIndex = 2;
            dialog.InitialDirectory = "C:\\Users\\Public\\Pictures";

            if (dialog.ShowDialog() == true)
            {
                imagePath= dialog.FileName;
                textBoxPath.Text = dialog.FileName;
            }
        }

        private void ClickSubmit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClickRemove(object sender, RoutedEventArgs e)
        {
            _deleteAction?.Invoke(_element);
            this.Close();
        }
    }
}
