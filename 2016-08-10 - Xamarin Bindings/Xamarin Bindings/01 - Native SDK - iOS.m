#import <MicrosoftBandKit_iOS/MicrosoftBandKit_iOS.h>

@implementation iOSApiTest

-(void)connectToBand
{
   [[MSBClientManager sharedManager] setDelegate:self];
   
   NSArray *attachedClients = [[MSBClientManager sharedManager] attachedClients];
   MSBClient *client = [attachedClients firstObject]; 

   // this method can be called from the main thread
   [[MSBClientManager sharedManager] connectClient:client];
}

// Note: The delegate methods of MSBClientManagerDelegate protocol are called in the main thread. 
-(void)clientManager:(MSBClientManager *)cm clientDidConnect:(MSBClient *)client
{
   // handle connected event.
   
   // TODO: work with the band
}
-(void)clientManager:(MSBClientManager *)cm clientDidDisconnect:(MSBClient *)client
{
   // handle disconnected event.
}
-(void)clientManager:(MSBClientManager *)cm client:(MSBClient *)client didFailToConnectWithError:(NSError *)error
{
   // handle failure event. 
}

-(void)getFirmwareVersion:(MSBClient *)client
{
   [client firmwareVersionWithCompletionHandler:^(NSString *version, NSError *error){
      if (error)
         // handle error 
      else
         // handle success
   }];  
}

@end
