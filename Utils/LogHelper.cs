using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognition.Utils
{
	
    public class LogHelper
	{
		public static void Error(Type t, Exception ex)
		{
			ILog logger = LogManager.GetLogger(t);
			logger.Error("Error", ex);
			//Console.WriteLine("Error" + ex.ToString());
		}

		public static void Info(Type t, string msg)
		{
			ILog logger = LogManager.GetLogger(t);
			logger.Info(msg);
			//Console.WriteLine(msg);
		}

		public static void Debug(Type t, string msg)
		{
			ILog logger = LogManager.GetLogger(t);
			logger.Debug(msg);
			//Console.WriteLine(msg);
		}
	}
}
