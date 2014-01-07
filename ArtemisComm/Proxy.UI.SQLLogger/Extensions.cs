using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ArtemisComm.Proxy.UI.SQLLogger
{
    public static class Extensions
    {
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static object UIThreadGetValue(this DependencyObject me, DependencyProperty dp)
        {
            object retVal = null;
            if (me != null)
            {
                if (Application.Current.Dispatcher != System.Windows.Threading.Dispatcher.CurrentDispatcher)
                {
                    retVal = Application.Current.Dispatcher.Invoke(
                        new Func<DependencyProperty, object>(me.GetValue), dp);

                }
                else
                {
                    retVal = me.GetValue(dp);

                }
            }
            else
            {
                retVal = null;
            }
            return retVal;

        }

        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void UIThreadSetValue(this DependencyObject me, DependencyProperty dp, object value)
        {
            if (me != null)
            {
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
                    me.SetValue(dp, value);
                }

            }
        }
    }
}
