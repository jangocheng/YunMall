using log4net.Layout;

namespace DF.Log
{
    internal class MongoAppenderFileld
	{
		public string Name { get; set; }
		public IRawLayout Layout { get; set; }
	}
}