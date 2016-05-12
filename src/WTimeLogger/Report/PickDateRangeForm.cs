using System;
using System.Windows.Forms;

namespace WTimeLogger.Report
{
	public partial class PickDateRangeForm : Form
	{
		public PickDateRangeForm()
		{
			InitializeComponent();
		}

		public DateTime From => dateTimePickerFrom.Value.Date;
		public DateTime To => dateTimePickerTo.Value.Date;
	}
}