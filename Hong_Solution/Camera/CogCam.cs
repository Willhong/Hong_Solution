using Cognex.VisionPro;
using Cognex.VisionPro.ImageProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hong_Solution.Camera
{
    internal class CogCam
    {
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

        public CogCam(ICogFrameGrabber cogFrameGrabber, int nCamNum)
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


            }
            catch (Exception e)
            { 

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
                    //cogImage8Grey = SetFlipRotate();//new CogImage8Grey(SetFlipRotate());
                    cogImage8Grey.SelectedSpaceName = "#";

                    cogAcqFifoTool.Operator.Flush();

                }
                catch (Exception e)
                {
                    bIsOpen = false;
                }

                bIsGrabFinish = true;

                return cogImage8Grey;
            }

        }
        private CogImage8Grey SetFlipRotate(CogImage8Grey InputImage,CogIPOneImageFlipRotateOperationConstants option)
        {
            cogIPOneImageTool = new CogIPOneImageTool();
            CogIPOneImageFlipRotate cogIPOneImageFlipRotate = new CogIPOneImageFlipRotate();
            cogIPOneImageFlipRotate.OperationInPixelSpace = option;
            cogIPOneImageTool.Operators.Add(cogIPOneImageFlipRotate);
            cogIPOneImageTool.InputImage = InputImage;
            cogIPOneImageTool.Run();
            return cogIPOneImageTool.OutputImage as CogImage8Grey;

        }
    }
}
