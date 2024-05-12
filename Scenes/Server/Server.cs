using System;
using Godot;

public partial class Server : Node
{
    ENetMultiplayerPeer peer;
    int port;

    public enum ActionMode
    {
        Stand,
        More
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        port = 9999;
        peer = new ENetMultiplayerPeer();

        peer.CreateServer(port);
        peer.Host.Compress(ENetConnection.CompressionMode.Fastlz);
        Multiplayer.MultiplayerPeer = peer;

        GD.Print("Server started");

        peer.PeerConnected += OnPeerConnected;
        peer.PeerDisconnected += OnPeerDisconnected;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    private void OnPeerConnected(long peerId)
    {
        GD.Print($"Peer {peerId} connected");
    }

    private void OnPeerDisconnected(long peerId)
    {
        GD.Print($"Peer {peerId} disconnected");
    }

    [Rpc(MultiplayerApi.RpcMode.AnyPeer)]
    private void CheckData(ActionMode action)
    {
        GD.Print($"Receiving data: action: {action}");
    }
}
