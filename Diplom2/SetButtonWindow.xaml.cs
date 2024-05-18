using System.Windows;

namespace Diplom2
{
    /// <summary>
    /// Логика взаимодействия для SetButtonWindow.xaml
    /// </summary>
    public partial class SetButtonWindow : Window
    {
        private FrameworkElement _element;
        private Action<FrameworkElement> _deleteAction;

        public SetButtonWindow(FrameworkElement element, Action<FrameworkElement> deleteAction)
        {
            InitializeComponent();

            _element = element;
            _deleteAction = deleteAction;
        }

        public double WidthButton
        {
            set { textBoxWidth.Text = value.ToString(); }
            get { return Convert.ToInt32(textBoxWidth.Text); }
        }

        public double HeightButton
        {
            set { textBoxHeight.Text = value.ToString(); }
            get { return Convert.ToInt32(textBoxHeight.Text); }
        }

        public string TextButton
        {
            set { textBoxText.Text = value; }
            get { return textBoxText.Text; }
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
