
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApplication1.Common;

namespace WpfApplication1
{
    public class MessageBoxWin : Control
    {
        #region Dependency Properties
        public static readonly DependencyProperty MessageDescriptionProperty =
            DependencyProperty.Register("MessageDescription", typeof(string), typeof(MessageBoxWin), new PropertyMetadata(""));

        public static readonly DependencyProperty MessageTextProperty =
            DependencyProperty.Register("MessageText", typeof(string), typeof(MessageBoxWin));

        public static readonly DependencyProperty MessageBoxButtonsProperty =
           DependencyProperty.Register("MessageBoxButtons", typeof(NS_MessageBoxButtons), typeof(MessageBoxWin), new PropertyMetadata(NS_MessageBoxButtons.OK));

        public static readonly DependencyProperty MessageBoxImageProperty =
            DependencyProperty.Register("MessageBoxImage", typeof(NS_MessageBoxIcon), typeof(MessageBoxWin), new PropertyMetadata(NS_MessageBoxIcon.None));

        public string MessageDescription
        {
            get { return (string)GetValue(MessageDescriptionProperty); }
            set { SetValue(MessageDescriptionProperty, value); }
        }
        public string MessageText
        {
            get { return (string)GetValue(MessageTextProperty); }
            set { SetValue(MessageTextProperty, value); }
        }
        public NS_MessageBoxButtons MessageBoxButtons
        {
            get { return (NS_MessageBoxButtons)GetValue(MessageBoxButtonsProperty); }
            set { SetValue(MessageBoxButtonsProperty, value); }
        }
        public NS_MessageBoxIcon MessageBoxImage
        {
            get { return (NS_MessageBoxIcon)GetValue(MessageBoxImageProperty); }
            set { SetValue(MessageBoxImageProperty, value); }
        }
        public NS_MessageBoxResult MessageBoxResult
        { get; private set; }

        #endregion

        static MessageBoxWin()
        {
            InitilizeButtonsCommands();
        }

        #region Commands
        public static RoutedCommand OKCommand { get; private set; }
        public static RoutedCommand CancelCommand { get; private set; }
        public static RoutedCommand YesCommand { get; private set; }
        public static RoutedCommand NoCommand { get; private set; }

        private static void InitilizeButtonsCommands()
        {
            OKCommand = new RoutedCommand("OKCommand", typeof(MessageBoxWin));
            CommandManager.RegisterClassCommandBinding(typeof(MessageBoxWin), new CommandBinding(OKCommand, OnOKCommand));
            CancelCommand = new RoutedCommand("CancelCommand", typeof(MessageBoxWin));
            CommandManager.RegisterClassCommandBinding(typeof(MessageBoxWin), new CommandBinding(CancelCommand, OnCancelCommand));
            YesCommand = new RoutedCommand("YesCommand", typeof(MessageBoxWin));
            CommandManager.RegisterClassCommandBinding(typeof(MessageBoxWin), new CommandBinding(YesCommand, OnYesCommand));
            NoCommand = new RoutedCommand("NoCommand", typeof(MessageBoxWin));
            CommandManager.RegisterClassCommandBinding(typeof(MessageBoxWin), new CommandBinding(NoCommand, OnNoCommand));

        }

        private static void OnOKCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxWin cmb = sender as MessageBoxWin;
            if (cmb == null) return;
            cmb.MessageBoxResult = NS_MessageBoxResult.OK;
            Helper.FindParent<Window>(cmb).Close();
            Helper.FindParent<Window>(cmb).Close();
        }
        private static void OnCancelCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxWin cmb = sender as MessageBoxWin;
            if (cmb == null) return;
            cmb.MessageBoxResult = NS_MessageBoxResult.Cancel;
            Helper.FindParent<Window>(cmb).Close();
        }
        private static void OnYesCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxWin cmb = sender as MessageBoxWin;
            if (cmb == null) return;
            cmb.MessageBoxResult = NS_MessageBoxResult.Yes;
            Helper.FindParent<Window>(cmb).Close();
        }
        private static void OnNoCommand(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxWin cmb = sender as MessageBoxWin;
            if (cmb == null) return;
            cmb.MessageBoxResult = NS_MessageBoxResult.No;
            Helper.FindParent<Window>(cmb).Close();
        }

        #endregion

        public MessageBoxWin()
        {
            MessageBoxResult = NS_MessageBoxResult.Cancel;
        }

        public static NS_MessageBoxResult ShowDialog(string messageText)
        {
            return ShowDialog(null, messageText, "", NS_MessageBoxButtons.OK, NS_MessageBoxIcon.None);
        }
        public static NS_MessageBoxResult ShowDialog(Window owner, string messageText)
        {
            return ShowDialog(owner, messageText, "", NS_MessageBoxButtons.OK, NS_MessageBoxIcon.None);
        }
        public static NS_MessageBoxResult ShowDialog(string messageText, string messageDesctiption)
        {
            return ShowDialog(null, messageText, messageDesctiption, NS_MessageBoxButtons.OK, NS_MessageBoxIcon.None);
        }
        public static NS_MessageBoxResult ShowDialog(Window owner, string messageText, string messageDesctiption)
        {
            return ShowDialog(owner, messageText, messageDesctiption, NS_MessageBoxButtons.OK, NS_MessageBoxIcon.None);
        }
        public static NS_MessageBoxResult ShowDialog(string messageText, string messageDesctiption, NS_MessageBoxButtons buttons)
        {
            return ShowDialog(null, messageText, messageDesctiption, buttons, NS_MessageBoxIcon.None);
        }
        public static NS_MessageBoxResult ShowDialog(Window owner, string messageText, string messageDesctiption, NS_MessageBoxButtons buttons)
        {
            return ShowDialog(owner, messageText, messageDesctiption, buttons, NS_MessageBoxIcon.None);
        }
        public static NS_MessageBoxResult ShowDialog(string messageText, string messageDesctiption, NS_MessageBoxButtons buttons, NS_MessageBoxIcon icon)
        {
            return ShowDialog(null, messageText, messageDesctiption, buttons, icon);
        }
        public static NS_MessageBoxResult ShowDialog(Window owner, string messageText, string messageDesctiption,  NS_MessageBoxButtons buttons, NS_MessageBoxIcon icon)
        {
            MessageBoxWin content = new MessageBoxWin() { MessageText = messageText, MessageDescription = messageDesctiption, MessageBoxButtons = buttons, MessageBoxImage = icon };
            Window win = new Window();
            //NS.Controls.CustomControls.CustomWindow win = new NS.Controls.CustomControls.CustomWindow();
            if (owner != null)
            {
                win.Owner = owner;
                win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                win.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

           

            //win.TitleLeft = true;
            win.SizeToContent = SizeToContent.WidthAndHeight;
            win.ResizeMode = ResizeMode.NoResize;
            //win.ShowFullScreenButton = false;
            //win.ShowMinimizeButton = false;
            win.Content = content;
            win.ShowDialog();

            return content.MessageBoxResult;
        }

    }

    public enum NS_MessageBoxIcon
    {
        None,
        Warning,
        Error,
        Question,
        Information
    }

    public enum NS_MessageBoxButtons
    {
        OK,
        YesNoCancel,
        YesNo,
        OK_Cancel
    }

    public enum NS_MessageBoxResult
    {
        OK,
        Yes,
        No,
        Cancel
    }
}
