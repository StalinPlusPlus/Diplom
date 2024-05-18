using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Diplom2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<FrameworkElement> _listElement = new List<FrameworkElement>();
        private Button? _setButton = null;
        private Label? _setLabel = null;
        private System.Windows.Controls.Image? _setImage = null;
        private UIElement? _draggedeElement;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            Button button = new Button();
            button.Content = "Новая кнопка " + (_listElement.Count + 1).ToString();
            button.Width = 200;
            button.Height = 30;
            button.Click += SetButton;
            button.MouseRightButtonDown += UiElementMouseButtonDown;
            button.Margin = button.Margin with { Top = 5.0d };
            stackPanel.Children.Add(button);
            _listElement.Add(button);
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();

            image.Width = 100;
            image.Height = 100;
            image.MouseLeftButtonDown += SetImage;
            image.MouseRightButtonDown += UiElementMouseButtonDown;

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("pack://application:,,,/Source/default.png");
            bitmap.EndInit();

            image.Source = bitmap;

            stackPanel.Children.Add(image);
            _listElement.Add(image);
        }

        private void SetButton(object sender, RoutedEventArgs e)
        {
            _setButton = (Button)sender;
            SetButtonWindow window = new SetButtonWindow(_setButton, DeleteElement);
            if (_setButton != null)
            {
                window.WidthButton = _setButton.Width;
                window.HeightButton = _setButton.Height;
                window.TextButton = (string)_setButton.Content;
            }

            window.Closing += SetButtonClosing!;

            window.ShowDialog();
        }

        private void UiElementMouseButtonDown(object sender, MouseButtonEventArgs e)
        {
            _draggedeElement = sender as UIElement;
            if (e.ClickCount == 1)
            {
                DragDrop.DoDragDrop(_draggedeElement, _draggedeElement, DragDropEffects.Move);
            }

            e.Handled = true;
        }

        private void SetButtonClosing(object sender, CancelEventArgs e)
        {
            SetButtonWindow window = (SetButtonWindow)sender;
            if (_setButton != null)
            {
                _setButton.Width = window.WidthButton;
                _setButton.Height = window.HeightButton;
                _setButton.Content = window.TextButton;
            }

            _setButton = null;
        }

        private void StackPanelDragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
        }

        private void StackPanelDrop(object sender, DragEventArgs e)
        {
            stackPanel.Children.Remove(_draggedeElement);
            stackPanel.Children.Insert(stackPanel.Children.IndexOf(e.Source as UIElement), _draggedeElement);
            _draggedeElement = null;
        }

        private void AddLabel(object sender, RoutedEventArgs e)
        {
            Label label = new Label();
            label.Content = "Новая надпись " + (_listElement.Count + 1).ToString();
            label.Width = 200;
            label.Height = 30;
            label.MouseLeftButtonDown += SetLabel;
            label.MouseRightButtonDown += UiElementMouseButtonDown;
            stackPanel.Children.Add(label);
            _listElement.Add(label);
        }

        private void SetLabelClosing(object sender, CancelEventArgs e)
        {
            SetLabelWindow? window = sender as SetLabelWindow;
            if (_setLabel != null)
            {
                _setLabel.Width = window.WidthLabel;
                _setLabel.Height = window.HeightLabel;
                _setLabel.Content = window.TextLabel;
            }

            _setLabel = null;
        }

        private void SetLabel(object sender, RoutedEventArgs e)
        {
            _setLabel = sender as Label;
            SetLabelWindow window = new SetLabelWindow(_setLabel, DeleteElement);
            if (_setLabel != null)
            {
                window.WidthLabel = _setLabel.Width;
                window.HeightLabel = _setLabel.Height;
                window.TextLabel = (string)_setLabel.Content;
            }

            window.Closing += SetLabelClosing;

            window.ShowDialog();
        }

        private void SetImage(object sender, RoutedEventArgs e)
        {
            _setImage = sender as System.Windows.Controls.Image;
            SetImageWindow window = new SetImageWindow(_setImage, DeleteElement);

            if (_setImage != null)
            {
                window.ImageHeight = _setImage.Height;
                window.ImageWidth = _setImage.Width;
                BitmapImage image = _setImage.Source as BitmapImage;

                if (image != null && image.UriSource != null)
                {
                    window.ImagePath = image.UriSource.ToString();
                }

                window.Closing += SetImageClosing;

                window.ShowDialog();

            }
        }

        private void SetImageClosing(object sender, CancelEventArgs e)
        {
            SetImageWindow? window = sender as SetImageWindow;

            if (_setImage != null)
            {
                _setImage.Width = window.ImageWidth;
                _setImage.Height = window.ImageHeight;

                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(window.ImagePath);
                image.EndInit();

                _setImage.Source = image;
            }

            _setImage = null;
        }

        private void CreatePdf(object sender, RoutedEventArgs e)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(QuestPDF.Helpers.Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(x =>
                        {

                            x.Item().Text("Инструкция по внедрению").SemiBold().FontSize(36).FontColor(QuestPDF.Helpers.Colors.Blue.Medium);
                            x.Item().Text("1. Открыть Android Stuido");
                            x.Item().Text("2. Скопировать представленный ниже код и вставить его в файл activity_main.xml " +
                                "удалив при этом старый код");
                            x.Item().Text("Код:");
                            x.Item().Text("<?xml version=\"1.0\" " +
                                "encoding=\"utf-8\"?>\r\n<LinearLayout xmlns:android=\"http://schemas.android.com/apk/res/android\"\r\n    " +
                                "xmlns:app=\"http://schemas.android.com/apk/res-auto\"\r\n    " +
                                "xmlns:tools=\"http://schemas.android.com/tools\"\r\n    " +
                                "android:id=\"@+id/main\"\r\n   " +
                                "android:orientation=\"vertical\"\r\n     " +
                                "android:layout_width=\"match_parent\"\r\n    " +
                                "android:layout_height=\"match_parent\"\r\n    " +
                                "tools:context=\".MainActivity\">");

                            foreach (var element in _listElement)
                            {
                                if (element.GetType().ToString() == "System.Windows.Controls.Button") 
                                {
                                    Button button = (Button)element;
                                    x.Item().Text($"<Button\r\n        " +
                                        $"android:layout_width=\"{button.Width}dp\"\r\n        " +
                                        $"android:layout_height=\"{button.Height}dp\"\r\n        " +
                                        $"android:text=\"{button.Content}\"/>"); 
                                } else 
                                    if (element.GetType().ToString() == "System.Windows.Controls.Label")
                                {
                                    Label label = (Label)element;
                                    x.Item().Text($"<TextView\r\n        " +
                                        $"android:layout_width=\"{label.Width}dp\"\r\n        " +
                                        $"android:layout_height=\"{label.Height}dp\"\r\n        " +
                                        $"android:text={label.Content}\"\"/>");
                                } else 
                                    if (element.GetType().ToString() == "System.Windows.Controls.Image")
                                {
                                    System.Windows.Controls.Image image = (System.Windows.Controls.Image)element;
                                    x.Item().Text($"<ImageView\r\n        " +
                                        $"android:layout_width=\"{image.Width}dp\"\r\n        " +
                                        $"android:layout_height=\"{image.Height}dp\"\r\n        " +
                                        $"android:src=\"\"/>");
                                }
                            }

                            x.Item().Text("3. Для добавления изображения, его нужно добавить в папку \"drawable\" проекта Android Studio." +
                                "Далее в параметре src элемента ImageView нужно указать путь до изображения в формате " +
                                "\"@drawable/Название_файла_без_формата\"");

                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.CurrentPageNumber();
                        });
                });
            })
            .GeneratePdfAndShow();
        }

        private void DeleteElement(FrameworkElement element)
        {
            _listElement.Remove(element);
            UpdateUI();
        }

        private void UpdateUI()
        {
            stackPanel.Children.Clear();
            foreach (var element in _listElement)
            {
                stackPanel.Children.Add(element);
            }
        }
    }
}