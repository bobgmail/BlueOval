
using Dapper;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;






public class DapperContext
{
    private readonly IConfiguration _configuration;
    //private readonly IHttpContextAccessor _httpContextAccessor;
    private string _SpEpConnectionString = "server=172.16.2.9\\SQLEXPRESS; database=MQLSB; Persist Security Info=False; Encrypt=False; User ID=sa;Password=programmingWizard56";
    private string _TransConnectionString = "server=172.16.2.9\\SQLEXPRESS; database=OFTP2TRANS; Persist Security Info=False; Encrypt=False; User ID=sa;Password=programmingWizard56";
    private string _SrConnectionString = "server=172.16.0.10; database=INS Data Files Oem; Persist Security Info=False; Encrypt=False; User ID=sa;Password=sw!ftSign2023";
    private string _QssiconnectionString = "server=hosted-ph-1.cytoituclriy.us-east-2.rds.amazonaws.com; database=hearn_prod; Persist Security Info=False; Encrypt=False; User ID=dbxx;Password=dbxx00";
    private string _IlvsconnectionString = "server = 192.168.11.15\\ILVSSQL; database=RFILVS; Persist Security Info=False; Encrypt=False; User ID=dnc;Password=dnc;Connection Timeout=900;TrustServerCertificate=True;";

    public DapperContext(IConfiguration configuration)//, IHttpContextAccessor httpContextAccessor)
    {
        _configuration = configuration;
       // _httpContextAccessor = httpContextAccessor;
    }

    public static HisSiteType hisSiteType { get; set; } = HisSiteType.BattleCreek;

    private string GetConnectionString()
    {
        //HisSiteType siteType = xType;
        switch (hisSiteType)
        {
            case HisSiteType.Windsor1:
                return "server = 172.16.2.5; database=KareIS_Windsor; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Windsor2:
                return "server = 192.168.36.13\\SQLEXPRESS; database=KareIS_Windsor2; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Romulus1:
                return "server = 192.168.5.8\\SQLEXPRESS; database=KareIS_Romulus; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Romulus2:
                return "server = 172.16.2.5; database=KareIS_Romulus2; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Cleveland:
                return "server = 172.16.2.5; database=KareIS_Cleaveland; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Canton:
                return "server = 192.168.24.33\\SQLEXPRESS; database=KareIS_Canton; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Louisville:
                return "server = 172.16.2.5; database=KareIS_Louisville; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Louisville2:
                return "server = 192.168.26.11\\SQLEXPRESS; database=KareIS_Louisville2; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Louisville3:
                return "server = 192.168.28.28\\SQLEXPRESS; database=KareIS_LVL3; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Memphis:
                return "server = 172.16.2.5; database=KareIS_Memphis; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Oakville:
                return "server = 172.16.2.5; database=KareIS_Oakville; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Strongsville:
                return "server = 172.16.2.5; database=KareIS_Strongsville; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Taylor:
                return "server = 172.16.2.5; database=KareIS_Taylor; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.Taylor2:
                return "server = 172.16.2.5; database=KareIS_Wayne; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            case HisSiteType.BattleCreek:
                return "server = 192.168.16.9\\SQLEXPRESS; database=KareIS_BattleCreek; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
            default:
                return "";
        }

    }
    private string GetConnectionString(string siteValue)
    {
        
        //var cookieValue = _httpContextAccessor.HttpContext.Request.Cookies["InseqSiteType"];
        //string siteValue = "Windsor1";
        if (siteValue != null)
        {
            if (Enum.TryParse(siteValue, out HisSiteType xType))
            {
                HisSiteType siteType = xType;
                switch (siteType)
                {
                    case HisSiteType.Windsor1:
                        return "server = 172.16.2.5; database=KareIS_Windsor; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Windsor2:
                        return "server = 192.168.36.13\\SQLEXPRESS; database=KareIS_Windsor2; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Romulus1:
                        return "server = 192.168.5.8\\SQLEXPRESS; database=KareIS_Romulus; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Romulus2:
                        return "server = 172.16.2.5; database=KareIS_Romulus2; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Cleveland:
                        return "server = 172.16.2.5; database=KareIS_Cleaveland; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Canton:
                        return "server = 192.168.24.33\\SQLEXPRESS; database=KareIS_Canton; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Louisville:
                        return "server = 172.16.2.5; database=KareIS_Louisville; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Louisville2:
                        return "server = 192.168.26.11\\SQLEXPRESS; database=KareIS_Louisville2; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Louisville3:
                        return "server = 192.168.28.28\\SQLEXPRESS; database=KareIS_LVL3; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Memphis:
                        return "server = 172.16.2.5; database=KareIS_Memphis; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Oakville:
                        return "server = 172.16.2.5; database=KareIS_Oakville; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Strongsville:
                        return "server = 172.16.2.5; database=KareIS_Strongsville; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Taylor:
                        return "server = 172.16.2.5; database=KareIS_Taylor; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    case HisSiteType.Taylor2:
                        return "server = 172.16.2.5; database=KareIS_Wayne; Persist Security Info=False; Encrypt=False; User ID=kareis;Password=Alpha15!;Connection Timeout=900;TrustServerCertificate=True;";
                    default:
                        return "";
                }

            }
        }

        return "";
    
    }

    public IDbConnection CreateConnection()
       => new SqlConnection(GetConnectionString());


    public IDbConnection CreateTransConnection()
        => new SqlConnection(_TransConnectionString);

    public IDbConnection CreateSpConnection()
        => new SqlConnection(_SpEpConnectionString);

    public IDbConnection CreateEpConnection()
     => new SqlConnection(_SpEpConnectionString);

    public IDbConnection CreateSrConnection()
        => new SqlConnection(_SrConnectionString);

    public IDbConnection CreateQssiConnection()
  => new SqlConnection(_QssiconnectionString);

    public IDbConnection CreateILVSConnection()
  => new SqlConnection(_IlvsconnectionString);
    public int SlotPosition { get; set; }
    public string RackGroup { get; set; }
    public string BOLNumber { get; set; }
    public DateTime RackDate { get; set; }

