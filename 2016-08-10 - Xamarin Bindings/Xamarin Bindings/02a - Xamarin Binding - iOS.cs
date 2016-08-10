namespace Microsoft.Band
{
	// @interface MSBClientManager : NSObject
	[BaseType (typeof(NSObject), Name = "MSBClientManager",
      Delegates = new string [] { "Delegate" }, Events = new Type [] { typeof(BandClientManagerDelegate) })]
	interface BandClientManager
	{
		// @property (nonatomic, weak) id<MSBClientManagerDelegate> delegate;
		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IBandClientManagerDelegate Delegate { get; set; }

		// @property (readonly) BOOL isPowerOn;
		[Export ("isPowerOn")]
		bool IsBluetoothPowerOn { get; }

		// +(MSBClientManager *)sharedManager;
		[Static]
		[Export ("sharedManager")]
		BandClientManager Instance { get; }

		// -(MSBClient *)clientWithConnectionIdentifier:(NSUUID *)identifer;
		[Export ("clientWithConnectionIdentifier:")]
		BandClient FindClient (NSUuid identifer);

		// -(NSArray *)attachedClients;
		[Export ("attachedClients")]
		BandClient[] AttachedClients { get; }

		// -(void)connectClient:(MSBClient *)client;
		[Export ("connectClient:")]
		void ConnectAsync (BandClient client);

		// -(void)cancelClientConnection:(MSBClient *)client;
		[Export ("cancelClientConnection:")]
		void DisconnectAsync (BandClient client);
	}

	// @protocol MSBClientManagerDelegate <NSObject>
	[Protocol, Model]
	[BaseType (typeof(NSObject), Name = "MSBClientManagerDelegate")]
	interface BandClientManagerDelegate
	{
		// @required -(void)clientManager:(MSBClientManager *)clientManager clientDidConnect:(MSBClient *)client;
		[Abstract]
		[Export ("clientManager:clientDidConnect:"), EventArgs ("ClientManagerConnected")]
		void Connected (BandClientManager clientManager, BandClient client);

		// @required -(void)clientManager:(MSBClientManager *)clientManager clientDidDisconnect:(MSBClient *)client;
		[Abstract]
		[Export ("clientManager:clientDidDisconnect:"), EventArgs ("ClientManagerDisconnected")]
		void Disconnected (BandClientManager clientManager, BandClient client);

		// @required -(void)clientManager:(MSBClientManager *)clientManager client:(MSBClient *)client didFailToConnectWithError:(NSError *)error;
		[Abstract]
		[Export ("clientManager:client:didFailToConnectWithError:"), EventArgs ("ClientManagerFailedToConnect")]
		void ConnectionFailed (BandClientManager clientManager, BandClient client, NSError error);
	}
   
}