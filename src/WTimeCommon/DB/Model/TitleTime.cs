using System;
using System.IO;
using LinqToDB.Mapping;

namespace WTimeCommon.DB.Model
{
	[Table(Name = "TitleTime")]
	public class TitleTime
	{
		[PrimaryKey, Identity]
		public long ID { get; set; }

		[Column(Name = "title"), NotNull]
		public string Title { get; set; }

		[Column(Name = "executable"), NotNull]
		public string Executable { get; set; }

		public string ExecutableName => new FileInfo(Executable).Name;

		[Column(Name = "startDateTime"), NotNull]
		public DateTime StartDateTime { get; set; }

		[Column(Name = "endDateTime"), NotNull]
		public DateTime EndDateTime { get; set; }

		public TimeSpan Duration => EndDateTime - StartDateTime;
	}
}