
using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using MigraDocCore.DocumentObjectModel.Shapes;
using MigraDocCore.DocumentObjectModel.Shapes.Charts;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.Rendering;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Pdf.Security;
using PdfSharpCore.Utils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using ZXing.ImageSharp.Rendering;



public class PdfUtility
{
    private readonly IWebHostEnvironment Env;
    //private readonly SqlService sqlService;

    public PdfUtility(IWebHostEnvironment env)//SqlService _sqlService)
    {
        Env = env;
        //sqlService = _sqlService;
        if (ImageSource.ImageSourceImpl == null)
        {
            ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();
        }
    }

    public Stream GenerateBarcodeStream(string QRCodeContent, int Width, int Height)//TagHelperContext context, TagHelperOutput output)
    {
        var margin = 0;
        var barcodeWriter = new ZXing.ImageSharp.BarcodeWriter<Rgba32>  //La32>
        {
            Format = ZXing.BarcodeFormat.CODE_128,
            Options = new ZXing.Common.EncodingOptions
            {
                Height = Height > 80 ? Height : 80,
                Width = Width > 400 ? Width : 400,
                PureBarcode = false,
                Margin = margin
            }
        };

        Image<Rgba32> image = barcodeWriter.Write(QRCodeContent);
        //string imageBase64 = image.ToBase64String(PngFormat.Instance);

        Stream imageStream = new MemoryStream();
        image.Save(imageStream, PngFormat.Instance);
        imageStream.Seek(0, SeekOrigin.Begin);
        return imageStream;
        //output.TagName = "img";
        //output.Attributes.Clear();
        //output.Attributes.Add("width", Width);
        //output.Attributes.Add("height", Height);
        //output.Attributes.Add("alt", Alt);
        //output.Attributes.Add("src", $"{image.ToBase64String(PngFormat.Instance)}");
    }
    public XImage Generate128BarcodeImage(string QRCodeContent, int width, int height)
    {
        if (QRCodeContent == null)
            QRCodeContent = "";
        var margin = 0;
        // Configure barcode writer
        var barcodeWriter = new ZXing.BarcodeWriter<Image<Rgba32>>
        {
            Format = ZXing.BarcodeFormat.CODE_128,
            Options = new ZXing.Common.EncodingOptions
            {
                Height = height,
                Width = width,
                PureBarcode = false,
                Margin = margin
            },
            Renderer = new ImageSharpRenderer<Rgba32>() // Use ImageSharp for rendering
        };

        // Generate barcode as a byte array
        byte[] imageBytes;
        using (var barcodeImage = barcodeWriter.Write(QRCodeContent))
        {
            using (var stream = new MemoryStream())
            {
                barcodeImage.SaveAsPng(stream); // Save as PNG
                imageBytes = stream.ToArray(); // Store bytes
            }
        }
        //return imageBytes;
        //File.WriteAllBytes("c:\\Developer\\test.png", imageBytes);

        // Use the byte array to create a FRESH stream via Func<Stream>
        XImage xImage = XImage.FromStream(() => new MemoryStream(imageBytes));


        return xImage;

    }
    public XImage GenerateQRcodeBarcodeImage(string QRCodeContent, int width)
    {
        if (QRCodeContent == null)
            QRCodeContent = "";
        var margin = 0;
        // Configure barcode writer
        var barcodeWriter = new ZXing.BarcodeWriter<Image<Rgba32>>
        {
            Format = ZXing.BarcodeFormat.QR_CODE,
            Options = new ZXing.Common.EncodingOptions
            {
                Height = width,
                Width = width,
                PureBarcode = false,
                Margin = margin
            },
            Renderer = new ImageSharpRenderer<Rgba32>() // Use ImageSharp for rendering
        };

        // Generate barcode as a byte array
        byte[] imageBytes;
        using (var barcodeImage = barcodeWriter.Write(QRCodeContent))
        {
            using (var stream = new MemoryStream())
            {
                barcodeImage.SaveAsPng(stream); // Save as PNG
                imageBytes = stream.ToArray(); // Store bytes
            }
        }
        //return imageBytes;
        //File.WriteAllBytes("c:\\Developer\\test.png", imageBytes);

        // Use the byte array to create a FRESH stream via Func<Stream>
        XImage xImage = XImage.FromStream(() => new MemoryStream(imageBytes));


        return xImage;

    }
    //private System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(String mimeType)
    //{
    //	int j;
    //	System.Drawing.Imaging.ImageCodecInfo[] encoders;
    //	encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
    //	for (j = 0; j < encoders.Length; ++j)
    //	{
    //		if (encoders[j].MimeType == mimeType)
    //			return encoders[j];
    //	}
    //	return null;

    //}
    //private System.Drawing.Image GenerateMatrix(string inputStr)
    //{
    //	System.Drawing.Image outputImage = null;
    //	string input = "";
    //	input = inputStr;

    //	DmtxImageEncoder encoder = new DmtxImageEncoder();
    //	DmtxImageEncoderOptions option = new DmtxImageEncoderOptions();

    //	//margin distance
    //	option.MarginSize = 0;
    //	//QR code dot matrix size
    //	option.ModuleSize = 4;
    //	//option.SizeIdx = DmtxSymbolSize.DmtxSymbol16x48;

    //	//options.Scheme = DmtxScheme.DmtxSchemeC40;

    //	//this because my input string contain numeric and upper case characters.
    //	//- C40 is used to encode data that contains only numeric and upper case characters.
    //	//- TEXT is used to encode data that mainly contains numeric and lowercase characters.

    //	outputImage = encoder.EncodeImage(input, option);

    //	System.Drawing.Imaging.ImageCodecInfo myImageCodecInfo;
    //	System.Drawing.Imaging.Encoder myEncoder;
    //	System.Drawing.Imaging.EncoderParameter myEncoderParameter;
    //	System.Drawing.Imaging.EncoderParameters myEncoderParameters;

    //	myImageCodecInfo = GetEncoderInfo("image/jpeg");
    //	myEncoder = System.Drawing.Imaging.Encoder.Quality;
    //	myEncoderParameters = new System.Drawing.Imaging.EncoderParameters(1);

    //	myEncoderParameter = new System.Drawing.Imaging.EncoderParameter(myEncoder, 75L);
    //	myEncoderParameters.Param[0] = myEncoderParameter;
    //	//outputImage.Save("TestImage.jpg", myImageCodecInfo, myEncoderParameters);


    //	return outputImage;

    //}

    public void Print()
    {

        GlobalFontSettings.FontResolver = new FontResolver();

        var document = new PdfDocument();
        var page = document.AddPage();

        var gfx = XGraphics.FromPdfPage(page);
        var font = new XFont("Arial", 20, XFontStyle.Bold);

        var textColor = XBrushes.Black;
        var layout = new XRect(20, 20, page.Width, page.Height);
        var format = XStringFormats.Center;

        gfx.DrawString("Hello World!", font, textColor, layout, format);

        document.Save("helloworld.pdf");
    }
    //MigraDocCore, built on top of PdfSharpCore to create PDF documents.
    public PdfDocument ToPDF(List<Country> input)
    {
        Document document = new();


        Section section = document.AddSection();

        foreach (var item in input)
        {
            section.AddParagraph($"Name: {item.Name}");
            section.AddParagraph($"Capital: {item.Capital}");
            section.AddParagraph($"Population: {item.Population}");
            section.AddParagraph($"Area KM Squared: {item.Area_KM_Squared}");
            section.AddParagraph();
        }

        PdfDocumentRenderer pdfRenderer = new() { Document = document };

        pdfRenderer.RenderDocument();

        return pdfRenderer.PdfDocument;
    }

