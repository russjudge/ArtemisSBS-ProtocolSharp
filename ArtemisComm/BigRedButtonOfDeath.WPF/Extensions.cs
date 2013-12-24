using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BigRedButtonOfDeath.WPF
{
    public static class Extensions
    {
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static object UIThreadGetValue(this DependencyObject me, DependencyProperty dp)
        {
            //if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            object retVal = null;
            if (me != null)
            {
                //#if false

                if (Application.Current.Dispatcher != System.Windows.Threading.Dispatcher.CurrentDispatcher)
                {
                    retVal = Application.Current.Dispatcher.Invoke(
                        new Func<DependencyProperty, object>(me.GetValue), dp);

                }
                else
                {
                    //#endif
                    retVal = me.GetValue(dp);
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
        public static void UIThreadSetValue(this DependencyObject me, DependencyProperty dp, object value)
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
                            new Action<DependencyProperty, object>(me.SetValue), dp, value);
                    }
                }
                else
                {
                    //if (_log.IsInfoEnabled && dp != null)
                    //{
                    //#endif
                    //    _log.InfoFormat("Setting {0}.{1} to value {2}", me.GetType().ToString(), dp.GetType().ToString(), value);

                    //}
                    me.SetValue(dp, value);
                    //#if false    
                }
                //#endif

            }
            //if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }
        }
    }
}
