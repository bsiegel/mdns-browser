using System;
using Gtk;
using Mono.Zeroconf;
using mDNSBrowser;

public partial class MainWindow : Gtk.Window
{
	private ServiceBrowser serviceBrowser;
	private ServiceBrowser clientBrowser;
	private NodeStore serviceNodes;
	private NodeStore clientNodes;

	public MainWindow () : base(Gtk.WindowType.Toplevel)
	{
		Build ();
		PopulateServices ();
		
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	private void PopulateServices ()
	{
		this.serviceNodes = new NodeStore (typeof(ServiceNode));
		this.clientNodes = new NodeStore (typeof(ClientNode));
		this.nvServices.NodeStore = this.serviceNodes;
		this.nvServices.AppendColumn ("Service", new Gtk.CellRendererText (), "text", 0);
		this.nvServices.NodeSelection.Changed += new EventHandler(HandleNodeSelectionChanged);
		this.nvClients.NodeStore = this.clientNodes;
		this.nvClients.AppendColumn ("Description", new CellRendererText(), "text", 0);
		this.nvClients.AppendColumn ("Host", new CellRendererText (), "text", 1);
		try {
			serviceBrowser = new ServiceBrowser ();
		} catch (Exception e) {
			Console.WriteLine (e.ToString ());
		}
		serviceBrowser.ServiceAdded += new ServiceBrowseEventHandler (HandleDiscoverServiceTypes);
		serviceBrowser.Browse ("_services._dns-sd._udp", "local");
	}
	
	private void PopulateClients(ServiceNode serviceNode)
	{
		if (clientBrowser != null)
		{
			clientBrowser.ServiceAdded -= new ServiceBrowseEventHandler (HandleServiceAdded);
			clientBrowser.ServiceRemoved -= new ServiceBrowseEventHandler (HandleServiceRemoved);
			clientBrowser.Dispose();
			clientBrowser = null;
		}
		
		try {
			clientBrowser = new ServiceBrowser ();
		} catch (Exception e) {
			Console.WriteLine (e.ToString ());
			return;
		}
		
		clientNodes.Clear();
		
		clientBrowser.ServiceAdded += new ServiceBrowseEventHandler (HandleServiceAdded);
		clientBrowser.ServiceRemoved += new ServiceBrowseEventHandler (HandleServiceRemoved);

		clientBrowser.Browse (serviceNode.Type, "local");
	}

	void HandleNodeSelectionChanged (object sender, EventArgs e)
	{
		NodeSelection selection = (NodeSelection) sender;
		ServiceNode serviceNode =((ServiceNode)selection.SelectedNode);
		PopulateClients(serviceNode);
	}

	void HandleDiscoverServiceTypes (object o, ServiceBrowseEventArgs args)
	{
		IResolvableService svc = args.Service;
		string svcType = svc.Name + "." + svc.RegType.Substring (0, svc.RegType.Length - 7);
		string svcName = ServiceNode.GetNameFromType(svcType);
		int insertPos = 0;
		foreach (ServiceNode node in serviceNodes)
		{
			if (node.Type == svcType)
				return;	
		}
		foreach (ServiceNode node in serviceNodes)
		{
			if (node.Name.CompareTo(svcName) > 0)
				break;
			insertPos++;
		}
						
		serviceNodes.AddNode (new ServiceNode (svcType, svcName), insertPos);
		this.nvServices.QueueDraw ();
	}

	void HandleServiceAdded (object o, ServiceBrowseEventArgs args)
	{
		IResolvableService svc = args.Service;
		svc.Resolved += new ServiceResolvedEventHandler(HandleServiceAddResolved);
		svc.Resolve();
	}

	void HandleServiceAddResolved (object o, ServiceResolvedEventArgs args)
	{
		IResolvableService svc = o as IResolvableService;
		string desc = svc.Name;
		string host = svc.HostEntry.AddressList[0].ToString();
		foreach (ClientNode node in nvClients.NodeStore)
		{
			if (node.Description == desc && node.Host == host)
				return;
		}
		clientNodes.AddNode(new ClientNode(desc, host));
		this.nvClients.QueueDraw ();
	}
	
	void HandleServiceRemoveResolved (object o, ServiceResolvedEventArgs args)
	{
		IResolvableService svc = o as IResolvableService;
		
		string desc = svc.Name;
		string host = svc.HostEntry.AddressList[0].ToString ();
		foreach (ClientNode node in nvClients.NodeStore) {
			if (node.Description == desc && node.Host == host) {
				clientNodes.RemoveNode(node);
				this.nvClients.QueueDraw ();
				return;
			}
		}
	}

	void HandleServiceRemoved (object o, ServiceBrowseEventArgs args)
	{
		IResolvableService svc = args.Service;
		svc.Resolved += new ServiceResolvedEventHandler (HandleServiceRemoveResolved);
		svc.Resolve ();
	}
}

