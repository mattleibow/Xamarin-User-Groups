using Microsoft.Band;

public class XamarinAndroidApiTest 
{	
	public static void ConnectToBand()
	{
		IBandInfo[] pairedBands = BandClientManager.Instance.GetPairedBands(); 
		IBandClient bandClient = BandClientManager.Instance.Create(Activity, pairedBands[0]); 
		
		IBandPendingResult pendingResult = bandClient.Connect();
		try {
			// this method must be invoked from a background thread
			ConnectionState state = (ConnectionState)pendingResult.Await();
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
	
    // this method must be invoked from a background thread
	public static void GetFirmwareVersionBackground(BandClient bandClient)
	{
		BandPendingResult pendingResult = bandClient.GetFirmwareVersion();
		string fwVersion = (string)pendingResult.Await();
	}
	
    // this method can be invoked from any thread
	public static void GetFirmwareVersionCallbacks(BandClient bandClient)
	{
		BandPendingResult pendingResult = bandClient.GetFirmwareVersion();
        pendingResult.RegisterResultCallback((result, error) => {
            if (error != null) {
                // handle any error
			} else {
				string fwVersion = (string)result;
			}
        });
	}
	
	// background example
	
	public static void ConnectToBandAsync()
	{
		IBandInfo[] pairedBands = BandClientManager.Instance.GetPairedBands(); 
		IBandClient bandClient = BandClientManager.Instance.Create(Activity, pairedBands[0]); 
			
		ConnectToBandAsyncTask task = new ConnectToBandAsyncTask();
		task.Execute(bandClient);
	}
	
	private class ConnectToBandAsyncTask : AsyncTask<IBandClient, object, ConnectionState>
	{
		protected override object DoInBackground(params IBandClient[] parameters)
		{
			IBandPendingResult pendingResult = parameters[0].Connect();
			try {
				// this method must be invoked from a background thread
				return (ConnectionState)pendingResult.Await();
			} catch (InterruptedException ex) { 
				// handle InterruptedException 
			} catch (BandException ex) {
				// handle BandException
			}
			
			return null;
		}
		
		protected override void OnPostExecute (ConnectionState result)
		{
			if (result == ConnectionState.Connected) {
				// do work on success
				
				// TODO: work with the band
			} else {   
				// do work on failure  
			}
		}
	}
}
