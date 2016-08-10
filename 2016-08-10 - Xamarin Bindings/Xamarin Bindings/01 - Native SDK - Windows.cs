using Microsoft.Band;

public class WindowsApiTest
{
   public static async Task ConnectToBand()
   {
      IBandInfo[] pairedBands = await BandClientManager.Instance.GetBandsAsync();
      try {
          // this method can be invoked from any thread
          IBandClient bandClient = await BandClientManager.Instance.ConnectAsync(pairedBands[0]);
          // do work after successful connect
          
          // TODO: work with the band
      } catch (BandException ex) {
         // handle a Band connection exception
      }
   }
   
   // this method can be invoked from any thread
   public static async Task GetFirmwareVersion(IBandClient bandClient)
   {
      string fwVersion = await bandClient.GetFirmwareVersionAsync(); 
   }
} 