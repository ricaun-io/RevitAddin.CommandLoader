using System;

namespace RevitAddin.CommandLoader.Revit
{
    public static class InfoCenterUtils
    {
        public static void ShowBalloon(string title, string category = null, string uriString = null)
        {
            if (title == null) return;
            Autodesk.Internal.InfoCenter.ResultItem ri = new Autodesk.Internal.InfoCenter.ResultItem();
            ri.Category = category ?? typeof(InfoCenterUtils).Assembly.GetName().Name;
            ri.Title = title.Trim();
            if (Uri.TryCreate(uriString, UriKind.RelativeOrAbsolute, out Uri uri))
                ri.Uri = uri;
            Autodesk.Windows.ComponentManager.InfoCenterPaletteManager.ShowBalloon(ri);
        }
    }
}
