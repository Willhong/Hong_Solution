using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using CoreScanner;

namespace Hong_Solution
{
    public class ZebraBarcode
    {
        CCoreScannerClass m_pCoreScanner = new CCoreScannerClass();
        bool m_bSuccessOpen;//Is open success
        XmlReader m_xml;
        int m_nTotalScanners;
        Scanner[] m_arScanners;
        public string BCR_Text;
        short[] m_arScannerTypes;
        short m_nNumberOfTypes;
        bool[] m_arSelectedTypes;
        List<string> claimlist = new List<string>();

        string SerialNumber = "";
        string ScannerID = "";
        #region BCR Param
        public bool isBCROpen;
        public bool isBCREventRegistered;

        public int BCR_appHandle;
        public const short BCR_NumberOfScannerTypes = 1;
        short[] BCR_scannerTypes;
        public int BCR_status = -1;
        public string inXml;
        public string outXml;
        public string BCR_Data;


        #endregion

        
        public ZebraBarcode()
        {
            try
            {
                m_pCoreScanner = new CoreScanner.CCoreScannerClass();
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                m_pCoreScanner = new CoreScanner.CCoreScannerClass();
            }

            m_arScanners = new Scanner[BCRDef.MAX_NUM_DEVICES];
            for (int i = 0; i < BCRDef.MAX_NUM_DEVICES; i++)
            {
                Scanner scanr = new Scanner();
                m_arScanners.SetValue(scanr, i);
            }

            m_arScannerTypes = new short[BCRDef.TOTAL_SCANNER_TYPES];
            m_arSelectedTypes = new bool[BCRDef.TOTAL_SCANNER_TYPES];

            m_xml = new XmlReader();

            m_pCoreScanner.BarcodeEvent += new CoreScanner._ICoreScannerEvents_BarcodeEventEventHandler(OnBarcodeEvent);
            m_pCoreScanner.PNPEvent += new CoreScanner._ICoreScannerEvents_PNPEventEventHandler(OnPNPEvent);

            registerForEvents();


        }



