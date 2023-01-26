using System.Reflection;

namespace MPS.Domain.Modules.ReportCreationalModules.Extra;

public static class ReportFooter
{
    public static byte[] GetFooterImage()
    {
        string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"../../../../MPS.Domain/Modules/ReportCreationalModules/Extra/caator.jpg");
        return File.ReadAllBytes(path);
    }
}