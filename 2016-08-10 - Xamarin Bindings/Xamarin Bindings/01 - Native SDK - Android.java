package com.microsoft.band.android;

import com.microsoft.band.BandClient; 
import com.microsoft.band.BandClientManager; 
import com.microsoft.band.BandException; 
import com.microsoft.band.BandInfo; 
import com.microsoft.band.BandIOException; 
import com.microsoft.band.ConnectionState; 

public class AndroidApiTest 
{	
	public static void connectToBand()
	{
		BandInfo[] pairedBands = BandClientManager.getInstance().getPairedBands(); 
		BandClient bandClient = BandClientManager.getInstance().create(getActivity(), pairedBands[0]); 
		
		BandPendingResult<ConnectionState> pendingResult = bandClient.connect();
		try {
			// this method must be invoked from a background thread
			ConnectionState state = pendingResult.await();
			if (state == ConnectionState.CONNECTED) {
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
	public static void getFirmwareVersion(BandClient bandClient)
	{
		BandPendingResult<String> pendingResult = bandClient.getFirmwareVersion();
		String fwVersion = pendingResult.await();
	}
}