        #region Event Class
        public string GetBarcodeText(string strXml)
        {
            System.Diagnostics.Debug.WriteLine("Initial XML" + strXml);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(strXml);

            string strData = String.Empty;
            string barcode = xmlDoc.DocumentElement.GetElementsByTagName("datalabel").Item(0).InnerText;
            string symbology = xmlDoc.DocumentElement.GetElementsByTagName("datatype").Item(0).InnerText;
            string[] numbers = barcode.Split(' ');

            foreach (string number in numbers)
            {
                if (String.IsNullOrEmpty(number))
                {
                    break;
                }

                strData += ((char)Convert.ToInt32(number, 16)).ToString();
            }

            return strData;
        }
        public string GetRegUnregIDs(out int nEvents)
        {
            string strIDs = "";
            nEvents = BCRDef.NUM_SCANNER_EVENTS;
            strIDs = BCRDef.SUBSCRIBE_BARCODE.ToString();
            strIDs += "," + BCRDef.SUBSCRIBE_IMAGE.ToString();
            strIDs += "," + BCRDef.SUBSCRIBE_VIDEO.ToString();
            strIDs += "," + BCRDef.SUBSCRIBE_RMD.ToString();
            strIDs += "," + BCRDef.SUBSCRIBE_PNP.ToString();
            strIDs += "," + BCRDef.SUBSCRIBE_OTHER.ToString();
            return strIDs;
        }
        public void registerForEvents()
        {
            if (IsConnected())
            {
                int nEvents = 0;
                string strEvtIDs = GetRegUnregIDs(out nEvents);
                string inXml = "<inArgs>" +
                                    "<cmdArgs>" +
                                    "<arg-int>" + nEvents + "</arg-int>" +
                                    "<arg-int>" + strEvtIDs + "</arg-int>" +
                                    "</cmdArgs>" +
                                    "</inArgs>";

                int opCode = BCRDef.REGISTER_FOR_EVENTS;
                string outXml = "";
                int status = BCRDef.STATUS_FALSE;
                ExecCmd(opCode, ref inXml, out outXml, out status);
                //FormMain.DebugPrint(String.Format("{0}:REGISTER_FOR_EVENTS", status));
            }
        }
        public void OnBarcodeEvent(short eventType, ref string scanData)
        {
            try
            {
                string tmpScanData = scanData;

                //FormMain.DebugPrint("Barcode Event fired");
                BCR_Text = GetBarcodeText(tmpScanData);
                //FormMain.Invoke(new Action(delegate ()
                //{
                //    FormMain.FormTitle.TitleLblID.Text = FormMain.BCRZebra.BCR_Text;
                //}));


            }
            catch (Exception e)
            {
                //FormMain.DebugPrint(e.Message);
            }
        }
        public void OnPNPEvent(short eventType, ref string ppnpData)
        {
            try
            {
                Scanner[] arScanr;
                string sStatus = String.Empty;
                m_xml.ReadXmlString_AttachDetachMulti(ppnpData, out arScanr, out sStatus);

                foreach (Scanner scnTmp in arScanr)
                {
                    if (null != scnTmp)
                    {
                        if ((BCRDef.SCANNER_ATTACHED == eventType && sStatus.Equals("1")) ||
                            (BCRDef.SCANNER_DETTACHED == eventType && sStatus.Equals("0")))
                        {
                            int nAdd = 0;
                            if (0 == m_nTotalScanners)//when there's no scanners in the list
                            {
                                nAdd = 1;
                            }
                            for (int i = 0; i < m_nTotalScanners; i++)
                            {
                                nAdd = 1;

                                Scanner scan = (Scanner)m_arScanners.GetValue(i);
                                if (scan.SCANNERID == scnTmp.SCANNERID)
                                {
                                    nAdd = 2;//already exist ...don't add
                                    if (BCRDef.SCANNER_ATTACHED == eventType)
                                    {
                                        //FormMain.SetDisplayStateColor(FormMain.FormTitle.TitleLblBCR1, true);

                                    }
                                    else if (BCRDef.SCANNER_DETTACHED == eventType)
                                    {
                                        //FormMain.DebugPrint("Scanner Detached Event fired");
                                        //FormMain.SetDisplayStateColor(FormMain.FormTitle.TitleLblBCR1, false);
                                        scan.ClearValues();
                                        if (i < (m_nTotalScanners - 1))//not the last item of array
                                        {
                                            for (int k = i; k < m_nTotalScanners; k++)
                                            {
                                                if (k == (m_nTotalScanners - 1))//last item of array
                                                {
                                                    m_arScanners.SetValue((Scanner)scan, k);//empty scanner object
                                                }
                                                else
                                                {
                                                    Scanner tmp = (Scanner)m_arScanners.GetValue(k + 1);
                                                    m_arScanners.SetValue((Scanner)tmp, k);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            m_arScanners.SetValue((Scanner)scan, i);//empty scanner object
                                        }
                                        m_nTotalScanners--;
                                    }
                                    break;
                                }
                            }
                            if (1 == nAdd)
                            {
                                //FormMain.SetDisplayStateColor(FormMain.FormTitle.TitleLblBCR1, true);
                                m_arScanners.SetValue((Scanner)scnTmp, m_nTotalScanners);
                                m_nTotalScanners++;

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Connect Class

        public void GetSelectedScannerTypes()
        {
            m_nNumberOfTypes = 0;
            for (int index = 0, k = 0; index < BCRDef.TOTAL_SCANNER_TYPES; index++)
            {
                if (m_arSelectedTypes[index])
                {
                    m_nNumberOfTypes++;
                    switch (index + 1)
                    {
                        case BCRDef.SCANNER_TYPES_ALL:
                            m_arScannerTypes[k++] = BCRDef.SCANNER_TYPES_ALL;
                            return;

                        case BCRDef.SCANNER_TYPES_SNAPI:
                            m_arScannerTypes[k++] = BCRDef.SCANNER_TYPES_SNAPI;
                            break;

                        case BCRDef.SCANNER_TYPES_SSI:
                            m_arScannerTypes[k++] = BCRDef.SCANNER_TYPES_SSI;
                            break;

                        case BCRDef.SCANNER_TYPES_NIXMODB:
                            m_arScannerTypes[k++] = BCRDef.SCANNER_TYPES_NIXMODB;
                            break;

                        case BCRDef.SCANNER_TYPES_RSM:
                            m_arScannerTypes[k++] = BCRDef.SCANNER_TYPES_RSM;
                            break;

                        case BCRDef.SCANNER_TYPES_IMAGING:
                            m_arScannerTypes[k++] = BCRDef.SCANNER_TYPES_IMAGING;
                            break;

                        case BCRDef.SCANNER_TYPES_IBMHID:
                            m_arScannerTypes[k++] = BCRDef.SCANNER_TYPES_IBMHID;
                            break;

                        case BCRDef.SCANNER_TYPES_HIDKB:
                            m_arScannerTypes[k++] = BCRDef.SCANNER_TYPES_HIDKB;
                            break;

                        case BCRDef.SCALE_TYPES_SSI_BT:
                            m_arScannerTypes[k++] = BCRDef.SCALE_TYPES_SSI_BT;
                            break;

                        default:
                            break;
                    }
                }
            }
        }
        public string GetScannerIDXml()
        {
            string strInXml = "";
            //if (0 < lstvScanners.Items.Count && 1 == lstvScanners.SelectedItems.Count && -1 < lstvScanners.SelectedItems[0].Index)
            //{
            strInXml = "<inArgs>" +
                            "<scannerID>" + ScannerID + "</scannerID>" +
                            "</inArgs>";
            //}
            return strInXml;
        }
        public void ExecCmd(int opCode, ref string inXml, out string outXml, out int status)
        {
            outXml = "";
            status = BCRDef.STATUS_FALSE;
            if (m_bSuccessOpen)
            {
                try
                {
                    //if (!chkAsync.Checked)  
                    //{
                    m_pCoreScanner.ExecCommand(opCode, ref inXml, out outXml, out status);
                    //}
                    //else
                    //{
                    //    m_pCoreScanner.ExecCommandAsync(opCode, ref inXml, out status);
                    //}
                }
                catch (Exception ex)
                {
                    //DisplayResult(status, "EXEC_COMMAND");
                    //UpdateResults("..." + ex.Message.ToString());
                }
            }
        }
        public void OnEnableScanner()
        {
            if (IsConnected())
            {
                ShowScanners();
                int iOpcode = BCRDef.DEVICE_SCAN_ENABLE;
                int status = BCRDef.STATUS_FALSE;
                string strSerialNumber = SerialNumber;
                string inXml = GetScannerIDXml();
                string outXml = "";
                string strCmd = "SCAN_ENABLE";

                ExecCmd(iOpcode, ref inXml, out outXml, out status);

                //FormMain.DebugPrint(status + ": " + strCmd);
            }
        }
        public void OnDisableScanner()
        {
            if (IsConnected())
            {
                ShowScanners();
                int iOpcode = BCRDef.DEVICE_SCAN_DISABLE;
                int status = BCRDef.STATUS_FALSE;
                string strSerialNumber = SerialNumber;
                string inXml = GetScannerIDXml();
                string outXml = "";
                string strCmd = "SCAN_DISABLE";

                ExecCmd(iOpcode, ref inXml, out outXml, out status);

                //FormMain.DebugPrint(status + ": " + strCmd);

            }
        }

        public bool IsConnected()
        {
            return m_bSuccessOpen;
        }
        public void ShowScanners()
        {
            int opCode = BCRDef.CLAIM_DEVICE;
            string inXml = String.Empty;
            string outXml = "";
            int status = BCRDef.STATUS_FALSE;
            m_arScanners.Initialize();
            if (m_bSuccessOpen)
            {
                m_nTotalScanners = 0;
                short numOfScanners = 0;
                int nScannerCount = 0;
                string outXML = "";
                int[] scannerIdList = new int[BCRDef.MAX_NUM_DEVICES];
                try
                {
                    m_pCoreScanner.GetScanners(out numOfScanners, scannerIdList, out outXML, out status);

                    //FormMain.DebugPrint(String.Format("{0}: GET_SCANNERS", status));

                    if (BCRDef.STATUS_SUCCESS == status)
                    {
                        m_nTotalScanners = numOfScanners;
                        m_xml.ReadXmlString_GetScanners(outXML, m_arScanners, numOfScanners, out nScannerCount);

                        Scanner objScanner = (Scanner)m_arScanners.GetValue(0);
                        string[] strItems = new string[] { "", "", "", "", "" };
                        SerialNumber = objScanner.SERIALNO;
                        ScannerID = objScanner.SCANNERID;
                        inXml = "<inArgs><scannerID>" + objScanner.SCANNERID + "</scannerID></inArgs>";


                        ExecCmd(opCode, ref inXml, out outXml, out status);

                    }

                }
                catch (Exception ex)
                {
                }
            }
        }
        public void BCR_Open()
        {
            if (m_bSuccessOpen)
            {
                return;
            }
            GetSelectedScannerTypes();

            BCR_appHandle = 0;
            BCR_scannerTypes = new short[BCR_NumberOfScannerTypes];
            BCR_scannerTypes[0] = (short)BCRDef.ScannerType.All; //  All scanner types
            BCR_status = -1;


            m_pCoreScanner.Open(BCR_appHandle, // Application handle     
                    BCR_scannerTypes,                 // Array of scanner types    
                    BCR_NumberOfScannerTypes,         // Length of scanner types array 
                    out BCR_status);                  // Command execution success/failure return status 

            if (BCR_status == (int)BCRDef.Status.Success)
            {
                //FormMain.g_bBCROpen = true;
                m_bSuccessOpen = true;
                registerForEvents();
                //FormMain.SetDisplayStateColor(FormMain.FormTitle.TitleLblBCR1, true);
                //FormMain.DebugPrint("BCR Connected");


            }
            else
            {

                //FormMain.SetDisplayStateColor(FormMain.FormTitle.TitleLblBCR1, false);

                //return false;
            }
        }
        public void Connect()
        {
            if (m_bSuccessOpen)
            {
                return;
            }
            int appHandle = 0;
            GetSelectedScannerTypes();
            int status = BCRDef.STATUS_FALSE;

            try
            {
                m_pCoreScanner.Open(appHandle, m_arScannerTypes, m_nNumberOfTypes, out status);
                //FormMain.DebugPrint(String.Format("{0}:BCR OPEN", status));
                if (BCRDef.STATUS_SUCCESS == status)
                {
                    m_bSuccessOpen = true;
                    //FormMain.SetDisplayStateColor(FormMain.FormTitle.TitleLblBCR1, true);

                }
            }
            catch (Exception exp)
            {
            }
        }
        public void Disconnect()
        {
            if (m_bSuccessOpen)
            {
                int appHandle = 0;
                int status = BCRDef.STATUS_FALSE;
                try
                {
                    m_pCoreScanner.Close(appHandle, out status);
                    //FormMain.DebugPrint(string.Format("{0}:CLOSE", status));
                    if (BCRDef.STATUS_SUCCESS == status)
                    {
                        m_bSuccessOpen = false;
                        m_nTotalScanners = 0;
                    }
                }
                catch (Exception exp)
                {
                }
            }
        }

        #endregion

        #region Scanner Class
        class Scanner
        {
            #region constants
            public const string SCANNER_SNAPI = "SNAPI";
            public const string SCANNER_SSI = "SSI";
            public const string SCANNER_NIXMODB = "NIXMODB";
            public const string SCANNER_IBMHID = "USBIBMHID";
            public const string SCANNER_IBMTT = "USBIBMTT";
            public const string SCALE_IBM = "USBIBMSCALE";
            public const string SCANNER_SSI_BT = "SSI_BT";
            public const string SCANNER_OPOS = "USBOPOS";
            public const string SCANNER_HIDKB = "USBHIDKB";
            public const string CAMERA_UVC = "UVC_CAMERA";

            public const int MAX_ATTRIBUTE_COUNT = 2000;
            public const int MAX_ATTRIBUTE_ITEMS = 5;//att-id, att-type, att-prop, att-value, att-name// ( is not used) 
            public const int POS_ATTR_ID = 0;
            public const int POS_ATTR_TYPE = 1;
            public const int POS_ATTR_PROPERTY = 2;
            public const int POS_ATTR_VALUE = 3;
            public const int POS_ATTR_NAME = 4;

            public const string TAG_OUTARGS = "outArgs";
            public const string TAG_ARG_XML = "arg-xml";
            public const string TAG_DISCOVERY = "discovery";
            public const string TAG_STATUS = "status";
            public const string TAG_OPCODE = "opcode";

            public const string TAG_SCANNER = "scanner";
            public const string TAG_SCANNER_SNAPI = SCANNER_SNAPI;
            public const string TAG_SCANNER_SSI = SCANNER_SSI;
            public const string TAG_SCANNER_NIXMODB = SCANNER_NIXMODB;
            public const string TAG_SCANNER_IBMHID = SCANNER_IBMHID;
            public const string TAG_SCANNER_OPOS = SCANNER_OPOS;
            public const string TAG_SCANNER_HIDKB = SCANNER_HIDKB;
            public const string TAG_SCANNER_IMBTT = SCANNER_IBMTT;
            public const string TAG_SCALE_IBM = SCALE_IBM;
            public const string TAG_SCANNER_SSI_BT = SCANNER_SSI_BT;

            public const string TAG_SCANNER_ID = "scannerID";
            public const string TAG_SCANNER_TYPE = "type";
            public const string TAG_SCANNER_SERIALNUMBER = "serialnumber";
            public const string TAG_SCANNER_MODELNUMBER = "modelnumber";
            public const string TAG_SCANNER_GUID = "GUID";
            public const string TAG_SCANNER_PORT = "port";
            public const string TAG_SCANNER_VID = "VID";
            public const string TAG_SCANNER_PID = "PID";
            public const string TAG_SCANNER_DOM = "DoM";
            public const string TAG_SCANNER_FW = "firmware";

            public const string TAG_ATTRIBUTE = "attribute";
            public const string TAG_ATTR_ID = "id";
            public const string TAG_ATTR_NAME = "name";
            public const string TAG_ATTR_TYPE = "datatype";
            public const string TAG_ATTR_PROPERTY = "permission";
            public const string TAG_ATTR_VALUE = "value";

            public const string TAG_SCALE_WEIGHT = "weight";
            public const string TAG_SCALE_WEIGHT_MODE = "weight_mode";
            public const string TAG_SCALE_STATUS = "status";
            public const string TAG_SCALE_RAWDATA = "rawdata";

            public const string TAG_SNAPI_PARAM_VAL = "param_value";
            #endregion

            public class RSMAttribute
            {
                public string ID;
                public string Type;
                public string property;
                public string value;
                public string name;
            }

            /// <summary>
            /// 5 Diamentional string array to store data retrieved from RSM_GET_ATTR call
            /// </summary>
            public Array m_arAttributes;  //string[,] m_arAttributes;
            public RSMAttribute m_rsmAttribute;
            #region Private Members
            private int handle;
            private string scannerName;// now scannerName = scannerID
            private string scannerID;// a unique id
            private string scannerType;//SCANNER_SNAPI, SCANNER_SSI
            private string serialNo;
            private string modelNo;
            private string guid;
            private string port;
            private string firmware;
            private string mnfdate; //manufacture date
            private bool claimed;//scanner is claimed by this client-app
            private bool useHID; // Scanner is using HID channel for Binary Data transfer
            #endregion

            public Scanner()
            {
                m_arAttributes = Array.CreateInstance(typeof(String), MAX_ATTRIBUTE_COUNT, MAX_ATTRIBUTE_ITEMS);
                m_rsmAttribute = new RSMAttribute();
                ClearValues();
            }

            //public RSMAttribute GetAttribute(int attributeID)
            //{
            //    RSMAttribute tempAttr;
            //    return tempAttr;
            //}
            //public void SetAttribute(RSMAttribute attribute)
            //{

            //}

            /// <summary>
            /// Clear the public properties of the object
            /// </summary>
            public void ClearValues()
            {
                CLAIMED = false;
                useHID = false;
                SCANNERNAME = "";
                SCANNERID = "";
                SERIALNO = "";
                MODELNO = "";
                GUID = "";
                SCANNERTYPE = "";
                SCANNERMNFDATE = "";
                SCANNERFIRMWARE = "";
            }

            #region Public Getters and Setters
            public string SCANNERMNFDATE
            {
                get { return mnfdate; }
                set { mnfdate = value; }
            }
            public string SCANNERFIRMWARE
            {
                get { return firmware; }
                set { firmware = value; }
            }
            public string SCANNERNAME
            {
                get { return scannerName; }
                set { scannerName = value; }
            }
            public string SCANNERTYPE
            {
                get { return scannerType; }
                set { scannerType = value; }
            }
            public int HANDLE
            {
                get { return handle; }
                set { handle = value; }
            }
            public string SCANNERID
            {
                get { return scannerID; }
                set { scannerID = value; }
            }
            public string SERIALNO
            {
                get { return serialNo; }
                set { serialNo = value; }
            }
            public string MODELNO
            {
                get { return modelNo; }
                set { modelNo = value; }
            }
            public string GUID
            {
                get { return guid; }
                set { guid = value; }
            }
            public string PORT
            {
                get { return port; }
                set { port = value; }
            }
            public bool CLAIMED
            {
                get { return claimed; }
                set { claimed = value; }
            }
            public bool UseHID
            {
                get { return useHID; }
                set { useHID = value; }
            }
            #endregion
        }

        #endregion

        #region XmlReader Class
        class XmlReader
        {
            const string TAG_MAXCOUNT = "maxcount";
            const string TAG_PROGRESS = "progress";
            const string TAG_PNP = "pnp";

            public void ReadXmlString_GetScanners(string strXml, Scanner[] arScanner, int nTotal, out int nScannerCount)
            {
                nScannerCount = 0;
                if (1 > nTotal || String.IsNullOrEmpty(strXml))
                {
                    return;
                }
                try
                {
                    XmlTextReader xmlRead = new XmlTextReader(new StringReader(strXml));
                    // Skip non-significant whitespace   
                    xmlRead.WhitespaceHandling = WhitespaceHandling.Significant;

                    string sElementName = "", sElmValue = "";
                    Scanner scanr = null;
                    int nIndex = 0;
                    bool bScanner = false;
                    while (xmlRead.Read())
                    {
                        switch (xmlRead.NodeType)
                        {
                            case XmlNodeType.Element:
                                sElementName = xmlRead.Name;
                                if (Scanner.TAG_SCANNER == sElementName)
                                {
                                    bScanner = false;
                                }

                                string strScannerType = xmlRead.GetAttribute(Scanner.TAG_SCANNER_TYPE);
                                if (xmlRead.HasAttributes && (
                                    (Scanner.TAG_SCANNER_SNAPI == strScannerType) ||
                                    (Scanner.TAG_SCANNER_SSI == strScannerType) ||
                                    (Scanner.TAG_SCANNER_NIXMODB == strScannerType) ||
                                    (Scanner.TAG_SCANNER_IBMHID == strScannerType) ||
                                    (Scanner.TAG_SCANNER_OPOS == strScannerType) ||
                                    (Scanner.TAG_SCANNER_IMBTT == strScannerType) ||
                                    (Scanner.TAG_SCALE_IBM == strScannerType) ||
                                    (Scanner.SCANNER_SSI_BT == strScannerType) ||
                                    (Scanner.CAMERA_UVC == strScannerType) ||
                                    (Scanner.TAG_SCANNER_HIDKB == strScannerType)))//n = xmlRead.AttributeCount;
                                {
                                    if (arScanner.GetLength(0) > nIndex)
                                    {
                                        bScanner = true;
                                        scanr = (Scanner)arScanner.GetValue(nIndex++);
                                        if (null != scanr)
                                        {
                                            scanr.ClearValues();
                                            nScannerCount++;
                                            scanr.SCANNERTYPE = strScannerType;
                                        }
                                    }
                                }
                                break;

                            case XmlNodeType.Text:
                                if (bScanner && (null != scanr))
                                {
                                    sElmValue = xmlRead.Value;
                                    switch (sElementName)
                                    {
                                        case Scanner.TAG_SCANNER_ID:
                                            scanr.SCANNERID = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_SERIALNUMBER:
                                            scanr.SERIALNO = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_MODELNUMBER:
                                            scanr.MODELNO = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_GUID:
                                            scanr.GUID = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_PORT:
                                            scanr.PORT = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_FW:
                                            scanr.SCANNERFIRMWARE = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_DOM:
                                            scanr.SCANNERMNFDATE = sElmValue;
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            /*
            public void ReadXmlString_AttachDetachSingle(string strXml, out Scanner scanr, out string sStatus)
            {
                scanr = null;
                sStatus = "";
                string sPnp = "";
                if ("" == strXml || null == strXml)
                    return;
                try
                {
                    XmlTextReader xmlRead = new XmlTextReader(new StringReader(strXml));
                    // Skip non-significant whitespace   
                    xmlRead.WhitespaceHandling = WhitespaceHandling.Significant;

                    string sElementName = "", sElmValue = "";
                    bool bScanner = false;
                    int nScannerCount = 0;//for multiple scanners as in cradle+cascaded
                    while (xmlRead.Read())
                    {
                        switch (xmlRead.NodeType)
                        {
                            case XmlNodeType.Element:
                                sElementName = xmlRead.Name;
                                if (Scanner.TAG_SCANNER_ID == sElementName)
                                {
                                    nScannerCount++;
                                    scanr = new Scanner();
                                    bScanner = true;
                                }
                                break;
                            case XmlNodeType.Text:
                                if (bScanner && (null != scanr))
                                {
                                    sElmValue = xmlRead.Value;
                                    switch (sElementName)
                                    {
                                        case Scanner.TAG_SCANNER_ID:
                                            scanr.SCANNERID = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_SERIALNUMBER:
                                            scanr.SERIALNO = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_MODELNUMBER:
                                            scanr.MODELNO = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_GUID:
                                            scanr.GUID = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_TYPE:
                                            scanr.SCANNERTYPE = sElmValue;
                                            break;
                                        case Scanner.TAG_STATUS:
                                            sStatus = sElmValue;
                                            break;
                                        case TAG_PNP:
                                            sPnp = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_FW:
                                            scanr.SCANNERFIRMWARE = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_DOM:
                                            scanr.SCANNERMNFDATE = sElmValue;
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
    */
            public void ReadXmlString_AttachDetachMulti(string strXml, out Scanner[] arScanr, out string sStatus)
            {
                arScanr = new Scanner[8];
                for (int index = 0; index < 5; index++)
                {
                    arScanr.SetValue(null, index);
                }

                sStatus = "";
                if (String.IsNullOrEmpty(strXml))
                {
                    return;
                }

                try
                {
                    XmlTextReader xmlRead = new XmlTextReader(new StringReader(strXml));
                    // Skip non-significant whitespace   
                    xmlRead.WhitespaceHandling = WhitespaceHandling.Significant;

                    string sElementName = "", sElmValue = "";
                    bool bScanner = false;
                    int nScannerCount = 0; //for multiple scanners as in cradle+cascaded
                    int nIndex = 0;
                    while (xmlRead.Read())
                    {
                        switch (xmlRead.NodeType)
                        {
                            case XmlNodeType.Element:
                                sElementName = xmlRead.Name;
                                string strScannerType = xmlRead.GetAttribute(Scanner.TAG_SCANNER_TYPE);
                                if (xmlRead.HasAttributes && (
                                    (Scanner.TAG_SCANNER_SNAPI == strScannerType) ||
                                    (Scanner.TAG_SCANNER_SSI == strScannerType) ||
                                    (Scanner.TAG_SCANNER_IBMHID == strScannerType) ||
                                    (Scanner.TAG_SCANNER_OPOS == strScannerType) ||
                                    (Scanner.TAG_SCANNER_IMBTT == strScannerType) ||
                                    (Scanner.TAG_SCALE_IBM == strScannerType) ||
                                    (Scanner.SCANNER_SSI_BT == strScannerType) ||
                                    (Scanner.CAMERA_UVC == strScannerType) ||
                                    (Scanner.TAG_SCANNER_HIDKB == strScannerType)))//n = xmlRead.AttributeCount;
                                {
                                    nIndex = nScannerCount;
                                    arScanr.SetValue(new Scanner(), nIndex);
                                    nScannerCount++;
                                    arScanr[nIndex].SCANNERTYPE = strScannerType;
                                }
                                if ((null != arScanr[nIndex]) && Scanner.TAG_SCANNER_ID == sElementName)
                                {
                                    bScanner = true;
                                }
                                break;

                            case XmlNodeType.Text:
                                if (bScanner && (null != arScanr[nIndex]))
                                {
                                    sElmValue = xmlRead.Value;
                                    switch (sElementName)
                                    {
                                        case Scanner.TAG_SCANNER_ID:
                                            arScanr[nIndex].SCANNERID = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_SERIALNUMBER:
                                            arScanr[nIndex].SERIALNO = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_MODELNUMBER:
                                            arScanr[nIndex].MODELNO = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_GUID:
                                            arScanr[nIndex].GUID = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_TYPE:
                                            arScanr[nIndex].SCANNERTYPE = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_FW:
                                            arScanr[nIndex].SCANNERFIRMWARE = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_DOM:
                                            arScanr[nIndex].SCANNERMNFDATE = sElmValue;
                                            break;
                                        case Scanner.TAG_STATUS:
                                            sStatus = sElmValue;
                                            break;
                                        case TAG_PNP:
                                            if ("0" == sElmValue)
                                            {
                                                arScanr[nIndex] = null;
                                                nScannerCount--;
                                            }
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            private void GetAttributePos(string strNumber, Scanner scanr, out int nIndex)
            {
                nIndex = -1;
                for (int index = 0; index < Scanner.MAX_ATTRIBUTE_COUNT; index++)
                {
                    string strInNumber = scanr.m_arAttributes.GetValue(index, Scanner.POS_ATTR_ID).ToString();
                    if (strNumber == strInNumber)
                    {
                        nIndex = index;
                        break;
                    }
                }
            }

            public void ReadXmlString_RsmAttrGet(string strXml, Scanner[] arScanner, out Scanner scanr, out int nIndex, out int nAttrCount, out int nOpCode)
            {
                nIndex = -1;
                nAttrCount = 0;
                scanr = null;
                nOpCode = -1;
                if (String.IsNullOrEmpty(strXml))
                {
                    return;
                }

                try
                {
                    XmlTextReader xmlRead = new XmlTextReader(new StringReader(strXml));
                    // Skip non-significant whitespace   
                    xmlRead.WhitespaceHandling = WhitespaceHandling.Significant;

                    string sElementName = "", sElmValue = "";
                    bool bValid = false, bFirst = false;
                    while (xmlRead.Read())
                    {
                        switch (xmlRead.NodeType)
                        {
                            case XmlNodeType.Element:
                                sElementName = xmlRead.Name;
                                if (Scanner.TAG_SCANNER_ID == sElementName)
                                {
                                    bValid = true;
                                    bFirst = true;
                                }
                                // for old att_getall.xml ....since name is not used(user can refer data-dictionary)  
                                else if (bValid && Scanner.TAG_ATTRIBUTE == sElementName && xmlRead.HasAttributes && (1 == xmlRead.AttributeCount))
                                {
                                    sElmValue = xmlRead.GetAttribute("name");
                                    if (null != scanr)
                                    {
                                        scanr.m_arAttributes.SetValue(sElmValue, nAttrCount, Scanner.POS_ATTR_NAME);
                                    }
                                }
                                break;

                            case XmlNodeType.Text:
                                if (bValid)
                                {
                                    sElmValue = xmlRead.Value;
                                    if (bFirst && Scanner.TAG_SCANNER_ID == sElementName)
                                    {
                                        bFirst = false;
                                        foreach (Scanner scanrTmp in arScanner)
                                        {
                                            if ((null != scanrTmp) &&
                                                 (sElmValue == scanrTmp.SCANNERID))
                                            {
                                                scanr = scanrTmp;
                                                break;
                                            }
                                        }
                                    }
                                    else if (null != scanr)
                                    {
                                        switch (sElementName)
                                        {
                                            case Scanner.TAG_OPCODE:
                                                nOpCode = Int32.Parse(sElmValue);
                                                if (!(BCRDef.RSM_ATTR_GET == nOpCode))
                                                {
                                                    return;
                                                }
                                                break;

                                            case Scanner.TAG_ATTRIBUTE:
                                                if (BCRDef.RSM_ATTR_GETALL == nOpCode)
                                                {
                                                    //scanr.m_arAttributes.SetValue(sElmValue, nAttrCount, Scanner.POS_ATTR_ID);
                                                    //scanr.rsmAttribute.ID = sElmValue;
                                                    nAttrCount++;
                                                }
                                                break;

                                            case Scanner.TAG_ATTR_ID:
                                                nIndex = -1;
                                                //GetAttributePos(sElmValue, scanr, out nIndex);
                                                scanr.m_rsmAttribute.ID = sElmValue;
                                                break;

                                            case Scanner.TAG_ATTR_NAME:
                                                //if (-1 != nIndex)
                                                //{
                                                //    scanr.m_arAttributes.SetValue(sElmValue, nIndex, Scanner.POS_ATTR_NAME);
                                                //}
                                                scanr.m_rsmAttribute.name = sElmValue;
                                                break;

                                            case Scanner.TAG_ATTR_TYPE:
                                                //if (-1 != nIndex)
                                                //{
                                                //    scanr.m_arAttributes.SetValue(sElmValue, nIndex, Scanner.POS_ATTR_TYPE);
                                                //}
                                                scanr.m_rsmAttribute.Type = sElmValue;
                                                break;

                                            case Scanner.TAG_ATTR_PROPERTY:
                                                // if (-1 != nIndex)
                                                // {
                                                //    scanr.m_arAttributes.SetValue(sElmValue, nIndex, Scanner.POS_ATTR_PROPERTY);
                                                // }
                                                scanr.m_rsmAttribute.property = sElmValue;
                                                break;

                                            case Scanner.TAG_ATTR_VALUE:
                                                //if (-1 != nIndex)
                                                //{
                                                //    scanr.m_arAttributes.SetValue(sElmValue, nIndex, Scanner.POS_ATTR_VALUE);
                                                //}
                                                scanr.m_rsmAttribute.value = sElmValue;
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            public void ReadXmlString_RsmAttr(string strXml, Scanner[] arScanner, out Scanner scanr, out int nIndex, out int nAttrCount, out int nOpCode)
            {
                nIndex = -1;
                nAttrCount = 0;
                scanr = null;
                nOpCode = -1;
                if (String.IsNullOrEmpty(strXml))
                {
                    return;
                }

                try
                {
                    XmlTextReader xmlRead = new XmlTextReader(new StringReader(strXml));
                    // Skip non-significant whitespace   
                    xmlRead.WhitespaceHandling = WhitespaceHandling.Significant;

                    string sElementName = "", sElmValue = "";
                    bool bValid = false, bFirst = false;
                    while (xmlRead.Read())
                    {
                        switch (xmlRead.NodeType)
                        {
                            case XmlNodeType.Element:
                                sElementName = xmlRead.Name;
                                if (Scanner.TAG_SCANNER_ID == sElementName)
                                {
                                    bValid = true;
                                    bFirst = true;
                                }
                                // for old att_getall.xml ....since name is not used(user can refer data-dictionary)  
                                else if (bValid && Scanner.TAG_ATTRIBUTE == sElementName && xmlRead.HasAttributes && (1 == xmlRead.AttributeCount))
                                {
                                    sElmValue = xmlRead.GetAttribute("name");
                                    if (null != scanr)
                                    {
                                        scanr.m_arAttributes.SetValue(sElmValue, nAttrCount, Scanner.POS_ATTR_NAME);
                                    }
                                }
                                break;

                            case XmlNodeType.Text:
                                if (bValid)
                                {
                                    sElmValue = xmlRead.Value;
                                    if (bFirst && Scanner.TAG_SCANNER_ID == sElementName)
                                    {
                                        bFirst = false;
                                        foreach (Scanner scanrTmp in arScanner)
                                        {
                                            if ((null != scanrTmp) &&
                                                 (sElmValue == scanrTmp.SCANNERID))
                                            {
                                                scanr = scanrTmp;
                                                break;
                                            }
                                        }
                                    }
                                    else if (null != scanr)
                                    {
                                        switch (sElementName)
                                        {
                                            case Scanner.TAG_OPCODE:
                                                nOpCode = Int32.Parse(sElmValue);
                                                if (!(BCRDef.RSM_ATTR_GETALL == nOpCode ||
                                                        BCRDef.RSM_ATTR_GET == nOpCode ||
                                                        BCRDef.RSM_ATTR_GETNEXT == nOpCode))
                                                {
                                                    return;
                                                }
                                                break;

                                            case Scanner.TAG_ATTRIBUTE:
                                                if (BCRDef.RSM_ATTR_GETALL == nOpCode)
                                                {
                                                    scanr.m_arAttributes.SetValue(sElmValue, nAttrCount, Scanner.POS_ATTR_ID);
                                                    nAttrCount++;
                                                }
                                                break;

                                            case Scanner.TAG_ATTR_ID:
                                                nIndex = -1;
                                                GetAttributePos(sElmValue, scanr, out nIndex);
                                                break;

                                            case Scanner.TAG_ATTR_NAME:
                                                if (-1 != nIndex)
                                                {
                                                    scanr.m_arAttributes.SetValue(sElmValue, nIndex, Scanner.POS_ATTR_NAME);
                                                }
                                                break;

                                            case Scanner.TAG_ATTR_TYPE:
                                                if (-1 != nIndex)
                                                {
                                                    scanr.m_arAttributes.SetValue(sElmValue, nIndex, Scanner.POS_ATTR_TYPE);
                                                }
                                                break;

                                            case Scanner.TAG_ATTR_PROPERTY:
                                                if (-1 != nIndex)
                                                {
                                                    scanr.m_arAttributes.SetValue(sElmValue, nIndex, Scanner.POS_ATTR_PROPERTY);
                                                }
                                                break;

                                            case Scanner.TAG_ATTR_VALUE:
                                                if (-1 != nIndex)
                                                {
                                                    scanr.m_arAttributes.SetValue(sElmValue, nIndex, Scanner.POS_ATTR_VALUE);
                                                }
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            public void clear_scanner_attributes(Scanner scanr)
            {
                try
                {
                    int length = scanr.m_arAttributes.Length / 5;
                    for (int index = 0; index < length; index++)
                    {
                        scanr.m_arAttributes.SetValue(null, index, Scanner.POS_ATTR_NAME);
                        scanr.m_arAttributes.SetValue(null, index, Scanner.POS_ATTR_ID);
                        scanr.m_arAttributes.SetValue(null, index, Scanner.POS_ATTR_TYPE);
                        scanr.m_arAttributes.SetValue(null, index, Scanner.POS_ATTR_PROPERTY);
                        scanr.m_arAttributes.SetValue(null, index, Scanner.POS_ATTR_VALUE);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            public void ReadXmlString_FW(string strXml, out int nMax, out int nProgress, out string sStatus, out string csScannerID)
            {
                nMax = 0;
                nProgress = 0;
                sStatus = "";
                csScannerID = "";
                if (String.IsNullOrEmpty(strXml))
                {
                    return;
                }

                string csSerial = "", csModel = "", csGuid = "";
                try
                {
                    XmlTextReader xmlRead = new XmlTextReader(new StringReader(strXml));
                    // Skip non-significant whitespace   
                    xmlRead.WhitespaceHandling = WhitespaceHandling.Significant;

                    string sElementName = "", sElmValue = "";
                    bool bScanner = false;
                    while (xmlRead.Read())
                    {
                        switch (xmlRead.NodeType)
                        {
                            case XmlNodeType.Element:
                                sElementName = xmlRead.Name;
                                if (Scanner.TAG_SCANNER_ID == sElementName)
                                {
                                    bScanner = true;
                                }
                                break;

                            case XmlNodeType.Text:
                                if (bScanner)
                                {
                                    sElmValue = xmlRead.Value;
                                    switch (sElementName)
                                    {
                                        case Scanner.TAG_SCANNER_ID:
                                            csScannerID = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_SERIALNUMBER:
                                            csSerial = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_MODELNUMBER:
                                            csModel = sElmValue;
                                            break;
                                        case Scanner.TAG_SCANNER_GUID:
                                            csGuid = sElmValue;
                                            break;
                                        case Scanner.TAG_STATUS:
                                            sStatus = sElmValue;
                                            break;
                                        case TAG_MAXCOUNT:
                                            nMax = Int32.Parse(sElmValue);
                                            break;
                                        case TAG_PROGRESS:
                                            nProgress = Int32.Parse(sElmValue);
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            internal string GetAttribXMLValue(string outXml)
            {
                string ret = String.Empty;
                try
                {
                    XmlTextReader xmlRead = new XmlTextReader(new StringReader(outXml));
                    xmlRead.Read();
                    xmlRead.ReadToFollowing("value");
                    ret = xmlRead.ReadString();
                }
                catch (Exception)
                {
                }

                return ret;
            }

            public void ReadXmlString_Scale(string strXml, out string weight, out string weightMode, out int Scalestatus)
            {

                weight = "";
                weightMode = "";
                Scalestatus = 0;
                if (String.IsNullOrEmpty(strXml))
                {
                    return;
                }

                try
                {
                    XmlTextReader xmlRead = new XmlTextReader(new StringReader(strXml));
                    // Skip non-significant whitespace   
                    xmlRead.WhitespaceHandling = WhitespaceHandling.Significant;

                    string sElementName = "", sElmValue = "";
                    bool bScanner = false;
                    while (xmlRead.Read())
                    {
                        switch (xmlRead.NodeType)
                        {
                            case XmlNodeType.Element:
                                sElementName = xmlRead.Name;
                                if (Scanner.TAG_SCANNER_ID == sElementName)
                                {
                                    bScanner = true;
                                }
                                break;

                            case XmlNodeType.Text:
                                if (bScanner)
                                {
                                    sElmValue = xmlRead.Value;
                                    switch (sElementName)
                                    {
                                        case Scanner.TAG_SCALE_WEIGHT:
                                            weight = sElmValue;
                                            break;
                                        case Scanner.TAG_SCALE_WEIGHT_MODE:
                                            weightMode = sElmValue;
                                            break;
                                        case Scanner.TAG_SCALE_STATUS:
                                            Scalestatus = Int32.Parse(sElmValue);
                                            break;
                                            //case Scanner.TAG_SCANNER_GUID:
                                            //    csGuid = sElmValue;
                                            //    break;
                                            //case Scanner.TAG_STATUS:
                                            //    sStatus = sElmValue;
                                            //    break;
                                            //case TAG_MAXCOUNT:
                                            //    nMax = Int32.Parse(sElmValue);
                                            //    break;
                                            //case TAG_PROGRESS:
                                            //    nProgress = Int32.Parse(sElmValue);
                                            //    break;
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            public void ReadXmlString_Snapi(string strXml, out int value)
            {

                value = -1;
                if (String.IsNullOrEmpty(strXml))
                {
                    return;
                }

                try
                {
                    XmlTextReader xmlRead = new XmlTextReader(new StringReader(strXml));
                    // Skip non-significant whitespace   
                    xmlRead.WhitespaceHandling = WhitespaceHandling.Significant;

                    string sElementName = "", sElmValue = "";
                    bool bScanner = false;
                    while (xmlRead.Read())
                    {
                        switch (xmlRead.NodeType)
                        {
                            case XmlNodeType.Element:
                                sElementName = xmlRead.Name;
                                if (Scanner.TAG_SCANNER_ID == sElementName)
                                {
                                    bScanner = true;
                                }
                                break;

                            case XmlNodeType.Text:
                                if (bScanner)
                                {
                                    sElmValue = xmlRead.Value;
                                    switch (sElementName)
                                    {
                                        case Scanner.TAG_SNAPI_PARAM_VAL:
                                            value = int.Parse(sElmValue);
                                            break;
                                            //case Scanner.TAG_SCALE_WEIGHT_MODE:
                                            //    weightMode = sElmValue;
                                            //    break;
                                            //case Scanner.TAG_SCALE_STATUS:
                                            //    Scalestatus = Int32.Parse(sElmValue);
                                            //    break;
                                    }
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            public void ReadXmlString_RSMIDList(string strXML, out List<KeyValuePair<int, string>> lstAttrList)
            {
                XmlDocument xmlDoc = new XmlDocument();
                lstAttrList = new List<KeyValuePair<int, string>>();

                try
                {
                    xmlDoc.LoadXml(strXML);

                    XmlNodeList xnList = xmlDoc.SelectNodes("/outArgs/arg-xml/response/attrib_list/attribute");
                    foreach (XmlNode xn in xnList)
                    {
                        int iKey = Convert.ToInt32(xn.InnerText);
                        string sValue = xn.Attributes[0].Value;

                        lstAttrList.Add(new KeyValuePair<int, string>(iKey, sValue));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

            public void ReadXmlString_RSMIDProperty(string strXML, out List<KeyValuePair<int, string[]>> lstProperty)
            {
                lstProperty = new List<KeyValuePair<int, string[]>>();
                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    xmlDoc.LoadXml(strXML);

                    XmlNodeList xnList = xmlDoc.SelectNodes("/outArgs/arg-xml/response/attrib_list/attribute");
                    foreach (XmlNode xn in xnList)
                    {
                        int iID = Convert.ToInt32(xn.ChildNodes[0].InnerText);
                        string sDataType = xn.ChildNodes[2].InnerText;
                        string sPermission = xn.ChildNodes[3].InnerText;
                        string sValue = xn.ChildNodes[4].InnerText;

                        string[] arr = new string[] { sDataType, sPermission, sValue };

                        lstProperty.Add(new KeyValuePair<int, string[]>(iID, arr));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        #endregion
    }
}
