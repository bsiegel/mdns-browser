using System;
using Gtk;
using System.Collections.Generic;

namespace mDNSBrowser
{
	
	public class ServiceNode : TreeNode
	{
		private static Dictionary<string, string> serviceNames = new Dictionary<string, string> {
			{ "_afpovertcp._tcp", "AppleTalk Filing Protocol (AFP)" },
			{ "_nfs._tcp", "Network File System (NFS)" },
			{ "_webdav._tcp", "WebDAV File System (WEBDAV)" },
			{ "_ftp._tcp", "File Transfer Protocol (FTP)" },
			{ "_ssh._tcp", "Secure Shell (SSH)" },
			{ "_eppc._tcp", "Remote AppleEvents" },
			{ "_http._tcp", "Web Server (HTTP)" },
			{ "_telnet._tcp", "Remote Login (Telnet)" },
			{ "_printer._tcp", "Line Printer Daemon (LPD/LPR)" },
			{ "_ipp._tcp", "Internet Printing Protocol (IPP)" },
			{ "_pdl-datastream._tcp", "PDL Data Stream (Port 9100)" },
			{ "_riousbprint._tcp", "Remote I/O USB Printer Protocol" },
			{ "_daap._tcp", "Digital Audio Access Protocol (DAAP)" },
			{ "_dpap._tcp", "Digital Photo Access Protocol (DPAP)" },
			{ "_ichat._tcp", "iChat Instant Messaging" },
			{ "_presence._tcp", "iChat Instant Messaging Presence" },
			{ "_ica-networking._tcp", "Image Capture Sharing" },
			{ "_airport._tcp", "Apple AirPort Base Station" },
			{ "_xserveraid._tcp", "Apple Xserve RAID" },
			{ "_distcc._tcp", "Distributed Compiler" },
			{ "_apple-sasl._tcp", "Apple Password Server" },
			{ "_workstation._tcp", "Workstation" },
			{ "_servermgr._tcp", "Apple Server Administration" },
			{ "_raop._tcp", "Remote Audio Output Protocol (RAOP)" },
			{ "_iTouch._tcp", "Logitech Touch Mouse Service" },
			{ "_pulse-server._tcp", "PulseAudio Sound Server" },
			{ "_pulse-sink._tcp", "PulseAudio Sound Sink" },
			{ "_pulse-source._tcp", "PulseAudio Sound Source" },
			{ "_rfb._tcp", "VNC Remote Access" },
			{ "_sftp-ssh._tcp", "Secure File Transfer over SSH (SFTP)" },
			{ "_smb._tcp", "Windows File Sharing (SMB)" },
			{ "_https._tcp", "Web Server (HTTPS)" },
			{ "_rss._tcp", "RSS Web Syndication" },
			{ "_domain._udp", "Domain Name System (DNS)" },
			{ "_ntp._udp", "Network Time Protocol (NTP)" },
			{ "_tftp._udp", "Trivial File Transfer Protocol (TFTP)" },
			{ "_webdavs._tcp", "Secure WebDAV File System (WEBDAVS)" },
			{ "_apt._tcp", "APT Package Repository" },
			{ "_timbuktu._tcp", "Timbuktu Remote Desktop" },
			{ "_net-assistant._udp", "Apple Net Assistant" },
			{ "_dacp._tcp", "Digital Audio Control Protocol (DACP)" },
			{ "_realplayfavs._tcp", "RealPlayer Shared Favorites" },
			{ "_rtsp._tcp", "RTSP Realtime Streaming Server" },
			{ "_rtp._udp", "RTP Realtime Streaming Server" },
			{ "_mpd._tcp", "Music Player Daemon" },
			{ "_touch-able._tcp", "iPod Touch Music Library" },
			{ "_vlc-http._tcp", "VLC Streaming" },
			{ "_sip._udp", "SIP Telephony" },
			{ "_h323._tcp", "H.323 Telephony" },
			{ "_presence_olpc._tcp", "OLPC Presence" },
			{ "_iax._udp", "Asterisk Exchange" },
			{ "_skype._tcp", "Skype VOIP" },
			{ "_see._tcp", "SubEthaEdit Collaborative Text Editor" },
			{ "_lobby._tcp", "Gobby Collaborative Editor Session" },
			{ "_postgresql._tcp", "PostgreSQL Server" },
			{ "_svn._tcp", "Subversion Revision Control" },
			{ "_adobe-vc._tcp", "Adobe Version Cue" },
			{ "_acrobatSRV._tcp", "Adobe Acrobat" },
			{ "_omni-bookmark._tcp", "OmniWeb Bookmark Sharing" },
			{ "_ksysguard._tcp", "KDE System Guard" },
			{ "_MacOSXDupSuppress._tcp", "Mac OS X Duplicate Machine Suppression" },
			{ "_pgpkey-hkp._tcp", "GnuPG/PGP HKP Key Server" },
			{ "_ldap._tcp", "LDAP Directory Server" },
			{ "_tp._tcp", "Thousand Parsec Server" },
			{ "_tps._tcp", "Thousand Parsec Server (Secure)" },
			{ "_tp-http._tcp", "Thousand Parsec Server (HTTP Tunnel)" },
			{ "_tp-https._tcp", "Thousand Parsec Server (Secure HTTP Tunnel)" }
		};
		
		
		string name;
		string type;

		public ServiceNode (string type, string name)
		{
			this.type = type;
			this.name = name;
		}

		[TreeNodeValue(Column = 0)]
		public string Name {

			
			get { return name; }
		
		}

		public string Type {
			get { return type;}
		}

		public static string GetNameFromType (string type)
		{
			if (serviceNames.ContainsKey(type))
				return serviceNames[type];
			
			return type;
		}
	}
}

