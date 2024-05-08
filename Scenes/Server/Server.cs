using System;
using Godot;

public partial class Server : Node
{
    ENetMultiplayerPeer network;
    int port;
    int maxClients;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        port = 1909;
        maxClients = 32;

        network = new ENetMultiplayerPeer();

        StartServer();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    private void StartServer()
    {
        GD.Print("Server started");
        network.CreateServer(port, maxClients);
        Multiplayer.MultiplayerPeer = network;

        network.PeerConnected += OnPeerConnected;
        network.PeerDisconnected += OnPeerDisconnected;
    }

    private void OnPeerConnected(long peerId)
    {
        GD.Print($"Peer {peerId} connected");
    }

    private void OnPeerDisconnected(long peerId)
    {
        GD.Print($"Peer {peerId} disconnected");
    }
}
