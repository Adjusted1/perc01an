# perc01an

perc01an
-> Much thanks to author of android gps service I apply in this app https://github.com/shernandezp/XamarinForms.LocationService <-

Android* [for now, ios later] based rescue recovery tool for densely populated areas. After a natural or other disaster it allows p2p propogation of last known location of cell phone using bluetooth.

It employs a cellular automata tuned, p2p protocol piggybacking on Bluetooth for the purpose of gps info propagation in the wake of failed wifi/cell. Cellular automata will dynamically tune best route decisions to avoid bottlenecks.

main app cycle-

runs as background service:

gets gps location every n seconds.
can share gps as bluetooth ssid (see below) with other nearby perc01an clients (p2p component).
cellular automata will dynamically tune 'lan flooding' aggressiveness and provide 'smart' routing based upon what it learns over time.
__I'm proposing a novel technique that hopes to allow p2p pseudo-broadcasting by having each node in the perc01an perform these steps at runtime:

compute a cellular automation for number of neighbors with perc01an ssid's
during this compute find automata rules that, if curr node were a whole area p2p lan, would allow fastest propogation across the p2p network.
train an ai model to reduce cost of needing to constantly have each node permute cellular automata rules when patterns can be learned. Essentially a holographic approach to routing is engendered.
consumes earthquake alert events if distance < x.
trains nn model to recognize dangerous sensor events (unsafe accelerations, etc).
starts p2p progogation when one or more of these conditions occur:

strong, nearby earthquake.
strong collision.
excessive temperature.
earthquake alert recvd.
your cat runs out of wet food.

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
