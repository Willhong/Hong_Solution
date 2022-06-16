using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Basler.Pylon;

namespace Hong_Solution
{
    public class CamManager
    {
        //hongm main;

        private CamManager() { }
        //public CamManager(MainForm parent)
        //{
        //    main = parent;
        //}

        public void Grab(Object obj)
        {
            try
            {
                int nCamNum = (int)obj;

                //if(main.jobManager.VisionRunMode == 0)
                //    main.AddLogVision($"Cam{nCamNum + 1} Grab Start.");

                if (nCamNum == 0)
                {
                    //main.arrCams[0].Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                    //main.arrCams[0].StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                }
                else if (nCamNum == 1)
                {
                    //main.arrCams[1].Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.SingleFrame);
                    //main.arrCams[1].StreamGrabber.Start(1, GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                }
            }
            catch (Exception ex)
            {
                //main.AddLogVision(ex.ToString());
            }
        }

        public void ContinuousShot(int nCamNum)
        {
            try
            {
                //main.AddLogVision($"Cam{nCamNum + 1} Live Start");
                //main.g_bIsLiveOn[nCamNum] = true;
                //if (nCamNum == 0)
                //{
                //    main.arrCams[0].Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                //    main.arrCams[0].StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                //}
                //else if (nCamNum == 1)
                //{
                //    main.arrCams[1].Parameters[PLCamera.AcquisitionMode].SetValue(PLCamera.AcquisitionMode.Continuous);
                //    main.arrCams[1].StreamGrabber.Start(GrabStrategy.OneByOne, GrabLoop.ProvidedByStreamGrabber);
                //}

            }
            catch (Exception ex)
            {
                //main.AddLogVision(ex.ToString());
            }
        }

        // Stops the grabbing of images and handles exceptions.
        //public void Stop(int nCamNum)
        //{
        //    // Stop the grabbing.
        //    try
        //    {
        //        //main.AddLogVision($"Cam{nCamNum + 1} Live Stop");

        //        //main.g_bIsLiveOn[nCamNum] = false;

        //        //main.arrCams[nCamNum].StreamGrabber.Stop();
        //    }
        //    catch (Exception ex)
        //    {
        //        //main.AddLogVision(ex.ToString());
        //    }
        //}

        //public void DestroyCam(int nCamNum)
        //{
        //    try
        //    {
        //        //if (nCamNum == 0 && main.arrCams[0] != null)
        //        //{
        //        //    main.arrCams[0].Close();
        //        //    main.arrCams[0].Dispose();
        //        //    main.arrCams[0] = null;
        //        //}
        //        //else if (nCamNum == 1 && main.arrCams[1] != null)
        //        //{
        //        //    main.arrCams[1].Close();
        //        //    main.arrCams[1].Dispose();
        //        //    main.arrCams[1] = null;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        //main.AddLogVision(ex.ToString());
        //    }
        //}

        //public void SaveImageToBMP(int nCamNum)
        //{
        //    try
        //    {
        //        string filepath = Statics.sImageSaveFolderPath + "\\OriginalImage\\" + DateTime.Now.Year.ToString("D2") + DateTime.Now.Month.ToString("D2") + DateTime.Now.Day.ToString("D2") + "\\Cam" + (nCamNum + 1);
        //        if (!System.IO.Directory.Exists(filepath))
        //            System.IO.Directory.CreateDirectory(filepath); // 폴더 경로 생성

        //        string[] tmp = main.m_insp.sTime.Split(new string[] { ":" }, StringSplitOptions.None);
        //        string filename = $"{filepath}\\{tmp[0]}{tmp[1]}{tmp[2]}_Cam{nCamNum + 1}.bmp";
        //        main.arrImage[nCamNum].Save(filename);
        //        main.arrImagePath[nCamNum] = filename;
        //    }
        //    catch (Exception ex)
        //    {
        //        main.AddLogVision(ex.ToString());
        //    }
        //}

        //public void SaveNGImageToBMP(int nCamNum)
        //{
        //    try
        //    {
        //        if (main.arrImage[nCamNum] == null)
        //            return;

        //        string sOriginalfilepath = Statics.sImageSaveFolderPath + "\\OriginalImage\\" + DateTime.Now.Year.ToString("D2") + DateTime.Now.Month.ToString("D2") + DateTime.Now.Day.ToString("D2") + "\\Cam" + (nCamNum + 1);
        //        string[] tmp = main.m_insp.sTime.Split(new string[] { ":" }, StringSplitOptions.None);
        //        string sOriginalfilename = $"{sOriginalfilepath}\\{tmp[0]}{tmp[1]}{tmp[2]}_Cam{nCamNum + 1}.bmp";

        //        if (File.Exists(sOriginalfilename))
        //        {
        //            string sNGfilepath = Statics.sImageSaveFolderPath + "\\NGImage\\" + DateTime.Now.Year.ToString("D2") + DateTime.Now.Month.ToString("D2") + DateTime.Now.Day.ToString("D2") + "\\Cam" + (nCamNum + 1);
        //            if (!System.IO.Directory.Exists(sNGfilepath))
        //                System.IO.Directory.CreateDirectory(sNGfilepath); // 폴더 경로 생성
        //            string sNGfilename = $"{sNGfilepath}\\{tmp[0]}{tmp[1]}{tmp[2]}_Cam{nCamNum + 1}.bmp";

        //            File.Copy(sOriginalfilename, sNGfilename, true);
        //        }
        //        else
        //        {
        //            main.AddLogVision($"Cam{nCamNum + 1} NG Image Save Fail");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        main.AddLogVision(ex.ToString());
        //    }
        //}

        //// Occurs when a device with an opened connection is removed.
        //public void OnConnectionLost(Object sender, EventArgs e)
        //{
        //    if (main.InvokeRequired)
        //    {
        //        // If called from a different thread, we must use the Invoke method to marshal the call to the proper thread.
        //        main.BeginInvoke(new EventHandler<EventArgs>(OnConnectionLost), sender, e);
        //        return;
        //    }

        //    Camera tmp = (Camera)sender;

        //    int nCamNum = -1;
        //    if (tmp.CameraInfo[CameraInfoKey.SerialNumber].Contains(main.setupDTO.sCamSerial1))
        //        nCamNum = 0;
        //    else if (tmp.CameraInfo[CameraInfoKey.SerialNumber].Contains(main.setupDTO.sCamSerial2))
        //        nCamNum = 1;

        //    main.AddLogVision($"Cam{nCamNum + 1} Connection Lost");

        //    main.g_bCamConnection[nCamNum] = false;

        //    if (nCamNum == 0)
        //        main.SetDisplayStateColor(main.dsCam1, false);
        //    else if (nCamNum == 1)
        //        main.SetDisplayStateColor(main.dsCam2, false);

        //    // Close the camera object.
        //    main.camManager.DestroyCam(nCamNum);

        //    main.StopAutoRun();

        //    // 카메라 재연결 스레드 시작
        //    if (!main.bCameraConnectThreadStart)
        //        new Task(main.UpdateCamConnect).Start();

        //    if (!main.bAutoReadyThreadStart)
        //        new Task(main.SetAutoReady).Start();


        //}

        // Occurs when an image has been acquired and is ready to be processed.
        //public void OnImageGrabbed1(Object sender, ImageGrabbedEventArgs e)
        //{
        //    //main.AddLogVision("OnImageGrabbed1.");
        //    if (main.InvokeRequired)
        //    {
        //        // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
        //        // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread. 
        //        main.BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed1), sender, e.Clone());
        //        return;
        //    }

        //    try
        //    {
        //        // Acquire the image from the camera. Only show the latest image. The camera may acquire images faster than the images can be displayed.
        //        //IStreamGrabber tmp = (IStreamGrabber)sender;
        //        // Get the grab result.
        //        main.Invoke(new Action(delegate ()
        //        {
        //            IGrabResult grabResult = e.GrabResult;

        //            if (grabResult.IsValid)
        //            {
        //                Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format8bppIndexed);
        //                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

        //                ColorPalette palette = bitmap.Palette;
        //                for (int i = 0; i < 256; i++)
        //                {
        //                    Color tmp = Color.FromArgb(255, i, i, i);
        //                    palette.Entries[i] = tmp;
        //                }
        //                bitmap.Palette = palette;

        //                IntPtr ptrBmp = bmpData.Scan0;
        //                int size = bmpData.Stride * bitmap.Height;

        //                System.Runtime.InteropServices.Marshal.Copy((byte[])grabResult.PixelData, 0, ptrBmp, size);
        //                bitmap.UnlockBits(bmpData);


        //                int nCamNum = 0;

        //                //bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
        //                main.arrImage[nCamNum] = bitmap;
        //                //main.arrImagebyte[nCamNum] = (byte[])grabResult.PixelData;

        //                if (!main.g_bIsLiveOn[nCamNum])
        //                {
        //                    //System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, 0, main.arrImagebyte[nCamNum], size);
        //                    //main.arrImagebyte[nCamNum] = (byte[])grabResult.PixelData;
        //                    Marshal.Copy(bmpData.Scan0, main.arrImagebyte[nCamNum], 0, size);
        //                }

        //                main.arrDisp[main.jobManager.VisionRunMode * 2 + nCamNum].SizeMode = PictureBoxSizeMode.StretchImage;
        //                main.arrDisp[main.jobManager.VisionRunMode * 2 + nCamNum].Image = bitmap;

        //                if (!main.g_bIsLiveOn[nCamNum] && main.m_setup.cbSaveImage.Checked)
        //                {
        //                    SaveImageToBMP(nCamNum);
        //                    main.AddLogVision($"Cam{nCamNum + 1} Grab Finish");
        //                }
        //            }
        //            main.nGrabCount++;
        //        }));
        //    }
        //    catch (Exception ex)
        //    {
        //        main.AddLogVision(e.ToString());
        //    }
        //    finally
        //    {
        //        // Dispose the grab result if needed for returning it to the grab loop.
        //        e.DisposeGrabResultIfClone();
        //    }
        //}
        //// Occurs when an image has been acquired and is ready to be processed.
        //public void OnImageGrabbed2(Object sender, ImageGrabbedEventArgs e)
        //{
        //    //main.AddLogVision("OnImageGrabbed2.");
        //    if (main.InvokeRequired)
        //    {
        //        // If called from a different thread, we must use the Invoke method to marshal the call to the proper GUI thread.
        //        // The grab result will be disposed after the event call. Clone the event arguments for marshaling to the GUI thread. 
        //        main.BeginInvoke(new EventHandler<ImageGrabbedEventArgs>(OnImageGrabbed2), sender, e.Clone());
        //        return;
        //    }

        //    try
        //    {
        //        main.Invoke(new Action(delegate ()
        //        {
        //            IGrabResult grabResult = e.GrabResult;

        //            if (grabResult.IsValid)
        //            {
        //                Bitmap bitmap = new Bitmap(grabResult.Width, grabResult.Height, PixelFormat.Format8bppIndexed); // 200110
        //                                                                                                                // Lock the bits of the bitmap.
        //                BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);

        //                ColorPalette palette = bitmap.Palette;
        //                for (int i = 0; i < 256; i++)
        //                {
        //                    Color tmp = Color.FromArgb(255, i, i, i);
        //                    palette.Entries[i] = tmp;
        //                }
        //                bitmap.Palette = palette;

        //                IntPtr ptrBmp = bmpData.Scan0;
        //                int size = bmpData.Stride * bitmap.Height;

        //                System.Runtime.InteropServices.Marshal.Copy((byte[])grabResult.PixelData, 0, ptrBmp, size);
        //                bitmap.UnlockBits(bmpData);

        //                int nCamNum = 1;

        //                //bitmap.RotateFlip(RotateFlipType.Rotate180FlipNone);
        //                main.arrImage[nCamNum] = bitmap;
        //                //main.arrImagebyte[nCamNum] = (byte[])grabResult.PixelData;

        //                if (!main.g_bIsLiveOn[1])
        //                {
        //                    main.arrImagebyte[nCamNum] = (byte[])grabResult.PixelData;
        //                }

        //                main.arrDisp[main.jobManager.VisionRunMode * 2 + nCamNum].SizeMode = PictureBoxSizeMode.StretchImage;
        //                main.arrDisp[main.jobManager.VisionRunMode * 2 + nCamNum].Image = bitmap;

        //                if (!main.g_bIsLiveOn[1] && main.m_setup.cbSaveImage.Checked)
        //                {
        //                    SaveImageToBMP(nCamNum);
        //                    main.AddLogVision($"Cam{nCamNum + 1} Grab Finish");
        //                }
        //            }
        //            main.nGrabCount++;
        //        }));
        //    }
        //    catch (Exception ex)
        //    {
        //        main.AddLogVision(e.ToString());
        //    }
        //    finally
        //    {
        //        // Dispose the grab result if needed for returning it to the grab loop.
        //        e.DisposeGrabResultIfClone();
        //    }
        //}
    }
}
