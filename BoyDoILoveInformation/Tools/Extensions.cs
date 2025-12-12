using System;
using System.Collections.Generic;
using Photon.Pun;

namespace BoyDoILoveInformation.Tools;

public enum GamePlatform
{
    Steam,
    OculusPC,
    PC,
    Standalone,
    Unknown,
}

public static class Extensions
{
    public static Dictionary<VRRig, GamePlatform> PlayerPlatforms      = new();
    public static Dictionary<VRRig, List<string>> PlayerMods           = new();
    public static Dictionary<string, DateTime>    AccountCreationDates = new();
    public static List<VRRig>                     PlayersWithCosmetics = [];

    public static GamePlatform GetPlatform(this VRRig rig) =>
            PlayerPlatforms.GetValueOrDefault(rig, GamePlatform.Unknown);

    public static string ParsePlatform(this GamePlatform gamePlatform)
    {
        return gamePlatform switch
               {
                       GamePlatform.Unknown    => "<color=#000000>Unknown</color>",
                       GamePlatform.Steam      => "<color=#0091F7>Steam</color>",
                       GamePlatform.OculusPC   => "<color=#0091F7>Oculus PCVR</color>",
                       GamePlatform.PC         => "<color=#000000>PC</color>",
                       GamePlatform.Standalone => "<color=#26A6FF>Standalone</color>",
                       var _                   => throw new ArgumentOutOfRangeException(),
               };
    }

    public static DateTime GetAccountCreationDate(this VRRig rig) => AccountCreationDates[rig.OwningNetPlayer.UserId];

    public static string[] GetPlayerMods(this VRRig rig) => PlayerMods[rig].ToArray();

    public static bool HasCosmetics(this VRRig rig) => PlayersWithCosmetics.Contains(rig);

    public static int GetPing(this VRRig rig)
    {
        try
        {
            CircularBuffer<VRRig.VelocityTime> history = rig.velocityHistoryList;
            if (history != null && history.Count > 0)
            {
                double ping = Math.Abs((history[0].time - PhotonNetwork.Time) * 1000);

                return (int)Math.Clamp(Math.Round(ping), 0, int.MaxValue);
            }
        }
        catch
        {
            // ignored
        }

        return int.MaxValue;
    }
}