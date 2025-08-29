

public enum InventoryLocationOp
{
    None = 0,
    Putaway =1,
    Pickup = 2,
    Movement = 3,
}
public enum SiteType
{
    Canton,     //default
    Windsor,
    Memphis,
    Oakville,
    Louisville,
    Louisville3,
    Romulus,
    Cleaveland,
    CleavelandRepack,
    Taylor,
    Wayne,
    Strongsville,
    BattleCreek,

}
public static class SiteControl
{
    private static SiteType SiteType = SiteType.BattleCreek;

    public static void SetSite(SiteType type)
    {
        SiteType = type;
    }
    public static SiteType GetSite()
    {
       return SiteType;
    }

        public static string GetConnectString()
    {
        switch (SiteType)
        {
            case SiteType.Canton:
                return "server = 192.168.24.33\\SQLEXPRESS; database = KareIS_Canton; Persist Security Info = False; Encrypt = False; User ID = kareis; Password = Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                break;
            case SiteType.Romulus:
                return "server = 192.168.5.8\\SQLEXPRESS; database = KareIS_Romulus; Persist Security Info = False; Encrypt = False; User ID = kareis; Password = Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                break;
            case SiteType.Memphis:
                return "server = 172.16.2.5; database = KareIS_Memphis; Persist Security Info = False; Encrypt = False; User ID = kareis; Password = Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                break;
            case SiteType.Taylor:
                return "server = 172.16.2.5; database = KareIS_Taylor; Persist Security Info = False; Encrypt = False; User ID = kareis; Password = Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                break;
            case SiteType.Wayne:
                return "server = 172.16.2.5; database = KareIS_Wayne; Persist Security Info = False; Encrypt = False; User ID = kareis; Password = Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                break;
			case SiteType.Cleaveland:
				return "server = 172.16.2.5; database = KareIS_Cleaveland; Persist Security Info = False; Encrypt = False; User ID = kareis; Password = Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
				break;
			case SiteType.CleavelandRepack:
				return "server = 172.16.2.5; database = KareIS_Cleveland_Repacking; Persist Security Info = False; Encrypt = False; User ID = kareis; Password = Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
				break;
			case SiteType.Louisville3:
                //return "server = 192.168.28.28\\SQLEXPRESS; database = KareIS_LVL3_DEV; Persist Security Info = False; Encrypt = False; User ID = kareis; Password = Alpha15!;Connection Timeout=900;";
                return "server = 192.168.28.28\\SQLEXPRESS; database = KareIS_LVL3; Persist Security Info = False; Encrypt = False; User ID = kareis; Password = Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                break;
        }
        return "server = 192.168.24.33\\SQLEXPRESS; database = KareIS_Canton; Persist Security Info = False; Encrypt = False; User ID = kareis; Password = Alpha15!;Connection Timeout=900;TrustServerCertificate=True;"; 
    }

    public static int GetShiftStart(int mode)
    {
        if (SiteType == SiteType.BattleCreek)
            return 5;

        if (mode == 0)
            return 6;
        //night        
        if (SiteType == SiteType.Louisville3 || SiteType == SiteType.Canton)
            return 5;
        else
            return 6;
    }
    //Default Receiving Area
    public static int GetReceivingLocID()
    {
        int LocID = 0;
        switch (SiteType)
        {
            case SiteType.Canton:
               // LocID = 7;
                break;
            case SiteType.Romulus:
                //LocID = 7;
                break;
            case SiteType.Memphis:
                //LocID = 13;
                break;
            case SiteType.Taylor:
                //LocID = 13;
                break;
        }
        return LocID;
    }

    public static string GetSkidBatchHeader()
    {
        if (SiteType == SiteType.Memphis)
            return "Batch #";
        return "Skid #";
    }
    public static bool IsMemphisSite()
    {
        if(SiteType == SiteType.Memphis) return true;
        return false;
    }
    public static int GetJobID()
    {
        int job = 0;
        switch(SiteType)
        {
            case SiteType.Canton:
                job = 200;
                break;
           case SiteType.Louisville3:
                job = 209;
                break;
            case SiteType.Romulus:
                job = 148;
                break;
            case SiteType.Memphis:
                job = 146;
                break;
            case SiteType.Wayne: // Taylor2
                job = 167;
                break;
        }
        return job;
    }

