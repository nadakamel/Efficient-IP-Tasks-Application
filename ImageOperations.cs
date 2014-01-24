using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Diagnostics;
///Algorithms Project
///Efficient IP Tasks
///

namespace Efficient_IP_Tasks
{
    /// <summary>
    /// Holds the pixel color in 3 byte values: red, green and blue
    /// </summary>
    public struct MyColor
    {
        public byte red, green, blue;
    }

    /// <summary>
    /// Holds the pixel color in 3 double values: red, green and blue
    /// </summary>
    public struct MyColorDouble
    {
        public double red, green, blue;
    }

    public class ImageOperations
    {
        public static Stopwatch stopWatch;
        public static double elapsedMs;
        public static double gammavalueEfficient;
        public static double gammavalueNormal;

        /// <summary>
        /// Open an image and load it into 2D ImageMatrixay of colors (size: Height x Width)
        /// </summary>
        /// <param name="ImagePath">Image file path</param>
        /// <returns>2D ImageMatrixay of colors</returns>
        public static MyColor[,] OpenImage(string ImagePath)
        {
            Bitmap original_bm = new Bitmap(ImagePath);

            int Height = original_bm.Height;
            int Width = original_bm.Width;

            MyColor[,] Buffer = new MyColor[Height, Width];

            unsafe
            {
                BitmapData bmd = original_bm.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, original_bm.PixelFormat);
                int x, y;
                int nWidth = 0;
                bool Format32 = false;
                bool Format24 = false;
                bool Format8 = false;

                if (original_bm.PixelFormat == PixelFormat.Format24bppRgb)
                {
                    Format24 = true;
                    nWidth = Width * 3;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format32bppArgb || original_bm.PixelFormat == PixelFormat.Format32bppRgb || original_bm.PixelFormat == PixelFormat.Format32bppPArgb)
                {
                    Format32 = true;
                    nWidth = Width * 4;
                }
                else if (original_bm.PixelFormat == PixelFormat.Format8bppIndexed)
                {
                    Format8 = true;
                    nWidth = Width;
                }
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (y = 0; y < Height; y++)
                {
                    for (x = 0; x < Width; x++)
                    {
                        if (Format8)
                        {
                            Buffer[y, x].red = Buffer[y, x].green = Buffer[y, x].blue = p[0];
                            p++;
                        }
                        else
                        {
                            Buffer[y, x].red = p[0];
                            Buffer[y, x].green = p[1];
                            Buffer[y, x].blue = p[2];
                            if (Format24) p += 3;
                            else if (Format32) p += 4;
                        }
                    }
                    p += nOffset;
                }
                original_bm.UnlockBits(bmd);
            }

            return Buffer;
        }

        //for byte image
        //==========================================
        /// <summary>
        /// Get the height of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D ImageMatrixay that contains the image</param>
        /// <returns>Image Height</returns>
        public static int GetHeight(MyColor[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(0);
        }

        /// <summary>
        /// Get the width of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D ImageMatrixay that contains the image</param>
        /// <returns>Image Width</returns>
        public static int GetWidth(MyColor[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(1);
        }

        //========================================================
        //for double image

        /// <summary>
        /// Get the height of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Height</returns>
        public static int GetHeightDouble(MyColorDouble[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(0);
        }

        /// <summary>
        /// Get the width of the image 
        /// </summary>
        /// <param name="ImageMatrix">2D array that contains the image</param>
        /// <returns>Image Width</returns>
        public static int GetWidthDouble(MyColorDouble[,] ImageMatrix)
        {
            return ImageMatrix.GetLength(1);
        }
        //=======================================================

        /// <summary>
        /// Display the given image on the given PictureBox object
        /// </summary>
        /// <param name="ImageMatrix">2D ImageMatrixay that contains the image</param>
        /// <param name="PicBox">PictureBox object to display the image on it</param>
        public static void DisplayImage(MyColor[,] ImageMatrix, PictureBox PicBox)
        {
            // Create Image:
            //==============
            int Height = ImageMatrix.GetLength(0);
            int Width = ImageMatrix.GetLength(1);

            Bitmap ImageBMP = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);

            unsafe
            {
                BitmapData bmd = ImageBMP.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, ImageBMP.PixelFormat);
                int nWidth = 0;
                nWidth = Width * 3;
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        p[0] = ImageMatrix[i, j].red;
                        p[1] = ImageMatrix[i, j].green;
                        p[2] = ImageMatrix[i, j].blue;
                        p += 3;
                    }

                    p += nOffset;
                }
                ImageBMP.UnlockBits(bmd);
            }

