

*** questions about privacy *** email me: sweetlawrence205@gmail.com

Legal Disclaimer: This App advertises your GPS location within the range and limitations of low energy bluetooth advertising. It shows your "bluetooth name" publicly as "bluetooth name = Your GPS coordinates". No Web/Internet/Wide Area network activity is involved (using Internet protocols and physical media), but your GPS coordinates can propogate without any personally identifier, for any arbitrary distance, provided a mesh of perc01an client are contiguously with bluetooth range of each other. Repeat - no personal data is sent to any Internet sites, this app uses bluetooth to send only your gps coordinates to other perc01an cell phones within bluetooth range - there is no identitfier preserved as part of the peer to peer traffic that identifies you, with the exception that the first advertisement has bluetooth hardware address information that would be available to nearby bluetooth consumers as part of the standard structure of bluetooth communications.



# perc01an - bluetooth low energy / long range (100m) disaster recovery peer to peer app, for populated areas.


Android [for now, ios later] based *rescue recovery tool for densely populated areas*. After a natural or other disaster it allows p2p propogation of last known location of cell phone using bluetooth name advertisements as the messenger.

After a natural disaster/emergency, Nearby Android Cell Phones running this App will catch and hold all GPS coordinates from other perc01an enabled cell phones, and propogate a randomly selected GPS for a fixed time interval. GPS coordinates of cell phones next to people buried in rubble (for instance) will be propogated across nearby perc01an cell phone users, allowing rescue of persons next to their phones by GPS coordinates via nearby perc01an clients who can use their harvested, local gps coordinates as locater information.

Offline GPS mapping will start mapping neighbors once peer GPS coordinates are harvested from Bluetooth advertisements from perc01an peers.

Congestion Mediation:
p2p networking can be subject to congestion if all nodes are able to broadcast - perc01an wil have a p2p model based upon dynamic per node computation whereby each node finds the conway/life automata rules that most promote contiguous interaction of a simulated 2-d map made of up perc01an peers. In this way, changing neighbor states (present/notpresent) can be part of an optimisation computation that selects the neighbor rules that allow each node to participate in an automata where the "population" (peer gps ingestors and transmitters) neither goes extinct nor overpopulates. Each node will simulate an arbitrary x by y dimensioned grid and iterate behavior of conway population that has "similar" (average/mean/or-? undetermined currently) charecteristics (1 to n homomorphism), n being the simulated population. This simulation will computer allow selection of best fit conway rules.      
