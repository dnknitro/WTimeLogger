using System.IO;
using System.Reflection;
using System.Resources;

namespace WTimeCommon
{
	public static class ResourceUtils
	{
		public static Stream GetEmbeddedResourceStream(string resourcePath, Assembly assembly = null)
		{
			if (assembly == null) assembly = Assembly.GetCallingAssembly();
			var resourceFileStream = assembly.GetManifestResourceStream(resourcePath);
			if (resourceFileStream == null)
			{
				var availableResources = assembly.GetManifestResourceNames();
				var ex =
					new MissingManifestResourceException(string.Format("Unable to find resource by path {0} in {1} assembly",
						resourcePath, assembly.FullName));
				ex.Data.Add("availableResources", string.Join("; ", availableResources));
				throw ex;
			}
			return resourceFileStream;
		}

		public static string ReadStringFromEmbeddedResource(string resourcePath, Assembly assembly = null)
		{
			if (assembly == null) assembly = Assembly.GetCallingAssembly();
			using (var stream = GetEmbeddedResourceStream(resourcePath, assembly))
			using (var reader = new StreamReader(stream))
				return reader.ReadToEnd();
		}

		public static byte[] ReadBytesFromEmbeddedResource(string resourcePath, Assembly assembly = null)
		{
			using (var stream = GetEmbeddedResourceStream(resourcePath, assembly))
			{
				var buffer = new byte[stream.Length];
				stream.Read(buffer, 0, buffer.Length);
				return buffer;
			}
		}

		//public static byte[] ReadBytesFromEmbeddedResource( string resourcePath )
		//{
		//    using( var stream = GetEmbeddedResourceStream( resourcePath, Assembly.GetCallingAssembly() ) )
		//        return StreamUtil.ReadStream2ByteArray( stream );
		//}
	}
}