    void DefineStyles(Document document)
    {
        // Get the predefined style Normal.
        Style style = document.Styles["Normal"];
        // Because all styles are derived from Normal, the next line changes the 
        // font of the whole document. Or, more exactly, it changes the font of
        // all styles and paragraphs that do not redefine the font.
        style.Font.Name = "Verdana";

        // Heading1 to Heading9 are predefined styles with an outline level. An outline level
        // other than OutlineLevel.BodyText automatically creates the outline (or bookmarks) 
        // in PDF.

        style = document.Styles["Heading1"];
        style.Font.Name = "Tahoma";
        style.Font.Size = 14;
        style.Font.Bold = true;
        style.Font.Color = Colors.Black;// DarkBlue;
        style.ParagraphFormat.PageBreakBefore = true;           //So there is no need to add a page break


        style.ParagraphFormat.SpaceAfter = 6;

        style = document.Styles["Heading2"];
        style.Font.Size = 12;
        style.Font.Bold = true;
        style.ParagraphFormat.PageBreakBefore = false;
        style.ParagraphFormat.SpaceBefore = 6;
        style.ParagraphFormat.SpaceAfter = 6;

        style = document.Styles["Heading3"];
        style.Font.Size = 10;
        style.Font.Bold = true;
        style.Font.Italic = true;
        style.ParagraphFormat.SpaceBefore = 6;
        style.ParagraphFormat.SpaceAfter = 3;

        style = document.Styles[StyleNames.Header];
        style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

        style = document.Styles[StyleNames.Footer];
        style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

        // Create a new style called Table based on style Normal
        style = document.Styles.AddStyle("Table", "Normal");
        style.Font.Name = "Verdana";
        style.Font.Size = 10;

        // Create a new style called Reference based on style Normal
        style = document.Styles.AddStyle("Reference", "Normal");
        style.ParagraphFormat.SpaceBefore = "5mm";
        style.ParagraphFormat.SpaceAfter = "5mm";
        style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);

        // Create a new style called TextBox based on style Normal
        style = document.Styles.AddStyle("TextBox", "Normal");
        style.ParagraphFormat.Alignment = ParagraphAlignment.Justify;
        style.ParagraphFormat.Borders.Width = 2.5;
        style.ParagraphFormat.Borders.Distance = "3pt";
        style.ParagraphFormat.Shading.Color = Colors.SkyBlue;