    public static string GetJobNo()
    {
        string jobNumber = "";
        switch (SiteType)
        {
            case SiteType.Canton:
                jobNumber = "CAS000001";
                break;
           case SiteType.Louisville3:
                jobNumber = "L2S000001";
                break;
            case SiteType.Romulus:
                jobNumber = "ROS000002";
                break;
            case SiteType.Memphis:
                jobNumber = "MMS000001";
                break;
        }
        return jobNumber;
    }

    //for receiving
    public static int GetSupplierID()
    {
        int supplierID = 0;
        switch (SiteType)
        {
            case SiteType.Canton:
                supplierID = 91;
                break;
           case SiteType.Louisville3:
                supplierID = 121;
                break;
            case SiteType.Romulus:
                supplierID = 92;
                break;
            case SiteType.Memphis:
                supplierID = 91;
                break;
        }
        return supplierID;
    }

    public static int GetCustomerID()
    {
        int CustomerID = 0;
        switch (SiteType)
        {
            case SiteType.Canton:
                CustomerID = 91;
                break;
           case SiteType.Louisville3:
                CustomerID = 121;
                break;
            case SiteType.Romulus:
                CustomerID = 91;
                break;
            case SiteType.Memphis:
                CustomerID = 91;
                break;
        }
        return CustomerID;
    }

    public static string GetCustomerName()
    {
        string CustomerName = "";
        switch (SiteType)
        {
            case SiteType.Canton:
                CustomerName = "Ford Michigan Assembly Plant";
                break;
           case SiteType.Louisville3:
                CustomerName = "Ford Louisville Assembly Plant";
                break;
            case SiteType.Romulus:
                CustomerName = "Ford Michigan Assembly Plant";
                break;
            case SiteType.Memphis:
                CustomerName = "Miraclon";
                break;
        }
        return CustomerName;
    }

    public static int GetShippingLocationID()
    {
        int LocID = 0;
        switch (SiteType)
        {
            case SiteType.Canton:
                LocID = 304;
                break;
            case SiteType.Louisville3:
                LocID = 6114;
                break;
            case SiteType.Romulus:
                LocID = 304;
                break;
            case SiteType.Memphis:
                LocID = 13;         //Shipping/Receiving Dock
                break;
        }
        return LocID;
    }

    public static string GetShippingLocationTitle()
    {
        string LocTitle = "";
        switch (SiteType)
        {
            case SiteType.Canton:
                LocTitle = "OUTBOUND 1";
                break;
           case SiteType.Louisville3:
                LocTitle = "OUTBOUND01";
                break;
            case SiteType.Romulus:
                LocTitle = "OUTBOUND 1";
                break;
            case SiteType.Memphis:
                LocTitle = "Shipping/Receiving Dock";
                break;
        }
        return LocTitle;
    }

    public static string GetSiteAddr()
    {
        string SiteAddr = "";
        switch (SiteType)
        {
            case SiteType.Canton:
                SiteAddr = "4250-4280 Haggerty Rd";
                break;
            case SiteType.Romulus:
                SiteAddr = "37350 Ecorse Rd";
                break;
            case SiteType.Memphis:
                SiteAddr = "2828 Business Park Dr, Building H";
                break;
           case SiteType.Louisville:
           case SiteType.Louisville3:
                SiteAddr = "2000 Fern Valley Louisville";
                break;
        }
        return SiteAddr;
    }
    public static string GetSiteCity()
    {
        string SiteAddr = "";
        switch (SiteType)
        {
            case SiteType.Canton:
                SiteAddr = "Canton";
                break;
            case SiteType.Romulus:
                SiteAddr = "Romulus";
                break;
            case SiteType.Memphis:
                SiteAddr = "Memphis";
                break;
            case SiteType.Louisville:
            case SiteType.Louisville3:
                SiteAddr = "Louisville";
                break;
        }
        return SiteAddr;
    }

    public static string GetSiteState()
    {
        string SiteProvice = "";
        switch (SiteType)
        {
            case SiteType.Canton:
                SiteProvice = "Michigan";
                break;
            case SiteType.Romulus:
                SiteProvice = "Michigan";
                break;
            case SiteType.Memphis:
                SiteProvice = "Tennessee";
                break;
            case SiteType.Louisville:
            case SiteType.Louisville3:
                SiteProvice = "Kentucky";
                break;
        }
        return SiteProvice;
    }
    public static string GetSitePostCode()
    {
        string sitePostcode = "";
        switch (SiteType)
        {
            case SiteType.Canton:
                sitePostcode = "Michigan";
                break;
            case SiteType.Romulus:
                sitePostcode = "Michigan";
                break;
            case SiteType.Memphis:
                sitePostcode = "Tennessee";
                break;
         }
        return sitePostcode;
    }

