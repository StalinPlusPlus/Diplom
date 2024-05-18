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
    /// Логика взаимодействия для SetLabelWindow.xaml
    /// </summary>
    public partial class SetLabelWindow : Window
    {
        private FrameworkElement _element;
        private Action<FrameworkElement> _deleteAction;

        public double WidthLabel
        {
            set { textBoxWidth.Text = value.ToString(); }
            get { return Convert.ToInt32(textBoxWidth.Text); }
        }

        public double HeightLabel
        {
            set { textBoxHeight.Text = value.ToString(); }
            get { return Convert.ToInt32(textBoxHeight.Text); }
        }

        public string TextLabel
        {
            set { textBoxText.Text = value; }
            get { return textBoxText.Text; }
        }

        public SetLabelWindow(FrameworkElement element, Action<FrameworkElement> deleteAction)
        {
            InitializeComponent();

            _element = element;
            _deleteAction = deleteAction;
        }

        public void ClickSubmit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void ClickRemove(object sender, RoutedEventArgs e)
        {
            _deleteAction?.Invoke(_element);
            this.Close();
        }
    }
}
