using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BigRedButtonOfDeath.WPF
{
    public static class Extensions
    {
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static object UIThreadGetValue(this DependencyObject me, DependencyProperty dependencyProperty)
        {
            //if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            object retVal = null;
            if (me != null)
            {
                //#if false

                if (Application.Current.Dispatcher != System.Windows.Threading.Dispatcher.CurrentDispatcher)
                {
                    retVal = Application.Current.Dispatcher.Invoke(
                        new Func<DependencyProperty, object>(me.GetValue), dependencyProperty);

                }
                else
                {
                    //#endif
                    retVal = me.GetValue(dependencyProperty);
                    //#if false

                }
                //#endif
            }
            else
            {
                retVal = null;
            }
            //if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }
            return retVal;

        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void UIThreadSetValue(this DependencyObject me, DependencyProperty dependencyProperty, object value)
        {
            //if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (me != null)
            {
                //#if false

                if (Application.Current.Dispatcher != System.Windows.Threading.Dispatcher.CurrentDispatcher)
                {
                    if (!Application.Current.Dispatcher.HasShutdownFinished
                        && !Application.Current.Dispatcher.HasShutdownStarted)
                    {
                        Application.Current.Dispatcher.Invoke(
                            new Action<DependencyProperty, object>(me.SetValue), dependencyProperty, value);
                    }
                }
                else
                {
                    //if (_log.IsInfoEnabled && dp != null)
                    //{
                    //#endif
                    //    _log.InfoFormat("Setting {0}.{1} to value {2}", me.GetType().ToString(), dp.GetType().ToString(), value);

                    //}
                    me.SetValue(dependencyProperty, value);
                    //#if false    
                }
                //#endif

            }
            //if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }
        }
        static public BitmapSource ToBitmapSource(this System.Drawing.Bitmap bitmap)
        {
            BitmapSource bm = null;
            if (bitmap != null)
            {
                IntPtr hBitmap = bitmap.GetHbitmap();
                BitmapSizeOptions sizeOptions = BitmapSizeOptions.FromEmptyOptions();
                bm = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, sizeOptions);
                bm.Freeze();
            }
            return bm;
        }
    }
}