    public static string GetCustomerAddr()
    {
        string SiteAddr = "";
        switch (SiteType)
        {
            case SiteType.Canton:
                SiteAddr = "38303 Michigan Avenue";
                break;
            case SiteType.Romulus:
                SiteAddr = "38303 Michigan Avenue";
                break;
            case SiteType.Memphis:
                SiteAddr = "2720 S Frontage Rd";
                break;
           case SiteType.Louisville3:
                SiteAddr = "2000 Fern Valley";
                break;
        }
        return SiteAddr;
    }
    public static string GetCustomerCity()
    {
        string SiteAddr = "";
        switch (SiteType)
        {
            case SiteType.Canton:
                SiteAddr = "Wayne";
                break;
            case SiteType.Romulus:
                SiteAddr = "Wayne";
                break;
            case SiteType.Memphis:
                SiteAddr = "WEATHERFORD";
                break;
           case SiteType.Louisville3:
           case SiteType.Louisville:
                SiteAddr = "Louisville";
                break;
        }
        return SiteAddr;
    }
    public static string GetCustomerState()
    {
        string State = "";
        switch (SiteType)
        {
            case SiteType.Canton:
                State = "Wayne";
                break;
            case SiteType.Romulus:
                State = "Wayne";
                break;
            case SiteType.Memphis:
                State = "Oklahoma";
                break;
        }
        return State;
    }

    public static string GetSiteDisplayName()
    {
        string siteDisName = "";
        switch (SiteType)
        {
            case SiteType.Canton:
                siteDisName = "Canton Inventory";
                break;
            case SiteType.Romulus:
                siteDisName = "Romulus Inventory";
                break;
            case SiteType.Memphis:
                siteDisName = "Memphis Inventory";
                break;
            case SiteType.Louisville:
                siteDisName = "Louisville Inventory";
                break;
             case SiteType.Louisville3:
                siteDisName = "Louisville3 Inventory";
                break;
            case SiteType.Taylor:
                siteDisName = "Taylor Inventory";
                break;
			case SiteType.Cleaveland:
				siteDisName = "Cleaveland Inventory";
				break;
			case SiteType.CleavelandRepack:
				siteDisName = "Cleaveland Repack Inventory";
				break;
			case SiteType.Wayne:
                siteDisName = "Taylor2 Inventory"; //"Wayne Inventory";
                break;
        }
        return siteDisName;
    }

    public static bool DisplayPicking()
    {
        if (SiteType == SiteType.Memphis || SiteType == SiteType.Louisville)
            return false;
        return true;
    }

    public static bool CanMakeSchedule()
    {
       // if (SiteType == SiteType.Romulus)
       //     return true;
        return false;
    }

    public static bool HasAsnInfo()
    {
        switch(SiteType)
        {
            case SiteType.Louisville3:
            case SiteType.Taylor:
            case SiteType.Romulus:
            case SiteType.Canton:
                return true;
            default: 
                return false;
        }
    }
    public static bool HasAsnCummInfo()
    {
        switch (SiteType)
        {
            case SiteType.Louisville3:
            case SiteType.Taylor:
            case SiteType.Romulus:
            case SiteType.Canton:
                return true;
            default:
                return false;
        }
    }

    public static bool HasPickingCummInfo()
    {
        switch (SiteType)
        {
            case SiteType.Louisville3:
            case SiteType.Canton:
                return true;
            default:
                return false;
        }
    }

    public static bool HasInboundOutboundFun()
    {
        switch (SiteType)
        {
            case SiteType.Louisville3:
            case SiteType.Canton:
                return true;
            default:
                return false;
        }
    }
    public static bool HasInventoryWaringFun()
    {
        switch (SiteType)
        {
            case SiteType.Louisville3:
            case SiteType.Canton:
                return true;
            default:
                return false;
        }
    }
      public static bool GenerateKitReport()
    {
        switch (SiteType)
        {
            case SiteType.Romulus:
                return true;
            default:
                return false;
        }
    }

    public static bool HasInventorySkipPickFun()
    {
        switch (SiteType)
        {
            case SiteType.Canton:
                return true;
            default:
                return false;
        }
    }

}
