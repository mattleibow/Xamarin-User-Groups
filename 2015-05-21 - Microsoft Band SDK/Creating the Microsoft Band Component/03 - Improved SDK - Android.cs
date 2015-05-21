using Microsoft.Band;

public class XamarinAndroidApiTest 
{	
	public static async Task ConnectToBand()
	{
		IBandInfo[] pairedBands = BandClientManager.Instance.GetPairedBands(); 
		IBandClient bandClient = BandClientManager.Instance.Create(Activity, pairedBands[0]); 
		
		try {
			// this method can be invoked from any thread
			ConnectionState state = (ConnectionState)await bandClient.Connect().AsTask();
			if (state == ConnectionState.Connected) {
				// do work on success
				
				// TODO: work with the band
			} else {   
				// do work on failure  
			}
		} catch (InterruptedException ex) { 
			// handle InterruptedException 
		} catch (BandException ex) {
			// handle BandException
		} 
	}
	
    // this method can be invoked from any thread
	public static async Task GetFirmwareVersion(BandClient bandClient)
	{
		string fwVersion = (string)await bandClient.GetFirmwareVersion().AsTask();
	}
	
	// helpers

    public static Task<Java.Lang.Object> AsTask(this IBandPendingResult result)
    {
        var t = new TaskCompletionSource<Java.Lang.Object>();
        result.RegisterResultCallback((r, f) =>
        {
            if (f != null)
                t.SetException(f);
            else
                t.SetResult(r);
        });
        return t.Task;
    }
	
	// extras
	
    public static async Task<string> GetFirmwareVersionTaskAsync(this IBandClient client)
    {
        return (string)await client.GetFirmwareVersionAsync().AsTask();
    }
}
