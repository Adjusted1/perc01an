Caution! Important remarks / API limitations
The BLE API implementation (especially on Android) has the following limitations:

Characteristic/Descriptor Write: make sure you call characteristic.WriteAsync(...) from the main thread, failing to do so will most probably result in a GattWriteError.
Sequential calls: Always wait for the previous BLE command to finish before invoking the next. The Android API needs it's calls to be serial, otherwise calls that do not wait for the previous ones will fail with some type of GattError. A more explicit example: if you call this in your view lifecycle (onAppearing etc) all these methods return void and 100% don't quarantee that any await bleCommand() called here will be truly awaited by other lifecycle methods.
Scan wit services filter: On specifically Android 4.3 the scan services filter does not work (due to the underlying android implementation). For android 4.3 you will have to use a workaround and scan without a filter and then manually filter by using the advertisement data (which contains the published service GUIDs).
Best practice
API
Surround Async API calls in try-catch blocks. Most BLE calls can/will throw an exception in certain cases, this is especially true for Android. We will try to update the xml doc to reflect this.
    try
    {
        await _adapter.ConnectToDeviceAsync(device);
    }
    catch(DeviceConnectionException ex)
    {
        //specific
    }
    catch(Exception ex)
    {
        //generic
    }
Avoid caching of Characteristic or Service instances between connection sessions. This includes saving a reference to them in you class between connection sessions etc. After a device has been disconnected all Service & Characteristic instances become invalid. Allways use GetServiceAsync and GetCharacteristicAsync to get a valid instance.
General BLE iOS, Android
Scanning: Avoid performing ble device operations like Connect, Read, Write etc while scanning for devices. Scanning is battery-intensive.
try to stop scanning before performing device operations (connect/read/write/etc)
try to stop scanning as soon as you find the desired device
never scan on a loop, and set a time limit on your scan
How to build the nuget package

// gatt status onClientConnectionState() - status=133 => device refused connection
