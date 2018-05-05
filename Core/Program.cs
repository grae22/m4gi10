using System;
using System.Threading;
using System.Windows.Forms;
using m4gi10.Ui;

namespace m4gi10.Core
{
  public static class Program
  {
    //---------------------------------------------------------------------------------------------

    [STAThread]
    public static void Main()
    {
      AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;
      Application.ThreadException += ThreadExceptionHandler;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new MainForm());
    }

    //---------------------------------------------------------------------------------------------

    private static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
    {
      MessageBox.Show(
        e.ToString(),
        "Unhandled Exception",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error);
    }

    //---------------------------------------------------------------------------------------------

    private static void ThreadExceptionHandler(object sender, ThreadExceptionEventArgs e)
    {
      MessageBox.Show(
        e.ToString(),
        "Thread Exception",
        MessageBoxButtons.OK,
        MessageBoxIcon.Error);
    }

    //---------------------------------------------------------------------------------------------
  }
}
