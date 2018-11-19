using System.Windows;
using System.Windows.Controls;

using AsmDude.Tools;

namespace AsmDude.QuickInfo
{
    public partial class BugWindow : UserControl
    {
        public BugWindow()
        {
            this.InitializeComponent();
            AsmDudeToolsStatic.Output_INFO("BugWindow:constructor");

            this.MainWindow.MouseLeftButtonDown += (o, i) => {
                AsmDudeToolsStatic.Output_INFO("BugWindow:MouseLeftButtonDown Event");
                //i.Handled = true; // dont let the mouse event from inside this window bubble up to VS
            }; 

            this.MainWindow.PreviewMouseLeftButtonDown += (o, i) =>
            {
                AsmDudeToolsStatic.Output_INFO("BugWindow:PreviewMouseLeftButtonDown Event");
                //i.Handled = true; // if true then no event is able to bubble to the gui
            };
        }

        private void GotMouseCapture_Click(object sender, RoutedEventArgs e)
        {
            AsmDudeToolsStatic.Output_INFO("BugWindow:GotMouseCapture_Click");
            e.Handled = true;
        }
    }
}
