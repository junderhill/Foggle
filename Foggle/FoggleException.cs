using System;

namespace Foggle
{
	internal class FoggleException : Exception
	{
		public FoggleException(string message):base(message)
		{
		}
	}
}