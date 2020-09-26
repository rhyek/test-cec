using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using CecSharp;

namespace test_cec
{
  class CecClient : CecCallbackMethods
  {

    private int LogLevel;
    private LibCecSharp Lib;
    private LibCECConfiguration Config;

    public CecClient()
    {
      Config = new LibCECConfiguration();
      Config.DeviceTypes.Types[0] = CecDeviceType.RecordingDevice;
      Config.DeviceName = "CEC Tester";
      Config.ClientVersion = LibCECConfiguration.CurrentVersion;
      LogLevel = (int)CecLogLevel.All;

      Lib = new LibCecSharp(this, Config);
      Lib.InitVideoStandalone();

      Console.WriteLine("CEC Parser created - libCEC version " + Lib.VersionToString(Config.ServerVersion));
    }

    public void Scan()
    {
      StringBuilder output = new StringBuilder();
      output.AppendLine("CEC bus information");
      output.AppendLine("===================");
      CecLogicalAddresses addresses = Lib.GetActiveDevices();
      for (int iPtr = 0; iPtr < addresses.Addresses.Length; iPtr++)
      {
        CecLogicalAddress address = (CecLogicalAddress)iPtr;
        if (!addresses.IsSet(address))
          continue;

        CecVendorId iVendorId = Lib.GetDeviceVendorId(address);
        bool bActive = Lib.IsActiveDevice(address);
        ushort iPhysicalAddress = Lib.GetDevicePhysicalAddress(address);
        string strAddr = Lib.PhysicalAddressToString(iPhysicalAddress);
        CecVersion iCecVersion = Lib.GetDeviceCecVersion(address);
        CecPowerStatus power = Lib.GetDevicePowerStatus(address);
        string osdName = Lib.GetDeviceOSDName(address);
        string lang = Lib.GetDeviceMenuLanguage(address);

        output.AppendLine("device #" + iPtr + ": " + Lib.ToString(address));
        output.AppendLine("address:       " + strAddr);
        output.AppendLine("active source: " + (bActive ? "yes" : "no"));
        output.AppendLine("vendor:        " + Lib.ToString(iVendorId));
        output.AppendLine("osd string:    " + osdName);
        output.AppendLine("CEC version:   " + Lib.ToString(iCecVersion));
        output.AppendLine("power status:  " + Lib.ToString(power));
        if (!string.IsNullOrEmpty(lang))
          output.AppendLine("language:      " + lang);
        output.AppendLine("");
      }
      Debug.WriteLine(output.ToString());
    }
  }
}