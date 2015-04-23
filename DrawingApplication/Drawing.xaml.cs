using DrawingApplication.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Input.Inking;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace DrawingApplication
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class Drawing : Page
    {

        InkManager inkManager = new Windows.UI.Input.Inking.InkManager();
        Color penColor = Colors.Black;
        Color solidColor = Colors.White;

        DispatcherTimer myDispatcherTimer = new DispatcherTimer();

        public uint pendId;
        public int touchId;

        Point prevDrawPoint;
        Point currentDrawPoint;

        Ellipse newCircle;
        Rectangle newRect;
        Line newLine;

        private double x1, y1, x2, y2;

        string drawMode;

        int timeLeft = 120;

        public Drawing()
        {
            this.InitializeComponent();

            CanvasMain.PointerPressed += new PointerEventHandler(OnCanvasPressed);
            CanvasMain.PointerMoved += new PointerEventHandler(OnCanvasMoved);
            CanvasMain.PointerReleased += new PointerEventHandler(OnCanvasReleased);
            CanvasMain.PointerExited += new PointerEventHandler(OnCanvasReleased);

            myDispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            myDispatcherTimer.Tick += Each_Tick;
            myDispatcherTimer.Start();

            var colours = typeof(Colors).GetTypeInfo().DeclaredProperties;
            
            foreach (var item in colours)
            {
                cbChangeColour.Items.Add(item);
                cbFillColour.Items.Add(item);
            }
        }

        public void Each_Tick(object sender, object e)
        {
            textBlock1.Text = "Time Left: " + timeLeft--.ToString();

            if (timeLeft == 0)
            {
                globalVar.NumOfPlayersThatDrew++;
                globalVar.isDrawingDone = true;
                myDispatcherTimer.Stop();
                Frame.Navigate(typeof(GamePage));
            }
        }

        #region Pointer Event Methods
        private void OnCanvasReleased(object sender, PointerRoutedEventArgs e)
        {
            if (e.Pointer.PointerId == pendId)
            {
                PointerPoint point = e.GetCurrentPoint(CanvasMain);

                inkManager.ProcessPointerUp(point);
            }

            pendId = 0;
            touchId = 0;
            newLine = null;
            newRect = null;
            newCircle = null;

            e.Handled = true;
        }

        private void OnCanvasMoved(object sender, PointerRoutedEventArgs e)
        {

            switch (drawMode)
            {
                case "pen":
                    {
                        if (e.Pointer.PointerId == pendId)
                        {
                            PointerPoint point = e.GetCurrentPoint(CanvasMain);

                            currentDrawPoint = point.Position;
                            x1 = prevDrawPoint.X;
                            y1 = prevDrawPoint.Y;
                            x2 = currentDrawPoint.X;
                            y2 = currentDrawPoint.Y;

                            if (Distance(x1, y1, x2, y2) > 2.0)
                            {
                                Line drawLine = new Line()
                                {
                                    X1 = x1,
                                    Y1 = y1,
                                    X2 = x2,
                                    Y2 = y2,
                                    StrokeThickness = 4.0,
                                    Stroke = new SolidColorBrush(penColor)
                                };

                                prevDrawPoint = currentDrawPoint;

                                CanvasMain.Children.Add(drawLine);

                                inkManager.ProcessPointerUpdate(point);
                            }
                        }
                    }
                    break;

                case "line":
                    {
                        PointerPoint point = e.GetCurrentPoint(CanvasMain);
                        prevDrawPoint = point.Position;

                        PointerDeviceType pointerType = e.Pointer.PointerDeviceType;

                        if (pointerType == PointerDeviceType.Pen ||
                            pointerType == PointerDeviceType.Mouse &&
                            point.Properties.IsLeftButtonPressed)
                        {
                            newLine.X2 = e.GetCurrentPoint(CanvasMain).Position.X;
                            newLine.Y2 = e.GetCurrentPoint(CanvasMain).Position.Y;
                        }
                    }
                break;

                case "rect":
                    {
                        PointerPoint point = e.GetCurrentPoint(CanvasMain);
                        prevDrawPoint = point.Position;

                        PointerDeviceType pointerType = e.Pointer.PointerDeviceType;

                        if (pointerType == PointerDeviceType.Pen ||
                            pointerType == PointerDeviceType.Mouse &&
                            point.Properties.IsLeftButtonPressed)
                        {
                            x2 = e.GetCurrentPoint(CanvasMain).Position.X;
                            y2 = e.GetCurrentPoint(CanvasMain).Position.Y;

                            if ((x2 - x1) > 0 && (y2 - y1) > 0)
                                newRect.Margin = new Thickness(x1, y1, x2, y2);
                            else if ((x2 - x1) < 0)
                                newRect.Margin = new Thickness(x2, y1, x1 ,y2);
                            else if ((x2 - x1) < 0 && (y2 - y1) < 0)
                                newRect.Margin = new Thickness(x2, y1, x1, y2);
                            else if ((y2 - y1) < 0)
                                newRect.Margin = new Thickness(x1, y2, x2 ,y1);

                            newRect.Width = Math.Abs(x2 - x1);
                            newRect.Height = Math.Abs(y2 - y1);
                        }
                    }
                break;

                case "circle":
                    {
                         PointerPoint point = e.GetCurrentPoint(CanvasMain);
                        prevDrawPoint = point.Position;

                        PointerDeviceType pointerType = e.Pointer.PointerDeviceType;

                        if (pointerType == PointerDeviceType.Mouse &&
                            point.Properties.IsLeftButtonPressed)
                        {
                            x2 = e.GetCurrentPoint(CanvasMain).Position.X;
                            y2 = e.GetCurrentPoint(CanvasMain).Position.Y;
                            
                            if ((x2 - x1) > 0 && (y2 - y1) > 0)
                                newCircle.Margin = new Thickness(x1, y1, x2, y2);
                            else if ((x2 - x1) < 0)
                                newCircle.Margin = new Thickness(x2, y1, x1, y2);
                            else if ((y2 - y1) < 0)
                                newCircle.Margin = new Thickness(x1, y2, x2, y1);
                            else if ((x2 - x1) < 0 && (y2 - y1) < 0)
                                newCircle.Margin = new Thickness(x2, y1, x1, y2);
                            
                            newCircle.Width = Math.Abs(x2 - x1);
                            newCircle.Height = Math.Abs(y2 - y1);
                        }
                    }
                break;
                
                default:
                    break;
            }
           
        }

        private double Distance(double x1, double y1, double x2, double y2)
        {
            double d = 0;
            d = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
            return d;
        }

        private void OnCanvasPressed(object sender, PointerRoutedEventArgs e)
        {
            PointerPoint point = e.GetCurrentPoint(CanvasMain);
            prevDrawPoint = point.Position;

            PointerDeviceType pointerType = e.Pointer.PointerDeviceType;

            switch (drawMode)
            {
                case "line":
                    {
                        newLine = new Line();
                        newLine.X1 = e.GetCurrentPoint(CanvasMain).Position.X;
                        newLine.Y1 = e.GetCurrentPoint(CanvasMain).Position.Y;
                        newLine.X2 = newLine.X1 + 1;
                        newLine.Y2 = newLine.Y2 + 1;
                        newLine.StrokeThickness = 4.0;
                        newLine.Stroke = new SolidColorBrush(penColor);

                        CanvasMain.Children.Add(newLine);
                    }
                    break;

                case "pen":
                    {
                        
                        if (pointerType == PointerDeviceType.Pen ||
                            pointerType == PointerDeviceType.Mouse &&
                            point.Properties.IsLeftButtonPressed)
                        {
                            inkManager.ProcessPointerDown(point);
                            pendId = point.PointerId;
                            e.Handled = true;
                        }
                        
                        
                    }
                    break;

                case "rect":
                    {
                        newRect = new Rectangle();
                        x1 = e.GetCurrentPoint(CanvasMain).Position.X;
                        y1 = e.GetCurrentPoint(CanvasMain).Position.Y;
                        x2 = x1;
                        y2 = y1;
                        newRect.Width = x2 - x1;
                        newRect.Height = y2 - y1;
                        newRect.Stroke = new SolidColorBrush(penColor);
                        newRect.Fill = new SolidColorBrush(solidColor);
                        CanvasMain.Children.Add(newRect);
                    }
                    break;

                case "circle":
                    {
                        newCircle = new Ellipse();
                        x1 = e.GetCurrentPoint(CanvasMain).Position.X;
                        y1 = e.GetCurrentPoint(CanvasMain).Position.Y;
                        x2 = x1;
                        y2 = y1;
                        newCircle.Width = x2 - x1;
                        newCircle.Height = y2 - y1;
                        newCircle.Stroke = new SolidColorBrush(penColor);
                        newCircle.Fill = new SolidColorBrush(solidColor);
                        CanvasMain.Children.Add(newCircle);
                    }
                    break;
                
                default:
                    break;
            } 
           
        }
        #endregion

        #region fill comboboxes
        private void cbChangeColour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbChangeColour.SelectedIndex != -1)
            {
                var pi = cbChangeColour.SelectedItem as PropertyInfo;
                penColor = (Color)pi.GetValue(null);
            }
        }

        private void cbFillColour_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFillColour.SelectedIndex != -1)
            {
                var pi = cbFillColour.SelectedItem as PropertyInfo;
                solidColor = (Color)pi.GetValue(null);
            }
        }
        #endregion

        #region drawMode buttons
        private void penbtn_Click(object sender, RoutedEventArgs e)
        {
            drawMode = "pen";
        }

        private void linebtn_Click(object sender, RoutedEventArgs e)
        {
            drawMode = "line";
        }

        private void circlebtn_Click(object sender, RoutedEventArgs e)
        {
            drawMode = "circle";
        }

        private void rectbtn_Click(object sender, RoutedEventArgs e)
        {
            drawMode = "rect";
        }

        private void erasebtn_Click(object sender, RoutedEventArgs e)
        {
            drawMode = "erase";
        }
        #endregion

        private async void savebtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Windows.Storage.Pickers.FileSavePicker savePic = new Windows.Storage.Pickers.FileSavePicker();

                savePic.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.Desktop;
                savePic.DefaultFileExtension = ".png";

                savePic.FileTypeChoices.Add("PNG", new string[] { ".png" });
                StorageFile saveFile = await savePic.PickSaveFileAsync();
                IOutputStream ab = await saveFile.OpenAsync(FileAccessMode.ReadWrite);

                if (ab != null)
                {
                    await inkManager.SaveAsync(ab);
                }
            }
            catch (Exception)
            {
                var error = new MessageDialog("Image is empty, cannot be saved.");
                error.ShowAsync();
            }
        }

        private void stopbtn_Click(object sender, RoutedEventArgs e)
        {
            myDispatcherTimer.Stop();
            globalVar.NumOfPlayersThatDrew++;
            globalVar.isDrawingDone = true;
            Frame.Navigate(typeof(GamePage));
        }

        private void clearbtn_Click(object sender, RoutedEventArgs e)
        {
            inkManager.Mode = InkManipulationMode.Erasing;

            var strokes = inkManager.GetStrokes();

            for (int i = 0; i < strokes.Count; i++)
            {
                strokes[i].Selected = true;
            }

            inkManager.DeleteSelected();

            CanvasMain.Background = new SolidColorBrush(Colors.White);
            CanvasMain.Children.Clear();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GamePage));
            myDispatcherTimer.Stop();
        }

       
    }
}
