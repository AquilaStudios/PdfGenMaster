using System.IO;
using PdfSharp.Fonts;
namespace FontResolver;

public class CustomFontResolver : IFontResolver
{
    public string DefaultFontName => throw new NotImplementedException();

    public byte[] GetFont(string faceName)
    {
        using (var ms = new MemoryStream())
        {
            using (var fs = File.Open(faceName, FileMode.Open))
            {
                fs.CopyTo(ms);
                ms.Position = 0;
                return ms.ToArray();
            }
        }
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        if (familyName.Equals("arial", StringComparison.CurrentCultureIgnoreCase))
        {
            if (isBold)
            {
                return new FontResolverInfo("C:\\MyRepos\\PdfApp\\fonts\\G_ari_bd.ttf");
            }
            else if (isItalic)
            {
                return new FontResolverInfo("C:\\MyRepos\\PdfApp\\fonts\\G_ari_i.TTF.ttf");
            }
            else
            {
                return new FontResolverInfo("C:\\MyRepos\\PdfApp\\fonts\\arial.ttf");
            }
        }

        return null;
    }
    
}