            PicBox.Image = ImageBMP;
        }

        /// <summary>
        /// Save the blurred image on the given PictureBox object
        /// </summary>
        /// <param name="ImageMatrix">2D ImageMatrix that contains the image</param>
        public static void SaveImage(MyColor[,] ImageMatrix)
        {
            // Create New Image:
            //======================
            int Height = ImageMatrix.GetLength(0);
            int Width = ImageMatrix.GetLength(1);

            Bitmap NewImageBMP = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
            unsafe
            {
                BitmapData bmd = NewImageBMP.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, NewImageBMP.PixelFormat);
                int nWidth = 0;
                nWidth = Width * 3;
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        p[0] = ImageMatrix[i, j].red;
                        p[1] = ImageMatrix[i, j].green;
                        p[2] = ImageMatrix[i, j].blue;
                        p += 3;
                    }

                    p += nOffset;
                }
                NewImageBMP.UnlockBits(bmd);
            }

            if (NewImageBMP != null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(saveFileDialog1.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Bmp;

                    if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }

                    string saveFile = saveFileDialog1.FileName + "." + imgFormat;
                    StreamWriter streamWriter = new StreamWriter(saveFile, false);
                    NewImageBMP.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();

                    NewImageBMP = null;
                }
            }
        }

        /// <summary>
        /// Normal resize of an image
        /// </summary>
        /// <param name="ImageMatrix">2D array of image values</param>
        /// <param name="NewWidth">desired width</param>
        /// <param name="NewHeight">desired height</param>
        /// <returns>Resized image</returns>
        public static MyColor[,] NormalResize(MyColor[,] ImageMatrix, int NewWidth, int NewHeight)
        {
            int i = 0, j = 0;
            int Height = ImageMatrix.GetLength(0);
            int Width = ImageMatrix.GetLength(1);

            double WidthRatio = (double)(Width) / (double)(NewWidth);
            double HeightRatio = (double)(Height) / (double)(NewHeight);

            int OldWidth = Width;
            int OldHeight = Height;

            MyColor P1, P2, P3, P4;

            MyColor Y1, Y2, X = new MyColor();

            MyColor[,] Data = new MyColor[NewHeight, NewWidth];

            Width = NewWidth;
            Height = NewHeight;

            int floor_x, ceil_x;
            int floor_y, ceil_y;

            double x, y;
            double fraction_x, one_minus_x;
            double fraction_y, one_minus_y;

            for (j = 0; j < NewHeight; j++)
                for (i = 0; i < NewWidth; i++)
                {
                    x = (double)(i) * WidthRatio;
                    y = (double)(j) * HeightRatio;

                    floor_x = (int)(x);
                    ceil_x = floor_x + 1;
                    if (ceil_x >= OldWidth) ceil_x = floor_x;

                    floor_y = (int)(y);
                    ceil_y = floor_y + 1;
                    if (ceil_y >= OldHeight) ceil_y = floor_y;

                    fraction_x = x - floor_x;
                    one_minus_x = 1.0 - fraction_x;

                    fraction_y = y - floor_y;
                    one_minus_y = 1.0 - fraction_y;

                    P1 = ImageMatrix[floor_y, floor_x];
                    P2 = ImageMatrix[ceil_y, floor_x];
                    P3 = ImageMatrix[floor_y, ceil_x];
                    P4 = ImageMatrix[ceil_y, ceil_x];

                    Y1.red = (byte)(one_minus_y * P1.red + fraction_y * P2.red);
                    Y1.green = (byte)(one_minus_y * P1.green + fraction_y * P2.green);
                    Y1.blue = (byte)(one_minus_y * P1.blue + fraction_y * P2.blue);

                    Y2.red = (byte)(one_minus_y * P3.red + fraction_y * P4.red);
                    Y2.green = (byte)(one_minus_y * P3.green + fraction_y * P4.green);
                    Y2.blue = (byte)(one_minus_y * P3.blue + fraction_y * P4.blue);

                    X.red = (byte)(one_minus_x * Y1.red + fraction_x * Y2.red);
                    X.green = (byte)(one_minus_x * Y1.green + fraction_x * Y2.green);
                    X.blue = (byte)(one_minus_x * Y1.blue + fraction_x * Y2.blue);

                    Data[j, i] = X;
                }

            return Data;
        }

        /// <summary>
        /// Efficient Algorithm to Get Min and Max pixel number at the same time
        /// </summary>
        /// <param name="ImageMatrix">2D ImageMatrixay that contains the image</param>
        /// <param name="redmin">The least minimum red color in the whole image</param>
        /// <param name="redmax">The most maximum red color in the whole image</param>
        /// <param name="bluemin">The least minimum blue color in the whole image</param>
        /// <param name="bluemax">The most maximum blue color in the whole image</param>
        /// <param name="greenmin">The least minimum green color in the whole image</param>
        /// <param name="greenmax">The most maximum green color in the whole image</param>
        public static void GetMinMax_Efficient(MyColor[,] ImageMatrix, ref double redmin, ref double redmax, ref double bluemin, ref double bluemax, ref double greenmin, ref double greenmax)
        {
            redmin = 100000;
            redmax = 0;
            bluemin = 100000;
            bluemax = 0;
            greenmin = 100000;
            greenmax = 0;

            int n = GetWidth(ImageMatrix) * GetHeight(ImageMatrix);

            for (int i = 0; i < GetHeight(ImageMatrix) - 1; i++)
            {
                for (int j = 0; j < GetWidth(ImageMatrix) - 1; )
                {
                    //if n is even
                    if (n % 2 == 0)
                    {
                        if (ImageMatrix[i, j].red < ImageMatrix[i, j + 1].red)
                        {
                            if (redmin > ImageMatrix[i, j].red)
                            {
                                redmin = ImageMatrix[i, j].red;
                            }
                            if (redmax < ImageMatrix[i, j + 1].red)
                            {
                                redmax = ImageMatrix[i, j + 1].red;
                            }
                        }
                        else
                        {
                            if (redmin > ImageMatrix[i, j + 1].red)
                            {
                                redmin = ImageMatrix[i, j + 1].red;
                            }
                            if (redmax < ImageMatrix[i, j].red)
                            {
                                redmax = ImageMatrix[i, j].red;
                            }
                        }

                        if (ImageMatrix[i, j].blue < ImageMatrix[i, j + 1].blue)
                        {
                            if (bluemin > ImageMatrix[i, j].blue)
                            {
                                bluemin = ImageMatrix[i, j].blue;
                            }
                            if (bluemax < ImageMatrix[i, j + 1].blue)
                            {
                                bluemax = ImageMatrix[i, j + 1].blue;
                            }
                        }
                        else
                        {
                            if (bluemin > ImageMatrix[i, j + 1].blue)
                            {
                                bluemin = ImageMatrix[i, j + 1].blue;
                            }
                            if (bluemax < ImageMatrix[i, j].blue)
                            {
                                bluemax = ImageMatrix[i, j].blue;
                            }
                        }

                        if (ImageMatrix[i, j].green < ImageMatrix[i, j + 1].green)
                        {
                            if (greenmin > ImageMatrix[i, j].green)
                            {
                                greenmin = ImageMatrix[i, j].green;
                            }
                            if (greenmax < ImageMatrix[i, j + 1].green)
                            {
                                greenmax = ImageMatrix[i, j + 1].green;
                            }
                        }
                        else
                        {
                            if (greenmin > ImageMatrix[i, j + 1].green)
                            {
                                greenmin = ImageMatrix[i, j + 1].green;
                            }
                            if (greenmax < ImageMatrix[i, j].green)
                            {
                                greenmax = ImageMatrix[i, j].green;
                            }
                        }

                        j += 2;
                    }

                    // if n is odd , make one comparison first then loop 
                    else if (n % 2 == 1)
                    {
                        if ((i == 0 && j == 0) || j == 0)
                        {
                            if (redmin > ImageMatrix[i, j].red)
                            {
                                redmin = ImageMatrix[i, j].red;
                            }

                            else if (redmax < ImageMatrix[i, j].red)
                            {
                                redmax = ImageMatrix[i, j].red;
                            }

                            if (bluemin > ImageMatrix[i, j].blue)
                            {
                                bluemin = ImageMatrix[i, j].blue;
                            }

                            else if (bluemax < ImageMatrix[i, j].blue)
                            {
                                bluemax = ImageMatrix[i, j].blue;
                            }

                            if (greenmin > ImageMatrix[i, j].green)
                            {
                                greenmin = ImageMatrix[i, j].green;
                            }

                            else if (greenmax < ImageMatrix[i, j].green)
                            {
                                greenmax = ImageMatrix[i, j].green;
                            }

                            j++;
                        }

                        else
                        {
                            if (ImageMatrix[i, j].red < ImageMatrix[i, j + 1].red)
                            {
                                if (redmin > ImageMatrix[i, j].red)
                                {
                                    redmin = ImageMatrix[i, j].red;
                                }
                                if (redmax < ImageMatrix[i, j + 1].red)
                                {
                                    redmax = ImageMatrix[i, j + 1].red;
                                }
                            }
                            else
                            {
                                if (redmin > ImageMatrix[i, j + 1].red)
                                {
                                    redmin = ImageMatrix[i, j + 1].red;
                                }
                                if (redmax < ImageMatrix[i, j].red)
                                {
                                    redmax = ImageMatrix[i, j].red;
                                }
                            }


                            if (ImageMatrix[i, j].blue < ImageMatrix[i, j + 1].blue)
                            {
                                if (bluemin > ImageMatrix[i, j].blue)
                                {
                                    bluemin = ImageMatrix[i, j].blue;
                                }
                                if (bluemax < ImageMatrix[i, j + 1].blue)
                                {
                                    bluemax = ImageMatrix[i, j + 1].blue;
                                }
                            }
                            else
                            {
                                if (bluemin > ImageMatrix[i, j + 1].blue)
                                {
                                    bluemin = ImageMatrix[i, j + 1].blue;
                                }
                                if (bluemax < ImageMatrix[i, j].blue)
                                {
                                    bluemax = ImageMatrix[i, j].blue;
                                }
                            }


                            if (ImageMatrix[i, j].green < ImageMatrix[i, j + 1].green)
                            {
                                if (greenmin > ImageMatrix[i, j].green)
                                {
                                    greenmin = ImageMatrix[i, j].green;
                                }
                                if (greenmax < ImageMatrix[i, j + 1].green)
                                {
                                    greenmax = ImageMatrix[i, j + 1].green;
                                }
                            }
                            else
                            {
                                if (greenmin > ImageMatrix[i, j + 1].green)
                                {
                                    greenmin = ImageMatrix[i, j + 1].green;
                                }
                                if (greenmax < ImageMatrix[i, j].green)
                                {
                                    greenmax = ImageMatrix[i, j].green;
                                }
                            }

                            j += 2;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///  Normal Algorithm to get Min and Max pixel number separetly 
        /// </summary>
        /// <param name="ImageMatrix">2D ImageMatrixay that contains the image</param>
        /// <param name="redmin">The least minimum red color in the whole image</param>
        /// <param name="redmax">The most maximum red color in the whole image</param>
        /// <param name="bluemin">The least minimum blue color in the whole image</param>
        /// <param name="bluemax">The most maximum blue color in the whole image</param>
        /// <param name="greenmin">The least minimum green color in the whole image</param>
        /// <param name="greenmax">The most maximum green color in the whole image</param>
        public static void GetMinMax_Normal(MyColor[,] ImageMatrix, ref double redmin, ref double redmax, ref double bluemin, ref double bluemax, ref double greenmin, ref double greenmax)
        {
            redmin = 100000;
            redmax = 0;
            bluemin = 100000;
            bluemax = 0;
            greenmin = 100000;
            greenmax = 0;

            for (int i = 0; i < GetHeight(ImageMatrix) - 1; i++)
            {
                for (int j = 0; j < GetWidth(ImageMatrix) - 1; j++)
                {

                    if (ImageMatrix[i, j].red < redmin)
                        redmin = ImageMatrix[i, j].red;

                    if (ImageMatrix[i, j].blue < bluemin)
                        bluemin = ImageMatrix[i, j].blue;

                    if (ImageMatrix[i, j].green < greenmin)
                        greenmin = ImageMatrix[i, j].green;
                }

            }

            for (int i = 0; i < GetHeight(ImageMatrix); i++)
            {
                for (int j = 0; j < GetWidth(ImageMatrix); j++)
                {

                    if (ImageMatrix[i, j].red > redmax)
                        redmax = ImageMatrix[i, j].red;

                    if (ImageMatrix[i, j].blue > bluemax)
                        bluemax = ImageMatrix[i, j].blue;

                    if (ImageMatrix[i, j].green > greenmax)
                        greenmax = ImageMatrix[i, j].green;
                }
            }

        }

        /// <summary>
        /// Contrast Image - Normal/Efficient Min-Max Algorithm
        /// </summary>
        /// <param name="ImageMatrix">2D ImageMatrix that contains the image</param>
        /// <param name="New">Contrast degree</param>
        /// <returns>2D ImageMatrix of contrasted image colors</returns>
        public static MyColor[,] ContrastImage_Normal(MyColor[,] ImageMatrix, double New)
        {
            // Start a new StopWatch object for calculating the time
            // The variable stopWatch is a GLOBAL variable.
            stopWatch = Stopwatch.StartNew();

            MyColorDouble OldMin = new MyColorDouble();
            MyColorDouble OldMax = new MyColorDouble();
            MyColorDouble NewMin = new MyColorDouble();
            MyColorDouble NewMax = new MyColorDouble();

            MyColorDouble NewPixel = new MyColorDouble();

            MyColor X = new MyColor();
            MyColor[,] NewImageMatrix = new MyColor[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];

            /* To calculate Minimum and Maximum Pixel in the image */
            /* Normal Algorithm */
            GetMinMax_Normal(ImageMatrix, ref OldMin.red, ref OldMax.red, ref OldMin.blue, ref OldMax.blue, ref OldMin.green, ref OldMax.green);

            NewMin.red = OldMin.red - New;
            NewMax.red = OldMax.red + New;

            NewMin.blue = OldMin.red - New;
            NewMax.blue = OldMax.red + New;

            NewMin.green = OldMin.red - New;
            NewMax.green = OldMax.red + New;

            for (int i = 0; i < GetHeight(ImageMatrix); i++)
            {
                for (int j = 0; j < GetWidth(ImageMatrix); j++)
                {
                    NewPixel.red = ((ImageMatrix[i, j].red - OldMin.red) / (OldMax.red - OldMin.red)) * (NewMax.red - NewMin.red) + NewMin.red;
                    NewPixel.blue = ((ImageMatrix[i, j].blue - OldMin.blue) / (OldMax.red - OldMin.blue)) * (NewMax.blue - NewMin.blue) + NewMin.blue;
                    NewPixel.green = ((ImageMatrix[i, j].green - OldMin.green) / (OldMax.green - OldMin.green)) * (NewMax.green - NewMin.green) + NewMin.green;

                    if (NewPixel.red > 255)
                        NewPixel.red = 255;
                    if (NewPixel.red < 0)
                        NewPixel.red = 0;

                    if (NewPixel.blue > 255)
                        NewPixel.blue = 255;
                    if (NewPixel.blue < 0)
                        NewPixel.blue = 0;

                    if (NewPixel.green > 255)
                        NewPixel.green = 255;
                    if (NewPixel.green < 0)
                        NewPixel.green = 0;

                    X.red = (byte)(NewPixel.red);
                    X.blue = (byte)(NewPixel.blue);
                    X.green = (byte)(NewPixel.green);

                    NewImageMatrix[i, j] = X;
                }
            }

            // Calculating the time
            stopWatch.Stop();
            elapsedMs = stopWatch.ElapsedMilliseconds;

            // Return the new contrasted image
            return NewImageMatrix;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ImageMatrix"></param>
        /// <param name="New"></param>
        /// <returns></returns>
        public static MyColor[,] ContrastImage_Efficient(MyColor[,] ImageMatrix, double New)
        {
            // Start a new StopWatch object for calculating the time
            // The variable stopWatch is a GLOBAL variable.
            stopWatch = Stopwatch.StartNew();

            MyColorDouble OldMin = new MyColorDouble();
            MyColorDouble OldMax = new MyColorDouble();
            MyColorDouble NewMin = new MyColorDouble();
            MyColorDouble NewMax = new MyColorDouble();

            MyColorDouble NewPixel = new MyColorDouble();

            MyColor X = new MyColor();
            MyColor[,] NewImageMatrix = new MyColor[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];

            /* To calculate Minimum and Maximum Pixel in the image */
            /* Efficient Algorithm */
            GetMinMax_Efficient(ImageMatrix, ref OldMin.red, ref OldMax.red, ref OldMin.blue, ref OldMax.blue, ref OldMin.green, ref OldMax.green);

            NewMin.red = OldMin.red - New;
            NewMax.red = OldMax.red + New;

            NewMin.blue = OldMin.red - New;
            NewMax.blue = OldMax.red + New;

            NewMin.green = OldMin.red - New;
            NewMax.green = OldMax.red + New;

            for (int i = 0; i < GetHeight(ImageMatrix); i++)
            {
                for (int j = 0; j < GetWidth(ImageMatrix); j++)
                {
                    NewPixel.red = ((ImageMatrix[i, j].red - OldMin.red) / (OldMax.red - OldMin.red)) * (NewMax.red - NewMin.red) + NewMin.red;
                    NewPixel.blue = ((ImageMatrix[i, j].blue - OldMin.blue) / (OldMax.red - OldMin.blue)) * (NewMax.blue - NewMin.blue) + NewMin.blue;
                    NewPixel.green = ((ImageMatrix[i, j].green - OldMin.green) / (OldMax.green - OldMin.green)) * (NewMax.green - NewMin.green) + NewMin.green;

                    if (NewPixel.red > 255)
                        NewPixel.red = 255;
                    if (NewPixel.red < 0)
                        NewPixel.red = 0;

                    if (NewPixel.blue > 255)
                        NewPixel.blue = 255;
                    if (NewPixel.blue < 0)
                        NewPixel.blue = 0;

                    if (NewPixel.green > 255)
                        NewPixel.green = 255;
                    if (NewPixel.green < 0)
                        NewPixel.green = 0;

                    X.red = (byte)(NewPixel.red);
                    X.blue = (byte)(NewPixel.blue);
                    X.green = (byte)(NewPixel.green);

                    NewImageMatrix[i, j] = X;
                }
            }

            // Calculating the time
            stopWatch.Stop();
            elapsedMs = stopWatch.ElapsedMilliseconds;

            // Return the new contrasted image
            return NewImageMatrix;
        }

        /// <summary>
        /// Blur Image - Normal Algorithm
        /// </summary>
        /// <param name="ImageMatrix">2D ImageMatrix that contains the image</param>
        /// <param name="BlurMaskSize">Blur Degree</param>
        /// <returns>2D ImageMatrix of contrasted image colors</returns>
        public static MyColor[,] BlurImage_Normal(MyColor[,] ImageMatrix, int BlurMaskSize)
        {
            // Start a new StopWatch object for calculating the time
            // The variable stopWatch is a GLOBAL variable.
            stopWatch = Stopwatch.StartNew();

            // MAP stores blurr degree and index of center of mask
            Dictionary<int, int> MaskCenterElement = new Dictionary<int, int>();
            MaskCenterElement.Add(3, 1);
            MaskCenterElement.Add(5, 2);
            MaskCenterElement.Add(7, 3);
            MaskCenterElement.Add(9, 4);
            MaskCenterElement.Add(11, 5);
            MaskCenterElement.Add(13, 6);
            MaskCenterElement.Add(15, 7);
            MaskCenterElement.Add(17, 8);

            // Blur Scalar number
            double MaskElementValue = 1.0 / (BlurMaskSize * BlurMaskSize);

            // Center Index of needed blurr degree
            int MaskCenterIndex = 0;
            foreach (var index in MaskCenterElement)
            {
                if (index.Key == BlurMaskSize)
                {
                    MaskCenterIndex = index.Value;
                    break;
                }
            }

            // Original image matrix (size: Height+(CenterIndex * 2) x Width+(CenterIndex * 2))
            MyColor[,] OriginalImage = new MyColor[GetHeight(ImageMatrix) + (MaskCenterIndex * 2), GetWidth(ImageMatrix) + (MaskCenterIndex * 2)];
            for (int x = MaskCenterIndex; x < GetHeight(ImageMatrix) + MaskCenterIndex; x++)
            {
                for (int y = MaskCenterIndex; y < GetWidth(ImageMatrix) + MaskCenterIndex; y++)
                    OriginalImage[x, y] = ImageMatrix[x - MaskCenterIndex, y - MaskCenterIndex];
            }

            // The new blurred image matrix
            MyColor[,] NewImageMatrix = new MyColor[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];
            // Blur Mask Matrix
            MyColor[,] BlurMask = new MyColor[BlurMaskSize, BlurMaskSize];

            // N rows
            for (int m = 0; m < GetHeight(OriginalImage) - BlurMaskSize; m++)
            {
                // N columns
                for (int n = 0; n < GetWidth(OriginalImage) - BlurMaskSize; n++)
                {
                    MyColorDouble BlurMaskPixel = new MyColorDouble();
                    BlurMaskPixel.red = 0; BlurMaskPixel.green = 0; BlurMaskPixel.blue = 0;

                    MyColorDouble SumOfMaskTimesOldPixel = new MyColorDouble();
                    SumOfMaskTimesOldPixel.red = 0; 
                    SumOfMaskTimesOldPixel.green = 0; 
                    SumOfMaskTimesOldPixel.blue = 0;

                    // Create-Fill Mask Values
                    for (int i = 0, v = m; v < m + BlurMaskSize; i++, v++)  //k  rows
                    {
                        for (int j = 0, x = n; x < n + BlurMaskSize; j++, x++)  //k cols
                        {
                            BlurMaskPixel.red = (MaskElementValue * OriginalImage[v, x].red);
                            BlurMaskPixel.green = (MaskElementValue * OriginalImage[v, x].green);
                            BlurMaskPixel.blue = (MaskElementValue * OriginalImage[v, x].blue);

                            SumOfMaskTimesOldPixel.red += BlurMaskPixel.red;
                            SumOfMaskTimesOldPixel.green += BlurMaskPixel.green;
                            SumOfMaskTimesOldPixel.blue += BlurMaskPixel.blue;
                        }
                    }

                    NewImageMatrix[m, n].red = (byte)SumOfMaskTimesOldPixel.red;
                    NewImageMatrix[m, n].green = (byte)SumOfMaskTimesOldPixel.green;
                    NewImageMatrix[m, n].blue = (byte)SumOfMaskTimesOldPixel.blue;
                }
            }

            // Calculating the time
            stopWatch.Stop();
            elapsedMs = stopWatch.ElapsedMilliseconds;

            // Return the new blurred image
            return NewImageMatrix;
        }

        /// <summary>
        /// Blur Image - Efficient Algorithm
        /// </summary>
        /// <param name="ImageMatrix">2D ImageMatrix that contains the image</param>
        /// <param name="BlurMaskSize">Blur Degree</param>
        /// <returns>2D ImageMatrix of contrasted image colors</returns>
        public static MyColor[,] BlurImage_Efficient(MyColor[,] ImageMatrix, int BlurMaskSize)
        {
            // Start a new StopWatch object for calculating the time
            // The variable stopWatch is a GLOBAL variable.
            stopWatch = Stopwatch.StartNew();

            // MAP stores blurr degree and index of center of mask
            Dictionary<int, int> MaskCenterElement = new Dictionary<int, int>();
            MaskCenterElement.Add(3, 1);
            MaskCenterElement.Add(5, 2);
            MaskCenterElement.Add(7, 3);
            MaskCenterElement.Add(9, 4);
            MaskCenterElement.Add(11, 5);
            MaskCenterElement.Add(13, 6);
            MaskCenterElement.Add(15, 7);
            MaskCenterElement.Add(17, 8);

            double MaskElementValue = 1.0 / (BlurMaskSize * BlurMaskSize);
            double Value = 1.0 / BlurMaskSize;

            // Center Index of needed blurr degree
            int MaskCenterIndex = 0;
            foreach (var index in MaskCenterElement)
            {
                if (index.Key == BlurMaskSize)
                {
                    MaskCenterIndex = index.Value;
                    break;
                }
            }

            // Original image matrix (size: Height+(CenterIndex * 2) x Width+(CenterIndex * 2))
            MyColor[,] OriginalImage = new MyColor[GetHeight(ImageMatrix) + (MaskCenterIndex * 2), GetWidth(ImageMatrix) + (MaskCenterIndex * 2)];
            for (int x = MaskCenterIndex; x < GetHeight(ImageMatrix) + MaskCenterIndex; x++)
            {
                for (int y = MaskCenterIndex; y < GetWidth(ImageMatrix) + MaskCenterIndex; y++)
                    OriginalImage[x, y] = ImageMatrix[x - MaskCenterIndex, y - MaskCenterIndex];
            }

            // The new blurred image matrix
            MyColor[,] NewImageMatrix = new MyColor[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];

            MyColor[,] tempMatrix = new MyColor[GetHeight(OriginalImage), GetWidth(OriginalImage)];

            MyColorDouble SumOfMaskTimesOldPixel = new MyColorDouble();
            SumOfMaskTimesOldPixel.red = 0; SumOfMaskTimesOldPixel.green = 0;
            SumOfMaskTimesOldPixel.blue = 0;

            //Move Horizontally
            //"mRows" moves all the rows in the picture
            //"nCols" ONLY is constrained to the #of columns - BlurMaskSize
            for (int mRows = 0; mRows < GetHeight(OriginalImage); mRows++)
            {
                for (int nCols = 0; nCols < GetWidth(OriginalImage) - BlurMaskSize; nCols++)
                {
                    SumOfMaskTimesOldPixel.red = 0;
                    SumOfMaskTimesOldPixel.green = 0;
                    SumOfMaskTimesOldPixel.blue = 0;
                    for (int k = 0; k < BlurMaskSize; k++)
                    {
                        SumOfMaskTimesOldPixel.red += (Value * OriginalImage[mRows, nCols + k].red);
                        SumOfMaskTimesOldPixel.green += (Value * OriginalImage[mRows, nCols + k].green);
                        SumOfMaskTimesOldPixel.blue += (Value * OriginalImage[mRows, nCols + k].blue);
                    }

                    tempMatrix[mRows, nCols + MaskCenterIndex].red = (byte)SumOfMaskTimesOldPixel.red;
                   tempMatrix[mRows, nCols + MaskCenterIndex].green = (byte)SumOfMaskTimesOldPixel.green;
                    tempMatrix[mRows, nCols + MaskCenterIndex].blue = (byte)SumOfMaskTimesOldPixel.blue;
                }
            }

            //Move Vertically
            /*"nCols" moves all the columns in the picture minus CenterIndex
             *"mRows" ONLY is constrained to the #of rows - BlurMaskSize
             *"nCols" starts at the CenterIndex column b/z NewImageMatrix starts from the 2nd column in tempMatrix and not the first.
             *For the same reason it ends at the last column "-1", because NewImageMatrix doesn't have the last column in the tempMatrix.
             */
            for (int mRows = 0; mRows < GetHeight(tempMatrix) - BlurMaskSize; mRows++)
            {
                for (int nCols = MaskCenterIndex; nCols < GetWidth(tempMatrix)-MaskCenterIndex; nCols++)
                {
                    SumOfMaskTimesOldPixel.red = 0;
                    SumOfMaskTimesOldPixel.green = 0;
                    SumOfMaskTimesOldPixel.blue = 0;
                    for (int k = 0; k < BlurMaskSize; k++)
                    {
                        SumOfMaskTimesOldPixel.red += (Value * tempMatrix[mRows + k, nCols].red);
                        SumOfMaskTimesOldPixel.green += (Value * tempMatrix[mRows + k, nCols].green);
                        SumOfMaskTimesOldPixel.blue += (Value * tempMatrix[mRows + k, nCols].blue);
                    }

                    NewImageMatrix[mRows, nCols - MaskCenterIndex].red = (byte)SumOfMaskTimesOldPixel.red;
                    NewImageMatrix[mRows, nCols - MaskCenterIndex].green = (byte)SumOfMaskTimesOldPixel.green;
                    NewImageMatrix[mRows, nCols - MaskCenterIndex].blue = (byte)SumOfMaskTimesOldPixel.blue;
                }
            }

            // Calculating the time
            stopWatch.Stop();
            elapsedMs = stopWatch.ElapsedMilliseconds;

            // Return the new blurred image
            return NewImageMatrix;
        }

        /// <summary>
        /// Gamma - Get Minimum and Maximum
        /// </summary>
        public static void Gamma_GetMinMax(MyColorDouble[,] GImageMatrix, ref double redmin, ref double redmax, ref double bluemin, ref double bluemax, ref double greenmin, ref double greenmax)
        {
            redmin = 100000;
            redmax = 0;
            bluemin = 100000;
            bluemax = 0;
            greenmin = 100000;
            greenmax = 0;

            int Height = GetHeightDouble(GImageMatrix);
            int Width = GetWidthDouble(GImageMatrix);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {

                    if (GImageMatrix[i, j].red < redmin)
                        redmin = GImageMatrix[i, j].red;

                    if (GImageMatrix[i, j].blue < bluemin)
                        bluemin = GImageMatrix[i, j].blue;

                    if (GImageMatrix[i, j].green < greenmin)
                        greenmin = GImageMatrix[i, j].green;
                }

            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {

                    if (GImageMatrix[i, j].red > redmax)
                        redmax = GImageMatrix[i, j].red;

                    if (GImageMatrix[i, j].blue > bluemax)
                        bluemax = GImageMatrix[i, j].blue;

                    if (GImageMatrix[i, j].green > greenmax)
                        greenmax = GImageMatrix[i, j].green;

                }
            }

        }

        /// <summary>
        /// Gamma - Contrast Image
        /// </summary>
        public static MyColor[,] GammaContrastImage(MyColorDouble[,] GImageMatrix)
        {
            MyColorDouble Old_Min, Old_Max;
            Old_Min.red = 0; Old_Min.green = 0; Old_Min.blue = 0;
            Old_Max.red = 0; Old_Max.green = 0; Old_Max.blue = 0;
            double NewPixelRed, NewPixelBlue, NewPixelGreen;

            MyColor X = new MyColor();
            MyColor[,] NewImageMatrix = new MyColor[GetHeightDouble(GImageMatrix), GetWidthDouble(GImageMatrix)];

            Gamma_GetMinMax(GImageMatrix, ref Old_Min.red, ref Old_Max.red, ref Old_Min.blue, ref Old_Max.blue, ref Old_Min.green, ref Old_Max.green);

            for (int i = 0; i < GetHeightDouble(GImageMatrix); i++)
            {
                for (int j = 0; j < GetWidthDouble(GImageMatrix); j++)
                {
                    NewPixelRed = ((GImageMatrix[i, j].red - Old_Min.red) / (Old_Max.red - Old_Min.red)) * (255 - 0) + 0;
                    NewPixelBlue = ((GImageMatrix[i, j].blue - Old_Min.blue) / (Old_Max.blue - Old_Min.blue)) * (255 - 0) + 0;
                    NewPixelGreen = ((GImageMatrix[i, j].green - Old_Min.green) / (Old_Max.green - Old_Min.green)) * (255 - 0) + 0;

                    if (NewPixelRed > 255)
                        NewPixelRed = 255;
                    if (NewPixelRed < 0)
                        NewPixelRed = 0;

                    if (NewPixelBlue > 255)
                        NewPixelBlue = 255;
                    if (NewPixelBlue < 0)
                        NewPixelBlue = 0;

                    if (NewPixelGreen > 255)
                        NewPixelGreen = 255;
                    if (NewPixelGreen < 0)
                        NewPixelGreen = 0;

                    X.red = (byte)(NewPixelRed);
                    X.blue = (byte)(NewPixelBlue);
                    X.green = (byte)(NewPixelGreen);

                    NewImageMatrix[i, j] = X;
                }
            }
            return NewImageMatrix;
        }

        /// <summary>
        /// Calculate Gamma Correction - Normal
        /// </summary>
        public static MyColor[,] CalculateGammaCorrection_Normal(MyColor[,] ImageMatrix, MyColor[,] NonImageMatrix)
        {
            // Start a new StopWatch object for calculating the time
            // The variable stopWatch is a GLOBAL variable.
            stopWatch = Stopwatch.StartNew();

            MyColorDouble Y = new MyColorDouble();
            MyColorDouble[,] GammaCImage = new MyColorDouble[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];
            MyColor[,] ModGammaCImage = new MyColor[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];
            var errors = new Dictionary<double, double>();


            MyColor X = new MyColor();
            MyColor[,] GammaCorrImage = new MyColor[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];

            MyColorDouble New, diff;
            double error, sum, minerror = 10000000, difference;
            double Height = GetHeight(ImageMatrix);
            double Width = GetWidth(ImageMatrix);

            for (double x = 0.1; x < 5; x += 0.1)
            {
                sum = 0;
                difference = 0;
                diff.red = 0;
                diff.blue = 0;
                diff.green = 0;

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        New.red = Math.Pow(NonImageMatrix[i, j].red, x);
                        New.blue = Math.Pow(NonImageMatrix[i, j].blue, x);
                        New.green = Math.Pow(NonImageMatrix[i, j].green, x);

                        Y.red = New.red;
                        Y.blue = New.blue;
                        Y.green = New.green;

                        GammaCImage[i, j] = Y;
                    }
                }

                ModGammaCImage = GammaContrastImage(GammaCImage);

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        diff.red = ImageMatrix[i, j].red - ModGammaCImage[i, j].red;
                        diff.blue = ImageMatrix[i, j].blue - ModGammaCImage[i, j].blue;
                        diff.green = ImageMatrix[i, j].green - ModGammaCImage[i, j].green;

                        difference = Math.Abs(diff.red) + Math.Abs(diff.blue) + Math.Abs(diff.green);
                        sum += Math.Pow(difference, 2);
                    }
                }
                error = (1 / (Height * Width)) * sum;
                errors[x] = error;
            }

            foreach (var index in errors)
            {
                if (minerror >= index.Value)
                {
                    minerror = index.Value;
                    gammavalueNormal = index.Key;
                }
            }

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    New.red = Math.Pow(NonImageMatrix[i, j].red, gammavalueNormal);
                    New.blue = Math.Pow(NonImageMatrix[i, j].blue, gammavalueNormal);
                    New.green = Math.Pow(NonImageMatrix[i, j].green, gammavalueNormal);

                    Y.red = New.red;
                    Y.blue = New.blue;
                    Y.green = New.green;

                    GammaCImage[i, j] = Y;
                }
            }

            ModGammaCImage = GammaContrastImage(GammaCImage);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    X.red = (byte)(ModGammaCImage[i, j].red);
                    X.blue = (byte)(ModGammaCImage[i, j].blue);
                    X.green = (byte)(ModGammaCImage[i, j].green);

                    GammaCorrImage[i, j] = X;
                }
            }

            // Calculating the time
            stopWatch.Stop();
            elapsedMs = stopWatch.ElapsedMilliseconds;

            return GammaCorrImage;
        }

        /// <summary>
        /// Unimodel Search
        /// </summary>
        public static double UNIMODALSEARCH(MyColor[,] ImageMatrix, MyColor[,] NonImageMatrix, double low, double high)
        {
            double mid = 0;
            MyColor[,] ModGammaCImage = new MyColor[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];
            MyColorDouble Y = new MyColorDouble();
            MyColorDouble[,] GammaCImage = new MyColorDouble[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];

            MyColorDouble New,diff;
            double Height = GetHeight(ImageMatrix);
            double Width = GetWidth(ImageMatrix);
            double difference , sum , error;

            double[] errors = new double[3];
            int a = 0;

            if (low == high)
            {
                return low;
            }
         
            if ( (high+1) % 2 == 0)
                mid = (low + high) / 2;
            else
                mid = (low + high) / 2 + 1;

            for (double k = mid - 1 ; k <= mid + 1; k++)
            {
                sum = 0;

                for (int i = 0; i < GetHeight(ImageMatrix); i++)
                {
                    for (int j = 0; j < GetWidth(ImageMatrix); j++)
                    {

                        New.red = Math.Pow(NonImageMatrix[i, j].red, k);
                        New.blue = Math.Pow(NonImageMatrix[i, j].blue, k);
                        New.green = Math.Pow(NonImageMatrix[i, j].green, k);

                        Y.red = New.red;
                        Y.blue = New.blue;
                        Y.green = New.green;

                        GammaCImage[i, j] = Y;
                    }
                }

                ModGammaCImage = GammaContrastImage(GammaCImage);

                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        diff.red = ImageMatrix[i, j].red - ModGammaCImage[i, j].red;
                        diff.blue = ImageMatrix[i, j].blue - ModGammaCImage[i, j].blue;
                        diff.green = ImageMatrix[i, j].green - ModGammaCImage[i, j].green;

                        difference = Math.Abs(diff.red) + Math.Abs(diff.blue) + Math.Abs(diff.green);
                        sum += Math.Pow(difference, 2);
                    }
                }
                error = (1 / (Height * Width)) * sum;
                errors[a] = error;
                a++;
            }
           
            if (errors[1] < errors[0] && errors[1] < errors[2])
                return mid;

            if (errors[1] < errors[0])
                return UNIMODALSEARCH(ImageMatrix , NonImageMatrix, mid + 1, high);

            else
                return UNIMODALSEARCH(ImageMatrix, NonImageMatrix, low, mid - 1);
        }

        /// <summary>
        /// Calculate Gamma Correction - Efficient
        /// </summary>
        public static MyColor[,] CalculateGammaCorrection_Efficient(MyColor[,] ImageMatrix, MyColor[,] NonImageMatrix)
        {
            // Start a new StopWatch object for calculating the time
            // The variable stopWatch is a GLOBAL variable.
            stopWatch = Stopwatch.StartNew();

            MyColorDouble New ;
            MyColor X = new MyColor();
            MyColor[,] GammaCorrImage = new MyColor[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];
            MyColor[,] ModGammaCImage = new MyColor[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];
            MyColorDouble Y = new MyColorDouble();
            MyColorDouble[,] GammaCImage = new MyColorDouble[GetHeight(ImageMatrix), GetWidth(ImageMatrix)];

            double Height = GetHeight(ImageMatrix);
            double Width = GetWidth(ImageMatrix);


            gammavalueEfficient = UNIMODALSEARCH(ImageMatrix, NonImageMatrix, 0.1, 5);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    New.red = Math.Pow(NonImageMatrix[i, j].red, gammavalueEfficient);
                    New.blue = Math.Pow(NonImageMatrix[i, j].blue, gammavalueEfficient);
                    New.green = Math.Pow(NonImageMatrix[i, j].green, gammavalueEfficient);

                    Y.red = New.red;
                    Y.blue = New.blue;
                    Y.green = New.green;

                    GammaCImage[i, j] = Y;
                }
            }

            ModGammaCImage = GammaContrastImage(GammaCImage);

            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    X.red = (byte)(ModGammaCImage[i, j].red);
                    X.blue = (byte)(ModGammaCImage[i, j].blue);
                    X.green = (byte)(ModGammaCImage[i, j].green);

                    GammaCorrImage[i, j] = X;
                }
            }

            // Calculating the time
            stopWatch.Stop();
            elapsedMs = stopWatch.ElapsedMilliseconds;

            return GammaCorrImage;
        }

    }

}
