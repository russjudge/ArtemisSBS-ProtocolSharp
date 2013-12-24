ArtemisSBS-ProtocolSharp
========================

Libraries and applications for utilizing the TCP/IP protocol used by Artemis SBS, written in C#.

Artemis SBS (Artemis Spaceship Bridge Simulator) can be found at http://www.artemis.eochu.com.

The library is based on the reverse-engineered documentation found at https://github.com/rjwut/ArtClientLib/wiki/Artemis-Packet-Protocol.  It is written both to make use of existing functionality provided by the protocol, and to provide a means for extending that functionality.

The following assumptions are made for this project:

1. The protocol is designed for use exclusively by Artemis SBS.  Use outside of Artemis SBS was never intended, but is the intent of this project.
2. The base protocol is out of the control of this project.  The protocol is defined by the developer of the Artemis SBS game, so no modifications can be made to the base protocol.  Since it is uncertain how Artemis SBS will behave with any variance to the protocol, it is assumed that any variance could cause unpredictable results, up to and including crashing the game, and possibly causing buffer overruns or underruns, and as such, the protocol is not to be modified.
3. Since the protocol itself is reverse-engineered, it is expected that some of the data transmitted from Artemis SBS might not be data that matches the known documentation, and could in some cases actually contradict the documented protocol.  

Just because this project is written in C# does not mean it will work only on Windows.  The intention is to have the bulk of the code in this project compile in Mono (http://mono-project.com), so that the resulting application is truly cross-platform.

For this reason, the following rules are set:

1. The .NET 4.0 is to be targetted.
2. Only the UI itself may make use of functionality not available in Mono, such as the WPF Framework.  All other code should compile in Mono, as well as within Microsoft's .NET framework.  See the Mono Project for notes on incompatibilities.
3. Any UI projects must have a minimum of code--the bulk of the logic process is to be in the libraries that are cross-platform.
4. Code that processes the ArtemisSBS protocol will need to be written to handle cases where ArtemsSBS transmits data that is contradictory to the reverse-engineered documentation for the protocol, whether it simply ignores the data, logs it, or some other action--it is not to crash the application that uses this library.
5. Extensions to the protocol for the purpose of providing functionality not available in Artemis SBS are never to be transmitted to Artemis SBS to avoid the unpredictable results, other than for further reverse-engineering of the protocol itself.


At this time we are not taking on new contributors.  However, if you are interested, please feel free to leave a note with a link to a sample of some code you have developed.  If we are in need of some extra hands, we may contact you.

Also, the disclaimer for ArtClientLib, which is a java-based API for working with the Artemis SBS protocol, is here: https://github.com/rjwut/ArtClientLib/wiki/Disclaimer.

So, for the disclaimer for this project, all I can say is, "Ditto."
