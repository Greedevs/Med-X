using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MedX.Desktop.Helpers;

public class HelperMethod
{
    public static T FindParent<T>(DependencyObject child) where T : DependencyObject
    {
        DependencyObject parentObject = VisualTreeHelper.GetParent(child);

        if (parentObject == null) return null;

        T parent = parentObject as T;
        if (parent != null)
            return parent;
        else
            return FindParent<T>(parentObject);
    }

}
