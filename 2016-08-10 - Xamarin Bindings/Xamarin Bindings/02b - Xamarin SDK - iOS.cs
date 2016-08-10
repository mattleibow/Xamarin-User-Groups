using Microsoft.Band;

public class XamariniOSApiTest
{
   public static void ConnectToBand()
   {
      MSBClientManager.SharedManager.OnConnected += (cm, client) => {
         // handle connected event.
         
         // TODO: work with the band
      };
      MSBClientManager.SharedManager.Disconnected += (cm, client) => {
         // handle disconnected event.
      };
      MSBClientManager.SharedManager.ConnectionFailed += (cm, client, error) => {
         // handle failure event.
      };
      
      MSBClient[] attachedClients = MSBClientManager.SharedManager.AttachedClients;
   
      // this method can be called from the main thread
      MSBClientManager.SharedManager.ConnectClient(attachedClients[0]);
   }
   
   public static void GetFirmwareVersion(MSBClient client)
   {
      client.GetFirmwareVersion((version, error) => {
         if (error)
            // handle error 
         else
            // handle success
            string fwVersion = version;
      });  
   }
}