    public async Task<List<BolInfo>> GetInsequenceBolInfo(string BolNumbers)
    {
        int BolNumber = 0;
        if(int.TryParse(BolNumbers.Trim(), out BolNumber))
        {
            BolNumber = int.Parse(BolNumbers);
        }
        else
        {
            return [];
        }

        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                string sql = """" 
                SELECT [BOLNumber],[BOLDetailId],[PartNumber],[PartQty],[ProcessDate]
                FROM [INS Data Files].[dbo].[BOL Detail]
                where BOLNumber = @BolNumber
                order by  BOLDetailId
                """";
                var result = await connection.QueryAsync<BolInfo>(sql, new { BolNumber });
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];

    }

    public async Task<List<RackInfo>> GetInsequenceRackInfo(string vinNumber)
    {
        vinNumber = "%" + vinNumber.Trim() + "%";
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                string sql = """" 
                SELECT v.VinNumber,v.PartNumber,v.PartDesc,v.MbModuleType,v.MbSupplierCode,v.ProcessSequence
                      ,s.SlotPosition,s.RackGroup,s.BOLNumber,s.RackDate
                FROM [Mes Vehicle Serial] v
                join [Mes Vehicle Parts] s on s.SerialNumber=v.SerialNumber 
                where v.VinNumber  like @vinNumber
                order by  s.RackGroup,s.SlotPosition,s.RackDate, v.VinNumber
                """";
                var result = await connection.QueryAsync<RackInfo>(sql, new { vinNumber});
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];

    }

    public async Task<List<ScanInfo>> GetInsequenceScanInfo(string vinNumber,int type)
    {
        vinNumber = "%" + vinNumber.Trim() + "%";
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                string sql = """" 
                SELECT v.VinNumber,v.PartNumber,v.PartDesc,v.MbModuleType,v.MbSupplierCode,v.ProcessSequence
                      ,s.ScanType,s.ScanData,s.DcTagDesc,s.ProductionLine,s.ProcessDate
                FROM [Mes Vehicle Serial] v
                join [Mes Serial Scan Data] s on s.SerialNumber=v.SerialNumber and s.ScanType=@type
                where v.VinNumber  like @vinNumber
                order by  s.ProcessDate, v.VinNumber
                """";
                var result = await connection.QueryAsync<ScanInfo>(sql, new { vinNumber,type });
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];  
        
    }

    public async Task<List<ScanInfo>> GetInsequenceScanReport(DateTime dts, DateTime dte,int type)
    {
       // vinNumber = "%" + vinNumber.Trim() + "%";
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                string sql = """" 
                SELECT v.VinNumber,v.PartNumber,v.PartDesc,v.MbModuleType,v.MbSupplierCode,v.ProcessSequence
                      ,s.ScanType,s.ScanData,s.DcTagDesc,s.ProductionLine,s.ProcessDate
                FROM [Mes Vehicle Serial] v
                join [Mes Serial Scan Data] s on s.SerialNumber=v.SerialNumber and s.ScanType=@type
                where v.MfgDate>=@dts and v.MfgDate<@dte
                order by  v.VinNumber, s.ProcessDate
                """";
                var result = await connection.QueryAsync<ScanInfo>(sql, new { dts, dte,type });
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];

    }
    public async Task<List<RackInfo>> GetInsequenceRackReport(DateTime dts, DateTime dte)
    {
        // vinNumber = "%" + vinNumber.Trim() + "%";
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                string sql = """" 
                SELECT v.VinNumber,v.PartNumber,v.PartDesc,v.MbModuleType,v.MbSupplierCode,v.ProcessSequence
                      ,s.SlotPosition,s.RackGroup,s.BOLNumber,s.RackDate
                FROM [Mes Vehicle Serial] v
                join [Mes Vehicle Parts] s on s.SerialNumber=v.SerialNumber 
                where v.MfgDate>=@dts and v.MfgDate<@dte
                order by   s.RackGroup,v.ProcessSequence,s.SlotPosition,s.RackDate
                """";
                var result = await connection.QueryAsync<RackInfo>(sql, new { dts, dte});
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];

    }

    public List<LabelPrintInfo> GetLabelPrintInfos(int ReceivingID)
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                string sql = """" 
                SELECT rd.[ReceivingID],rd.ReceivingPartID, rd.[PartNumber],rd.BinQuantity, rm.BOL,p.PartNo as PalletQRCode,rm.ASN_Number as ContainerQRCode,p.description as PalletNo
                FROM [ReceivingDetails] rd
                join Receivings_Master rm on rd.ReceivingID = rm.ReceivingID
                join Parts p on p.PartID = rd.PartID
                where rm.ReceivingStatus = 'Open' and rm.ReceivingDate > '2025-05-15'
                and rd.ReceivingID = @ReceivingID 
                order by ReceivingPartID
                """";
                var result =  connection.Query<LabelPrintInfo>(sql, new { ReceivingID });
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];

    }

    public async Task<List<LabelPrintBolInfo>> GetLabelPrintBolsAsync()
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                string sql = """" 
                SELECT m.[ReceivingID] ,[BOL],[SupplierName] ,[CustomerName] ,[CarrierName],[TrailerNo],[ReceivingDate], p.LabelsCount
                FROM Receivings_Master m
                join (select ReceivingID,count(*) as LabelsCount from [ReceivingDetails]  group by ReceivingID ) as p on p.ReceivingID=m.ReceivingID
                where m.[ReceivingStatus] = 'Open' and ReceivingDate > '2025-05-15' 
                order by m.ReceivingID desc
                """";
                var result = await connection.QueryAsync<LabelPrintBolInfo>(sql);
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];

    }

    public async Task<List<PalletInfo>> GetInboundWithTimeRange(DateTime s, DateTime e, int mode)
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                int nightStart = SiteControl.GetShiftStart(mode);
                List<PalletInfo> ret = new();
                PalletInfo ctrl;

                string sql = "SELECT r.[ReceiveOrderID] as ID, r.[CreatedDate] ,r.[SkidsSerialno] as SkidNo ,r.[PartId] ,r.[PartNumber] as PartNo ,r.[ReceivedQty] as Qty ,rm.Notes ,rm.ReceivingID as MasterID";
                sql += " FROM ReceiveOrder r";
                sql += " join Receivings_Master rm on rm.ReceivingID = r.ReceivingId";
                sql += " where rm.ReceivingStatus is not null";//= 'Closed'";
                if (mode == 0)       //6am~6pm
                {
                    sql += "  and   DATEPART(hh, r.[CreatedDate]) >= " + nightStart + "  AND DATEPART(hh, r.[CreatedDate]) < " + (12 + nightStart).ToString();
                }
                else if (mode == 1) //6pm~6am
                {
                    sql += "  and  ( (DATEPART(hh, r.[CreatedDate]) >= 0 AND DATEPART(hh, r.[CreatedDate]) < " + nightStart + ")";
                    sql += "  or   (DATEPART(hh, r.[CreatedDate]) >= " + (12 + nightStart).ToString() + " AND DATEPART(hh, r.[CreatedDate]) < 24))";

                }
                sql += " and  r.[CreatedDate] >='" + s + "' and r.[CreatedDate] <'" + e + "'";
                sql += " order by r.CreatedDate desc";

                var result = await connection.QueryAsync<PalletInfo>(sql);
                if(result is not null)
                {
                    return result.ToList();
                }
    

            }
        }
        catch (Exception ex)
        {

        }
        return [];
    }

    public async Task<List<PalletInfo>> GetInboundWithTimeRangeBattery(DateTime s, DateTime e, int mode)
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                int nightStart = SiteControl.GetShiftStart(mode);
                List<PalletInfo> ret = new();
                PalletInfo ctrl;

                string sql = """
                    SELECT r.[ReceiveOrderID] as ID, r.[CreatedDate] ,r.[SkidsSerialno] as SkidNo ,r.[PartId] ,p.PartNo as PalletQRCode ,r.[ReceivedQty] as Qty ,rm.Notes ,rm.ReceivingID as MasterID
                    ,rm.BOL as ContainerNo, p.Description as PalletNo,rm.ASN_Number as ContainerQRCode
                    FROM ReceiveOrder r
                    join Receivings_Master rm on rm.ReceivingID = r.ReceivingId
                    Join Parts p on p.PartID = r.PartID
                    where rm.ReceivingStatus is not null

                    """;
                if (mode == 0)       //6am~6pm
                {
                    sql += "  and   DATEPART(hh, r.[CreatedDate]) >= " + nightStart + "  AND DATEPART(hh, r.[CreatedDate]) < " + (12 + nightStart).ToString();
                }
                else if (mode == 1) //6pm~6am
                {
                    sql += "  and  ( (DATEPART(hh, r.[CreatedDate]) >= 0 AND DATEPART(hh, r.[CreatedDate]) < " + nightStart + ")";
                    sql += "  or   (DATEPART(hh, r.[CreatedDate]) >= " + (12 + nightStart).ToString() + " AND DATEPART(hh, r.[CreatedDate]) < 24))";

                }
                sql += " and  r.[CreatedDate] >='" + s + "' and r.[CreatedDate] <'" + e + "'";
                sql += " order by r.CreatedDate desc";

                var result = await connection.QueryAsync<PalletInfo>(sql);
                if (result is not null)
                {
                    return result.ToList();
                }


            }
        }
        catch (Exception ex)
        {

        }
        return [];
    }

    public async Task<List<InfoSummary>> GetInboundPartsSummary(DateTime s, DateTime e, int mode)
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                int nightStart = SiteControl.GetShiftStart(mode);
                List<InfoSummary> ret = new();
                InfoSummary ctrl;

                string sql = " SELECT r.[PartNumber] as Name, sum(r.[ReceivedQty]) as Qty";
                sql += " FROM ReceiveOrder r";
                sql += " join Receivings_Master rm on rm.ReceivingID = r.ReceivingId";
                sql += " where rm.ReceivingStatus is not null";//= 'Closed'";
                if (mode == 0)       //6am~6pm
                {
                    sql += "  and   DATEPART(hh, r.[CreatedDate]) >= " + nightStart + " AND DATEPART(hh, r.[CreatedDate]) < " + (12 + nightStart).ToString();
                }
                else if (mode == 1) //6pm~6am
                {
                    sql += "  and  ( (DATEPART(hh, r.[CreatedDate]) >= 0 AND DATEPART(hh, r.[CreatedDate]) < " + nightStart + ")";
                    sql += "  or   (DATEPART(hh, r.[CreatedDate]) >= " + (12 + nightStart).ToString() + " AND DATEPART(hh, r.[CreatedDate]) < 24))";

                }

                sql += " and  r.[CreatedDate] >='" + s + "' and r.[CreatedDate] <'" + e + "'";
                sql += " group by r.PartNumber";
                sql += " order by r.PartNumber";
                var result = await connection.QueryAsync<InfoSummary>(sql);
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];
    }

    public async Task<List<PalletInfo>> GetOutboundWithTimeRange(DateTime s, DateTime e, int mode)
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                int nightStart = SiteControl.GetShiftStart(mode);
                List<PalletInfo> ret = new();
                PalletInfo ctrl;

                string sql = "select s.[LoadID] as ID, s.[ShippingID] as MasterID ,s.[PartID] ,p.PartNo ,s.[SkidNo] ,s.[PartQuantity] as Qty ,s.[CreatedDate] ,sm.TrailerNo as Notes";
                sql += "  FROM ShippingLoadTransactions s";
                sql += " join Shipping_Master sm on sm.ShippingID = s.ShippingID";
                sql += " join Parts p on p.PartID = s.PartID";
                sql += " where sm.ShippingStatus is not null";//= 'closed'";
                if (mode == 0)       //6am~6pm
                {
                    sql += "  and   DATEPART(hh, s.[CreatedDate]) >= " + nightStart + " AND DATEPART(hh, s.[CreatedDate]) < " + (12 + nightStart).ToString();
                }
                else if (mode == 1) //6pm~6am
                {
                    sql += "  and  ( (DATEPART(hh, s.[CreatedDate]) >= 0 AND DATEPART(hh, s.[CreatedDate]) < " + nightStart + ")";
                    sql += "  or   (DATEPART(hh, s.[CreatedDate]) >= " + (12 + nightStart).ToString() + " AND DATEPART(hh, s.[CreatedDate]) < 24))";

                }

                sql += " and  s.[CreatedDate] >='" + s + "' and s.[CreatedDate] <'" + e + "'";
                sql += " order by s.CreatedDate desc";

                var result = await connection.QueryAsync<PalletInfo>(sql);
                if (result is not null)
                {
                    return result.ToList();
                }


            }
        }
        catch (Exception ex)
        {

        }
        return [];
    }

    public async Task<List<PalletInfo>> GetOutboundWithTimeRangeBattery(DateTime s, DateTime e, int mode)
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                int nightStart = SiteControl.GetShiftStart(mode);
                List<PalletInfo> ret = new();
                PalletInfo ctrl;

                string sql = """
                    select s.[LoadID] as ID, s.[ShippingID] as MasterID ,s.[PartID] ,p.PartNo as PalletQRCode,s.[SkidNo] ,s.[PartQuantity] as Qty ,s.[CreatedDate] ,sm.TrailerNo
                    ,rm.BOL as ContainerNo, p.Description as PalletNo, rm.ASN_Number as ContainerQRCode, sm.DockCode as DeliveryDoor
                    FROM ShippingLoadTransactions s
                    join Shipping_Master sm on sm.ShippingID = s.ShippingID
                    join Parts p on p.PartID = s.PartID
                    left join ReceiveOrder r on r.[SkidsSerialno] = s.SkidNo
                    left join Receivings_Master rm on r.ReceivingId=rm.ReceivingID
                    where sm.ShippingStatus is not null
                    """;
                if (mode == 0)       //6am~6pm
                {
                    sql += "  and   DATEPART(hh, s.[CreatedDate]) >= " + nightStart + " AND DATEPART(hh, s.[CreatedDate]) < " + (12 + nightStart).ToString();
                }
                else if (mode == 1) //6pm~6am
                {
                    sql += "  and  ( (DATEPART(hh, s.[CreatedDate]) >= 0 AND DATEPART(hh, s.[CreatedDate]) < " + nightStart + ")";
                    sql += "  or   (DATEPART(hh, s.[CreatedDate]) >= " + (12 + nightStart).ToString() + " AND DATEPART(hh, s.[CreatedDate]) < 24))";

                }

                sql += " and  s.[CreatedDate] >='" + s + "' and s.[CreatedDate] <'" + e + "'";
                sql += " order by s.CreatedDate desc";

                var result = await connection.QueryAsync<PalletInfo>(sql);
                if (result is not null)
                {
                    return result.ToList();
                }


            }
        }
        catch (Exception ex)
        {

        }
        return [];
    }


    public async Task<List<InfoSummary>> GetOutboundPartsSummary(DateTime s, DateTime e, int mode)
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                //return getData(connection);
                int nightStart = SiteControl.GetShiftStart(mode);
                List<InfoSummary> ret = new();
                InfoSummary ctrl;

                string sql = " SELECT p.PartNo as Name, sum(s.PartQuantity) as Qty";
                sql += " FROM ShippingLoadTransactions s";
                sql += " join Shipping_Master sm on sm.ShippingID = s.ShippingID";
                sql += " join Parts p on p.PartID = s.PartID";
                sql += " where sm.ShippingStatus is not null";//= 'closed'";
                if (mode == 0)       //6am~6pm
                {
                    sql += "  and   DATEPART(hh, s.[CreatedDate]) >=" + nightStart + " AND DATEPART(hh, s.[CreatedDate]) < " + (12 + nightStart).ToString();
                }
                else if (mode == 1) //6pm~6am
                {
                    sql += "  and  ( (DATEPART(hh, s.[CreatedDate]) >= 0 AND DATEPART(hh, s.[CreatedDate]) < " + nightStart + ")";
                    sql += "  or   (DATEPART(hh, s.[CreatedDate]) >= " + (12 + nightStart).ToString() + " AND DATEPART(hh, s.[CreatedDate]) < 24))";

                }
                sql += " and  s.[CreatedDate] >='" + s + "' and s.[CreatedDate] <'" + e + "'";
                sql += " group by p.PartNo";
                sql += " order by p.PartNo";

                var result = await connection.QueryAsync<InfoSummary>(sql);
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];
    }


    public async Task<List<ReceiveOrder>> GetReceivingAsync(string SkidNo)
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                string sql = """
                    Select r.*,CONCAT(e.First_Name, ',' , e.Last_Name) as EmployeeName 
                    from ReceiveOrder r
                    Join Employees e on e.EmployeeId=r.CreatedById
                    where SkidsSerialno=@SkidNo 
                    order by CreatedDate
                    """;
                var result = await connection.QueryAsync<ReceiveOrder>(sql, new { SkidNo });
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];
 
    }

    public async Task<List<PickingLoadTransactions>> GetPickingLoadAsync(string SkidNo)
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                string sql = """
                    Select r.*,CONCAT(e.First_Name, ',' , e.Last_Name) as EmployeeName, l.LocationTitle as LocationName
                    from PickingLoadTransactions r
                    Join Employees e on e.EmployeeId=r.CreatedById
                    Left Join [LocationsWithinWarehouse] l on l.LocationID=r.OutBoundLocationID
                    where r.SkidNo = @SkidNo 
                    order by CreatedDate
                    """;
                var result = await connection.QueryAsync<PickingLoadTransactions>(sql, new { SkidNo });
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];

    }

    public async Task<List<ShippingLoadTransactions>> GetShippingAsync(string SkidNo)
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                string sql = """
                    Select r.*,CONCAT(e.First_Name, ',' , e.Last_Name) as EmployeeName, l.PartNo as PartNumber
                    from [ShippingLoadTransactions] r
                    Join Employees e on e.EmployeeId=r.CreatedById
                    Join Parts l on l.PartID=r.PartID
                    where r.SkidNo = @SkidNo 
                    order by CreatedDate
                    """;
                var result = await connection.QueryAsync<ShippingLoadTransactions>(sql, new { SkidNo });
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];
   
    }

    public async Task<List<InventoryMovementDetails>> GetMovementsAsync(string SkidNo)
    {
        if (SkidNo == "")
        {
            return new List<InventoryMovementDetails>();
        }
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                string sql = """
                    Select r.*,CONCAT(e.First_Name, ',' , e.Last_Name) as EmployeeName
                    from [InventoryMovementDetails] r
                    Join Employees e on e.EmployeeId=r.CreatedById
                    where r.SourceScanBoxBin = @SkidNo and r.PartQuantity is not null
                    order by CreatedDate
                    """;
                var result = await connection.QueryAsync<InventoryMovementDetails>(sql, new { SkidNo });
                if (result is not null)
                {
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];
    }

    public async Task<List<PartCycle>> GetInventoryCycleInfoPart(string partNo)
    {
        int LocID = SiteControl.GetReceivingLocID();
        string sql = "";
        if (LocID != 0)
        {   //All the parts in ReceiveOrder, but not in Movement & Shipment
            sql += " select PartID, PartNumber as PartNo, SkidsSerialno as SkidNo, ReceivedQty as PartQty, 1 as BinQty," + LocID + " as LocationId, 'Receiving Area' as Location";
            sql += " from";
            sql += " (";
            sql += " select * from ReceiveOrder r";
            sql += " where (not exists(select Skidno from ShippingLoadTransactions s where r.SkidsSerialno = s.SkidNo)";
            sql += " and not exists(select SourceScanBoxBin from InventoryMovementDetails m where r.SkidsSerialno = m.SourceScanBoxBin))";
            sql += " and PartNumber='" + partNo + "') as t";
        }
        else
        { //All the parts in ReceiveOrder, but not in Movement & Shipment!!!!! and Get Locations
            sql += " select PartID, PartNumber as PartNo, SkidsSerialno as SkidNo, ReceivedQty as PartQty, 1 as BinQty, LocationId, Location";
            sql += " from";
            sql += " (";
            sql += " select r.*,l.LocationID, l.LocationTitle as Location  from ReceiveOrder r";
            sql += " join ReceivingDetails rr on rr.ReceivingPartID = r.ReceivingPartID";
            sql += " join LocationsWithinWarehouse l on l.LocationID = rr.LocationID";
            sql += " where (not exists(select Skidno from ShippingLoadTransactions s where r.SkidsSerialno = s.SkidNo)";
            sql += " and not exists(select SourceScanBoxBin from InventoryMovementDetails m where r.SkidsSerialno = m.SourceScanBoxBin))";
            sql += " and r.PartNumber='" + partNo + "') as t";
            //Special for: LocationID = 0 
            sql += " UNION ALL";
            sql += " select PartID, PartNumber as PartNo, SkidsSerialno as SkidNo, ReceivedQty as PartQty, 1 as BinQty, LocationId, Location";
            sql += " from";
            sql += " (";
            sql += " select r.*,rr.LocationID, 'LOCATION UNKNOWN' as Location  from ReceiveOrder r";
            sql += " join ReceivingDetails rr on rr.ReceivingPartID = r.ReceivingPartID and rr.LocationID = 0";
            sql += " where (not exists(select Skidno from ShippingLoadTransactions s where r.SkidsSerialno = s.SkidNo)";
            sql += " and not exists(select SourceScanBoxBin from InventoryMovementDetails m where r.SkidsSerialno = m.SourceScanBoxBin))";
            sql += " and r.PartNumber='" + partNo + "') as t";

        }
        string subTableEnull = "( SELECT c.*  FROM [InventoryMovementDetails] c  WHERE c.[InventoryMovementDetailID] = (SELECT MAX([InventoryMovementDetailID]) FROM [InventoryMovementDetails] c2 WHERE c2.SourceScanBoxBin = c.SourceScanBoxBin and PartQuantity is null) ) as e ";
        string subTableE = "( SELECT c.*  FROM [InventoryMovementDetails] c  WHERE c.[InventoryMovementDetailID] = (SELECT MAX([InventoryMovementDetailID]) FROM [InventoryMovementDetails] c2 WHERE c2.SourceScanBoxBin = c.SourceScanBoxBin and PartQuantity is not null) ) as e ";

        //PartQuantity is null: in Receiving Area-----the case for Recording for receiving in Movements
        sql += " UNION ALL";
        sql += " SELECT distinct PartID, PartNo, SourceScanBoxBin as SkidNo, [PartQuantitytoMove] as PartQty, [BinQuantitytoMove] as BinQty, " + LocID + " as LocationId, 'Receiving Area' as Location";
        sql += " FROM " + subTableEnull;// (select *, row_number() over(partition by[SourceScanBoxBin] order by[InventoryMovementDetailID] desc) as r from [InventoryMovementDetails] where PartQuantity is null )  e";
        sql += " where  not exists(select SkidNo from[ShippingLoadTransactions] f where e.SourceScanBoxBin = f.SkidNo)";
        sql += " and PartNo='" + partNo + "'";
        //sql += " and e.r = 1 and PartNo='" + partNo + "'";

        sql += " UNION ALL";
        sql += " SELECT distinct PartID, PartNo, SourceScanBoxBin as SkidNo, PartQuantity as PartQty, BinQuantity as BinQty, DestinationLocationID as LocationID, DestinationLocation as Location";
        sql += " FROM " + subTableE;// (select *, row_number() over(partition by[SourceScanBoxBin] order by[InventoryMovementDetailID] desc) as r from [InventoryMovementDetails] where PartQuantity is not null )  e";
        sql += " where  not exists(select SkidNo from[ShippingLoadTransactions] f where e.SourceScanBoxBin = f.SkidNo)";
        sql += " and PartNo='" + partNo + "'";
        //sql += " and e.r = 1 and PartNo='" + partNo + "'";

        sql += " order by PartNo, Location";

        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

  
                var result = await connection.QueryAsync<PartCycle>(sql);
                if (result is not null)
                {
                    foreach (var part in result)
                    {
                        if (part.Location == "Unknow For Rescan")
                            part.Location = "Unknown For Rescan";
                        if (part.DstLocation == "Unknow For Rescan")
                            part.DstLocation = "Unknown For Rescan";
                    }
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }
        return [];
    }
    public async Task<Dictionary<int, string>> GetPartsDef()
    {
        Dictionary<int, string> dict = new Dictionary<int, string>();
     
        string sql = "select PartID as [Index], Description as Name from Parts ";
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();


                var result = await connection.QueryAsync<SelectionDef>(sql);
                if (result is not null)
                {
                    foreach (var part in result)
                    {
                       dict.Add(part.Index, part.Name);
                    }
                }

            }
        }
        catch (Exception ex)
        {

        }
    
        return dict;
    }

    public async Task<List<PartCycleCtrl>> GetInventoryCycleInfoCtrl(List<PartCycle> SortedList)
    {

        var dict = await GetPartsDef();
        List<PartCycleCtrl> Parts = new List<PartCycleCtrl>();
        PartCycleCtrl partCtrl = new PartCycleCtrl();

        if (SortedList.Count == 0)
            return Parts;

        int partID = -1;
        int index = 0;
        foreach (var part in SortedList)
        {

            if (partID != part.PartID)
            {
                partCtrl = new PartCycleCtrl
                {
                    IndexS = index,
                    IndexD = index,
                    PartID = part.PartID,
                    PartNo = part.PartNo,

                    PartQtySum = part.PartQty,
                    CyclePartQtySum = part.PartQty,
                    BinQtySum = part.BinQty,
                    CycleBinQtySum = part.BinQty,
                };
                if (dict.ContainsKey(part.PartID))
                {
                    partCtrl.PartDesc = dict[part.PartID];
                }

                Parts.Add(partCtrl);
                partID = part.PartID;
            }
            else
            {
                partCtrl.IndexD = index;
                partCtrl.PartQtySum += part.PartQty;
                partCtrl.CyclePartQtySum = partCtrl.PartQtySum;
                partCtrl.BinQtySum += part.BinQty;
                partCtrl.CycleBinQtySum = partCtrl.BinQtySum;
            }
            index++;
        }

        return Parts;
    }


    public async Task<List<PartCycle>> GetInventoryCycleInfo()
    {
        string subTableEnull = "( SELECT c.*  FROM [InventoryMovementDetails] c  WHERE c.[InventoryMovementDetailID] = (SELECT MAX([InventoryMovementDetailID]) FROM [InventoryMovementDetails] c2 WHERE c2.SourceScanBoxBin = c.SourceScanBoxBin and PartQuantity is null) ) as e ";
        string subTableE = "( SELECT c.*  FROM [InventoryMovementDetails] c  WHERE c.[InventoryMovementDetailID] = (SELECT MAX([InventoryMovementDetailID]) FROM [InventoryMovementDetails] c2 WHERE c2.SourceScanBoxBin = c.SourceScanBoxBin and PartQuantity is not null) ) as e ";

        int LocID = SiteControl.GetReceivingLocID();
        string sql = "";
        //In Receiving Area, but not recorded in Movements-----for the case: Not recording Receiving in Movements, that is direct make movements in Receiving operating
        if (LocID != 0)
        {
            sql = $"""
                select PartID, PartNumber as PartNo, SkidsSerialno as SkidNo, ReceivedQty as PartQty,1 as BinQty,{LocID} as LocationId, 'Receiving Area' as Location
                from
                (select * from ReceiveOrder r
                 where (not exists(select Skidno from ShippingLoadTransactions s where r.SkidsSerialno = s.SkidNo)
                 and not exists(select SourceScanBoxBin from InventoryMovementDetails m where r.SkidsSerialno = m.SourceScanBoxBin ))
                ) as t
                """;
        }
        else
        {
            sql = """
                select PartID, PartNumber as PartNo, SkidsSerialno as SkidNo, ReceivedQty as PartQty, 1 as BinQty, LocationId, Location
                from
                (select r.*,l.LocationID, l.LocationTitle as Location  from ReceiveOrder r
                join ReceivingDetails rr on rr.ReceivingPartID = r.ReceivingPartID
                join LocationsWithinWarehouse l on l.LocationID = rr.LocationID
                where (not exists(select Skidno from ShippingLoadTransactions s where r.SkidsSerialno = s.SkidNo)
                and not exists(select SourceScanBoxBin from InventoryMovementDetails m where r.SkidsSerialno = m.SourceScanBoxBin))
                ) as t
                """;

        }

        //PartQuantity is null: in Receiving Area-----the case for Recording for receiving in Movements
        sql += $"""
             UNION ALL
            SELECT distinct PartID, PartNo, SourceScanBoxBin as SkidNo, [PartQuantitytoMove] as PartQty, [BinQuantitytoMove] as BinQty, {LocID} as LocationId, 'Receiving Area' as Location
            FROM  {subTableEnull}
            where  not exists(select SkidNo from[ShippingLoadTransactions] f where e.SourceScanBoxBin = f.SkidNo)
            """;

        //PartQuantity is not null: in Location other than Receiving Area
        sql += $"""
             UNION ALL
            SELECT distinct PartID, PartNo, SourceScanBoxBin as SkidNo, PartQuantity as PartQty, BinQuantity as BinQty, DestinationLocationID as LocationID, DestinationLocation as Location
            FROM {subTableE}
            where  not exists(select SkidNo from [ShippingLoadTransactions] f where e.SourceScanBoxBin = f.SkidNo)
            """;

        sql += " order by PartNo, Location";
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();


                var result = await connection.QueryAsync<PartCycle>(sql);
                if (result is not null)
                {
                    foreach (var item in result)
                    {
                        if (item.Location == "Unknow For Rescan")
                            item.Location = "Unknown For Rescan";
                        item.DstLocationID = item.LocationID;
                        item.DstLocation = item.Location;
                        item.IsShipped = false;
                    }
                    return result.ToList(); 
                }

            }
        }
        catch (Exception ex)
        {

        }

        return [];

    
    }

    public async Task<List<PartCycle>> GetInventoryCycleInfoBattery()
    {
         
        //In Receiving Area, but not recorded in Movements-----for the case: Not recording Receiving in Movements, that is direct make movements in Receiving operating
        string   sql = """
                select PartID, PartNumber as PartNo, SkidsSerialno as SkidNo, ReceivedQty as PartQty, 1 as BinQty, LocationId, Location,LocationCode,ContainerNo,ContainerQRCode,PalletNo,PalletQRCode,EquipmentOp,SectionComp,ProcessEquip,SectionCompName
                from
                (select r.*,l.LocationID, l.LocationTitle as Location,l.LocationCode, m.BOL as ContainerNo, m.ASN_Number as ContainerQRCode,p.Description as PalletNo,p.PartNo as PalletQRCode,p.EquipmentOp,p.SectionComp,p.ProcessEquip,p.SectionCompName
                from ReceiveOrder r
                join ReceivingDetails rr on rr.ReceivingPartID = r.ReceivingPartID
                join [Receivings_Master] m on m.ReceivingID = rr.ReceivingID
                join LocationsWithinWarehouse l on l.LocationID = rr.LocationID
                join Parts p on p.PartID = r.PartID
                where (not exists(select 1 from ShippingLoadTransactions s where r.SkidsSerialno = s.SkidNo)
                and not exists(select 1 from InventoryMovementDetails m where r.SkidsSerialno = m.SourceScanBoxBin))
                ) as t
                where ContainerQRCode is not null
                """;


        
        sql += $"""
             UNION ALL
              select PartID, PartNumber as PartNo, SkidsSerialno as SkidNo, ReceivedQty as PartQty, 1 as BinQty, t.LocationId, Location,ll.LocationCode,ContainerNo,ContainerQRCode,PalletNo,PalletQRCode,EquipmentOp,SectionComp,ProcessEquip,SectionCompName
            from
            (select r.*,l.DestinationLocationID as LocationID, l.DestinationLocation  as Location, m.BOL as ContainerNo, m.ASN_Number as ContainerQRCode,p.Description as PalletNo,p.PartNo as PalletQRCode,p.EquipmentOp,p.SectionComp,p.ProcessEquip,p.SectionCompName
            from ReceiveOrder r
            join ReceivingDetails rr on rr.ReceivingPartID = r.ReceivingPartID
            join [Receivings_Master] m on m.ReceivingID = rr.ReceivingID
            join (SELECT c.*  FROM [InventoryMovementDetails] c 
                  WHERE c.[InventoryMovementDetailID] = 
                       (SELECT MAX([InventoryMovementDetailID])
                       FROM [InventoryMovementDetails] c2 WHERE c2.SourceScanBoxBin = c.SourceScanBoxBin 
                       and PartQuantity is not null)) as l on  l.SourceScanBoxBin = r.[SkidsSerialno]
            left join Parts p on p.PartID = r.PartID
            where (not exists(select 1 from ShippingLoadTransactions s where r.SkidsSerialno = s.SkidNo))
            and  exists(select 1 from InventoryMovementDetails m where r.SkidsSerialno = m.SourceScanBoxBin)
            ) as t
            join LocationsWithinWarehouse ll on ll.LocationID = t.LocationID
            where ContainerQRCode is not null
            """;
        
        sql += " order by ContainerNo, Location, PalletNo";
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();


                var result = await connection.QueryAsync<PartCycle>(sql,commandTimeout:5*60);
                if (result is not null)
                {
                    foreach (var item in result)
                    {
                        if (item.Location == "Unknow For Rescan")
                            item.Location = "Unknown For Rescan";
                        item.DstLocationID = item.LocationID;
                        item.DstLocation = item.Location;
                        item.IsShipped = false;
                    }
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }

        return [];


    }

    public  bool IsPalletOrdered(string palletQrCode)    //this QR code is unique
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                string sql = "SELECT COUNT(*) FROM EquipmentOrderDetail WHERE PalletQRCode = @PalletQRCode";
                int count =  connection.ExecuteScalar<int>(sql, new { PalletQRCode = palletQrCode });
                if (count > 0)
                {
                    return true; // The pallet is ordered
                }
                else
                {
                    return false; // The pallet is not ordered
                }
            }

        }
        catch (Exception)
        {

            return false;
        }
    }

    public async Task<(bool bSucess,int ID)> InsertOrderRequest(EquipmentOrderInfo orderInfo, List<PartCycle> orderDetails)
    {
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //string sql = "INSERT INTO EquipmentOrderRequest (RequestDate, RequestBy, RequestNotes) VALUES (@RequestDate, @RequestBy, @RequestNotes); SELECT CAST(SCOPE_IDENTITY() as int)";
                        string sql = """
                        INSERT INTO EquipmentOrderInfo (Installer, Requestor, DockDoor, DeliveryDate, DeliveryTime,CreatedBy,InstallerContact,InstallerPhone) 
                        OUTPUT INSERTED.ID
                        VALUES (@Installer, @Requestor, @DockDoor, @DeliveryDate,@DeliveryTime,@CreatedBy,@InstallerContact,@InstallerPhone)
                        """;
                        int OrderID = await connection.ExecuteScalarAsync<int>(sql, new { orderInfo.Installer, orderInfo.Requestor, orderInfo.DockDoor, orderInfo.DeliveryDate, orderInfo.DeliveryTime,orderInfo.CreatedBy,orderInfo.InstallerContact,orderInfo.InstallerPhone }, transaction);
                        foreach (var detail in orderDetails)
                        {
                            if(detail.IsPalletSelected  == false && detail.IsContainerSelected == false)
                                continue; //Skip the details that are not selected

                            sql = """
                            INSERT INTO EquipmentOrderDetail (OrderID, ContainerNo, ContainerQRCode, PalletNo,PalletQRCode,WhseSite,ContainerSelected,PalletSelected)
                            VALUES (@OrderID, @ContainerNo, @ContainerQRCode, @PalletNo,@PalletQRCode,@WhseSite,@IsContainerSelected,@IsPalletSelected)
                            """;
                            await connection.ExecuteAsync(sql, new { OrderID,detail.ContainerNo,detail.ContainerQRCode,detail.PalletNo,detail.PalletQRCode,detail.WhseSite,detail.IsContainerSelected,detail.IsPalletSelected }, transaction);
                            sql = """
                                INSERT INTO [Equipment_PickList] (PartID, PartNumber, ShipDate, ShipTime,DockCode,pickStatus,PickSite,PickRequestID)
                                VALUES (@PartID, @PartNo, @DeliveryDate,@shipTime, @DockDoor,'open',@WhseSite,@OrderID)
                                """;
                            TimeSpan shipTime = TimeSpan.Zero; // Default to zero if DeliveryTime is null
                            switch(orderInfo.DeliveryTime)
                            {
                                case "8AM-10AM":
                                    shipTime = new TimeSpan(8, 0, 0); // 8:00 AM
                                    break;
                                case "10AM-12PM":
                                    shipTime = new TimeSpan(10, 0, 0); 
                                    break;
                                case "12PM-2PM":  
                                    shipTime = new TimeSpan(12, 0, 0); 
                                    break;
                                case "2PM-4PM":
                                    shipTime = new TimeSpan(14, 0, 0);
                                    break;
                                case "4PM-6PM":
                                    shipTime = new TimeSpan(16, 0, 0);
                                    break;
                                case "6PM-8PM":
                                    shipTime = new TimeSpan(18, 0, 0);
                                    break;
                                case "8PM-10PM":
                                    shipTime = new TimeSpan(20, 0, 0);
                                    break;
                                case "10PM-12AM":
                                    shipTime = new TimeSpan(22, 0, 0);
                                    break;
                                case "12AM-2AM":
                                    shipTime = new TimeSpan(0, 0, 0);
                                    break;
                            }
                            await connection.ExecuteAsync(sql, new { OrderID, detail.PartID, detail.PartNo, orderInfo.DeliveryDate, shipTime, orderInfo.DockDoor, detail.WhseSite }, transaction);
                        }
                        transaction.Commit();
                        return (true,OrderID);

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return (false,0);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            return (false, 0);
        }
    }

    public async Task<bool> HasPermission(int userID)
    {
        if (userID == 0)
            return false;
        string sql = "select * from Employees where EmployeeID=" + userID;
        string firstName = "";
        string lastName = "";

        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

              
                var userinfo = await connection.QueryFirstOrDefaultAsync(sql);
                if (userinfo == null)
                    return false;

                var userdata = (IDictionary<string, object>)userinfo;
                lastName = userdata["Last_Name"].ToString().ToLower();
                firstName = userdata["First_Name"].ToString().ToLower();

                string name = firstName + " " + lastName;
                switch (name)
                {
                    case "david kim":
                    case "rochelle symss":
                    case "zhibo liu":
                    case "zhibo lui":
                    case "sam shabo":

                    case "patrick stieler":
                    case "jarod coughenour":
                    case "clinton lucas":
                    case "jacob pate":
                    case "andreas hunt":
                    case "jeff brand":
                    //Canton
                    case "robert sajewski":
                    case "robert even":
                    case "jessica celima":
                        return true;
                }
                return false;

            }
        }
        catch (Exception ex)
        {
            return false;
        }
   
  
    }

    public async Task<List<PartCycle>> GetInventoryCycleInfoBatteryForOrder()
    {

        //In Receiving Area, but not recorded in Movements-----for the case: Not recording Receiving in Movements, that is direct make movements in Receiving operating
        string sql = """
                select PartID, PartNumber as PartNo, SkidsSerialno as SkidNo, ReceivedQty as PartQty, 1 as BinQty, LocationId, Location,ContainerNo,ContainerQRCode,PalletNo,PalletQRCode,EquipmentOp,SectionComp,ProcessEquip,SectionCompName
                from
                (select r.*,l.LocationID, l.LocationTitle as Location, m.BOL as ContainerNo, m.ASN_Number as ContainerQRCode,p.Description as PalletNo,p.PartNo as PalletQRCode,p.EquipmentOp,p.SectionComp,p.ProcessEquip,p.SectionCompName
                from ReceiveOrder r
                join ReceivingDetails rr on rr.ReceivingPartID = r.ReceivingPartID
                join [Receivings_Master] m on m.ReceivingID = rr.ReceivingID
                join LocationsWithinWarehouse l on l.LocationID = rr.LocationID
                join Parts p on p.PartID = r.PartID
                where (not exists(select 1 from ShippingLoadTransactions s where r.SkidsSerialno = s.SkidNo)
                and not exists(select 1 from EquipmentOrderDetail ss where r.SkidsSerialno = ss.PalletQRCode)
                and not exists(select 1 from InventoryMovementDetails m where r.SkidsSerialno = m.SourceScanBoxBin))
                ) as t
                where ContainerQRCode is not null
                """;



        sql += $"""
             UNION ALL
            select PartID, PartNumber as PartNo, SkidsSerialno as SkidNo, ReceivedQty as PartQty, 1 as BinQty, LocationId, Location,ContainerNo,ContainerQRCode,PalletNo,PalletQRCode,  EquipmentOp,SectionComp,ProcessEquip,SectionCompName
            from
            (select r.*,l.DestinationLocationID as LocationID, l.DestinationLocation  as Location, m.BOL as ContainerNo, m.ASN_Number as ContainerQRCode,p.Description as PalletNo,p.PartNo as PalletQRCode,p.EquipmentOp,p.SectionComp,p.ProcessEquip,p.SectionCompName
            from ReceiveOrder r
            join ReceivingDetails rr on rr.ReceivingPartID = r.ReceivingPartID
            join [Receivings_Master] m on m.ReceivingID = rr.ReceivingID
            join (SELECT c.*  FROM [InventoryMovementDetails] c 
                  WHERE c.[InventoryMovementDetailID] = 
                       (SELECT MAX([InventoryMovementDetailID])
                       FROM [InventoryMovementDetails] c2 WHERE c2.SourceScanBoxBin = c.SourceScanBoxBin 
                       and PartQuantity is not null)) as l on  l.SourceScanBoxBin = r.[SkidsSerialno]
            join Parts p on p.PartID = r.PartID
            where (not exists(select 1 from ShippingLoadTransactions s where r.SkidsSerialno = s.SkidNo)
            and not exists(select 1 from EquipmentOrderDetail ss where r.SkidsSerialno = ss.PalletQRCode)
            and  exists(select 1 from InventoryMovementDetails m where r.SkidsSerialno = m.SourceScanBoxBin))
            ) as t
            where ContainerQRCode is not null
            """;

        sql += " order by ContainerNo,Location, PalletNo";
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();


                var result = await connection.QueryAsync<PartCycle>(sql, commandTimeout: 5 * 60);
                if (result is not null)
                {
                    foreach (var item in result)
                    {
                        //if (item.Location == "Unknow For Rescan")
                         //   item.Location = "Unknown For Rescan";
                        item.DstLocationID = item.LocationID;
                        item.DstLocation = item.Location;
                        item.IsShipped = false;

                        if(item.Location.StartsWith("WHSE"))
                            item.WhseSite = "WHSE";
                        else
                            item.WhseSite = "SITE";
                    }
                    return result.ToList();
                }

            }
        }
        catch (Exception ex)
        {

        }

        return [];


    }

    public async Task<AspNetUsers?> GetUserInfoFromNetCore(string userName)
    {
        string sql = "select * from AspNetUsers where UserName = @userName";
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var user = await connection.QueryFirstOrDefaultAsync<AspNetUsers>(sql, new { userName });
                return user;

            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<List<string>> GetUserRoles(string userId)
    {
        string sql = """
            select r.RoleName from AspNetUserRoles ur
            join AspNetRoles r on r.Id = ur.RoleId
            where ur.UserId = @userId
            """;
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var roles = await connection.QueryAsync<string>(sql, new { userId });
                if (roles == null)
                    return [];
                return roles.ToList();
            }
        }
        catch (Exception ex)
        {
            return [];
        }
    }

    public async Task<AspNetUsers?> GetUserInfoFromHIS(string userName)
    {
        AspNetUsers ret = new();
        string sql = "select UserId as ID, UserName from aspnet_Users where UserName = @userName";
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                var user = await connection.QueryFirstOrDefaultAsync(sql, new {userName});
                if (user == null)
                    return null;
                var data = (IDictionary<string, object>)user;
                ret.Id = data["ID"].ToString();
                ret.UserName = data["UserName"].ToString(); ;
                
                
                sql = "select Password,PasswordSalt from aspnet_Membership where Userid='" + ret.Id + "'";
                var userinfo = await connection.QueryFirstOrDefaultAsync(sql);
                if (userinfo == null)
                    return null;

                var userdata = (IDictionary<string, object>)userinfo;
                ret.PasswordHash = userdata["Password"].ToString() + "|1|" + userdata["PasswordSalt"].ToString();
                return ret;
              
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<int> GetEmplyeeID(string userId)
    {
        string sql = "select EmployeeID from [Employees] where userid='" + userId + "'";
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                var users = await connection.QueryAsync(sql);
                if (users == null)
                    return 0;
                foreach (var user in users)
                {
                    return int.Parse(user.EmployeeID.ToString());
                }
                return 0;

            }
        }
        catch (Exception ex)
        {
            return 0;
        }
       
     
    }

    public async Task<List<EquipmentOrderInfo>> GetEquipmentOrderInfos(DateTime startDate, DateTime endDate, int mode)
    {
        endDate = endDate.AddDays(1); //Include the end date in the query
        string sql;
         if(mode == 1)
            sql = """
            select * from EquipmentOrderInfo eo
            where eo.DeliveryDate >= @startDate and eo.DeliveryDate < @endDate
            order by eo.DeliveryDate desc, eo.DeliveryTime desc
            """;
        else
            sql = """
            select * from EquipmentOrderInfo eo
            where eo.CreateTime >= @startDate and eo.CreateTime < @endDate 
            order by eo.CreateTime desc
            """;
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                var infos = await connection.QueryAsync<EquipmentOrderInfo>(sql, new { startDate ,endDate});
                if (infos == null)
                    return [];

                return infos.ToList(); ;

            }
        }
        catch (Exception)
        {

        }
        return [];
    }

    public async Task<List<EquipmentOrderDetail>> GetEquipmentOrderInfos(int OrderID)
    {
        string sql = """
            select * from [EquipmentOrderDetail] eo
            where OrderID = @OrderID
            order by ID
            """;
    
        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                var infos = await connection.QueryAsync<EquipmentOrderDetail>(sql, new { OrderID });
                if (infos == null)
                    return [];

                return infos.ToList(); ;

            }
        }
        catch (Exception)
        {

        }
        return [];
    }

    public async Task<List<LocationInfo>> GetLocationInfoListBattery()
    {
        string sql = """
            SELECT ID, LocationType, LocationCount
            from (
               select 1 as ID,'WHSE 40ft' as LocationType, count(*) as LocationCount from LocationsWithinWarehouse where LocationTitle like 'WHSE%' and LocationTitle <> 'WHSE' and LocationTitle not like '%BOUND%' and LocationTitle <>'WHSE-QUARANTINE' and LocationCode like '40%' 
               UNION
               select 2 as ID,'WHSE 20ft' as LocationType, count(*) as LocationCount from LocationsWithinWarehouse where LocationTitle like 'WHSE%' and LocationTitle <> 'WHSE' and LocationTitle not like '%BOUND%' and LocationTitle <>'WHSE-QUARANTINE' and LocationCode like '20%' 
               UNION
               select 3 as ID,'T1' as LocationType, count(*) as LocationCount from LocationsWithinWarehouse where (LocationTitle like 'TA%' or LocationTitle like 'TB%') and LocationTitle <> 'TA' and LocationTitle <> 'TB' and LocationTitle not like '%BOUND%' and LocationTitle <>'T1-QUARANTINE' 
               UNION 
               select 4 as ID,'T2' as LocationType, count(*) as LocationCount from LocationsWithinWarehouse where (LocationTitle like 'TC%' or LocationTitle like 'TD%') and LocationTitle <> 'TC' and LocationTitle <> 'TD' and LocationTitle not like '%BOUND%' and LocationTitle <>'T2-QUARANTINE'
               UNION 
               select 5 as ID, 'T3' as LocationType, count(*) as LocationCount from LocationsWithinWarehouse where (LocationTitle like 'TE%' or LocationTitle like 'TF%') and LocationTitle <> 'TE' and LocationTitle <> 'TF' and LocationTitle not like '%BOUND%' and LocationTitle <>'T3-QUARANTINE'
               UNION 
               select 6 as ID,'LD' as LocationType, count(*) as LocationCount from LocationsWithinWarehouse where LocationTitle like 'LD%'  and LocationTitle <> 'LD' and LocationTitle not like '%BOUND%' and LocationTitle <>'LD-QUARANTINE'
               UNION 
               select 7 as ID,'P' as LocationType, count(*) as LocationCount from LocationsWithinWarehouse where LocationTitle like 'P%'  and LocationTitle <> 'P' and LocationTitle not like '%BOUND%' and LocationTitle <>'P-QUARANTINE'
               UNION 
               select 8 as ID,'Ring Road' as LocationType, count(*) as LocationCount from LocationsWithinWarehouse where LocationTitle like 'RR%'  and LocationTitle <> 'RR' and LocationTitle not like '%BOUND%' and LocationTitle <>'P-QUARANTINE'
                ) as t
            where LocationCount<>0
            ORDER by ID
            """;

        try
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                var infos = await connection.QueryAsync<LocationInfo>(sql);
                if (infos == null)
                    return [];

                return infos.ToList(); ;

            }
        }
        catch (Exception)
        {

        }
        return [];
    }
}

//In ADO.NET, correctly handling null is a constant source of confusion.
//The key point in dapper is that you don't have to; it deals with it all internally.

//1. parameter values that are null are correctly sent as DBNull.Value
//2. values read that are null are presented as null, or (in the case of mapping to a known type) simply ignored (leaving their type-based default)