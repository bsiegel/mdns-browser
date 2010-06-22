using System;
using Gtk;

namespace mDNSBrowser
{
	public class ClientNode : TreeNode
	{
		string desc;
		string host;

		
		public ClientNode (string desc, string host)
		{
			this.desc = desc;
			this.host = host;
		}

		[TreeNodeValue(Column=0)]
		public string Description {
			
			get { return desc; }
		
		}

		[TreeNodeValue(Column=1)]
		public string Host {
			get { return host; }
		}
	}
}

