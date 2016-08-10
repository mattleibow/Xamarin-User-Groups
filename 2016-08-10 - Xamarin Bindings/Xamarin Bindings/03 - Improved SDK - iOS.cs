using Microsoft.Band;

public class XamariniOSApiTest
{
   public static void ConnectToBand()
   {
      BandClient[] attachedClients = BandClientManager.SharedManager.AttachedClients;
	  
	  try {
         await BandClientManager.SharedManager.Connect(attachedClients[0]);
		 // do work on success
		
		 // TODO: work with the band
	  } catch (BandException ex) {
		 // handle BandException or connection failure
	  }
   }
   
   public static async Task GetFirmwareVersion(BandClient client)
   {
      string fwVersion = await client.GetFirmwareVersion(); 
   }

   // helpers

	public static Task Connect (this BandClientManager manager, BandClient client)
	{
		var tcs = new TaskCompletionSource<object> ();

		EventHandler<ClientManagerConnectedEventArgs> onConnected = null;
		EventHandler<ClientManagerDisconnectedEventArgs> onDisconnect = null;
		EventHandler<ClientManagerFailedToConnectEventArgs> onFailed = null;

		// setup the completed event
		onConnected = (sender, args) => {
			if (args.Client == client) {
				manager.Connected -= onConnected;
				manager.Disconnected -= onDisconnect;
				manager.ConnectionFailed -= onFailed;

				// we are finished
				tcs.SetResult (null);
			}
		};
		manager.Connected += onConnected;

		// setup the canceled event
		onDisconnect = (sender, args) => {
			if (args.Client == client) {
				manager.Connected -= onConnected;
				manager.Disconnected -= onDisconnect;
				manager.ConnectionFailed -= onFailed;

				// we were canceled
				tcs.SetCanceled();
			}
		};
		manager.Disconnected += onDisconnect;

		// setup the failed event
		onFailed = (sender, args) => {
			if (args.Client == client) {
				manager.Connected -= onConnected;
				manager.Disconnected -= onDisconnect;
				manager.ConnectionFailed -= onFailed;

				// we failed
				tcs.SetException (new BandException(args.Error));
			}
		};
		manager.ConnectionFailed += onFailed;

		// run async
		manager.ConnectClient (client);

		return tcs.Task;
	}
   
   public static Task GetFirmwareVersion(this BandClient client)
   {
      var tcs = new TaskCompletionSource<string> ();
      client.GetFirmwareVersion((version, error) => {
         if (error)
            tcs.SetException (new BandException (error));
         else
            tcs.SetResult (data);
      });
      return tcs.Task; 
   }
}
