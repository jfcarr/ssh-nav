using System.Net.NetworkInformation;

namespace SSH.Navigator.Utility;

public static class NetworkUtil
{
    public static bool Ping(string hostName)
    {
        try
        {
            Ping myPing = new();
            PingReply reply = myPing.Send(hostName, 500);

            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }
}
