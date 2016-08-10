#if __ANDROID__
using NativeBandClientManager = Microsoft.Band.BandClientManager;
using NativeBandDeviceInfo = Microsoft.Band.IBandInfo;
#elif __IOS__
using NativeBandClientManager = Microsoft.Band.BandClientManager;
using NativeBandDeviceInfo = Microsoft.Band.BandClient;
#elif WINDOWS_PHONE_APP
using NativeBandClientManager = Microsoft.Band.BandClientManager;
using NativeBandDeviceInfo = Microsoft.Band.IBandInfo;
#endif

// BandClientManager

    public class BandClientManager
	{
        public async Task<IEnumerable<BandDeviceInfo>> GetPairedBandsAsync()
        {
#if __ANDROID__
            return NativeBandClientManager.Instance.GetPairedBands().Select(b => new BandDeviceInfo(b));
#elif __IOS__
            return NativeBandClientManager.Instance.AttachedClients.Select(b => new BandDeviceInfo(b));
#elif WINDOWS_PHONE_APP
            return (await NativeBandClientManager.Instance.GetBandsAsync()).Select(b => new BandDeviceInfo(b));
#else
            return null;
#endif
        }
		
        public async Task<BandClient> ConnectAsync(BandDeviceInfo info)
        {
#if __ANDROID__
            var nativeClient = NativeBandClientManager.Instance.Create(Application.Context, info.Native);
            var result = await nativeClient.ConnectTaskAsync() == ConnectionState.Connected;
            return new BandClient(nativeClient);
#elif __IOS__
            await NativeBandClientManager.Instance.ConnectTaskAsync(info.Native);
            return new BandClient(info.Native);
#elif WINDOWS_PHONE_APP
            var nativeClient = await NativeBandClientManager.Instance.ConnectAsync(info.Native);
            return new BandClient(nativeClient);
#else
            return null;
#endif
        }
	}
	
// BandDeviceInfo	
	
	public class BandDeviceInfo
    {
        private BandDeviceInfo() { }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal readonly NativeBandDeviceInfo Native;
        internal BandDeviceInfo(NativeBandDeviceInfo info)
        {
            this.Native = info;
        }
#endif

        public string Name
        {
            get
            {
#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
                return Native.Name; 
#else // PORTABLE
                return null;
#endif
            }
        }
	}
	
// BandClient	
	
    public class BandClient
    {
        private BandClient() { }

#if __ANDROID__ || __IOS__ || WINDOWS_PHONE_APP
        internal NativeBandClient Native;

        internal BandClient(NativeBandClient client)
        {
            this.Native = client;
        }
#endif

        public async Task<string> GetFirmwareVersionAsync()
        {
#if __ANDROID__
            return await Native.GetFirmwareVersionTaskAsync();
#elif __IOS__
            return (string) await Native.GetFirmwareVersionAsyncAsync();
#elif WINDOWS_PHONE_APP
            return await Native.GetFirmwareVersionAsync();
#else // PORTABLE
            return null;
#endif
        }
	}