        // Create a new style called TOC based on style Normal
        style = document.Styles.AddStyle("TOC", "Normal");
        style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right, TabLeader.Dots);
        style.ParagraphFormat.Font.Color = Colors.Blue;
    }
    public void DefineTableOfContents(Document document)
    {
        Section section = document.LastSection;

        section.AddPageBreak();                                     //********************
        Paragraph paragraph = section.AddParagraph("Table of Contents");
        paragraph.Format.Font.Size = 14;
        paragraph.Format.Font.Bold = true;
        paragraph.Format.SpaceAfter = 24;
        paragraph.Format.OutlineLevel = OutlineLevel.Level1;

        paragraph = section.AddParagraph();
        paragraph.Style = "TOC";
        Hyperlink hyperlink = paragraph.AddHyperlink("Paragraphs"); //********************
        hyperlink.AddText("Paragraphs\t");
        hyperlink.AddPageRefField("Paragraphs");                    //********************

        paragraph = section.AddParagraph();
        paragraph.Style = "TOC";
        hyperlink = paragraph.AddHyperlink("Tables");
        hyperlink.AddText("Tables\t");
        hyperlink.AddPageRefField("Tables");

        paragraph = section.AddParagraph();
        paragraph.Style = "TOC";
        hyperlink = paragraph.AddHyperlink("Charts");
        hyperlink.AddText("Charts\t");
        hyperlink.AddPageRefField("Charts");
    }
    public void DefineCover(Document document)
    {
        Section section = document.AddSection();
        var config = section.PageSetup;
        config.Orientation = Orientation.Portrait;
        config.PageFormat = PageFormat.Letter;                  //**********Page Setup
        config.PageWidth = Unit.FromPoint(612);     //8.5*72
        config.PageHeight = Unit.FromPoint(792);    //11*72

        float sectionWidth = section.PageSetup.PageWidth - document.DefaultPageSetup.LeftMargin - document.DefaultPageSetup.RightMargin;

        Paragraph paragraph = section.AddParagraph();
        paragraph.Format.SpaceAfter = "3cm";


        string filename = Path.Combine(Env.WebRootPath, "images", "HearnPrint.png");

        var image = section.AddImage(ImageSource.FromFile(filename));
        image.LockAspectRatio = true;
        image.Width = "4cm";


        paragraph = section.AddParagraph("Barcode");
        Stream stream = GenerateBarcodeStream("ASDSADSADSAD", 400, 100);
        image = section.AddImage(ImageSource.FromStream("ASDSADSADSAD.png", () => { return stream; }));

        paragraph = section.AddParagraph("abc A sample document that demonstrates the\ncapabilities of MigraDoc");
        paragraph.Format.Font.Name = "Webdings";
        paragraph.Format.Font.Size = 16;
        paragraph.Format.Font.Color = Colors.DarkRed;
        paragraph.Format.SpaceBefore = "8cm";
        paragraph.Format.SpaceAfter = "3cm";

        paragraph = section.AddParagraph("date: ");
        paragraph.AddDateField();                                   //******************
    }

    /// <summary>
    /// Defines page setup, headers, and footers.
    /// </summary>
    static void DefineContentSection(Document document)
    {
        Section section = document.AddSection();
        var config = section.PageSetup;
        config.Orientation = Orientation.Portrait;
        config.PageFormat = PageFormat.Letter;                  //**********Page Setup

        config.TopMargin = "3cm";
        config.LeftMargin = 15;
        config.RightMargin = 15;
        config.BottomMargin = "3cm";


        config.OddAndEvenPagesHeaderFooter = true;
        config.StartingNumber = 1;

        HeaderFooter header = section.Headers.Primary;
        header.AddParagraph("\tOdd Page Header");

        header = section.Headers.EvenPage;
        header.AddParagraph("Even Page Header");

        // Create a paragraph with centered page number. See definition of style "Footer".
        Paragraph paragraph = new Paragraph();
        //paragraph.AddTab();
        paragraph.Format.Alignment = ParagraphAlignment.Center;
        paragraph.AddText(" Page ");
        paragraph.AddPageField();                           //********************
        paragraph.AddText(" of ");
        paragraph.AddNumPagesField();

        // Add paragraph to footer for odd pages.
        section.Footers.Primary.Add(paragraph);

        // Add clone of paragraph to footer for odd pages. Cloning is necessary because an object must
        // not belong to more than one other object. If you forget cloning an exception is thrown.
        section.Footers.EvenPage.Add(paragraph.Clone());
    }

    public void DefineParagraphs(Document document)
    {
        Paragraph paragraph = document.LastSection.AddParagraph("Paragraph Layout Overview", "Heading1");
        paragraph.AddBookmark("Paragraphs");            //********************

        var section = document.LastSection;
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageWidth = Unit.Empty;
        section.PageSetup.PageHeight = Unit.Empty;
        section.PageSetup.PageFormat = PageFormat.Letter;
        section.PageSetup.TopMargin = "3cm";

        DemonstrateAlignment(document);
        DemonstrateIndent(document);
        DemonstrateFormattedText(document);
        DemonstrateBordersAndShading(document);
    }

    void DemonstrateAlignment(Document document)        //ParagraphAlignment
    {
        document.LastSection.AddParagraph("Alignment", "Heading2");

        document.LastSection.AddParagraph("Left Aligned", "Heading3");

        Paragraph paragraph = document.LastSection.AddParagraph();
        paragraph.Format.Alignment = ParagraphAlignment.Left;
        paragraph.AddText("FillerText.Text");

        document.LastSection.AddParagraph("Right Aligned", "Heading3");

        paragraph = document.LastSection.AddParagraph();
        paragraph.Format.Alignment = ParagraphAlignment.Right;
        paragraph.AddText("FillerText.Text");

        document.LastSection.AddParagraph("Centered", "Heading3");

        paragraph = document.LastSection.AddParagraph();
        paragraph.Format.Alignment = ParagraphAlignment.Center;
        paragraph.AddText("FillerText.Text");

        document.LastSection.AddParagraph("Justified", "Heading3");

        paragraph = document.LastSection.AddParagraph();
        paragraph.Format.Alignment = ParagraphAlignment.Justify;
        paragraph.AddText("FillerText.MediumText");
    }

    static void DemonstrateIndent(Document document)
    {
        document.LastSection.AddParagraph("Indent", "Heading2");

        document.LastSection.AddParagraph("Left Indent", "Heading3");

        Paragraph paragraph = document.LastSection.AddParagraph();
        paragraph.Format.LeftIndent = "2cm";
        paragraph.AddText("FillerText.Text");

        document.LastSection.AddParagraph("Right Indent", "Heading3");

        paragraph = document.LastSection.AddParagraph();
        paragraph.Format.RightIndent = "1in";
        paragraph.AddText("FillerText.Text");

        document.LastSection.AddParagraph("First Line Indent", "Heading3");

        paragraph = document.LastSection.AddParagraph();
        paragraph.Format.FirstLineIndent = "12mm";
        paragraph.AddText("FillerText.Text");

        document.LastSection.AddParagraph("First Line Negative Indent", "Heading3");

        paragraph = document.LastSection.AddParagraph();
        paragraph.Format.LeftIndent = "1.5cm";
        paragraph.Format.FirstLineIndent = "-1.5cm";
        paragraph.AddText("FillerText.Text");
    }

    void DemonstrateFormattedText(Document document)        //TextFormat
    {
        document.LastSection.AddParagraph("Formatted Text", "Heading2");

        //document.LastSection.AddParagraph("Left Aligned", "Heading3");

        Paragraph paragraph = document.LastSection.AddParagraph();
        paragraph.AddText("Text can be formatted ");
        paragraph.AddFormattedText("bold", TextFormat.Bold);
        paragraph.AddText(", ");
        paragraph.AddFormattedText("italic", TextFormat.Italic);
        paragraph.AddText(", or ");
        var m = paragraph.AddFormattedText("bold & italic", TextFormat.Bold | TextFormat.Italic);
        // m.FontName = "mmmmm";
        paragraph.AddText(".");
        paragraph.AddLineBreak();
        paragraph.AddText("You can set the ");
        FormattedText formattedText = paragraph.AddFormattedText("size ");
        formattedText.Size = 15;
        paragraph.AddText("the ");
        formattedText = paragraph.AddFormattedText("color ");
        formattedText.Color = Colors.Firebrick;
        paragraph.AddText("the ");

        formattedText = paragraph.AddFormattedText("font", new Font("Verdana"));
        paragraph.AddText(".");
        paragraph.AddLineBreak();
        paragraph.AddText("You can set the ");
        formattedText = paragraph.AddFormattedText("subscript");
        formattedText.Subscript = true;
        paragraph.AddText(" or ");
        formattedText = paragraph.AddFormattedText("superscript");
        formattedText.Superscript = true;
        paragraph.AddText(".");
    }

    void DemonstrateBordersAndShading(Document document)
    {
        document.LastSection.AddPageBreak();
        document.LastSection.AddParagraph("Borders and Shading", "Heading2");

        document.LastSection.AddParagraph("Border around Paragraph", "Heading3");

        Paragraph paragraph = document.LastSection.AddParagraph();
        paragraph.Format.Borders.Width = 2.5;
        paragraph.Format.Borders.Color = Colors.Navy;
        paragraph.Format.Borders.Distance = 3;
        paragraph.AddText("FillerText.MediumText");

        document.LastSection.AddParagraph("Shading", "Heading3");

        paragraph = document.LastSection.AddParagraph();
        paragraph.Format.Shading.Color = Colors.LightCoral;             //change background
        paragraph.AddText("FillerText.Text");

        document.LastSection.AddParagraph("Borders & Shading", "Heading3");

        paragraph = document.LastSection.AddParagraph();
        paragraph.Style = "TextBox";                                    //use the defined style
        paragraph.AddText("FillerText.MediumText");
    }

    public void DefineTables(Document document)
    {
        Paragraph paragraph = document.LastSection.AddParagraph("Table Overview", "Heading1");
        paragraph.AddBookmark("Tables");

        var section = document.LastSection;
        //section.PageSetup = document.DefaultPageSetup.Clone();
        //section.PageSetup.PageWidth = Unit.Empty;
        //section.PageSetup.PageHeight = Unit.Empty;
        //section.PageSetup.PageFormat = PageFormat.Letter;
        //section.PageSetup.TopMargin = "3cm";

        DemonstrateSimpleTable(document);
        DemonstrateTableAlignment(document);
        DemonstrateCellMerge(document);
    }

    public void DemonstrateSimpleTable(Document document)
    {
        document.LastSection.AddParagraph("Simple Tables", "Heading2");

        Table table = new Table();
        table.Style = "table";      //use defined table Style
        table.Borders.Color = Colors.Black;
        table.Borders.Visible = true;
        table.Borders.Style = BorderStyle.DashDot;
        table.Rows.LeftIndent = 0;              //adjust position

        table.Borders.Width = 0.75;

        Column column = table.AddColumn(Unit.FromCentimeter(2));
        column.Format.Alignment = ParagraphAlignment.Center;

        table.AddColumn(Unit.FromCentimeter(5));

        Row row = table.AddRow();
        //row.Borders.Visible = false;
        row.Shading.Color = Colors.PaleGoldenrod;
        Cell cell = row.Cells[0];
        cell.AddParagraph("Itemus");
        cell = row.Cells[1];
        cell.AddParagraph("Descriptum");

        row = table.AddRow();
        cell = row.Cells[0];
        cell.AddParagraph("1");
        cell = row.Cells[1];
        cell.AddParagraph("FillerText.ShortText");

        row = table.AddRow();
        cell = row.Cells[0];
        cell.AddParagraph("2");
        cell = row.Cells[1];
        cell.AddParagraph("FillerText.Text");

        table.SetEdge(0, 0, 2, 3, Edge.Box, BorderStyle.Single, 1.5, Colors.Black);

        document.LastSection.Add(table);
    }

    public void DemonstrateTableAlignment(Document document)
    {
        document.LastSection.AddParagraph("Cell Alignment", "Heading2");

        Table table = document.LastSection.AddTable();
        table.Borders.Visible = true;
        table.Format.Shading.Color = Colors.LavenderBlush;      //para text background
        table.Shading.Color = Colors.Salmon;                    //table background
        table.TopPadding = 5;
        table.BottomPadding = 5;

        Column column = table.AddColumn();
        column.Format.Alignment = ParagraphAlignment.Left;

        column = table.AddColumn();
        column.Format.Alignment = ParagraphAlignment.Center;

        column = table.AddColumn();
        column.Format.Alignment = ParagraphAlignment.Right;

        table.Rows.Height = 35;                                 //define row height

        Row row = table.AddRow();
        row.VerticalAlignment = VerticalAlignment.Top;
        row.Cells[0].AddParagraph("Text");
        row.Cells[1].AddParagraph("Text");
        row.Cells[2].AddParagraph("Text");

        row = table.AddRow();
        row.VerticalAlignment = VerticalAlignment.Center;
        row.Cells[0].AddParagraph("Text");
        row.Cells[1].AddParagraph("Text");
        row.Cells[2].AddParagraph("Text");

        row = table.AddRow();
        row.VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[0].AddParagraph("Text");
        row.Cells[1].AddParagraph("Text");
        row.Cells[2].AddParagraph("Text");
    }

    public void DemonstrateCellMerge(Document document)
    {
        document.LastSection.AddParagraph("Cell Merge", "Heading2");

        Table table = document.LastSection.AddTable();
        table.Borders.Visible = true;
        table.TopPadding = 5;
        table.BottomPadding = 5;

        Column column = table.AddColumn();
        column.Format.Alignment = ParagraphAlignment.Left;

        column = table.AddColumn();
        column.Format.Alignment = ParagraphAlignment.Center;

        column = table.AddColumn();
        column.Format.Alignment = ParagraphAlignment.Right;

        table.Rows.Height = 35;

        Row row = table.AddRow();
        row.Cells[0].AddParagraph("Merge Right");
        row.Cells[0].MergeRight = 1;

        row = table.AddRow();
        row.VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[0].MergeDown = 1;
        row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[0].AddParagraph("Merge Down");                        //add 2 rows

        table.AddRow();
    }

    public void DefineCharts(Document document)
    {
        Paragraph paragraph = document.LastSection.AddParagraph("Chart Overview", "Heading1");

        paragraph.AddBookmark("Charts");

        var section = document.LastSection;
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageWidth = Unit.Empty;
        section.PageSetup.PageHeight = Unit.Empty;
        section.PageSetup.PageFormat = PageFormat.Letter;
        section.PageSetup.TopMargin = "3cm";
        document.LastSection.AddParagraph("Sample Chart", "Heading2");

        Chart chart = new Chart();
        chart.Left = 0;

        chart.Width = Unit.FromCentimeter(16);
        chart.Height = Unit.FromCentimeter(12);
        Series series = chart.SeriesCollection.AddSeries();

        series.ChartType = ChartType.Column2D;
        series.Add(new double[] { 1, 17, 45, 5, 3, 20, 11, 23, 8, 19 });
        series.HasDataLabel = true;

        series = chart.SeriesCollection.AddSeries();
        series.ChartType = ChartType.Line;
        series.Add(new double[] { 41, 7, 5, 45, 13, 10, 21, 13, 18, 9 });

        XSeries xseries = chart.XValues.AddXSeries();
        xseries.Add("A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N");

        chart.XAxis.MajorTickMark = TickMarkType.Outside;
        chart.XAxis.Title.Caption = "X-Axis";

        chart.YAxis.MajorTickMark = TickMarkType.Outside;
        chart.YAxis.HasMajorGridlines = true;

        chart.PlotArea.LineFormat.Color = Colors.DarkGray;
        chart.PlotArea.LineFormat.Width = 1;

        document.LastSection.Add(chart);
    }
    private (TextFrame frame, Table table) CreatePage(Document document)
    {
        // Each MigraDoc document needs at least one section.
        Section section = document.AddSection();

        if (ImageSource.ImageSourceImpl == null)
        {
            ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();
        }

        //Put a logo in the header
        string filename = Path.Combine(Env.WebRootPath, "Images", "HernPrint.png");
        var image = section.Headers.Primary.AddImage(ImageSource.FromFile(filename));
        image.Height = "2.5cm";
        image.LockAspectRatio = true;
        image.RelativeVertical = RelativeVertical.Line;
        image.RelativeHorizontal = RelativeHorizontal.Margin;
        image.Top = ShapePosition.Top;
        image.Left = ShapePosition.Right;
        image.WrapFormat.Style = WrapStyle.Through;

        image.LineFormat = new LineFormat() { Color = Colors.DarkGray };

        // Create footer
        Paragraph paragraph = section.Footers.Primary.AddParagraph();
        paragraph.AddText("PowerBooks Inc · Sample Street 42 · 56789 Cologne · Germany");
        paragraph.Format.Font.Size = 9;
        paragraph.Format.Alignment = ParagraphAlignment.Center;

        // Create the text frame for the address
        var addressFrame = section.AddTextFrame();
        addressFrame.Height = "3.0cm";
        addressFrame.Width = "7.0cm";
        addressFrame.Left = ShapePosition.Left;
        addressFrame.RelativeHorizontal = RelativeHorizontal.Margin;
        addressFrame.Top = "5.0cm";
        addressFrame.RelativeVertical = RelativeVertical.Page;

        // Put sender in address frame
        paragraph = addressFrame.AddParagraph("PowerBooks Inc · Sample Street 42 · 56789 Cologne");
        paragraph.Format.Font.Name = "Times New Roman";
        paragraph.Format.Font.Size = 7;
        paragraph.Format.SpaceAfter = 3;

        // Add the print date field
        paragraph = section.AddParagraph();
        paragraph.Format.SpaceBefore = "8cm";
        paragraph.Style = "Reference";
        paragraph.AddFormattedText("BOL", TextFormat.Bold);
        paragraph.AddTab();
        paragraph.AddText("Cologne, ");
        paragraph.AddDateField("dd-MM-yyyy HH:mm:ss");

        // Create the item table
        var table = section.AddTable();
        table.Style = "Table";
        table.Borders.Color = Colors.Black;// TableBorder;
        table.Borders.Width = 0.25;
        table.Borders.Left.Width = 0.5;
        table.Borders.Right.Width = 0.5;
        table.Rows.LeftIndent = 0;

        // Before you can add a row, you must define the columns
        Column column = table.AddColumn("1cm");
        column.Format.Alignment = ParagraphAlignment.Center;

        column = table.AddColumn("2.5cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("3cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("3.5cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        column = table.AddColumn("2cm");
        column.Format.Alignment = ParagraphAlignment.Center;

        column = table.AddColumn("4cm");
        column.Format.Alignment = ParagraphAlignment.Right;

        // Create the header of the table
        Row row = table.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Center;
        row.Format.Font.Bold = true;
        row.Shading.Color = Colors.Gray;// TableBlue;
        row.Cells[0].AddParagraph("Item");
        row.Cells[0].Format.Font.Bold = false;
        row.Cells[0].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[0].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[0].MergeDown = 1;
        row.Cells[1].AddParagraph("Title and Author");
        row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[1].MergeRight = 3;
        row.Cells[5].AddParagraph("Extended Price");
        row.Cells[5].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
        row.Cells[5].MergeDown = 1;

        row = table.AddRow();
        row.HeadingFormat = true;
        row.Format.Alignment = ParagraphAlignment.Center;
        row.Format.Font.Bold = true;
        row.Shading.Color = Colors.Gray;// TableBlue;
        row.Cells[1].AddParagraph("Quantity");
        row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[2].AddParagraph("Unit Price");
        row.Cells[2].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[3].AddParagraph("Discount (%)");
        row.Cells[3].Format.Alignment = ParagraphAlignment.Left;
        row.Cells[4].AddParagraph("Taxable");
        row.Cells[4].Format.Alignment = ParagraphAlignment.Left;


        table.SetEdge(0, 0, 6, 2, Edge.Box, BorderStyle.Single, 0.75, MigraDocCore.DocumentObjectModel.Color.Empty);
        return (addressFrame, table);
    }

    void FillContent(Document document, TextFrame addressFrame, Table table)
    {
        // Fill address in address text frame
        //XPathNavigator item = SelectItem("/invoice/to");
        //Paragraph paragraph = addressFrame.AddParagraph();
        //paragraph.AddText(GetValue(item, "name/singleName"));
        //paragraph.AddLineBreak();
        //paragraph.AddText(GetValue(item, "address/line1"));
        //paragraph.AddLineBreak();
        //paragraph.AddText(GetValue(item, "address/postalCode") + " " + GetValue(item, "address/city"));

        //// Iterate the invoice items
        //double totalExtendedPrice = 0;
        //XPathNodeIterator iter = navigator.Select("/invoice/items/*");
        //while (iter.MoveNext())
        //{
        //    item = iter.Current;
        //    double quantity = GetValueAsDouble(item, "quantity");
        //    double price = GetValueAsDouble(item, "price");
        //    double discount = GetValueAsDouble(item, "discount");

        //    // Each item fills two rows
        //    Row row1 = table.AddRow();
        //    Row row2 = table.AddRow();
        //    row1.TopPadding = 1.5;
        //    row1.Cells[0].Shading.Color = TableGray;
        //    row1.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        //    row1.Cells[0].MergeDown = 1;
        //    row1.Cells[1].Format.Alignment = ParagraphAlignment.Left;
        //    row1.Cells[1].MergeRight = 3;
        //    row1.Cells[5].Shading.Color = TableGray;
        //    row1.Cells[5].MergeDown = 1;

        //    row1.Cells[0].AddParagraph(GetValue(item, "itemNumber"));
        //    paragraph = row1.Cells[1].AddParagraph();
        //    paragraph.AddFormattedText(GetValue(item, "title"), TextFormat.Bold);
        //    paragraph.AddFormattedText(" by ", TextFormat.Italic);
        //    paragraph.AddText(GetValue(item, "author"));
        //    row2.Cells[1].AddParagraph(GetValue(item, "quantity"));
        //    row2.Cells[2].AddParagraph(price.ToString("0.00") + " €");
        //    row2.Cells[3].AddParagraph(discount.ToString("0.0"));
        //    row2.Cells[4].AddParagraph();
        //    row2.Cells[5].AddParagraph(price.ToString("0.00"));
        //    double extendedPrice = quantity * price;
        //    extendedPrice = extendedPrice * (100 - discount) / 100;
        //    row1.Cells[5].AddParagraph(extendedPrice.ToString("0.00") + " €");
        //    row1.Cells[5].VerticalAlignment = VerticalAlignment.Bottom;
        //    totalExtendedPrice += extendedPrice;

        //    table.SetEdge(0, table.Rows.Count - 2, 6, 2, Edge.Box, BorderStyle.Single, 0.75);
        //}

        // Add an invisible row as a space line to the table
        Row row = table.AddRow();
        row.Borders.Visible = false;

        // Add the total price row
        row = table.AddRow();
        row.Cells[0].Borders.Visible = false;
        row.Cells[0].AddParagraph("Total Price");
        row.Cells[0].Format.Font.Bold = true;
        row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        row.Cells[0].MergeRight = 4;
        row.Cells[5].AddParagraph("1.2 €");

        // Add the VAT row
        row = table.AddRow();
        row.Cells[0].Borders.Visible = false;
        row.Cells[0].AddParagraph("VAT (19%)");
        row.Cells[0].Format.Font.Bold = true;
        row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        row.Cells[0].MergeRight = 4;
        row.Cells[5].AddParagraph("0.19 €");

        // Add the additional fee row
        row = table.AddRow();
        row.Cells[0].Borders.Visible = false;
        row.Cells[0].AddParagraph("Shipping and Handling");
        row.Cells[5].AddParagraph(0.ToString("0.00") + " €");
        row.Cells[0].Format.Font.Bold = true;
        row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        row.Cells[0].MergeRight = 4;

        // Add the total due row
        row = table.AddRow();
        row.Cells[0].AddParagraph("Total Due");
        row.Cells[0].Borders.Visible = false;
        row.Cells[0].Format.Font.Bold = true;
        row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        row.Cells[0].MergeRight = 4;
        var totalExtendedPrice = 0.19 * 12;
        row.Cells[5].AddParagraph(totalExtendedPrice.ToString("0.00") + " €");

        // Set the borders of the specified cell range
        table.SetEdge(5, table.Rows.Count - 4, 1, 4, Edge.Box, BorderStyle.Single, 0.75);

        // Add the notes paragraph
        var paragraph = document.LastSection.AddParagraph();
        paragraph.Format.SpaceBefore = "1cm";
        paragraph.Format.Borders.Width = 0.75;
        paragraph.Format.Borders.Distance = 3;
        paragraph.Format.Borders.Color = Colors.Black;// TableBorder;
        paragraph.Format.Shading.Color = Colors.Gray;// TableGray;
        //item = SelectItem("/invoice");
        //paragraph.AddText(GetValue(item, "notes"));
    }
    public Table BuildSpacerTable(Document document, float width, Unit height)
    {
        Table _table = new Table();
        _table.AddColumn(width);
        try
        {
            Row _row = _table.AddRow();
            _row.Height = height;
            _row.HeightRule = RowHeightRule.Exactly;
            Cell _cell = _row.Cells[0];
            _cell.Borders.Width = 0;
            document.LastSection.Add(_table);
        }
        catch
        {
        }
        return _table;
    }
    public async Task<(Document document, BolInfo bolInf)> CreateDocument(int bolID)
    {
        BolInfo bolInfo = new();// await sqlService.GetBolInfo(bolID);
        Customer shipper = new();// await sqlService.GetCustomer(bolInfo.ShipperID);
        Customer carrier = new();// await sqlService.GetCustomer(bolInfo.CarrierId);
        Customer receiver = new();// await sqlService.GetCustomer(bolInfo.ConsigneeId);
        List<InventoryPartsWithLocation> partDetail = [];// await sqlService.GetInventoryOperationDetailWithPartInfo(bolInfo.ShippingId);

        // Create a new MigraDoc document
        var document = new Document();
        document.Info.Title = "Bill of Landing";
        document.Info.Subject = "BOL";
        document.Info.Author = "Hearn Industrial Services";
        document.Info.Keywords = "BOL";

        //No effect!!!
        //document.DefaultPageSetup.PageFormat = PageFormat.Letter;
        //document.DefaultPageSetup.Orientation = Orientation.Portrait;
        //document.DefaultPageSetup.LeftMargin = "2cm";
        //document.DefaultPageSetup.RightMargin = "2cm";
        //document.DefaultPageSetup.TopMargin = "2cm";
        //document.DefaultPageSetup.BottomMargin = "2cm";
        //document.DefaultPageSetup.HeaderDistance = "0.5cm";
        //document.DefaultPageSetup.FooterDistance = "0.5cm";

        DefineStyles(document);

        Section section = document.AddSection();
        var config = section.PageSetup;
        config.Orientation = Orientation.Portrait;
        config.PageFormat = PageFormat.Letter;                  //**********Page Setup

        //Internally, the default measurement is point
        float PageWidth = Unit.FromPoint(612);     //8.5*72
        float PgeHeight = Unit.FromPoint(792);     //11*72

        float sectionWidth = PageWidth - document.DefaultPageSetup.LeftMargin - document.DefaultPageSetup.RightMargin;
        float colWidth = sectionWidth / 6;      //6 columns for table

        config.OddAndEvenPagesHeaderFooter = true;
        config.TopMargin = "1.8cm";
        config.StartingNumber = 1;

        //HeaderFooter header = section.Headers.Primary;
        //header.AddParagraph("\tOdd Page Header");

        //header = section.Headers.EvenPage;
        //header.AddParagraph("Even Page Header");

        // Create a paragraph with centered page number. See definition of style "Footer".
        Paragraph paragraph = new Paragraph();
        //paragraph.AddTab();
        paragraph.Format.Font.Size = 8;
        paragraph.Format.Alignment = ParagraphAlignment.Center;
        paragraph.AddText(" Page ");
        paragraph.AddPageField();                           //********************
        paragraph.AddText(" / ");
        paragraph.AddNumPagesField();

        // Add paragraph to footer for odd pages.
        section.Footers.Primary.Add(paragraph);

        // Add clone of paragraph to footer for odd pages. Cloning is necessary because an object must
        // not belong to more than one other object. If you forget cloning an exception is thrown.
        section.Footers.EvenPage.Add(paragraph.Clone());

        // Put a logo in the header
        string filename = Path.Combine(Env.WebRootPath, "images", "HearnPrint.png");

        //var image = section.AddImage(ImageSource.FromFile(filename));
        //image.Height = "2.0cm";
        //image.LockAspectRatio = true;
        //image.RelativeVertical = RelativeVertical.Line;
        ////image.RelativeHorizontal = RelativeHorizontal.Margin;
        //image.Top = ShapePosition.Top;
        //image.Left = ShapePosition.Left;
        //image.WrapFormat.Style = WrapStyle.Through;
        //image.RelativeHorizontal = RelativeHorizontal.Column;

        //// Create the text frame for the address
        //var txtFrame = section.AddTextFrame();
        //txtFrame.Height = "2.0cm";
        //txtFrame.Width = "7.0cm";
        //txtFrame.Left = ShapePosition.Right;
        //txtFrame.RelativeHorizontal = RelativeHorizontal.Margin;
        //txtFrame.Top = "0.0cm";
        //txtFrame.RelativeVertical = RelativeVertical.Page;

        //// Put sender in address frame
        //paragraph = txtFrame.AddParagraph("BILL OF LANDING");
        //paragraph.Format.SpaceBefore = "0.1cm";
        //paragraph.Format.Font.Name = "Arial Black";
        //paragraph.Format.Font.Size = 18;
        //paragraph.Format.SpaceBefore = "3.1cm";
        //paragraph.Format.Font.Color = Colors.Green;

        //Create Table
        Table table = new Table();
        table.Style = "Table";      //use defined table Style
        table.Borders.Visible = false;
        table.Rows.LeftIndent = 0;              //adjust position

        Column column = table.AddColumn("4.1cm");

        column = table.AddColumn(sectionWidth - Unit.FromCentimeter(4.1) - colWidth);
        column.Format.Alignment = ParagraphAlignment.Center;
        column.Format.SpaceBefore = 12;

        column = table.AddColumn(colWidth);
        column.Format.Alignment = ParagraphAlignment.Right;
        column.Format.SpaceBefore = 16;

        Row row = table.AddRow();

        //row.Borders.Visible = false;
        // row.Shading.Color = Colors.PaleGoldenrod;
        Cell cell = row.Cells[0];
        //string filename = Path.Combine(Env.WebRootPath, "images", "HearnPrint.png");

        var image = cell.AddImage(ImageSource.FromFile(filename));
        image.LockAspectRatio = true;
        image.Width = "4cm";
        image.RelativeVertical = RelativeVertical.Line;

        cell = row.Cells[1];
        cell.Format.Font.Name = "Arial";
        cell.Format.Font.Size = 24;
        cell.Format.Font.Bold = true;
        // cell.Format.Font.Color = Colors.MediumBlue;
        var paraghraph = cell.AddParagraph("BILL OF LANDING");


        cell = row.Cells[2];
        cell.AddParagraph(bolInfo.CreateDate.ToString("yyyy-MM-dd hh:mm:ss tt"));


        section.Add(table);

        //Add Space after table
        BuildSpacerTable(document, PageWidth, 6);

        //Add detail table
        table = new Table();
        table.Style = "Table";      //use defined table Style
        //table.Borders.Width = 0.75;
        table.Rows.LeftIndent = 0;              //adjust position

        table.LeftPadding = -1;
        table.TopPadding = -1;

        column = table.AddColumn(colWidth);
        column = table.AddColumn(colWidth - 10);
        column = table.AddColumn(colWidth - 6);
        column = table.AddColumn(colWidth - 4);
        column = table.AddColumn(colWidth + 10);
        column.Format.Alignment = ParagraphAlignment.Center;
        column = table.AddColumn(colWidth + 10);
        column.Format.Alignment = ParagraphAlignment.Center;

        int leftshift = -10;
        int AddRows = 0;
        //cell.Borders.Bottom.Width = 0.75;

        var lefttable = new Table();
        lefttable.Style = "Table";      //use defined table Style
        lefttable.Borders.Visible = true;
        lefttable.Borders.Color = Colors.Black;
        lefttable.Borders.Style = BorderStyle.Single;
        lefttable.Borders.Width = 0;
        lefttable.Borders.Bottom.Width = .75;
        //lefttable.Borders.Right.Width = .75;
        lefttable.Rows.LeftIndent = leftshift;              //adjust position
        //lefttable.LeftPadding = 0;

        column = lefttable.AddColumn(colWidth * 4 - 20);
        row = lefttable.AddRow();
        cell = row.Cells[0];
        cell = row.Cells[0];
        cell.Format.Font.Size = 12;
        cell.Format.Font.Bold = true;
        var para = cell.AddParagraph("SHIP FROM");
        para.Format.Alignment = ParagraphAlignment.Center;
        para.Format.Borders.Distance = 4;

        row = lefttable.AddRow();
        cell = row.Cells[0];
        cell.Borders.Distance = 6;
        para = cell.AddParagraph(shipper.CustName);
        para.Format.Font.Bold = true;
        para.Format.Font.Size = 12;
        para.Format.Borders.Distance = 4;
        para = cell.AddParagraph(shipper.Addr);
        para.Format.LeftIndent = 10;
        if (shipper.City != null && shipper.City.Length > 0)
        {
            para = cell.AddParagraph(shipper.City);
            para.Format.LeftIndent = 10;
        }
        if (shipper.Province != null && shipper.Province.Length > 0)
        {
            para = cell.AddParagraph(shipper.Province);
            para.Format.LeftIndent = 10;
        }
        if (shipper.PostCode != null && shipper.PostCode.Length > 0)
        {
            para = cell.AddParagraph(shipper.PostCode);
            para.Format.LeftIndent = 10;
        }
        para.Format.Borders.DistanceFromBottom = 4;
        para.Format.SpaceAfter = 10;

        row = lefttable.AddRow();
        cell = row.Cells[0];
        cell.Format.Font.Size = 12;
        cell.Format.Font.Bold = true;
        para = cell.AddParagraph("SHIP TO");
        para.Format.Alignment = ParagraphAlignment.Center;
        para.Format.Borders.Distance = 4;

        row = lefttable.AddRow();
        cell = row.Cells[0];
        cell.Borders.Distance = 6;
        para = cell.AddParagraph(receiver.CustName);
        para.Format.Font.Bold = true;
        para.Format.Font.Size = 12;
        para.Format.Borders.Distance = 4;

        cell.Borders.Bottom.Width = 0;
        para.Format.Font.Bold = true;
        para = cell.AddParagraph(receiver.Addr);
        para.Format.LeftIndent = 10;
        if (receiver.City != null && receiver.City.Length > 0)
        {
            para = cell.AddParagraph(shipper.City);
            para.Format.LeftIndent = 10;
        }
        if (receiver.Province != null && receiver.Province.Length > 0)
        {
            para = cell.AddParagraph(receiver.Province);
            para.Format.LeftIndent = 10;
        }
        if (receiver.PostCode != null && receiver.PostCode.Length > 0)
        {
            para = cell.AddParagraph(receiver.PostCode);
            para.Format.LeftIndent = 10;
        }
        para.Format.Borders.DistanceFromBottom = 4;
        //---------------------------------------------------------------------------
        var righttable = new Table();
        righttable.Style = "Table";      //use defined table Style
        righttable.Borders.Visible = true;
        righttable.Borders.Color = Colors.Black;
        righttable.Borders.Style = BorderStyle.Single;
        righttable.Borders.Width = 0;
        righttable.Borders.Bottom.Width = 0.75;
        righttable.Rows.LeftIndent = colWidth * 4 - leftshift;              //adjust position
                                                                            // righttable.LeftPadding = 0;

        column = righttable.AddColumn(colWidth * 2 + 20);

        row = righttable.AddRow();
        cell = row.Cells[0];
        cell.Borders.Bottom.Width = 0.75;
        cell.Borders.Style = BorderStyle.Dot;
        para = cell.AddParagraph("Bill of Landing Number");
        para.Format.Font.Size = 12;
        para.Format.Borders.Distance = 4;
        para.Format.Alignment = ParagraphAlignment.Center;
        row = righttable.AddRow();
        cell = row.Cells[0];
        para = cell.AddParagraph(bolInfo.BolNo());
        para.Format.Alignment = ParagraphAlignment.Center;
        para.Format.SpaceAfter = 2;
        Stream stream = GenerateBarcodeStream(bolInfo.BolNo(), 400, 100);
        para = cell.AddParagraph();
        para.Format.Alignment = ParagraphAlignment.Center;
        image = para.AddImage(ImageSource.FromStream("bol.png", () => { return stream; }));
        image.LockAspectRatio = true;
        image.Width = colWidth * 2;

        para = cell.AddParagraph("");
        para.Format.Font.Size = 2;

        row = righttable.AddRow();
        cell = row.Cells[0];
        para = cell.AddParagraph();
        para.AddFormattedText("Shap Date: " + bolInfo.CreateDate.ToString("MM/dd/yyyy"), TextFormat.Bold);
        para.Format.Borders.Distance = 3;
        para = cell.AddParagraph();
        para.AddFormattedText("Carrier: " + carrier.CustName, TextFormat.Bold);
        para.Format.Borders.Distance = 3;
        para = cell.AddParagraph();
        para.AddFormattedText("SCAC:", TextFormat.Bold);
        para.Format.Borders.Distance = 3;
        para = cell.AddParagraph("Trailer Number: " + bolInfo.TrailerNo);
        para.Format.Borders.Distance = 3;
        para = cell.AddParagraph("Seal Number(s): ");
        para.Format.Borders.Distance = 3;
        para = cell.AddParagraph();
        para.AddFormattedText("Shipping #: " + bolInfo.ShippingNo(), TextFormat.Bold);
        para.Format.Borders.Distance = 3;

        row = righttable.AddRow();
        cell = row.Cells[0];
        cell.Borders.Bottom.Width = 0;
        para = cell.AddParagraph();
        para.AddFormattedText("FOB:", TextFormat.Bold);
        para.Format.Borders.Distance = 3;
        para = cell.AddParagraph();
        para.AddFormattedText("Freight Terms:", TextFormat.Bold);
        para.Format.SpaceAfter = 3;

        //paraghraph.Format.Font.Size = 4;


        row = table.AddRow();
        AddRows++;
        row.Cells[0].Elements.Add(lefttable);
        row.Cells[0].MergeRight = 3;

        row.Cells[3].Borders.Right.Width = 0.75;
        row.Cells[4].Elements.Add(righttable);
        row.Cells[0].MergeRight = 1;

        row = table.AddRow();
        row.Shading.Color = Colors.LightGray;
        AddRows++;
        cell = row.Cells[0];
        cell.MergeRight = 5;
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        para = cell.AddParagraph();
        para.AddFormattedText("Special Instruction", TextFormat.Bold);
        para.Format.Borders.Distance = 3;
        para.Format.LeftIndent = 3;

        row = table.AddRow();
        row.Height = 60;
        AddRows++;
        cell = row.Cells[0];
        cell.MergeRight = 5;

        row = table.AddRow();
        row.Shading.Color = Colors.LightGray;
        AddRows++;
        cell = row.Cells[0];
        cell.MergeRight = 5;
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        para = cell.AddParagraph();
        para.AddFormattedText("PARTS INFORMATION", TextFormat.Bold);
        para.Format.Borders.Distance = 3;
        para.Format.Alignment = ParagraphAlignment.Center;

        row = table.AddRow();
        AddRows++;
        cell = row.Cells[0];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        cell.Borders.Right.Width = 0.75;
        para = cell.AddParagraph();
        para.AddFormattedText("PART #", TextFormat.Bold);
        para.Format.Borders.Distance = 2;
        para.Format.Alignment = ParagraphAlignment.Center;
        para.Format.Font.Size = 8;

        cell = row.Cells[1];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        cell.Borders.Right.Width = 0.75;
        para = cell.AddParagraph();
        para.AddFormattedText("SKID #", TextFormat.Bold);
        para.Format.Borders.Distance = 2;
        para.Format.Alignment = ParagraphAlignment.Center;
        para.Format.Font.Size = 8;

        cell = row.Cells[2];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        cell.MergeRight = 1;
        para = cell.AddParagraph();
        para.AddFormattedText("DESCRIPTION", TextFormat.Bold);
        para.Format.Borders.Distance = 2;
        para.Format.Alignment = ParagraphAlignment.Center;
        para.Format.Font.Size = 8;

        row.Cells[3].Borders.Right.Width = 0.75;

        cell = row.Cells[4];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        cell.Borders.Right.Width = 0.75;
        para = cell.AddParagraph();
        para.AddFormattedText("QUANTITY", TextFormat.Bold);
        para.Format.Borders.Distance = 2;
        para.Format.Alignment = ParagraphAlignment.Center;
        para.Format.Font.Size = 8;

        cell = row.Cells[5];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        para = cell.AddParagraph();
        para.AddFormattedText("WEIGHT (lbs)", TextFormat.Bold);
        para.Format.Borders.Distance = 2;
        para.Format.Alignment = ParagraphAlignment.Center;
        para.Format.Font.Size = 8;

        float totalWeight = 0f;
        int totalQty = 0;
        foreach (var part in partDetail)
        {
            row = table.AddRow();
            AddRows++;
            cell = row.Cells[0];
            cell.Borders.Top.Width = 0.75;
            cell.Borders.Bottom.Width = 0.75;
            cell.Borders.Right.Width = 0.75;
            para = cell.AddParagraph(part.PartNo);
            para.Format.Borders.Distance = 2;
            para.Format.Alignment = ParagraphAlignment.Center;
            para.Format.Font.Size = 8;

            cell = row.Cells[1];
            cell.Borders.Top.Width = 0.75;
            cell.Borders.Bottom.Width = 0.75;
            cell.Borders.Right.Width = 0.75;
            para = cell.AddParagraph(part.SkidNo);
            para.Format.Borders.Distance = 2;
            para.Format.Alignment = ParagraphAlignment.Center;
            para.Format.Font.Size = 8;

            cell = row.Cells[2];
            cell.Borders.Top.Width = 0.75;
            cell.Borders.Bottom.Width = 0.75;
            cell.MergeRight = 1;
            para = cell.AddParagraph(part.Info);
            para.Format.Borders.Distance = 2;
            para.Format.Alignment = ParagraphAlignment.Center;
            para.Format.Font.Size = 8;

            row.Cells[3].Borders.Right.Width = 0.75;

            cell = row.Cells[4];
            cell.Borders.Top.Width = 0.75;
            cell.Borders.Bottom.Width = 0.75;
            cell.Borders.Right.Width = 0.75;
            para = cell.AddParagraph(part.Qty.ToString());
            para.Format.Borders.Distance = 2;
            para.Format.Alignment = ParagraphAlignment.Center;
            para.Format.Font.Size = 8;

            totalQty += part.Qty;

            cell = row.Cells[5];
            cell.Borders.Top.Width = 0.75;
            cell.Borders.Bottom.Width = 0.75;
            para = cell.AddParagraph();
            if (part.PartWeight != null)
            {
                float weight = (float)part.PartWeight * part.Qty;
                para.AddText(weight.ToString("N"));
                totalWeight += weight;
            }
            para.Format.Borders.Distance = 2;
            para.Format.Alignment = ParagraphAlignment.Right;
            para.Format.Font.Size = 8;

        }
        //--------------------------------------------------
        row = table.AddRow();
        AddRows++;
        cell = row.Cells[0];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        cell.Borders.Right.Width = 0.75;
        cell.MergeRight = 3;
        cell.Shading.Color = Colors.LightGray;
        para = cell.AddParagraph();
        para.AddFormattedText("GRAND TOTAL", TextFormat.Bold);
        para.Format.Borders.Distance = 2;
        para.Format.Alignment = ParagraphAlignment.Center;
        para.Format.Font.Size = 10;


        cell = row.Cells[4];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        cell.Borders.Right.Width = 0.75;
        para = cell.AddParagraph();
        para.AddFormattedText(totalQty.ToString(), TextFormat.Bold);
        para.Format.Borders.Distance = 2;
        para.Format.Alignment = ParagraphAlignment.Center;
        row.VerticalAlignment = VerticalAlignment.Center;
        para.Format.Font.Size = 8;

        cell = row.Cells[5];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        para = cell.AddParagraph();
        if (totalWeight > 0f)
            para.AddFormattedText(totalWeight.ToString("N"), TextFormat.Bold);
        para.Format.Borders.Distance = 2;
        para.Format.Alignment = ParagraphAlignment.Right;
        row.VerticalAlignment = VerticalAlignment.Center;
        para.Format.Font.Size = 8;

        //-----------------------------------
        row = table.AddRow();
        AddRows++;
        cell = row.Cells[0];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        cell.Borders.Right.Width = 0.75;
        cell.MergeRight = 5;
        para = cell.AddParagraph("Fee Terms: ");
        para.Format.Font.Bold = true;

        var txt = para.AddFormattedText("c ", TextFormat.Bold);
        txt.Font.Name = "Webdings";
        para.AddFormattedText("Collect   ", TextFormat.Bold);

        txt = para.AddFormattedText("c ", TextFormat.Bold);
        txt.Font.Name = "Webdings";
        para.AddFormattedText("Prepaid   ", TextFormat.Bold);

        txt = para.AddFormattedText("c ", TextFormat.Bold);
        txt.Font.Name = "Webdings";
        para.AddFormattedText("Customer check acceptable ", TextFormat.Bold);

        para.AddText("        ");
        para.AddFormattedText("COD AMOUNT: $", TextFormat.Bold);
        para.AddFormattedText("___________", TextFormat.Bold);


        para.Format.Borders.Distance = 5;
        para.Format.Alignment = ParagraphAlignment.Center;
        row.VerticalAlignment = VerticalAlignment.Center;
        para.Format.Font.Size = 8;
        //-----------------------------------------------------------------------------
        row = table.AddRow();
        AddRows++;
        cell = row.Cells[0];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        cell.Borders.Right.Width = 0.75;
        cell.MergeRight = 1;
        para = cell.AddParagraph("SHIPPER SIGNATURE / DATE");
        para.Format.Font.Bold = true;
        para.Format.Font.Size = 8;
        para.Format.Borders.Distance = 4;
        para.Format.LeftIndent = 4;
        para = cell.AddParagraph("This is to certify that above named materials are properly classified, described, packaged, marked, and labeled, and are in proper condition of transportation according to the applicable regulations of the DOT.");
        para.Format.Font.Bold = true;
        para.Format.Font.Size = 6.8;
        para.Format.Borders.Distance = 2;
        para.Format.LeftIndent = 4;
        para.Format.SpaceAfter = 45;
        para.Format.Alignment = ParagraphAlignment.Justify;

        //---------------------------------------------
        cell = row.Cells[2];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        cell.Borders.Right.Width = 0.75;
        para = cell.AddParagraph();
        para.AddFormattedText("Trailer Loaded", TextFormat.Bold | TextFormat.Underline);
        para.Format.Font.Size = 8;
        para.Format.Borders.Distance = 4;
        para.Format.LeftIndent = 4;
        para.Format.SpaceBefore = 10;

        para = cell.AddParagraph();
        txt = para.AddFormattedText("c ", TextFormat.Bold);
        txt.Font.Name = "Webdings";
        para.AddFormattedText("By Shipper ", TextFormat.Bold);
        para.Format.Font.Size = 8;
        para.Format.Borders.Distance = 4;
        para.Format.LeftIndent = 4;
        para.Format.SpaceBefore = 5;

        para = cell.AddParagraph();
        txt = para.AddFormattedText("c ", TextFormat.Bold);
        txt.Font.Name = "Webdings";
        para.AddFormattedText("By Driver ", TextFormat.Bold);
        para.Format.Font.Size = 8;
        para.Format.Borders.Distance = 4;
        para.Format.LeftIndent = 4;
        //------------------------------------------------------------
        cell = row.Cells[3];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        cell.Borders.Right.Width = 0.75;
        para = cell.AddParagraph();
        para.AddFormattedText("Fright Counted", TextFormat.Bold | TextFormat.Underline);
        para.Format.Font.Size = 8;
        para.Format.Borders.Distance = 4;
        para.Format.LeftIndent = 4;
        para.Format.SpaceBefore = 10;

        para = cell.AddParagraph();
        txt = para.AddFormattedText("c ", TextFormat.Bold);
        txt.Font.Name = "Webdings";
        para.AddFormattedText("By Shipper ", TextFormat.Bold);
        para.Format.Font.Size = 8;
        para.Format.Borders.Distance = 4;
        para.Format.LeftIndent = 4;
        para.Format.SpaceBefore = 5;

        para = cell.AddParagraph();
        txt = para.AddFormattedText("c ", TextFormat.Bold);
        txt.Font.Name = "Webdings";
        para.AddFormattedText("By Driver ", TextFormat.Bold);
        para.Format.Font.Size = 8;
        para.Format.Borders.Distance = 4;
        para.Format.LeftIndent = 4;

        //--------------------------------------
        cell = row.Cells[4];
        cell.Borders.Top.Width = 0.75;
        cell.Borders.Bottom.Width = 0.75;
        cell.Borders.Right.Width = 0.75;
        cell.MergeRight = 1;
        para = cell.AddParagraph("CARRIER SIGNATURE / PICKUP DATE");
        para.Format.Font.Bold = true;
        para.Format.Font.Size = 8;
        para.Format.Borders.Distance = 4;
        para.Format.LeftIndent = 4;
        para.Format.Alignment = ParagraphAlignment.Left;

        para = cell.AddParagraph("Carrier acknowledges the receipt of the package and required placards. Carrier certifies emergency response information was made available and/or carrier has DOT emergency guidebook or equivalent documentation in the vehicle.");
        para.Format.Font.Bold = true;
        para.Format.Font.Size = 6.8;
        para.Format.Borders.Distance = 2;
        para.Format.LeftIndent = 4;
        para.Format.SpaceAfter = 45;
        para.Format.Alignment = ParagraphAlignment.Justify;


        table.SetEdge(0, 0, 6, AddRows, Edge.Box, BorderStyle.Single, 1, Colors.Black);
        section.Add(table);
        //var layout =CreatePage(document);

        //FillContent(document,layout.frame,layout.table);
        //DefineCover(document);
        //DefineTableOfContents(document);
        //DefineContentSection(document);
        //DefineParagraphs(document);
        //DefineTables(document);
        ////DefineParagraphs(document);
        //DefineCharts(document);
        return (document, bolInfo);
    }


    public async Task<string> CreateBolPDF(int bolID, int shippingID)
    {

        // Create a MigraDoc document
        (Document document, BolInfo bolInfo) = await CreateDocument(bolID);

        PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer() { Document = document };

        pdfRenderer.RenderDocument();

        PdfDocument pdf = pdfRenderer.PdfDocument;

        pdf.Info.Title = "Bill of Landing";
        pdf.Info.Subject = "BOL";
        pdf.Info.Author = "Hearn Industrial Services";
        pdf.Info.Keywords = "BOL";

        //****Make Bookmark codeing, displaying mess charactors

        //PdfSecuritySettings securitySettings = pdf.SecuritySettings;

        //// Setting one of the passwords automatically sets the security level to 
        //// PdfDocumentSecurityLevel.Encrypted128Bit.
        ////securitySettings.UserPassword = "user";       //open password!!
        //securitySettings.OwnerPassword = "owner";

        //// Don´t use 40 bit encryption unless needed for compatibility reasons
        //securitySettings.DocumentSecurityLevel = PdfDocumentSecurityLevel.Encrypted40Bit;

        //// Restrict some rights.
        //securitySettings.PermitAccessibilityExtractContent = false;
        //securitySettings.PermitAnnotations = false;
        //securitySettings.PermitAssembleDocument = false;
        //securitySettings.PermitExtractContent = false;
        //securitySettings.PermitFormsFill = true;
        //securitySettings.PermitFullQualityPrint = true;
        //securitySettings.PermitModifyDocument = false;
        //securitySettings.PermitPrint = true;

        // Save the document...
        string filename = Path.Combine(Env.WebRootPath, "BOL", bolInfo.GetBolPdfFileName());
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
        pdf.Save(filename);

        // ...and start a viewer.
        //Process.Start(filename);
        filename = Path.Combine("BOL", bolInfo.GetBolPdfFileName());
        return filename;
    }

    public bool CreateLabelPDF(int ReceivindID, string BOL,string fileName, DapperContext context)
    {

        // Create a new PDF document
        PdfDocument document = new PdfDocument();

        // Create a font
        XFont font = new XFont("Times", 25, XFontStyle.Bold);
        // Fix for CS1503: Argument 1: cannot convert from 'System.IO.Stream' to 'System.Func<System.IO.Stream>'
        // Update the line where XImage.FromStream is called to pass a lambda function that returns the stream.
        try
        {
            var parts =  context.GetLabelPrintInfos(ReceivindID);
            int pageNumber = 1;
            foreach (var part in parts)
            {
                // Create an empty page
                PdfPage page = document.AddPage();
                page.Width = 4 * 72;  // 4 inches * 72 points/inch
                page.Height = 6 * 72; // 6 inches * 72 points/inch
                page.Orientation = PageOrientation.Landscape;

                // Get an XGraphics object for drawing
                XGraphics gfx = XGraphics.FromPdfPage(page);

                //gfx.DrawRectangle(XBrushes.LightGray, 0, 0, 6 * 72, 4 * 72);

                // Draw the text
                gfx.DrawString(part.PalletNo, font, XBrushes.Black, new XRect(0, 30, 6 * 72, 30),  XStringFormats.Center);
               // gfx.DrawString($"P{part.PartNumber};Q{part.BinQuantity};S{BOL}-{pageNumber.ToString().PadLeft(4,'0')}", font, XBrushes.Black, new XRect(0, 60, 6 * 72, 30), XStringFormats.Center);

                //XImage image = GenerateQRcodeBarcodeImage($"P{part.PartNumber};Q{part.BinQuantity};S{BOL}-{pageNumber.ToString().PadLeft(4,'0')}", 300*2);
                XImage image = GenerateQRcodeBarcodeImage(part.PalletQRCode!, 300*2);
                int x = (6 * 72 - 72 * 2) / 2;
                
                gfx.DrawImage(image, x, 90,  72*2,  72*2);

                pageNumber++;
            }

        }
        catch (Exception ex)
        {

        }
        // Create a MigraDoc document
        //Document document = new();// = await CreateDocument(ReceivindID);

        //PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer() { Document = document };        // Pass the document to the renderer:

        //pdfRenderer.RenderDocument();       // Let the renderer do its job:

        //PdfDocument pdf = pdfRenderer.PdfDocument;
        PdfDocument pdf = document;

        pdf.Info.Title = "Container Label";
        pdf.Info.Subject = "Labels";
        pdf.Info.Author = "Hearn Industrial Services";
        pdf.Info.Keywords = "Label";

        // Save the document...
        string filename = Path.Combine(Env.WebRootPath, "Labels", fileName);
        if (File.Exists(filename))
        {
            File.Delete(filename);
        }
        pdf.Save(filename);

        // ...and start a viewer.
        //Process.Start(filename);
        // filename = Path.Combine("BOL", bolInfo.GetBolPdfFileName());

        return true;
    }
}

public class LayoutHelper
{
    private readonly PdfDocument _document;
    private readonly XUnit _topPosition;
    private readonly XUnit _bottomMargin;
    private XUnit _currentPosition;

    public LayoutHelper(PdfDocument document, XUnit topPosition, XUnit bottomMargin)
    {
        _document = document;
        _topPosition = topPosition;
        _bottomMargin = bottomMargin;

        // Set a value outside the page - a new page will be created on the first request.
        _currentPosition = bottomMargin + 10000;
    }

    public XUnit GetLinePosition(XUnit requestedHeight)
    {
        return GetLinePosition(requestedHeight, -1f);
    }

    public XUnit GetLinePosition(XUnit requestedHeight, XUnit requiredHeight)
    {
        XUnit required = requiredHeight == -1f ? requestedHeight : requiredHeight;
        if (_currentPosition + required > _bottomMargin)
            CreatePage();
        XUnit result = _currentPosition;
        _currentPosition += requestedHeight;
        return result;
    }

    public XGraphics Gfx { get; private set; }
    public PdfPage Page { get; private set; }

    void CreatePage()
    {
        Page = _document.AddPage();
        Page.Size = PageSize.A4;
        //Page.Size.Equals(PageSize.Letter);
        Gfx = XGraphics.FromPdfPage(Page);
        _currentPosition = _topPosition;
    }
}
