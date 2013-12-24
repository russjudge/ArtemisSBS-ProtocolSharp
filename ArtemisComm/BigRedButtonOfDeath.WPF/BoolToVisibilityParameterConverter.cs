using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace BigRedButtonOfDeath.WPF
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BoolToVisibilityParameterConverter : IValueConverter
    {
        #region IValueConverter Members

        /// <summary>
        /// Converts a boolean value to visibility.  Parameter is optional. Default is Visible|Collapsed.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.  Format: VISIBILITYifTRUE|VISIBILITYifFALSE</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility TrueVisibility = Visibility.Visible;
            Visibility FalseVisibility = Visibility.Collapsed;
            if (parameter != null)
            {
                //throw new ArgumentException(GeneralUtility.CallingMethod + ": Parameter is required! Format: (VisibilityIfTrue|VisibilityIfFalse)");


                string[] parmSettings = ((string)parameter).Split('|');

                TrueVisibility = (Visibility)Enum.Parse(typeof(Visibility), parmSettings[0]);
                FalseVisibility = (Visibility)Enum.Parse(typeof(Visibility), parmSettings[1]);
            }
            if (value == null)
            {
                return FalseVisibility;
            }
            else
            {
                try
                {
                    bool TestedValue = (bool)value;
                    return TestedValue ? TrueVisibility : FalseVisibility;
                }
                catch
                {
                    return FalseVisibility;
                }
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            Visibility TrueVisibility = Visibility.Visible;
            Visibility FalseVisibility = Visibility.Collapsed;
            if (parameter != null)
            {
                //throw new ArgumentException("Parameter is required! Format: (VisibilityIfTrue|VisibilityIfFalse)");

                string[] parmSettings = ((string)parameter).Split('|');

                TrueVisibility = (Visibility)Enum.Parse(typeof(Visibility), parmSettings[0]);
                FalseVisibility = (Visibility)Enum.Parse(typeof(Visibility), parmSettings[1]);
            }
            if (value != null)
            {
                try
                {
                    Visibility testedValue = (Visibility)value;
                    if (testedValue == FalseVisibility)
                    {
                        return false;
                    }
                    if (testedValue == TrueVisibility)
                    {
                        return true;
                    }
                    return false;


                }
                catch
                {
                    return false; ;
                }
            }
            return false;
        }

        #endregion
    }
}
