using Cognex.VisionPro;
using Cognex.VisionPro.ImageFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cognex.VisionPro.Exceptions;
using System.Windows.Forms;
using System.IO;
using Cognex.VisionPro.ImageProcessing;

namespace Hong_Solution
{
    public class VisionProCam
    {
        public HongMain FormMain = null;                                                                                                                                                                                             

        public bool bIsOpen = false;
        public bool bIsGrabFinish = false;
        public bool bLiveOn = false;
        public ICogAcqFifo cogAcqFifo = null;
        public CogAcqFifoTool cogAcqFifoTool = null;
        public ICogFrameGrabber cogFrameGrabber = null;
        public int nCamNum;
        string videoFormat = "Generic GigEVision (Mono)";

        public ICogAcqExposure cogAcqExposure;
        public ICogAcqBrightness cogAcqBrightness;
        public ICogAcqContrast cogAcqContrast;
        public ICogAcqTrigger cogAcqTrigger;

        public CogImage8Grey cogImage8Grey = null;

        public CogStopwatch stopwatch = new CogStopwatch();

        object lockGrabObject = new object();

        CogIPOneImageTool cogIPOneImageTool;

        public VisionProCam(ICogFrameGrabber cogFrameGrabber, int nCamNum)
        {
            
            this.cogFrameGrabber = cogFrameGrabber;

            try
            {
                this.nCamNum = nCamNum;

                cogAcqFifoTool = new CogAcqFifoTool();

                cogAcqFifo = cogFrameGrabber.CreateAcqFifo(videoFormat, CogAcqFifoPixelFormatConstants.Format8Grey, 0, true);
                cogAcqBrightness = cogAcqFifo.OwnedBrightnessParams;
                cogAcqContrast = cogAcqFifo.OwnedContrastParams;
                cogAcqExposure = cogAcqFifo.OwnedExposureParams;
                cogAcqFifo.Timeout = 5000;

                bIsOpen = true;

                cogAcqFifoTool.Operator.Flush();

                SetIPOneImageTool();
            }
            catch (Exception e)
            {
                //FormMain.DebugPrint(e.StackTrace);

                bIsOpen = false;
            }
        }

        public ICogImage Grab()
        {
            lock (lockGrabObject)
            {
                int trigNum;

                bIsGrabFinish = false;
                
                try
                {
                    cogAcqFifo.AcquiredPixelFormat();
                    cogImage8Grey = cogAcqFifo.Acquire(out trigNum) as CogImage8Grey;
                    cogImage8Grey = SetFlipRotate();//new CogImage8Grey(SetFlipRotate());
                    cogImage8Grey.SelectedSpaceName = "#";

                    cogAcqFifoTool.Operator.Flush();

                    //if (!FormMain.g_bflgAlignInspectionStart && FormMain.g_bManualGrab)
                    //{
                        //string sFolderPath = $"{VisionDef.IMAGE_FOLDER}\\{DateTime.Now.Year}\\{DateTime.Now.Month.ToString("d2")}\\{DateTime.Now.Day.ToString("d2")}\\Grab";
                        //if (!Directory.Exists(sFolderPath)) Directory.CreateDirectory(sFolderPath); // 폴더 경로 생성
                        //string sFilePath = $"{sFolderPath}\\{DateTime.Now.Hour.ToString("d2")}{DateTime.Now.Minute.ToString("d2")}{DateTime.Now.Second.ToString("d2")}_Cam{nCamNum+1}.bmp";


                        //if (!FormMain.g_bLiveOn)
                            //FormMain.SaveImageBMP(cogImage8Grey, sFilePath);
                   // }

                    //FormMain.alignGreyImage[nCamNum] = cogImage8Grey;
                }
                catch (Exception e)
                {
                    //FormMain.DebugPrint(e.StackTrace);
                    //FormMain.g_bCamOpen[nCamNum] = false;
                }

                bIsGrabFinish = true;

                return cogImage8Grey;
            }
        }

        
        private void SetIPOneImageTool()
        {
            cogIPOneImageTool = new CogIPOneImageTool();
            CogIPOneImageFlipRotate cogIPOneImageFlipRotate = new CogIPOneImageFlipRotate();
            cogIPOneImageFlipRotate.OperationInPixelSpace = CogIPOneImageFlipRotateOperationConstants.Rotate180Deg;
            cogIPOneImageTool.Operators.Add(cogIPOneImageFlipRotate);
        }

        private CogImage8Grey SetFlipRotate()
        {
            cogIPOneImageTool.InputImage = cogImage8Grey;
            cogIPOneImageTool.Run();

            return cogIPOneImageTool.OutputImage as CogImage8Grey;
        }
    }
}
