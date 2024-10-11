using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RDBIM.Model
{
	public class IO
	{
		public static MessageBoxResult YesNo(string content, string title = "Question")
		{
			return MessageBox.Show(content, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
		}

		public static void Warning(string content, string title = "Warning")
		{
			MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Warning);
		}

		public static void Info(string content, string title = "Info")
		{
			MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Information);
		}

		public static void Exception(Exception ex, string title = "Exception")
		{
			string content = ex.Message + "\n" + ex.StackTrace.ToString();
			MessageBox.Show(content, title, MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
}
