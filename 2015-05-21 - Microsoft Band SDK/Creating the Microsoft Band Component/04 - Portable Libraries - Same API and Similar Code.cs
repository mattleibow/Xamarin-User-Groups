// Logger.dll

public interface ILoggingType
{
	void LodString(string text);
}

// Logger.Android.dll

public class AndroidType : ILoggingType
{
	public void LodString(string text)
	{
		Log.Debug("AndroidType", text);
	}
}

// Logger.Windows.dll

public class WindowsType : ILoggingType
{
	public void LodString(string text)
	{
		Log.Debug("WindowsType", text);
	}
}