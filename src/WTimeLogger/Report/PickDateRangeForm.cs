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

		private void buttonLeft_Click( object sender, EventArgs e )
		{
			dateTimePickerFrom.Value = dateTimePickerFrom.Value.AddDays(-1);
			dateTimePickerTo.Value = dateTimePickerTo.Value.AddDays(-1);
		}

		private void buttonRight_Click( object sender, EventArgs e )
		{
			dateTimePickerFrom.Value = dateTimePickerFrom.Value.AddDays(1);
			dateTimePickerTo.Value = dateTimePickerTo.Value.AddDays(1);
		}
	}
}