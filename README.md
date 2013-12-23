ArtemisSBS-ProtocolSharp
========================

Libraries and applications for utilizing the TCP/IP protocol used by Artemis SBS, written in C#.

Artemis SBS (Artemis Spaceship Bridge Simulator) can be found at http://www.artemis.eochu.com.

The library is based on the reverse-engineered documentation found at https://github.com/rjwut/ArtClientLib/wiki/Artemis-Packet-Protocol.

The following assumptions are made for this project:

1. The protocol is designed for use exclusively by Artemis SBS.  Use outside of Artemis SBS was never intended, but is the intent of this project.
2. The base protocol is out of the control of this project.  The protocol is defined by the developer of the Artemis SBS game, so no modifications can be made to the base protocol.  Since it is uncertain how Artemis SBS will behave with any variance to the protocol, it is assumed that any variance could cause unpredictable results, up to and including crashing the game, and possibly causing buffer overruns or underruns, and as such, the protocol is not to be modified.
3. Extensions to the protocol for the purpose of providing functionality not available in Artemis SBS are never to be transmitted to Artemis SBS to avoid the unpredictable results noted in #2, other than for further reverse-engineering of the protocol itself.

Just because this project is written in C# does not mean it will work only on Windows.  The intention is to have the bulk of the code in this project compile in Mono (http://mono-project.com), so that the resulting application is truly cross-platform.
