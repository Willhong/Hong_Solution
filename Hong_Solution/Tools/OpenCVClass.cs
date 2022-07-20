using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using OpenCvSharp.Blob;
namespace Hong_Solution
{
    public class OpenCVClass
    {
        public  OpenCVClass()
        {

        }


        #region CreateMat

        public Mat MatFromFile(string filepath, ImreadModes mode)
        {
            Mat mat = new Mat(filepath, mode);
            return mat;
        }
        public Mat ToMat(Bitmap img)
        {
            return BitmapConverter.ToMat(img);
        }
        public Bitmap ToBitmap(Mat img)
        {
            return BitmapConverter.ToBitmap(img);
        }

        public struct ImageMode
        {
            public const int Unchanged = (int)ImreadModes.Unchanged;
            public const int Grayscale = (int)ImreadModes.Grayscale;
            public const int Color = (int)ImreadModes.Color;
            public const int AnyDepth = (int)ImreadModes.AnyDepth;
            public const int AnyColor = (int)ImreadModes.AnyColor;
            public const int LoadGdal = (int)ImreadModes.LoadGdal;
            public const int ReducedGrayscale2 = (int)ImreadModes.ReducedGrayscale2;
            public const int ReducedColor2 = (int)ImreadModes.ReducedColor2;
            public const int ReducedGrayscale4 = (int)ImreadModes.ReducedGrayscale4;
            public const int ReducedColor4 = (int)ImreadModes.ReducedColor4;
            public const int ReducedGrayscale8 = (int)ImreadModes.ReducedGrayscale8;
            public const int ReducedColor8 = (int)ImreadModes.ReducedColor8;
            public const int IgnoreOrientation = (int)ImreadModes.IgnoreOrientation;
        }
        #endregion CreateMat

        public Mat LoadMat(string dir)
        {
            return Cv2.ImRead(dir, ImreadModes.AnyColor);
        }

        public Mat ToGray(Mat mat)
        {
            Mat retMat = new Mat();
            Cv2.CvtColor(mat, retMat, ColorConversionCodes.BGR2GRAY);
            return retMat;
        }

        public Bitmap Resize(Bitmap bmp, OpenCvSharp.Size size)
        {
            Mat tmp = BitmapConverter.ToMat(bmp);
            tmp.Resize(size);
            return BitmapConverter.ToBitmap(tmp);
        }

        public Mat SubMat(Mat src,int x,int y, int width, int height)
        {
            Rect rect = new Rect(x,y,width,height);
            return src.SubMat(rect);
        }
        public Mat SubMat(Mat src, Rectangle rect)
        {
            Rect rectangle = new Rect(rect.X,rect.Y,rect.Width,rect.Height);
            return src.SubMat(rectangle);
        }

        public Mat Binarize(Mat src, int Threshold)
        {
            Mat GrayImage = new Mat();
            Mat binary = new Mat();

            Cv2.CvtColor(src, GrayImage, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(GrayImage, GrayImage, Threshold, 255, ThresholdTypes.Binary);

            Cv2.ImShow("src", src);
            Cv2.ImShow("dst", GrayImage);
            return GrayImage;
        }

        public Mat FindBlob(Mat src, int Threshold)
        {
            Mat result = new Mat(src.Size(), MatType.CV_8UC3);
            CvBlobs blobs = new CvBlobs();
            blobs.Label(src);
            blobs.RenderBlobs(src, result);

            foreach (var item in blobs)
            {
                CvBlob b = item.Value;
                if (b.Area > 100)
                {
                    //Cv2.Circle(result, b.Contour.StartingPoint, 4, Scalar.Red, 2, LineTypes.AntiAlias);
                    Cv2.PutText(result, b.Area.ToString(), new OpenCvSharp.Point(b.Centroid.X, b.Centroid.Y),
                        HersheyFonts.HersheyComplex, 1, Scalar.Yellow, 2, LineTypes.Link8);
                }
            }
            Cv2.ImShow("BlobResult", result);
            return result;
        }
    }
}
