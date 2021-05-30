using FaceRecognition.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace upload_files_winform
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += Application_ThreadException;

                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                string text3 = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
                LogHelper.Info(msg: (ex == null) ? $"应用程序线程错误:{ex}" : string.Format(text3 + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n", ex.GetType().Name, ex.Message, ex.StackTrace), t: typeof(Program));
                MessageBox.Show("系统出错了", "系统错误:", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string text2 = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            Exception exception = e.Exception;
            LogHelper.Info(
                msg: (exception == null) ? $"应用程序线程错误:{e}" :
                string.Format(text2 + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n",
                exception.GetType().Name, exception.Message, exception.StackTrace),
                t: typeof(Program));

            MessageBox.Show("系统出错了", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            string text2 = "出现应用程序未处理的异常：" + DateTime.Now.ToString() + "\r\n";
            LogHelper.Info(msg: (ex == null) ? $"Application UnhandledError:{e}" : string.Format(text2 + "Application UnhandledException:{0};\n\r堆栈信息:{1}", ex.Message, ex.StackTrace), t: typeof(Program));
            MessageBox.Show("系统出错了", "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
    }
}
