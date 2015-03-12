//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.18444
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using AssemblyCSharp.Utils;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AssemblyCSharp
{


	public class TcpParser:BaseParser{
		private Queue<StoredAction> actions;
		private Models models;
		public static ManualResetEvent tcpClientConnected;
		TcpListener server=null;
		public TcpParser(Models models){
			this.models = models;
			this.actions = new Queue<StoredAction>();
			tcpClientConnected = new ManualResetEvent(false);
			server = new TcpListener(IPAddress.Parse("127.0.0.1"),8080);
			server.Start();
			// Set the event to nonsignaled state.
			tcpClientConnected.Reset();
			Debug.Log("Waiting for a connection...");
			server.BeginAcceptTcpClient(
				new AsyncCallback(DoAcceptTcpClientCallback), server);
			// Wait until a connection is made and processed before  
			// continuing.
			//tcpClientConnected.WaitOne();
		}
		public override bool update(){
			for (var i=0; i < this.actions.Count; i++){
				StoredAction action = this.actions.Dequeue();
				if(action.duration > 0){
					//If action isn't finished, re-enqueue it.
					action.duration -= Time.deltaTime;
					this.actions.Enqueue(action);
				}
				return parseActionDeffered(action);
			}
			return false;
		}	
		/* WARNING
		 * No verification of args in execution.
		 */
		private bool parseActionDeffered(StoredAction action){

			Model model = this.models.getCurrent();

			if(action.method == "rotation"){
				string[] argv = action.args.Split(',');
				if(argv.Length == 3){
					model.setRotation(new Vector3(float.Parse(argv[0]),float.Parse(argv[1]),float.Parse(argv[2])));
					return true;
				}
			}else if(action.method =="hide"){
				model.hide (action.args);
				return true;
			}else if(action.method == "meshes"){
				NetworkStream stream = action.client.Client.GetStream();
				byte[] mes =System.Text.Encoding.ASCII.GetBytes(string.Join(",",this.models.getCurrent().getMeshes()));
				stream.Write(mes, 0, mes.Length);
				stream.Flush();
				return true;
			}
			return false;

		}
		/**
		 * Works out of the normal unity thread. return true if action have been treated immediately"
		 */
		private bool parseActionImmediate(string message, ClientData cli){

			//Model model = this.models.getCurrent();


			return false;
			
		}
		// Process the client connection. 
		public void DoAcceptTcpClientCallback(IAsyncResult ar) {
			string completeMessage = "";
			ClientData cli = new ClientData();
			// Get the listener that handles the client request.
			TcpListener listener = (TcpListener) ar.AsyncState;
			// End the operation and display the received data on  
			// the console.
			cli.Client = listener.EndAcceptTcpClient(ar);
			NetworkStream stream = cli.Client.GetStream();
			if(stream.CanRead){
				stream.BeginRead(cli.data, 0, cli.data.Length, 
				                 new AsyncCallback(ReadCallBack), 
				                          cli);  
			}
			// Signal the calling thread to continue.
			tcpClientConnected.Set();
			listener.BeginAcceptTcpClient(
				new AsyncCallback(DoAcceptTcpClientCallback), listener);
		}

		public void ReadCallBack(IAsyncResult ar ){
			ClientData cli = (ClientData)ar.AsyncState;
			NetworkStream stream = cli.Client.GetStream();
			int bytesCount = stream.EndRead(ar);
			cli.message = String.Concat(cli.message, Encoding.ASCII.GetString(cli.data, 0, bytesCount));    
			
			// message received may be larger than buffer size so loop through until you have it all.
			if(stream.DataAvailable){
				stream.BeginRead(cli.data, 0, cli.data.Length, 
				                          new AsyncCallback(ReadCallBack), 
				                          cli);  

			}else{
				string[] messages = cli.message.Split(';');
				foreach( string message in messages){
					string[] parse = message.Split(' ');
					if(parse.Length == 3){
						this.actions.Enqueue(new StoredAction(parse[0],parse[1],parse[2],cli));
					}else if (parse.Length == 2){
						this.actions.Enqueue(new StoredAction(parse[0],parse[1],cli));
					}else if (parse.Length == 1){
						this.actions.Enqueue(new StoredAction(parse[0],cli));
					}
				}
			}
			// Print out the received message to the console.

		}
	}

}

