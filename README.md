# perc01an - bluetooth low energy / long range (100m) disaster recovery peer to peer app, for populated areas.


Thanks to author of android gps service I apply in this app https://github.com/shernandezp/XamarinForms.LocationService

Android* [for now, ios later] based rescue recovery tool for densely populated areas. After a natural or other disaster it allows p2p propogation of last known location of cell phone using bluetooth.

It employs a cellular automata tuned, p2p protocol piggybacking on Bluetooth for the purpose of gps info propagation in the wake of failed wifi/cell. Cellular automata will dynamically be used as optimization model to tune least congestion, highest node traversal automatae rules.

main app cycle-

runs as background service:

gets gps location every n seconds.
can share gps as bluetooth ssid "friendly name" with peer perc01an nodes.
cellular automata will dynamically tune 'lan flooding' aggressiveness and provide 'smart' routing based upon what it learns over time.

1) get # peers advertising their friendly bluetooth name
2) compute optimal (conway?) rules to insure propogation of bluetooth named location data (eg name = "gps-coords") with minimal congestion
3) grab/hold peer bluetooth names per results of 2)

starts p2p progogation when one or more of these conditions occur:

* strong, nearby earthquake (ML - I'm looking for the 'best' pretrained model!)
* strong collision (sensor inference)
* excessive temperature (sensor)
* earthquake alert recvd (external app consumer possibly, or - ?)
* your cat runs out of wet food (meow ^..^)

--- internal notes
Gatt Sequence for bare bones "scan and add to device list" tests:
---
[BluetoothLeScanner] Start Scan with callback
[BluetoothLeScanner] onScannerRegistered() - status=0 scannerId=7 mScannerId=0
[BluetoothAdapter] STATE_ON
[BluetoothAdapter] STATE_ON
[BluetoothLeScanner] Stop Scan with callback
[BluetoothAdapter] STATE_ON
[BluetoothAdapter] STATE_ON
[BluetoothAdapter] STATE_ON
[BluetoothAdapter] STATE_ON
[BluetoothLeScanner] Start Scan with callback
[BluetoothLeScanner] onScannerRegistered() - status=0 scannerId=7 mScannerId=0
Thread finished: <Thread Pool> #2
Thread started: <Thread Pool> #10
The thread 0x2 has exited with code 0 (0x0).
[BluetoothAdapter] STATE_ON
[BluetoothAdapter] STATE_ON
[BluetoothLeScanner] Stop Scan with callback
