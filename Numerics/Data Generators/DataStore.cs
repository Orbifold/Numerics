
using System.Collections.Generic;

namespace Orbifold.Numerics
{
	public class DataStore
	{
		public static readonly string[] TaskNames = {
			"Write document", 
			"Research", 
			"Google search", 
			"Develop plan",
			"Consulting", 
			"Buget effort", 
			"Master plan", 
			"Management",
			"Project management", 
			"Testing", 
			"Inventarization", 
			"Create slides",
			"Documentation",
			"Travelling", 
			"Stock update", 
			"Literature",
			"Getting sample data",
			"Conference", 
			"Reading",
			"Summarizing",
			"Team event", 
			"Integration",
			"Planning",
			"Copyright check", 
			"Trademark research", 
			"Sales planning",
			"Time management", 
			"Team event",
			"Scrum meeting",
			"Sales pitch",
			"Budgetting",
		};
		public static readonly string[,] SalesTerms = { {"Absorbed Business"," A company that has been merged into another company."},
                                                {"Absorbed costs","The indirect costs associated with manufacturing, for example, insurance or property taxes."},
                                                {"Account balance","The difference between the debit and the credit sides of an account."},
                                                {"Accrual basis","A method of keeping accounts that shows expenses incurred and income  earned for a given fiscal period, even though such expenses and income have not been actually paid or received in cash."},
                                                {"Amortization"," To liquidate on an installment basis; the process of grad­ually paying off a liability over a period of time."},
                                                {"Audiotaping"," The act of recording onto an audiotape."},
                                                {"Back-to-back loan","an arrangement in which two companies in different countries borrow offsetting amounts in each other's currency and each repays it at a specified future date in its domestic currency. Such a loan, often between a company and its foreign subsidiary, eliminates the risk of loss from exchange rate fluctuations."},
                                                {"Back pay","pay that is owed to an employee for work carried out before the current payment period and is either overdue or results from a backdated pay increase.  "},
                                                {"Ballpark"," an informal term for a rough, estimated figure."},
                                                {"Bill of sale","Formal legal document that conveys title to or interest in specific property from the seller to the buyer."},
                                                {"Business  venture"," Taking financial risks in a commercial enterprise"},
                                                {"Seed money"," a usually modest amount of money used to convert an idea into a viable business. Seed money is a form of venture capital."},
                                                {"Settlement","the payment of a debt or charge."},
                                                {"Sole proprietorship","Business legal structure in which one individual owns the business."},
                                                {"Venture funding","the round of funding for a new company that follows seed funding provided by venture capitalists.    "},
                                                {"Viral marketing","the rapid spread of a message about a new product or service in a similar way to the spread of a virus  "},
                                                {"Vulture capitalist","a venture capitalist who structures deals on behalf of an entrepreneur in such a way that the investors benefit rather than the entrepreneur"},
                                                {"Wallet technology","a software package providing digital wallets or purses on the computers of merchants and customers to facilitate payment by digital cash"},
                                                {"Whistleblowing"," speaking out to the media or the public on malpractice, misconduct, corruption, or mismanagement witnessed in an organization"},
                                                {"Withholding tax","the money that an employer pays directly to the U.S. government as a payment of the income tax on the employee "},
                                                {"Patent","a type of copyright granted as a fixed-term monopoly to an inventor by the state to prevent others copying an invention, or improvement  of a product or process"},
                                                {"Point of purchase","the place at which a product is purchased by the customer. The point of sale can be a retail outlet, a display case, or even a  legal business relationship of two or more people who share responsibilities, resources, profits and liabilities."},
                                                {"Price floor","The lowest amount a business owner can charge for a prod­uct or service and still meet all expenses"},
                                                {"Product mix","All of the products in a seller's total product line."},
                                                {"Psychographics","The system of explaining market behavior in terms of attitudes and life styles."},
                                                {"Added value ","the element(s) of service or product that a sales person or selling organization provides, that a customer is prepared to pay for because of the benefit(s) obtained. "},
                                                {"Advantage","the aspect of a product or service that makes it better than another, especially the one in-situ or that of a competitor"},
                                                {"Benefit","the gain (usually a tangible cost, but can be intangible) that accrues to the customer from the product or service."},
                                                {"Buying warmth","behavioural, non-verbal and other signs that a prospect likes what he sees; very positive from the sales person's perspective, but not an invitation to jump straight to the close"},
                                                {"Commodities","typically a term applied to describe products which are mature in development, produced and sold in vast scale, involving little or no uniqueness between variations of different suppliers; high volume, low price, low profit margin, de-skilled ('ease of use' in consumption, application, installation, etc)."},
                                                {"Deal ","common business parlance for the sale or purchase (agreement or arrangement). It is rather a colloquial term so avoid using it in serious company as it can sound flippant and unprofessional."},
                                                {"Deliverable","an aspect of a proposal that the provider commits to do or supply, usually and preferably clearly measurable."},
                                                {"Influencer ","a person in the prospect organization who has the power to influence and persuade a decision-maker."},
                                                {"Intangible ","in a selling context this describes, or is, an aspect of the product or service offering that has a value but is difficult to see or quantify (for instance, peace-of-mind, reliability, consistency). "},
                                                {"Lead-time","time between order and delivery, installation or commencement of a product or service"},
                                                {"Open plan selling","a modern form of selling, heavily dependent on the sales person understanding and interpreting the prospect's organizational and personal needs, issues, processes, constraints and strategic aims, which generally extends the selling discussion far beyond the obvious product application"},
                                                {"Referral ","a recommendation or personal introduction or permission/suggestion made by someone, commonly but not necessarily a buyer, which enables the seller to approach or begin dialogue with a new perspective buyer or decision"},
                                                {"Sales funnel","describes the pattern, plan or actual achievement of conversion of prospects into sales, pre-enquiry and then through the sales cycle."},
                                           
                                            };
        public static Dictionary<string, string> CommonExtensions = new Dictionary<string, string>
			{
				{"ace", "ACE Archiver compression file."},
				{"aif", "Audio Interchange File used with SGI and Macintosh applications."},
				{"ani", "Animated cursors used in Microsoft Windows."},
				{"api", "Application Program Interface."},
				{"art", "Clipart."},
				{"asc", "ASCII text file."},
				{"asm", "Assembler code."},
				{"asp", "Microsoft Active Server Page."},
				{"avi", "Audio/Video Interleaved used for Windows based movies."},
				{"bak", "Backup Files."},
				{"bas", "BASIC programming language sourcecode."},
				{"bat", "MS-DOS batch file."},
				{"bfc", "Briefcase document used in Windows."},
				{"bin", "Binary File."},
			
				{"bmp", "Bitmap format."},
				{"bud", "Backup Disk for Quicken by Intuit."},
				{"bz2", "Bzip2-compressed files."},
				{"c", "C source file."},
				{"cat", "Security Catalog file."},
				{"cbl", "Cobol code."},
				{"cbt", "Computer Based Training."},
				{"cda", "Compact Disc Audio Track."},
				{"cdt", "Corel Draw Template file."},
				{"cfml", "ColdFusion Markup Language."},
				{"cgi", "Common Gateway Interface. Web based programs and scripts."},
				{"chm", "Compiled HTML Help files used by Windows."},
				{"class", "Javascript Class file."},
				{"clp", "Windows Clipboard file."},
				{"cmd", "Dos Command File."},
				{"com", "Command File."},
				{"cpl", "Control panel item"},

				{"cp", "C++ programming language source code."},
				{"css", "Cascading Style Sheet. Creates a common style reference for a set of web pages."},
				{"csv", "Comma Separated Values format."},
				{"cmf", "Corel Metafile."},
				{"cur", "Cursor in Microsoft Windows."},
				{"dao", "Registry Backup file for Windows registry."},
				{"dat", "Data file."},
				{"dd", "Compressed Archive by Macintosh DiskDoubler."},
				{"deb", "Debian packages."},
				{"dev", "Device Driver."},
				{"dic", "Dictionary file."},
				{"dir", "Macromedia Director file."},
				{"dll", "Dynamic Linked Library. Microsoft application file."},
				{"doc", "Document format for Word Perfect and Microsoft Word."},
				{"dot", "Microsoft Word Template."},
				{"drv", "Device Driver."},
				{"ds", "TWAIN Data source file."},
				{"dun", "Dial-up networking configuration file."},
				{"dwg", "Autocad drawing."},
				{"dxf", "Autocad drawing exchange format file."},
				{"emf", "Enhanced Windows Metafile."},
				{"eml", "Microsoft Outlook e-mail file."},
				{"eps", "Encapsulated PostScript supported by most graphics programs."},
				{"eps2", "Adobe PostScript Level II Encapsulated Postscript."},
				{"exe", "DOS based executable file which is also known as a program."},
				{"ffl", "Microsoft Fast Find file."},
				{"ffo", "Microsoft Fast Find file."},
				{"fla", "Macromedia Flash movie format."},
				{"fnt", "Font file."},

				{"gif", "Graphics Interchange Format that supports animation. Created by CompuServe and used primarily for web use."},
				{"gid", "Windows global index. Contains the index information used by Help in Windows."},
				{"grp", "Microsoft Program Manager Group."},
				{"gz", "Unix compressed file."},
				{"hex", "Macintosh binary hex(binhex) file."},
				{"hlp", "Standard help file."},
				{"ht", "HyperTerminal files."},
				{"hqx", "Macintosh binary hex(binhex) file."},
				{"htm", "Hyper Text Markup. This markup language is used for web design."},
				{"html", "Hyper Text Markup Language. This markup language is used for web design."},
				{"icl", "Icon Library File."},
				{"icm", "Image Color Matching profile."},
				{"ico", "Microsoft icon image."},
				{"inf", "Information file used in Windows."},
				{"ini", "Initialization file used in Windows."},
				{"jar", "Java Archive. A compressed java file format."},
				{"jpeg", "Compression scheme supported by most graphics programs and used predominantly for web use."},
				{"jpg", "More common extension for JPEG described above."},
				{"js", "JavaScript File - A text file containing JavaScript programming code."},
				{"lab", "Microsoft Excel mailing labels."},
				{"lit", "eBooks in Microsoft Reader format."},
				{"lnk", "Windows 9x shortcut file."},
				{"log", "Application log file."},
				{"lsp", "Autocad(visual) lisp program."},
				{"maq", "Microsoft Access Query."},

				{"mar", "Microsoft Access Report."},
				{"mdb", "Microsoft Access DataBase File."},
				{"mdl", "Rose model file. Opens with Visual Modeler or Rational Rose."},
				{"mid", "MIDI music file."},
				{"mod", "Microsoft Windows 9.x kernel module."},
				{"mov", "Quicktime movie."},
				{"mp3", "MPEG Audio Layer 3."},
				{"mpeg", "Animation file format."},
				{"mpp", "Microsoft Project File."},
				{"msg", "Microsoft Outlook message file."},
			
				{"ncf", "Netware command File."},
				{"nlm", "Netware loadable Module."},
				{"o", "Object file, used by linkers."},
				{"ocx", "ActiveX Control: A component of the Windows environment."},
				{"ogg", "Ogg Vorbis digitally encoded music file."},
				{"ost", "Microsoft Exchange/Outlook offline file."},
				{"pak", "WAD file that contains information about levels, settings, maps, etc for Quake and Doom."},
				{"pcl", "Printer Control Language file. PCL is a Page Description Language developed by HP."},
				{"pct", "Macintosh drawing format."},
				{"pdf", "Portable Document File by Adobe. "},
			
				{"pdr", "Port driver for windows 95. It is actually a virtual device driver (vxd)."},
				{"php", "Web page that contains a PHP script."},
				{"phtml", "Web page that contains a PHP script."},
			
			
				{"pif", "Program Information File"},
				{"pl", "Perl source code file."},
				{"pm", "Perl Module."},
				{"png", "Portable Network Graphic file."},
				{"pol", "System Policy file for Windows NT."},
				{"pot", "Microsoft PowerPoint design template."},
				{"pps", "Microsoft PowerPoint slide show."},
				{"ppt", "Microsoft PowerPoint presentation(default extension)."},
				{"prn", "A print file created as the result of printing to file."},
				{"ps", "PostScript file."},
				{"psd", "Native Adobe Photoshop format."},
				{"psp", "Paint Shop Pro image."},
				{"pst", "Personal Folder File for Microsoft Outlook."},
				{"pub", "Microsoft Publisher document."},
				{"qif", "Quicken Import file."},
				{"ram", "RealAudio Metafile."},
				{"rar", "RAR compressed archive created by Eugene Roshall."},
				{"raw", "Raw File Format."},
				{"rdo", "Raster Document Object. Proprietary file type used on Xerox"},
				{"reg", "Registry file that contains registry settings."},
				{"rm", "RealAudio video file."},
				{"rpm", "RedHat Package Manager."},
				{"rsc", "Standard resource file."},
				{"rtf", "Rich Text Format."},
				{"scr", "Screen Saver file."},
				{"sea", "Self-extracting archive for Macintosh Stuffit files."},
				{"sgml", "Standard Generalized Markup Language."},
				{"sh", "Unix shell script."},
				{"shtml", "HTML file that supports Server Side Includes(SSI)."},
				{"sit", "Compressed Macintosh Stuffit files."},
				{"smd", "SEGA mega drive ROM file."},
				{"svg", "Adobe scalable vector graphics file."},
				{"swf", "Shockwave Flash file by Macromedia."},
				{"swp", "DOS swap file."},
				{"sys", "Windows system file used for hardware configuration or drivers."},
				{"tar", "Unix Tape Archive."},
				{"tga", "Targa bitmap."},
				{"tiff", "Tagged Image File Format."},
				{"tmp", "Windows temporary file."},
				{"ttf", "True Type font."},
				{"txt", "Text Format."},
				{"udf", "Uniqueness Definition File. Used for Windows unattended installations."},
				{"uue", "UU-encoded file."},
				{"vbx", "Microsoft Visual basic extension."},
				{"vm", "Virtual Memory file."},			
				{"vxd", "Windows 9x virtual device driver."},
				{"wav", "Waveform sound file."},
				{"wmf", "Windows Metafile (graphics format)."},
				{"xcf", "The GIMP's native image format."},
				{"xif", "Xerox Image file (same as TIFF)."},
				{"xls", "Microsoft Excel Spreadsheet."},
				{"xlt", "Microsoft Excel Template."},
				{"xml", "Extensible markup language."},
				{"xsl", "XML style sheet."},
				{"zip", "Compressed Zip archive."},
			};

        public static Dictionary<string, string> OfficeExtensions = new Dictionary<string, string>
			{
				{"accda","Microsoft Access 2007/2010 add-in file"},
				{"accdb","Microsoft Access 2007/2010 database file"},
				{"accdc","Microsoft Access 2007/2010 digitally signed database file"},
				{"accde","Microsoft Access 2007/2010 compiled execute only file"},
				{"accdp","Microsoft Access 2007/2010 project file"},
				{"accdr","Microsoft Access 2007/2010 runtime mode database file"},
				{"accdt","Microsoft Access 2007/2010 database template file"},
				{"accdu","Microsoft Access 2007/2010 database wizard file"},
				{"acl","Microsoft Office automatic correction list"},
				{"ade","Microsoft Access compiled project file"},
				{"adp","icrosoft Access project file"},
				{"asd","Microsoft Word auto-save document file"},
				{"cnv","Microsoft Word text conversion file"},
				{"doc","Microsoft Word 97 to 2003 document file"},
				{"docm","Microsoft Word 2007/2010 Open XML macro-enabled document file"},
				{"docx","Microsoft Word 2007/2010 Open XML document file"},
				{"dot","Microsoft Word 97 to 2003 document template file"},
				{"dotm","Microsoft Word 2007/ 2010 Open XML macro-enabled document template file"},
				{"dotx","Microsoft Word 2007 or Word 2010 XML document template file"},
				{"grv","Microsoft SharePoint WorkSpace Groove file"},
				{"iaf","Microsoft Outlook exported account and email setting file"},
				{"laccd","Microsoft Access 2007/2010 database lock file"},
				{"maf","Microsoft Access form shortcut file"},
				{"mam","Microsoft Access macro shortcut file"},
				{"maq","Microsoft Access query shortcut file"},
				{"mar","Microsoft Access report shortcut file"},
				{"mat","Microsoft Access table shortcut file"},
				{"mda","Microsoft Access add-in file"},
				{"mdb","Microsoft Access database file"},
				{"mde","Microsoft Access compiled database (application) file"},
				{"mdt","Microsoft Access database template file"},
				{"mdw","Microsoft Access workgroup information file"},
				{"mpd","Microsoft Project database file"},
				{"mpp","Microsoft Project plan file"},
				{"mpt","Microsoft Project template tile"},
				{"oab","Microsoft Outlook offline address book file"},
				{"obi","Microsoft Outlook 2007/2010 RSS subscription file"},
				{"oft","Microsoft Outlook template file"},
				{"olm","Microsoft Outlook for Mac data file"},
				{"one","Microsoft OneNote document file"},
				{"onepk","Microsoft OneNote package file"},
				{"ops","Microsoft Office profile settings file"},
				{"ost","Microsoft Outlook inbox off-line folder file"},
				{"pa","Microsoft PowerPoint add-in file"},
				{"pip","Microsoft Office personalized settings file"},
				{"pot","Microsoft PowerPoint 97 to 2003 template file"},
				{"potm","Microsoft PowerPoint 2007/2010 macro-enabled Open XML template file"},
				{"potx","Microsoft PowerPoint 2007/2010 Open XML presentation template file"},
				{"ppa","Microsoft PowerPoint 97 to 2003 add-in file"},
				{"ppam","Microsoft PowerPoint 2007/2010 macro-enabled Open XML add-in file"},
				{"pps","Microsoft PowerPoint 97 to 2003 complete slide show file"},
				{"ppsm","Microsoft PowerPoint 2007/2010 macro-enabled Open XML complete slide show file"},
				{"ppsx","Microsoft PowerPoint 2007/2010 Open XML complete slide show file"},
				{"ppt","Microsoft PowerPoint 97 to 2003 Presentation file"},
				{"pptm","Microsoft PowerPoint 2007/2010 macro-enabled Open XML presentation file"},
				{"pptx","Microsoft PowerPoint 2007/2010 Open XML presentation file"},
				{"prf","Microsoft Outlook profile file"},
				{"pst","Microsoft Outlook personal folder file"},
				{"pub","Microsoft Publisher document file"},
				{"puz","Microsoft Publisher packed file"},
				{"rpmsg","Microsoft Restricted Permission Message file"},
				{"sldm","Microsoft PowerPoint 2007/2010 macro-enabled Open XML slide file"},
				{"sldx","Microsoft PowerPoint 2007/2010 Open XML slide file"},
				{"slk","Microsoft Symbolic Link format file"},
				{"snp","Microsoft Access report shapshot file"},
				{"svd","Microsoft Word document autosave file"},
				{"thmx","Microsoft Office 2007/2010 theme file"},
				{"vdx","Microsoft Visio drawing XML file"},
				{"vsd","Microsoft Visio diagram document file"},
				{"vss","Microsoft Visio smartshapes file"},
				{"vst","Microsoft Visio flowchart file"},
				{"vsx","Microsoft Visio stencil XML file"},
				{"vtx","Microsoft Visio XML template file"},
				{"wbk","Microsoft Word auto-backup document file"},
				{"wll","Microsoft Word add-in file"},
				{"xar","Microsoft Excel AutoRecover backup file"},
				{"xl","Microsoft Excel spreadsheet file"},
				{"xla","Microsoft Excel add-in file"},
				{"xlam","Microsoft Excel 2007/2010 Open XML macro-enabled add-in file"},
				{"xlb","Microsoft Excel Toolbars file"},
				{"xlc","Microsoft Excel Chart file"},
				{"xll","Microsoft Excel add-in file"},
				{"xlm","Microsoft Excel Macro file"},
				{"xls","Microsoft Excel 97 to 2003 workbook file"},
				{"xlsb","Microsoft Excel 2007/2010 binary workbook file"},
				{"xlsm","Microsoft Excel 2007/2010 Open XML macro-enabled workbook file"},
				{"xlsx","Microsoft Excel 2007/2010 Open XML workbook file"},
				{"xlt","Microsoft Excel Microsoft Excel 97 to 2003 Workbook template file"},
				{"xltm","Microsoft Excel 2007/2010 Open XML macro-enabled workbook template file"},
				{"xltx","Microsoft Excel 2007/2010 Open XML workbook template file"},
				{"xlw","Microsoft Excel Workspace file"},
				{"xsf","Microsoft Office InfoPath file"},
				{"xsn","Microsoft Office InfoPath template form file"},				
			};

        public static readonly string[] LipsumData = new[]
			{
				"consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et",
				"dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et",
				"justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata",
				"sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur",
				"sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore",
				"magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo",
				"dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est",
				"lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor", "sit", "amet", "consetetur", "sadipscing",
				"elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna",
				"aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo",
				"dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est",
				"lorem", "ipsum", "dolor", "sit", "amet", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in"
				, "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat", "nulla",
				"facilisis", "at", "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui", "blandit",
				"praesent", "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla", "facilisi",
				"lorem", "ipsum", "dolor", "sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh",
				"euismod", "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim",
				"ad", "minim", "veniam", "quis", "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut"
				, "aliquip", "ex", "ea", "commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in",
				"hendrerit", "in", "vulputate", "velit", "esse", "molestie", "consequat", "vel", "illum", "dolore", "eu", "feugiat"
				, "nulla", "facilisis", "at", "vero", "eros", "et", "accumsan", "et", "iusto", "odio", "dignissim", "qui",
				"blandit", "praesent", "luptatum", "zzril", "delenit", "augue", "duis", "dolore", "te", "feugait", "nulla",
				"facilisi", "nam", "liber", "tempor", "cum", "soluta", "nobis", "eleifend", "option", "congue", "nihil",
				"imperdiet", "doming", "id", "quod", "mazim", "placerat", "facer", "possim", "assum", "lorem", "ipsum", "dolor",
				"sit", "amet", "consectetuer", "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod", "tincidunt", "ut"
				, "laoreet", "dolore", "magna", "aliquam", "erat", "volutpat", "ut", "wisi", "enim", "ad", "minim", "veniam",
				"quis", "nostrud", "exerci", "tation", "ullamcorper", "suscipit", "lobortis", "nisl", "ut", "aliquip", "ex", "ea",
				"commodo", "consequat", "duis", "autem", "vel", "eum", "iriure", "dolor", "in", "hendrerit", "in", "vulputate",
				"velit", "esse", "molestie", "consequat", "vel", "selam", "veri", "dolami", "ordatum", "xolymi",
				"illum", "dolore", "eu", "feugiat", "nulla", "facilisis", "at", "presucta", "veroridis", "lummina",
				"vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd",
				"gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum",
				"dolor", "sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor",
				"invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam", "voluptua", "at", "vero",
				"eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet", "clita", "kasd", "gubergren",
				"no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor",
				"sit", "amet", "consetetur", "sadipscing", "elitr", "at", "accusam", "aliquyam", "diam", "diam", "dolore",
				"dolores", "duo", "eirmod", "eos", "erat", "et", "nonumy", "sed", "tempor", "et", "et", "invidunt", "justo",
				"labore", "stet", "clita", "ea", "et", "gubergren", "kasd", "magna", "no", "rebum", "sanctus", "sea", "sed",
				"takimata", "ut", "vero", "voluptua", "est", "lorem", "ipsum", "dolor", "sit", "amet", "lorem", "ipsum", "dolor",
				"sit", "amet", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy", "eirmod", "tempor", "invidunt", "ut",
				"labore", "et", "dolore", "magna", "aliquyam", "erat", "consetetur", "sadipscing", "elitr", "sed", "diam", "nonumy"
				, "eirmod", "tempor", "invidunt", "ut", "labore", "et", "dolore", "magna", "aliquyam", "erat", "sed", "diam",
				"voluptua", "at", "vero", "eos", "et", "accusam", "et", "justo", "duo", "dolores", "et", "ea", "rebum", "stet",
				"clita", "kasd", "gubergren", "no", "sea", "takimata", "sanctus", "est", "lorem", "ipsum"
			};

        public static string[] ZipCodes = new[]
			{
				"D4Y 1X3", "2963LM", "O6P 5W6", "D5 8YH", "48947", "2538VT", "99553", "N2J 1B3", "H7K 1GF", 
				"K9X 2UR", "QO2 2YR", "8748CW", "6858AA", "9467CI", "X6Y 8IJ", "E7B 3P4", "H2D 3F6", "B4X 2T3", 
				"BW4K 2RO", "6600IH", "K6S 0JW", "72411", "B5P 3G1", "TJ78 6ZD", "7941UN", "UK7Z 5YA", "H2 6AZ", 
				"6206KG", "E4I 9Z2", "62712", "T1M 4A4", "1147MT", "XF98 1FJ", "66983", "L6 0LC", "29394", 
				"19918", "2028TE", "I3O 9G0", "X3C 3H5", "S4B 8E1", "2420PX", "17948", "S3 6ZR", "7867TW", 
				"22343", "5952XX", "48307", "1718NX", "8972LC", "62391", "B6V 4T6", "U1E 6O0", "Z3V 3S8", 
				"NT8 4WB", "72249", "6662FS", "6809PZ", "N3Y 9D0", "S6I 2O9", "5258ZE", "B1 6RE", "63764", 
				"54686", "24450", "E31 2NU", "XR4H 4AT", "I6R 2H8", "I4 0JR", "ZU7 5OR", "3677RN", "VP3A 8FB", 
				"M5W 1DK", "16489", "O7Y 2F9", "N2B 4K1", "53875", "33582", "39431", "0672TF", "05443", 
				"69394", "X9P 8L9", "F1U 1H5", "32487", "64904", "R4E 3F6", "S4F 5L5", "K4 9XH", "22223", 
				"NL06 7FI", "0365UD", "5034JZ", "H58 5IL", "47560", "D8R 7Q8", "J4L 8T4", "6612ZP", "01272", 
				"35402", "3577KM", "7819DD", "XO6 2HW", "F6N 3U7", "J4W 7U0", "X45 0NB", "6269CP", "1459GE", 
				"M9 3HA", "90405", "A0U 6AW", "14473", "43347", "3544ZL", "0057RO", "58305", "42186", 
				"EE06 5BB", "BO0 8FQ", "F4N 3Y2", "4121YT", "V9S 9A2", "V5Y 1D4", "8757ZP", "7466EB", "A5Y 1U3", 
				"W1 6AP", "4692QO", "H8U 9B8", "W3Z 9C3", "F2 4FV", "G95 5PB", "JU5J 8JE", "4915GH", "5763AG", 
				"70580", "V8O 6F7", "1467TY", "L7X 8U1", "84374", "24496", "D40 7ET", "Z2A 8Z7", "5979QG", 
				"NB0 4TH", "79687", "R6D 5P9", "54740", "R5C 7Y8", "77846", "72598", "30388", "77628", 
				"S4B 5C9", "NV4U 3KQ", "A6P 8K3", "E5L 1V6", "R3I 6E0", "O9J 9O8", "L74 8EH", "7435AC", "JD35 1MN", 
				"8697WO", "A9 8IZ", "K5R 4P4", "64479", "IW2H 5CV", "87517", "1295VK", "01280", "4157TR", 
				"4429PY", "09757", "5248BN", "9396BQ", "B67 0GS", "T7X 3N1", "J1Z 4N1", "4814ML", "C7 5JM", 
				"2035ON", "W1 6HR", "E3Y 4S4", "YX55 2AW", "9184RY", "C8 8UP", "89353", "3088HW", "D6F 9B8", 
				"K1A 3U6", "41005", "8216NA", "D97 1ZP", "A1F 3W8", "I6S 3EY", "18668", "Z8Z 1W5", "P2G 8A1", 
				"M93 8DG", "L4U 1E7",
			};
        public static string[] EnglishMaleNames = new[]
			{
				"Aaron", "Abdiel", "Abdullah", "Abel", "Abraham", "Abram", "Adam", "Adan", "Addison", "Aden", "Aditya", "Adolfo",
				"Adonis", "Adrian", "Adriel", "Adrien", "Agustin", "Ahmad", "Ahmed", "Aidan", "Aiden", "Alan", "Albert", "Alberto",
				"Alden", "Aldo", "Alec", "Alejandro", "Alessandro", "Alex", "Alexander", "Alexandre", "Alexandro", "Alexis",
				"Alexzander", "Alfonso", "Alfred", "Alfredo", "Ali", "Alijah", "Allan", "Allen", "Alonso", "Alonzo", "Alvaro",
				"Alvin", "Amari", "Amarion", "Amir", "Anderson", "Andre", "Andres", "Andrew", "Andy", "Angel", "Angelo", "Anthony",
				"Antoine", "Anton", "Antonio", "Antony", "Antwan", "Ari", "Ariel", "Arjun", "Armando", "Armani", "Arnold", "Aron",
				"Arthur", "Arturo", "Aryan", "Asa", "Asher", "Ashton", "Aubrey", "August", "Augustus", "Austen", "Austin", "Austyn"
				, "Avery", "Axel", "Ayden", "Baby", "Bailey", "Barrett", "Barry", "Beau", "Ben", "Benjamin", "Bennett", "Benny",
				"Bernard", "Bernardo", "Bilal", "Billy", "Blaine", "Blaise", "Blake", "Blaze", "Bo", "Bobby", "Brad", "Braden",
				"Bradley", "Brady", "Bradyn", "Braeden", "Braedon", "Braiden", "Branden", "Brandon", "Braulio", "Braxton", "Brayan"
				, "Brayden", "Braydon", "Brendan", "Brenden", "Brendon", "Brennan", "Brennen", "Brent", "Brenton", "Bret", "Brett",
				"Brian", "Brice", "Brock", "Brodie", "Brody", "Brooks", "Bruce", "Bruno", "Bryan", "Bryant", "Bryce", "Brycen",
				"Bryson", "Byron", "Cade", "Caden", "Cael", "Caiden", "Cale", "Caleb", "Calvin", "Camden", "Cameron", "Camren",
				"Camron", "Carl", "Carlo", "Carlos", "Carlton", "Carson", "Carter", "Casey", "Cason", "Cayden", "Cedric", "Cesar",
				"Chad", "Chaim", "Chance", "Chandler", "Charles", "Charlie", "Chase", "Chaz", "Chris", "Christian", "Christopher",
				"Clarence", "Clark", "Clay", "Clayton", "Clifford", "Clifton", "Clinton", "Coby", "Cody", "Colby", "Cole",
				"Coleman", "Colin", "Collin", "Colt", "Colten", "Colton", "Conner", "Connor", "Conor", "Conrad", "Cooper", "Corbin"
				, "Cordell", "Corey", "Cornelius", "Cortez", "Cory", "Craig", "Cristian", "Cristobal", "Cristopher", "Cruz",
				"Cullen", "Curtis", "Cyrus", "Dakota", "Dale", "Dallas", "Dallin", "Dalton", "Damian", "Damien", "Damion", "Damon",
				"Dandre", "Dane", "Dangelo", "Daniel", "Danny", "Dante", "Daquan", "Darian", "Darien", "Darin", "Dario", "Darion",
				"Darius", "Darnell", "Darrell", "Darren", "Darrin", "Darrion", "Darrius", "Darryl", "Darwin", "Daryl", "Dashawn",
				"David", "Davin", "Davion", "Davis", "Davon", "Dawson", "Dayton", "Dean", "Deandre", "Deangelo", "Declan",
				"Demarcus", "Demetrius", "Dennis", "Denzel", "Deon", "Deonte", "Derek", "Derick", "Derrick", "Deshaun", "Deshawn",
				"Desmond", "Destin", "Devan", "Devante", "Deven", "Devin", "Devon", "Devonte", "Devyn", "Dexter", "Diego", "Dillan"
				, "Dillon", "Dimitri", "Dion", "Domenic", "Dominic", "Dominick", "Dominik", "Dominique", "Donald", "Donavan",
				"Donovan", "Dontae", "Donte", "Dorian", "Douglas", "Drake", "Draven", "Drew", "Duane", "Duncan", "Dustin", "Dwayne"
				, "Dwight", "Dylan", "Dylon", "Ean", "Earl", "Easton", "Eddie", "Eddy", "Edgar", "Edgardo", "Eduardo", "Edward",
				"Edwin", "Efrain", "Efren", "Eli", "Elian", "Elias", "Eliezer", "Elijah", "Eliseo", "Elisha", "Elliot", "Elliott",
				"Ellis", "Elmer", "Elvin", "Elvis", "Emanuel", "Emerson", "Emiliano", "Emilio", "Emmanuel", "Emmett", "Enrique",
				"Eric", "Erick", "Erik", "Ernest", "Ernesto", "Esteban", "Estevan", "Ethan", "Ethen", "Eugene", "Evan", "Everett",
				"Ezekiel", "Ezequiel", "Ezra", "Fabian", "Felipe", "Felix", "Fernando", "Fidel", "Finn", "Forrest", "Francis",
				"Francisco", "Frank", "Frankie", "Franklin", "Fred", "Freddie", "Freddy", "Frederick", "Fredrick", "Gabriel",
				"Gael", "Gage", "Gaige", "Gannon", "Garett", "Garret", "Garrett", "Garrison", "Gary", "Gaven", "Gavin", "Gavyn",
				"Geoffrey", "George", "Gerald", "Gerard", "Gerardo", "German", "Gian", "Giancarlo", "Gianni", "Gideon", "Gilbert",
				"Gilberto", "Gino", "Giovanni", "Giovanny", "Glen", "Glenn", "Gonzalo", "Gordon", "Grady", "Graham", "Grant",
				"Grayson", "Gregory", "Greyson", "Griffin", "Guadalupe", "Guillermo", "Gunnar", "Gunner", "Gustavo", "Guy", "Haden"
				, "Hamza", "Harley", "Harold", "Harrison", "Harry", "Hassan", "Hayden", "Heath", "Hector", "Henry", "Herbert",
				"Heriberto", "Holden", "Houston", "Howard", "Hudson", "Hugh", "Hugo", "Humberto", "Hunter", "Ian", "Ibrahim",
				"Ignacio", "Immanuel", "Irvin", "Irving", "Isaac", "Isaak", "Isai", "Isaiah", "Isaias", "Isiah", "Ismael", "Israel"
				, "Issac", "Ivan", "Izaiah", "Jabari", "Jace", "Jack", "Jackson", "Jacob", "Jacoby", "Jaden", "Jadon", "Jadyn",
				"Jaeden", "Jagger", "Jaheem", "Jaheim", "Jahiem", "Jaiden", "Jaime", "Jair", "Jairo", "Jake", "Jakob", "Jakobe",
				"Jalen", "Jamal", "Jamar", "Jamari", "Jamel", "James", "Jameson", "Jamie", "Jamil", "Jamir", "Jamison", "Jan",
				"Jaquan", "Jaquez", "Jared", "Jaren", "Jarod", "Jaron", "Jarred", "Jarrett", "Jarrod", "Jarvis", "Jase", "Jason",
				"Jasper", "Javen", "Javier", "Javion", "Javon", "Jaxon", "Jaxson", "Jay", "Jayce", "Jayden", "Jaydon", "Jaylan",
				"Jaylen", "Jaylin", "Jaylon", "Jayson", "Jean", "Jeff", "Jefferson", "Jeffery", "Jeffrey", "Jeremiah", "Jeremy",
				"Jermaine", "Jerome", "Jerry", "Jesse", "Jessie", "Jesus", "Jett", "Jevon", "Jimmy", "Joan", "Joaquin", "Joe",
				"Joel", "Joey", "Johan", "John", "Johnathan", "Johnathon", "Johnny", "Jon", "Jonah", "Jonas", "Jonatan", "Jonathan"
				, "Jonathon", "Jordan", "Jorden", "Jordon", "Jordy", "Jorge", "Jose", "Josef", "Joseph", "Josh", "Joshua", "Josiah"
				, "Josue", "Jovan", "Jovani", "Jovany", "Juan", "Judah", "Jude", "Julian", "Julien", "Julio", "Julius", "Junior",
				"Justice", "Justin", "Justus", "Justyn", "Kade", "Kaden", "Kadin", "Kai", "Kaiden", "Kale", "Kaleb", "Kameron",
				"Kamron", "Kane", "Kareem", "Karl", "Karson", "Kasey", "Kayden", "Keagan", "Keanu", "Keaton", "Keegan", "Keenan",
				"Keith", "Kellen", "Kelly", "Kelton", "Kelvin", "Kendall", "Kendrick", "Kennedy", "Kenneth", "Kenny", "Kent",
				"Kenyon", "Keon", "Keshawn", "Keven", "Kevin", "Kevon", "Keyon", "Keyshawn", "Khalid", "Khalil", "Kian", "Kieran",
				"Kirk", "Kobe", "Koby", "Kody", "Kolby", "Kole", "Kolton", "Korbin", "Korey", "Kory", "Kristian", "Kristofer",
				"Kristopher", "Kurt", "Kurtis", "Kylan", "Kyle", "Kyler", "Kyree", "Lamar", "Lamont", "Lance", "Landen", "Landon",
				"Lane", "Larry", "Latrell", "Lawrence", "Lawson", "Layne", "Layton", "Lee", "Leo", "Leon", "Leonard", "Leonardo",
				"Leonel", "Leroy", "Levi", "Lewis", "Liam", "Lincoln", "Lisandro", "Logan", "London", "Lonnie", "Lorenzo", "Louis",
				"Luc", "Luca", "Lucas", "Luciano", "Luis", "Lukas", "Luke", "Malachi", "Malakai", "Malcolm", "Malik", "Manuel",
				"Marc", "Marcel", "Marcelo", "Marco", "Marcos", "Marcus", "Mariano", "Mario", "Mark", "Markus", "Marlon", "Marquez"
				, "Marquis", "Marquise", "Marshall", "Martin", "Marvin", "Mason", "Mateo", "Mathew", "Matteo", "Matthew", "Maurice"
				, "Mauricio", "Maverick", "Max", "Maxim", "Maximilian", "Maximillian", "Maximo", "Maximus", "Maxwell", "Mekhi",
				"Melvin", "Micah", "Michael", "Micheal", "Miguel", "Mike", "Mikel", "Miles", "Milo", "Milton", "Misael", "Mitchel",
				"Mitchell", "Mohamed", "Mohammad", "Mohammed", "Moises", "Morgan", "Moses", "Moshe", "Muhammad", "Mustafa", "Myles"
				, "Nash", "Nasir", "Nathan", "Nathanael", "Nathanial", "Nathaniel", "Nathen", "Neal", "Nehemiah", "Neil", "Nelson",
				"Nestor", "Nicholas", "Nick", "Nickolas", "Nico", "Nicolas", "Nigel", "Nikhil", "Nikolas", "Noah", "Noe", "Noel ",
				"Nolan", "Norman", "Octavio", "Oliver", "Omar", "Omari", "Omarion", "Orion", "Orlando", "Osbaldo", "Oscar",
				"Osvaldo", "Oswaldo", "Owen", "Pablo", "Parker", "Patrick", "Paul", "Paxton", "Payton", "Pedro", "Perry", "Peter",
				"Peyton", "Philip", "Phillip", "Phoenix", "Pierce", "Pierre", "Porter", "Pranav", "Preston", "Prince", "Quentin",
				"Quincy", "Quinn", "Quinten", "Quintin", "Quinton", "Rafael", "Rahul", "Ralph", "Ramiro", "Ramon", "Randall",
				"Randy", "Raphael", "Rashad", "Raul", "Ray", "Raymond", "Raymundo", "Reagan", "Reece", "Reed", "Reese", "Reginald",
				"Reid", "Reilly", "Remington", "Rene", "Reuben", "Rey", "Reynaldo", "Rhett", "Ricardo", "Richard", "Rickey",
				"Ricky", "Rigoberto", "Riley", "River", "Robert", "Roberto", "Rocco", "Roderick", "Rodney", "Rodolfo", "Rodrigo",
				"Rogelio", "Roger", "Rohan", "Roland", "Rolando", "Roman", "Romeo", "Ronald", "Ronaldo", "Ronan", "Ronnie", "Rory",
				"Ross", "Rowan", "Roy", "Ruben", "Rudy", "Russell", "Ryan", "Ryder", "Rylan", "Rylee", "Ryley", "Sabastian", "Sage"
				, "Salvador", "Salvatore", "Sam", "Samir", "Sammy", "Samson", "Samuel", "Santiago", "Santino", "Santos", "Saul",
				"Savion", "Sawyer", "Scott", "Seamus", "Sean", "Sebastian", "Semaj", "Sergio", "Seth", "Shamar", "Shane", "Shannon"
				, "Shaun", "Shawn", "Shayne", "Shea", "Sheldon", "Shemar", "Sidney", "Silas", "Simon", "Sincere", "Skylar",
				"Skyler", "Solomon", "Sonny", "Spencer", "Stanley", "Stefan", "Stephan", "Stephen", "Stephon", "Sterling", "Steve",
				"Steven", "Stone", "Stuart", "Sullivan", "Syed", "Talon", "Tanner", "Tariq", "Tate", "Tavion", "Taylor", "Terrance"
				, "Terrell", "Terrence", "Terry", "Thaddeus", "Theodore", "Thomas", "Timothy", "Titus", "Tobias", "Toby", "Todd",
				"Tomas", "Tommy", "Tony", "Trace", "Travis", "Travon", "Tre", "Trent", "Trenton", "Trever", "Trevin", "Trevion",
				"Trevon", "Trevor", "Trey", "Treyton", "Tristan", "Tristen", "Tristian", "Tristin", "Triston", "Troy", "Trystan",
				"Tucker", "Turner", "Ty", "Tyler", "Tylor", "Tyree", "Tyrell", "Tyrese", "Tyrone", "Tyshawn", "Tyson", "Ulises",
				"Ulysses", "Uriel", "Vance", "Vaughn", "Vernon", "Vicente", "Victor", "Vincent", "Wade", "Walker", "Walter",
				"Warren", "Waylon", "Wayne", "Wesley", "Weston", "Will", "William", "Willie", "Wilson", "Winston", "Wyatt",
				"Xander", "Xavier", "Xzavier", "Yadiel", "Yahir", "Yosef", "Zachariah", "Zachary", "Zachery", "Zack", "Zackary",
				"Zackery", "Zain", "Zaire", "Zakary", "Zander", "Zane", "Zavier", "Zayne", "Zechariah"
			};

        public static string[] EnglishFemaleNames = new[] { 
				"Aaliyah", "Abagail", "Abbey", "Abbie", "Abbigail", "Abby", "Abigail", "Abigale", "Abigayle", "Abril",
				"Addison", "Adeline", "Adriana", "Adrianna", "Adrienne", "Aileen", "Aimee", "Ainsley", "Aisha", "Aiyana",
				"Aja", "Akira", "Alaina", "Alana", "Alanis", "Alanna", "Alayna", "Aleah", "Alejandra", "Alena",
				"Alessandra", "Alex", "Alexa", "Alexandra", "Alexandrea", "Alexandria", "Alexia", "Alexis", "Alexus", "Alexys",
				"Alia", "Alice", "Alicia", "Alina", "Alisa", "Alisha", "Alison", "Alissa", "Alivia", "Aliya",
				"Aliyah", "Aliza", "Alize", "Allie", "Allison", "Ally", "Allyson", "Allyssa", "Alma", "Alondra",
				"Alycia", "Alysa", "Alysha", "Alyson", "Alyssa", "Amanda", "Amani", "Amara", "Amari", "Amaya",
				"Amber", "Amelia", "America", "Amina", "Amira", "Amy", "Amya", "Ana", "Anabel", "Anahi",
				"Anais", "Anastasia", "Anaya", "Andrea", "Angel", "Angela", "Angelica", "Angelina", "Angeline", "Angelique",
				"Angie", "Anika", "Anissa", "Anita", "Aniya", "Aniyah", "Anjali", "Ann", "Anna", "Annabel",
				"Annabella", "Annabelle", "Annalise", "Anne", "Annette", "Annie", "Annika", "Ansley", "Antonia", "Anya",
				"April", "Araceli", "Aracely", "Arely", "Aria", "Ariana", "Arianna", "Ariel", "Arielle", "Arlene",
				"Armani", "Aryanna", "Ashanti", "Ashlee", "Ashleigh", "Ashley", "Ashly", "Ashlyn", "Ashlynn", "Ashton",
				"Ashtyn", "Asia", "Aspen", "Astrid", "Athena", "Aubree", "Aubrey", "Aubrie", "Audrey", "Aurora",
				"Autumn", "Ava", "Avery", "Ayana", "Ayanna", "Ayla", "Aylin", "Baby", "Bailee", "Bailey",
				"Barbara", "Baylee", "Beatriz", "Belen", "Bella", "Berenice", "Bethany", "Bianca", "Blanca", "Brandi",
				"Brandy", "Breana", "Breanna", "Brenda", "Brenna", "Breonna", "Bria", "Briana", "Brianna", "Brianne",
				"Bridget", "Brielle", "Brionna", "Brisa", "Britney", "Brittany", "Brittney", "Brooke", "Brooklyn", "Brooklynn",
				"Bryana", "Bryanna", "Brynn", "Cadence", "Caitlin", "Caitlyn", "Cali", "Calista", "Callie", "Cameron",
				"Camila", "Camille", "Camryn", "Candace", "Candice", "Cara", "Carina", "Carissa", "Carla", "Carlee",
				"Carley", "Carli", "Carlie", "Carly", "Carmen", "Carol", "Carolina", "Caroline", "Carolyn", "Carrie",
				"Carson", "Casandra", "Casey", "Cassandra", "Cassidy", "Cassie", "Catalina", "Catherine", "Cayla", "Cecelia",
				"Cecilia", "Celeste", "Celia", "Celina", "Celine", "Chana", "Charity", "Charlotte", "Chasity", "Chaya",
				"Chelsea", "Chelsey", "Cheyanne", "Cheyenne", "Chloe", "Christa", "Christian", "Christiana", "Christina", "Christine",
				"Christy", "Ciara", "Ciera", "Cierra", "Cindy", "Citlali", "Claire", "Clara", "Clare", "Clarissa	Claudia",
				"Colleen", "Cora", "Corinne", "Courtney", "Cristal", "Cristina", "Crystal", "Cynthia", "Daisy", "Dakota",
				"Dalia", "Damaris", "Dana", "Dania", "Daniela", "Daniella", "Danielle", "Danna", "Daphne", "Darby",
				"Darlene", "Dasia", "Dayana", "Deanna", "Deasia", "Deborah", "Deja", "Delaney", "Delilah", "Denise",
				"Denisse", "Desirae", "Desiree", "Destinee", "Destiney", "Destini", "Destiny", "Devin", "Devon", "Devyn",
				"Diamond", "Diana", "Diane", "Dianna", "Dominique", "Donna", "Dorothy", "Dulce", "Dylan", "Ebony",
				"Eden", "Edith", "Eileen", "Elaina", "Elaine", "Eleanor", "Elena", "Eliana", "Elisa", "Elisabeth",
				"Elise", "Elissa", "Eliza", "Elizabeth", "Ella", "Elle", "Ellen", "Ellie", "Elsa", "Elyse",
				"Elyssa", "Emely", "Emerson", "Emilee", "Emilia", "Emilie", "Emily", "Emma", "Emmalee", "Erica",
				"Ericka", "Erika", "Erin", "Esmeralda", "Esperanza", "Essence", "Estefani", "Estefania", "Estefany", "Esther",
				"Estrella", "Eva", "Eve", "Evelin", "Evelyn", "Fabiola", "Faith", "Fatima", "Felicia", "Felicity",
				"Fernanda", "Fiona", "Frances", "Francesca", "Frida", "Gabriela", "Gabriella", "Gabrielle", "Galilea", "Genesis",
				"Genevieve", "Georgia", "Gia", "Giana", "Gianna", "Gillian", "Gina", "Giovanna", "Giselle", "Gisselle",
				"Gloria", "Grace", "Gracie", "Graciela", "Greta", "Gretchen", "Guadalupe", "Gwendolyn", "Hadley", "Hailee",
				"Hailey", "Hailie", "Haleigh", "Haley", "Halie", "Halle", "Hallie", "Hana", "Hanna", "Hannah",
				"Harley", "Harmony", "Haven", "Hayden", "Haylee", "Hayley", "Haylie", "Hazel", "Heather", "Heaven",
				"Heidi", "Helen", "Helena", "Holly", "Hope", "Hunter", "Iliana", "Imani", "India", "Ingrid",
				"Ireland", "Irene", "Iris", "Isabel", "Isabela", "Isabell", "Isabella", "Isabelle", "Isis", "Itzel",
				"Ivy", "Iyana", "Iyanna", "Izabella", "Jacey", "Jackeline", "Jaclyn", "Jacqueline", "Jacquelyn", "Jada",
				"Jade", "Jaden", "Jadyn", "Jaelyn", "Jaida", "Jaiden", "Jaidyn", "Jailyn", "Jaime", "Jakayla",
				"Jaliyah", "Jalyn", "Jalynn", "Jamie", "Jamya", "Jana", "Janae", "Jane", "Janelle", "Janessa",
				"Janet", "Janice", "Janie", "Janiya", "Jaquelin", "Jaqueline", "Jasmin", "Jasmine", "Jasmyn", "Jaycee",
				"Jayda", "Jayde", "Jayden", "Jayla", "Jaylene", "Jaylin", "Jaylyn", "Jaylynn", "Jazlyn", "Jazmin",
				"Jazmine", "Jazmyn", "Jazmyne", "Jeanette", "Jena", "Jenifer", "Jenna", "Jennifer", "Jenny", "Jessica",
				"Jessie", "Jewel", "Jillian", "Jimena", "Joana", "Joanna", "Jocelyn", "Joelle", "Johana", "Johanna",
				"Jolie", "Jordan", "Jordyn", "Joselyn", "Josephine", "Josie", "Joslyn", "Journey", "Joy 	Joyce", "Judith",
				"Julia", "Juliana", "Julianna", "Julianne", "Julie", "Juliet", "Juliette", "Julissa", "Justice", "Justine",
				"Kacie", "Kaela", "Kaelyn", "Kaia", "Kaila", "Kailee", "Kailey", "Kailyn", "Kaitlin", "Kaitlyn",
				"Kaitlynn", "Kaiya", "Kaleigh", "Kaley", "Kali", "Kaliyah", "Kallie", "Kalyn", "Kamryn", "Kara",
				"Karen", "Kari", "Karina", "Karissa", "Karla", "Karlee", "Karley", "Karli", "Karlie", "Karly",
				"Kasandra", "Kasey", "Kassandra", "Kassidy", "Katarina", "Kate", "Katelin", "Katelyn", "Katelynn", "Katerina",
				"Katharine", "Katherine", "Kathleen", "Kathryn", "Kathy", "Katie", "Katlyn", "Katrina", "Katy", "Kaya",
				"Kayla", "Kaylah", "Kaylee", "Kayleigh", "Kayley", "Kayli", "Kaylie", "Kaylin", "Kaylyn", "Kaylynn",
				"Keeley", "Keely", "Keila", "Keira", "Kelli", "Kellie", "Kelly", "Kelsey", "Kelsi", "Kelsie",
				"Kendal", "Kendall", "Kendra", "Kenia", "Kenna", "Kennedi", "Kennedy", "Kenya", "Kenzie", "Keyla",
				"Kiana", "Kianna", "Kiara", "Kiera", "Kierra", "Kiersten", "Kiley", "Kimberly", "Kira", "Kirsten",
				"Kiya", "Kourtney", "Krista", "Kristen", "Kristin", "Kristina", "Krystal", "Kya", "Kyla", "Kylee",
				"Kyleigh", "Kylie", "Kyra", "Lacey", "Laila", "Lana", "Laney", "Lara", "Larissa", "Laura",
				"Laurel", "Lauren", "Lauryn", "Layla", "Lea", "Leah", "Leanna", "Leila", "Leilani", "Lena",
				"Lesley", "Leslie", "Lesly", "Leticia", "Lexi", "Lexie", "Lexus", "Lia", "Liana", "Libby",
				"Liberty", "Lila", "Lilian", "Liliana", "Lillian", "Lilliana", "Lillie", "Lilly", "Lily", "Lina",
				"Linda", "Lindsay", "Lindsey", "Lisa", "Lisbeth", "Litzy", "Lizbeth", "Lizeth", "Lizette", "Logan",
				"Lola", "London", "Loren", "Lorena", "Lucia", "Lucy", "Luisa", "Luz", "Lydia", "Lyndsey",
				"Lyric", "Macey", "Maci", "Macie", "Mackenzie", "Macy", "Madalyn", "Madalynn", "Maddison", "Madeleine",
				"Madeline", "Madelyn", "Madelynn", "Madilyn", "Madisen", "Madison", "Madisyn", "Madyson", "Maegan", "Maeve",
				"Magdalena", "Maggie", "Maia", "Makaila", "Makayla", "Makena", "Makenna", "Makenzie", "Maleah", "Malia",
				"Maliyah", "Mallory", "Mandy", "Mara", "Marcella", "Margaret", "Margarita", "Maria", "Mariah", "Mariam",
				"Mariana", "Marianna", "Maribel", "Marie", "Mariela", "Marilyn", "Marina", "Marisa", "Marisol", "Marissa",
				"Maritza", "Marlee", "Marlene", "Marley", "Martha", "Mary", "Maryam", "Mattie", "Maura", "Maya",
				"Mayra", "Mckayla", "Mckenna", "Mckenzie", "Meadow", "Meagan", "Meaghan", "Megan", "Meghan", "Melanie",
				"Melany", "Melina", "Melinda", "Melissa", "Melody", "Mercedes", "Meredith", "Mia 	Miah", "Micaela", "Micah",
				"Michaela", "Michelle", "Mikaela", "Mikayla", "Mina", "Miracle", "Miranda", "Mireya", "Miriam", "Miya",
				"Mollie", "Molly", "Monica", "Monique", "Monserrat", "Montana", "Morgan", "Moriah", "Mya", "Myah",
				"Myra", "Nadia", "Nancy", "Naomi", "Natalia", "Natalie", "Nataly", "Natasha", "Nathalie", "Nayeli",
				"Nevaeh", "Nia", "Nichole", "Nicole", "Nicolette", "Nikki", "Nina", "Noelia", "Noelle", "Noemi",
				"Nora", "Norma", "Nya", "Nyah", "Nyasia", "Nyla", "Odalys", "Olivia", "Paige", "Paloma",
				"Pamela", "Paola", "Paris", "Parker", "Patience", "Patricia", "Paula", "Paulina", "Payton", "Penelope",
				"Perla", "Peyton", "Phoebe", "Piper", "Precious", "Presley", "Princess", "Priscila", "Priscilla", "Quinn",
				"Rachael", "Rachel", "Rachelle", "Raegan", "Raina", "Raquel", "Raven", "Rayna", "Reagan", "Reanna",
				"Rebeca", "Rebecca", "Rebekah", "Reese", "Regan", "Regina", "Reilly", "Reina", "Renee", "Reyna",
				"Rhiannon", "Rianna", "Riley", "Rita", "Riya", "Robin", "Robyn", "Rocio", "Rosa", "Rose",
				"Rosemary", "Roxana", "Ruby", "Ruth", "Ryan", "Ryann", "Rylee", "Ryleigh", "Rylie", "Sabrina",
				"Sade", "Sadie", "Sage", "Saige", "Sally", "Salma", "Samantha", "Samara", "Samira", "Sandra",
				"Sandy", "Sara", "Sarah", "Sarahi", "Sarai", "Sarina", "Sasha", "Savana", "Savanah", "Savanna",
				"Savannah", "Scarlett", "Selena", "Selina", "Serena", "Serenity", "Shakira", "Shania", "Shaniya", "Shannon",
				"Sharon", "Shawna", "Shayla", "Shaylee", "Shayna", "Shea", "Sheila", "Shelby", "Shirley", "Shreya",
				"Shyann", "Shyanne", "Sidney", "Sienna", "Sierra", "Simone", "Sky", "Skye", "Skyla", "Skylar",
				"Skyler", "Sofia", "Sonia", "Sonya", "Sophia", "Sophie", "Stacey", "Stacy", "Stella", "Stephanie",
				"Stephany", "Summer", "Susan", "Susana", "Sydnee", "Sydney", "Sydni", "Sydnie", "Sylvia", "Tabitha",
				"Talia", "Taliyah", "Tamara", "Tamia", "Tania", "Taniya", "Tanya", "Tara", "Taryn", "Tatiana",
				"Tatum", "Tatyana", "Taya", "Tayler", "Taylor", "Teagan", "Teresa", "Tess", "Tessa", "Thalia",
				"Theresa", "Tia", "Tiana", "Tianna", "Tiara", "Tierra", "Tiffany", "Tina", "Toni", "Tori",
				"Tracy", "Trinity", "Trista", "Tristan", "Tyler", "Tyra", "Unique", "Valentina", "Valeria", "Valerie",
				"Vanesa", "Vanessa", "Veronica", "Victoria", "Violet", "Virginia", "Vivian", "Viviana", "Wendy", "Whitney",
				"Willow", "Ximena", "Xiomara", "Yadira", "Yasmin", "Yasmine", "Yazmin", "Yesenia", "Yessenia", "Yolanda",
				"Yuliana", "Yvette", "Yvonne", "Zaria", "Zoe", "Zoey", "Zoie"
				};

        public static string[] StreetNames = new[]
			{
				"High Street", "Station Road", "Main Street", "Park Road", "Church Road", "Church Street", "London Road", "Victoria Road", "Green Lane", 
				"Manor Road", "Church Lane", "Park Avenue", "The Avenue", "The Crescent", "Queens Road", "New Road", "Grange Road", "Kings Road", 
				"Kingsway", "Windsor Road", "Highfield Road", "Mill Lane", "Alexander Road", "York Road", "St. John's Road", 
				"Main Road", "Broadway", "King Street", "The Green", "Springfield Road", "George Street", "Park Lane", "Victoria Street", "Albert Road", 
				"Queensway", "New Street", "Queen Street", "West Street", "North Street", "Manchester Road", "The Grove", "Richmond Road", "Grove road", 
				"South Street", "School Lane", "The Drive", "North Road", "Stanley Road", "Chester Road", "Mill Road",
			};

        public static string[] StateNames = new[]
			{
				"New Brunswick", "Gr.", "Zeeland", "Devon", "Groningen", "Wyoming", "Saskatchewan", "Powys", "KY", 
				"OKI", "TYR", "Newfoundland and Labrador", "Nova Scotia", "Friesland", "CWD", "Utrecht", "WAT", "Iowa", 
				"New Brunswick", "Groningen", "FIF", "BC", "Z.-H.", "NT", "HUM", "Co. Kildare", "MS", 
				"Arkansas", "PEM", "MS", "SK", "Mississippi", "NS", "LOG", "Tennessee", "New Mexico", 
				"New Brunswick", "NM", "Co. Galway", "ON", "Minnesota", "WEX", "Flevoland", "QC", "Gr.", 
				"Alberta", "POW", "NH", "Limburg", "YT", "OH", "Zuid Holland", "N.-Br.", "MAY", 
				"NS", "Groningen", "West Sussex", "South Carolina", "Nottinghamshire", "Minnesota", "Z.-H.", "NT", "AK", 
				"Cardiganshire", "ON", "NY", "Derbyshire", "Minnesota", "Minnesota", "Nunavut", "South Dakota", "NY", 
				"AB", "North Yorkshire", "Georgia", "Utrecht", "North Carolina", "Dumfries and Galloway", "Alberta", "Westmorland", "SC", 
				"Hawaii", "MB", "BKM", "Georgia", "South Dakota", "Gr.", "QC", "Michigan", "Noord Brabant", 
				"LOG", "Gelderland", "Lanarkshire", "Fl.", "Fl.", "BC", "Arkansas", "YT", "Quebec", 
				"Gelderland", "Montana", "MN", "Gloucestershire", "U.", "RAD", "KY", "MT", "Noord Brabant", 
				"HI", "SK", "Utrecht", "OH", "NU", "Derbyshire", "NS", "PE", "N.-H.", 
				"Yukon", "Ov.", "Gelderland", "Michigan", "OR", "Zld.", "RI", "South Dakota", "Dr.", 
				"Virginia", "Cleveland", "South Dakota", "STD", "Gr.", "Manitoba", "SK", "Iowa", "N.-Br.", 
				"NS", "Groningen", "Minnesota", "Zuid Holland", "Noord Brabant", "Oxfordshire", "MB", "Rutland", "GSY", 
				"West Yorkshire", "British Columbia", "WRY", "NH", "CAV", "Alderney", "ON", "Northwest Territories", "Stirlingshire", 
				"Arkansas", "Manitoba", "Limburg", "BRE", "Yukon", "DGY", "Louisiana", "Quebec", "TX", 
				"North Carolina", "ANS", "Kansas", "Northwest Territories", "Friesland", "Washington", "Zuid Holland", "Co. Mayo", "AB", 
				"New Hampshire", "Gelderland", "Caernarvonshire", "North Dakota", "Zuid Holland", "NS", "Saskatchewan", "RFW", "GLS", 
				"HUN", "YT", "North Carolina", "Saskatchewan", "Newfoundland and Labrador", "Prince Edward Island", "North Riding of Yorkshire", "Nunavut", "VT", 
				"Zuid Holland", "Buckinghamshire", "Fr.", "Nunavut", "NJ", "U.", "Clwyd", "ON", "MLN", 
				"LIM", "Noord Brabant",
			};
        public static string[] CityNames = new[]
			{
				"Beverly Hills", "Woodward", "Yigo", "Centennial", "Catskill", "Culver City", "Bayamon", "Springfield", "Alhambra", 
				"Avalon", "Lockport", "Effingham", "Sacramento", "Radford", "Independence", "Farmer City", "Muskegon", "Hope", 
				"Clarksburg", "Overland Park", "Topeka", "Rawlins", "Anaheim", "Sioux Falls", "Kankakee", "Akron", "Burlingame", 
				"Vacaville", "Tustin", "Houston", "Baytown", "Yonkers", "Hidden Hills", "Barrow", "Cary", "Mandan", 
				"Elko", "Covington", "Hialeah", "Fairbanks", "Easthampton", "Texas City", "McAllen", "Annapolis", "New London", 
				"West Hartford", "Fresno", "Dana Point", "Grass Valley", "Lawton", "Terre Haute", "Duquesne", "Derby", "Tustin", 
				"Beverly", "Overland Park", "Vacaville", "Cranston", "Moreno Valley", "Seattle", "Natchitoches", "Ocean City", "Schaumburg", 
				"Yonkers", "Plymouth", "West Jordan", "Macon", "Wilmington", "Chicago Heights", "Tupelo", "Eden Prairie", "Irvine", 
				"San Jose", "Sandy Springs", "Kansas City", "Indianapolis", "Riverton", "Rock Springs", "Newport", "Bozeman", "Durango", 
				"Lowell", "Grass Valley", "Lakeland", "Jackson", "Jonesboro", "Centennial", "Glendale", "North Las Vegas", "Rancho Cordova", 
				"Chester", "Durant", "Hickory", "Williamsburg", "DuBois", "Bloomington", "Vineland", "Rolling Hills", "Bell", 
				"Blacksburg", "Oro Valley", "Gary", "Long Beach", "La Crosse", "Cambridge", "West Allis", "Sugar Land", "Temecula", 
				"Plantation", "Lincoln", "Woodward", "West Haven", "Alexandria", "Gilette", "Johnson City", "Joliet", "Rutland", 
				"Monterey", "Rutland", "Independence", "Fredericksburg", "Villa Park", "Omaha", "Phoenix", "Tucson", "Trenton", 
				"San Francisco", "Shreveport", "Orange", "Stamford", "Vernon", "Gatlinburg", "Columbus", "Joliet", "San Francisco", 
				"Fulton", "Hialeah", "Oneonta", "Cortland", "Birmingham", "Cheyenne", "Colorado Springs", "Auburn Hills", "Citrus Heights", 
				"Macon", "Lake Forest", "Narragansett", "Farmer City", "Bangor", "Hattiesburg", "Pine Bluff", "Hannibal", "Forrest City", 
				"Gulfport", "Bayamon", "Council Bluffs", "Edina", "Springfield", "Aspen", "Vancouver", "Port Orford", "West Warwick", 
				"East Rutherford", "Tulsa", "Hope", "Attleboro", "Dayton", "Salem", "Pittston", "Jacksonville", "Suffolk", 
				"Starkville", "Nenana", "Utica", "Arvada", "Canandaigua", "Hollister", "Norman", "Jackson", "Kenner", 
				"Albuquerque", "Burlingame", "Savannah", "Fredericksburg", "Lansing", "Tallahassee", "Lubbock", "Redding", "Rancho Cordova", 
				"Meridian", "Opelousas", "Kennewick", "Lubbock", "Vacaville", "Greenfield", "Union City", "West Lafayette", "Monongahela", 
				"Aberdeen", "St Albans", "Birmingham", "Bath", "Blackburn", "Bradford", "Bournemouth", "Bolton", "Brighton", 
				"Bromley", "Bristol", "Carlisle", "Cambridge", "Cardiff", "Chester", "Chelmsford", "Colchester", "Croydon", 
				"Canterbury", "Coventry", "Crewe", "Dartford", "Dundee", "Derby", "Dumfries", "Durham", "Darlington", 
				"Doncaster", "Dorchester", "Dudley", "London", "Edinburgh", "Enfield", "Exeter", "Falkirk", "Blackpool", 
				"Glasgow", "Gloucester", "Guildford", "Harrow", "Huddersfield", "Harrogate", "Hemel Hempstead", "Hereford", "Outer Hebrides", 
				"Hull", "Halifax", "Ilford", "Ipswich", "Inverness", "Kilmarnock", "Kingston upon Thames", "Kirkwall", "Kirkcaldy", 
				"Liverpool", "Lancaster", "Llandrindod Wells", "Leicester", "Llandudno", "Lincoln", "Leeds", "Luton", "Manchester", 
				"Rochester", "Milton Keynes", "Motherwell", "Newcastle upon Tyne", "Nottingham", "Northampton", "Newport", "Norwich", "Oldham", 
				"Oxford", "Paisley", "Peterborough", "Perth", "Plymouth", "Portsmouth", "Preston", "Reading", "Redhill", 
				"Romford", "Sheffield", "Swansea", "Stevenage", "Stockport", "Slough", "Sutton", "Swindon", "Southampton", 
				"Salisbury", "Sunderland", "Southend-on-Sea", "Stoke-on-Trent", "Shrewsbury", "Taunton", "Galashiels", "Telford", "Tonbridge", 
				"Torquay", "Truro", "Cleveland", "Twickenham", "Southall", "Warrington", "Watford", "Wakefield", "Wigan", 
				"Worcester", "Walsall", "Wolverhampton", "York", "Lerwick", 
				"Pendleton", "Oneonta", 
			};

        public static string[] CountryNames = new[]
			{
				"Saint Lucia", "Switzerland", "France", "Slovakia", "Swaziland", "Estonia", "Nicaragua", "Malaysia", "United Arab Emirates", 
				"Indonesia", "Namibia", "Austria", "Wallis and Futuna", "Cambodia", "Marshall Islands", "Netherlands Antilles", "Bolivia", "Egypt", 
				"Saint Lucia", "Poland", "Uganda", "Turkmenistan", "Saint Kitts and Nevis", "Mauritania", "Greenland", "Australia", "Gibraltar", 
				"American Samoa", "Belgium", "Finland", "Tunisia", "Sudan", "Cameroon", "Comoros", "Niger", "Albania", 
				"Kyrgyzstan", "Falkland Islands (Malvinas)", "Maldives", "India", "Paraguay", "Burundi", "Korea", "Palau", "Finland", 
				"Algeria", "France", "Hungary", "Eritrea", "Australia", "Bosnia and Herzegovina", "Cocos (Keeling) Islands", "Viet Nam", "Switzerland", 
				"Saint Lucia", "French Southern Territories", "Turkmenistan", "Belgium", "Georgia", "Zimbabwe", "Heard Island and Mcdonald Islands", "Cyprus", "Sudan", 
				"Mali", "Israel", "Latvia", "Malta", "Tunisia", "Saint Vincent and The Grenadines", "Barbados", "French Southern Territories", "Kazakhstan", 
				"Cyprus", "Saudi Arabia", "Brazil", "Macedonia", "Seychelles", "Ireland", "Tuvalu", "Austria", "Latvia", 
				"Antarctica", "Ecuador", "Jordan", "Togo", "Afghanistan", "Saint Vincent and The Grenadines", "Singapore", "Belize", "Belarus", 
				"Singapore", "Kuwait", "French Southern Territories", "New Zealand", "Algeria", "Saint Helena", "Bosnia and Herzegovina", "Martinique", "Nigeria", 
				"Tanzania, United Republic of", "New Zealand", "Vanuatu", "Georgia", "Switzerland", "Russian Federation", "Jordan", "Haiti", "Madagascar", 
				"Tanzania, United Republic of", "France", "Monaco", "Guadeloupe", "Ghana", "Sudan", "Singapore", "Croatia", "Sudan", 
				"Mauritius", "Andorra", "Uganda", "Nigeria", "Somalia", "Hungary", "Uzbekistan", "Kazakhstan", "Indonesia", 
				"Peru", "Mayotte", "Viet Nam", "Greenland", "Jamaica", "Myanmar", "French Guiana", "Saint Vincent and The Grenadines", "Korea", 
				"Argentina", "Cook Islands", "Suriname", "Mayotte", "Belgium", "Burkina Faso", "Burundi", "Jamaica", "Kuwait", 
				"Western Sahara", "Djibouti", "Paraguay", "South Africa", "Singapore", "Portugal", "Saint Kitts and Nevis", "Japan", "Malawi", 
				"Burundi", "Hungary", "Wallis and Futuna", "Mauritania", "Bermuda", "Malta", "Malawi", "Japan", "Wallis and Futuna", 
				"British Indian Ocean Territory", "Saint Kitts and Nevis", "Saint Helena", "Montserrat", "Serbia and Montenegro", "Uzbekistan", "Israel", "Grenada", "Argentina", 
				"Micronesia", "Libyan Arab Jamahiriya", "Latvia", "Armenia", "Latvia", "Puerto Rico", "Rwanda", "France", "Macao", 
				"Kazakhstan", "Aruba", "Vanuatu", "Wallis and Futuna", "Cocos (Keeling) Islands", "Guam", "Vanuatu", "Saint Lucia", "Kiribati", 
				"Lithuania", "Ethiopia", "Trinidad and Tobago", "Lesotho", "Iceland", "Nigeria", "Czech Republic", "Zimbabwe", "Israel", 
				"Christmas Island", "Ireland"
			};

        public static string[] CompanySuffixes = new[]
			{
				"Metals", "Sport","Software", "Manufacturing", "AG", "BVBA", "Ltd.","Inc.","Finance","Insurances","Clothing", "Lingerie", "Outerwear", "Sprl",  "International",
				"Couture","Wear", "Worldwide", "Productions", "Sportswear", "Shirts", "Corp.", "XL", "NV", "CVBA", "AAT", "Pty.", "SE", "AD", "SD", "KDA", "E.I.R.L.", "Hardware",
				"Language Services", "Services", "Logistics", "Constructions"

			};
        public static string[] CompanyNames = new[]
			{
				"Ecolas", "Fetion", "Nautus", "Catat", "Kiloth", "Presite", "Digiving", "Magnux",
				"Harth", "Nons", "Monaros", "Extiline", "Maesonet", "Damorph", "Zencer", "Weros", "Mitics", "Portable", "Getaur",
				"GetronX", "Escact", "Herogue", "Mephos+", "Mrotort", "Homolyse", "Digtra", "Orangox", "Oraculepa", "Ramalynt",
				"Macrogite", "Epilica", "Ramaweme ", "Figue", "Easthama", "Xephason", "Zosh", "Tensite", "Nyrast", "Pepa",
				"Unicede", "Arcadic", "Neuronct", "Subster", "Fountace", "Analonsion", "Divilate", "Thornunt", "Goethax", "Gangure"
				, "Saturnome", "Nix", "PaTact", "In@Teginet", "Bylite", "Audi Gong", "Xenom", "Xerolum", "Pyrost", "Xerodict",
				"Unistre", "Gnomic", "Gracio", "Xylere",
			};
        public static string[] FamilyNames = new[]
			{
				"Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor", "Anderson",
				"Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson", "Clark",
				"Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King", "Wright", "Lopez", "Hill",
				"Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter", "Mitchell", "Perez", "Roberts", "Turner",
				"Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins", "Stewart", "Sanchez", "Morris", "Rogers  ", "Reed"
				, "Coook", "Morgan", "Bell", "Murphy", "Bailey", "Rivera", "Cooper", "Richardson", "Cox  ", "Howard", "Ward",
				"Torres", "Peterson", "Gray", "Ramirez", "James", "Watson", "Brooks", "Kelly", "Sanders", "Price", "Bennett",
				"Wood", "Barnes", "Ross", "Henderson", "Coleman", "Jenkins", "Perry", "Powell", "Long", "Patterson", "Hughes",
				"Flores", "Washington", "Butler", "Simmons", "Foster", "Gonzales", "Bryant ", "Alexander", "ussell", "Griffin",
				"Diaz", "Hayes",
				"Smih", "Jones", "Williams", "Brown", "Taylor", "Davies", "Wilson", "Evans", "Thomas", 
				"Johnson", "Roberts", "Walker", "Wright", "Robinson", "Thompson", "White", "Hughes", "Edwards    ", 
				"Green", "Hall", "Wood", "Harris", "Lewis", "Martin", "Jackson", "Clarke", "Clark", 
				"Turner", "Hill", "Scott", "Cooper", "Morris", "Ward", "Moore", "King", "Watson", 
				"Baker", "Harrison", "Morgan", "Patel", "Young", "Allen", "Mitchell", "James", "Anderson", 
				"Phillips", "Lee", "Bell", "Parker", "Davis", "Angelo", "Boulstridge", "Bungard", "Bursnell", 
				"Cabrera", "Chaisty", "Clayworth", "Denial", "Dissanayake", "Domville", "Dua", "Edeson", "Garrott", 
				"Gaspar", "Gauge", "Gelson", "Happer", "Hawa", "Helling", "Hollingberry", "Howsham", "Husher", 
				"Huth", "Khambaita", "Kinlan", "Le Feuvre", "Leatherby", "Lowsley", "Mardling", "Mc Cart", "McCalman", 
				"McKiddie", "McQuillen", "Meath", "Mustow", "Nana", "Pepall", "Perdue", "Ravensdale", "Rukin", 
				"Selvaratnam", "Shelsher", "Silsbury", "Southway", "Upadhyad", "Valji", "Virji", "Wadd", "Weild", 
				"Witte", 
			};

		public static string MedicalSymptoms = @"
					Baseline pulmonary questionnaires were completed by 907 HIV-infected and 989 HIV-uninfected participants in the MACS cohort and by 1405 HIV-infected 
					and 571 HIV-uninfected participants in the WIHS cohort. In MACS, dyspnea, cough, wheezing, sleep apnea, and incident chronic obstructive pulmonary disease (COPD)
					were more common in HIV-infected participants. In WIHS, wheezing and sleep apnea were more common in HIV-infected participants. Smoking (MACS and WIHS) and greater 
					body mass index (WIHS) were associated with more respiratory symptoms and diagnoses. While sputum studies, bronchoscopies, and chest computed tomography scans were 
					more likely to be performed in HIV-infected participants, pulmonary function tests were no more common in HIV-infected individuals. Respiratory symptoms in HIV-infected
					individuals were associated with history of pneumonia, cardiovascular disease, or use of HAART. A diagnosis of asthma or COPD was associated with previous pneumonia.
					Fraction of exhaled nitric oxide (Feno) has been proposed to be a noninvasive marker of airway inflammation. Levels of Feno are elevated in subjects with asthma1and 
					atopy,2and have been positively associated with eosinophils in BAL,3with eosinophils in bronchial biopsy specimens,4and with eosinophils in induced sputum.5 However,
					in addition to atopy, asthma, and respiratory symptoms, there are several other determinants of Feno. Their importance and interactions as determinant of Feno are 
					incompletely understood.
					Perceived control of asthma, which is defined as individuals’ perceptions of their ability to deal with asthma and its exacerbations, is a psychological factor that may have an important impact on adult asthma outcomes.7 Its effect could theoretically be mediated by self-management behaviors, such as the use of peak flow monitoring or inhaled corticosteroids, that can attenuate the severity of asthma. The perceived control of asthma construct, which is related to the general theories of self-efficacy, locus of control, and learned helplessness, has been associated with asthma severity and quality of life but has not been well studied in association with asthma outcomes like health-care utilization.7 If higher perceived control were associated with improved health outcomes, programs designed to increase perceived control may be useful targets for intervention. In the present study, we examined the impact of perceived control of asthma on a broad array of health outcomes, including asthma severity, health status, and emergency health-care utilization for asthma in a large cohort of adults with severe asthma.
				";
        public static string DocumentTitleSample = @"
				Software services as a problem solver.
				Eliminating risk in China using modern techniques
				The greatest improvements in contemporary analytics.
				WCF services for SOA architecture.
				Sequence analysis using modern algebraic approaches.
				Graph databases for server-side intelligence.
				Great tricks to develop a simple appliance using VMWare on Linux.
				Diverging trends in computing using iPad technology.
				Global economy on a micro-scale using indexing and searching mechanisms.
				Marketing of clinical trials in developing countries.
				Risks and opportunities through middle-management in Western Europe.
				Abstract patterns of JavaScript development.
				Invariant systems with generic parametrization.
				Automatic processing of arbitrary large data sets.
				Data visualization of large datasets.
				Geographic visualization of multi-point data at extreme height.
				Personalization and emotions in computer games.
				Articificial intelligence of pseudo-variant systems in relation to assurance margins.
				Content management system for knowledge transfer.
				Fluid motion graphics in manufacturing.
				Data transformation challenges using Sharepoint.
				Google search infrastructure at low altitude.
				Minor changes in fluid motion of blue gas.
				Algorithmic beauty of natural disasters at low temperature.
				Infinite resource scheduling with constraints.
				Variable scaling of metallic structures through reuse of basic methods.
				Bivalent traffic problems with analytical methods.
				Short term finance management: simple solutions for complex problems.";

    public static string BulgarianSample = @"Остави си лудо-младо
               на бачия рудо стадо -
               яхна коня, слезе в село.
               С китка сминова на чело,
               в път застигна мома мала
               на седенки закъсняла.
               Викна лудо отдалеко:
               Чакай, момне, по-полека,
               заедно да идем двама!
               - Не е дума, ни измама,
               по мен стара майка иди -
               бягай, лудо, ще ни види! -
               Мома бърза и нехае;
               момку сръце бий, играе,
               като в чаша руйно вино.
               Варай, варай, детелино,
               верни ли са думи тие?
               Ясен месец облак крие -
               и да иди, що ще види!
               Стигнаха до равни двори:
               мома сегна да отвори,
               да отвие заключари, -
               лудо сегна и превари -
               от чело й китка дръпна...
               Виком мома вкъщи пръпна:
               - Помощ, помощ, дружки млади,
               лудо китка ми укради!
               Лудо-младо уловете,
               тук при мен го доведете,
               сама ази да посегна,
               да обвържа, да обтегна,
               с руса коса враг неверни -
               да го стрелям с очи черни,
               с тънки вежди да го бия,
               с али устни да го пия,
               нек` се учат луди-млади
               как се росна китка кради
			   Стройна се Калина вие над брегът усамотени,
          кичест Явор клони сплита в нейни вейчици зелени.

          Уморен, под тях на сянка аз отбих се да почина,
          и така ми тайната си повери сама Калина -

          с шепота на плахи листи, шепот сладък и тъжовен:
          Някога си бях девойка аз на тоя свят лъжовен.

          Грееше ме драголюбно ясно слънце от небето,
          ах, но друго слънце мене вече грееше в сърцето!

          И не грееше туй слънце от високо, от далеко -
          грееше ме, гледаше ме от съседски двор напреко.

          Гледаше ме сутрин, вечер Иво там от бели двори
          и тъжовна аз го слушах, той да пее и говори:

          Първо либе, първа севдо, не копней, недей се вайка,
          че каил за нас не стават моя татко, твойта майка.

          Верни думи, верна обич, има ли за тях развала?
          За сърцата що се любят и смъртта не е раздяла.

          Думите му бяха сладки - бяха мъките горчиви -
          писано било та ние да се не сбереме живи...

          Привечер веднъж се връщах с бели менци от чешмата
          и навалица заварих да се трупа от махлата,

          тъкмо пред високи порти, там на Ивовите двори, -
          Клетника - дочух между им да се шушне и говори: -

          право се убол в сърцето - ножчето му още тамо!
          Аз изтръпнах и изпуснах бели медници от рамо.

          През навалицата виком полетях и се промъкнах,
          видях Ива, видях кърви... и не сетих как измъкнах

          остро ножче из сърце му и в сърцето си забих го,
          върху Ива мъртва паднах и в прегръдки си обвих го...

          Нек' сега ни се нарадват, мене майка, нему татко:
          мъртви ние пак се любим и смъртта за нас е сладка!

          Не в черковний двор зариха на любовта двете жъртви -
          тамо ровят само тия, дето истински са мъртви -

          а погребаха ни тука, на брегът край таз долина...
          Той израстна кичест Явор, а до него аз Калина; -

          той ме е прегърнал с клони, аз съм в него вейки свряла,
          За сърцата що се любят и смъртта не е раздяла.

          Дълго аз стоях и слушах, там под сянката унесен,
          и това що чух, изпях го в тази моя тъжна песен.";

        public static string BiologySample = @"No doubt many instincts of very difficult explanation could be opposed to
the theory of natural selection,--cases, in which we cannot see how an
instinct could possibly have originated; cases, in which no intermediate
gradations are known to exist; cases of instinct of apparently such
trifling importance, that they could {236} hardly have been acted on by
natural selection; cases of instincts almost identically the same in
animals so remote in the scale of nature, that we cannot account for their
similarity by inheritance from a common parent, and must therefore believe
that they have been acquired by independent acts of natural selection. I
will not here enter on these several cases, but will confine myself to one
special difficulty, which at first appeared to me insuperable, and actually
fatal to my whole theory. I allude to the neuters or sterile females in
insect-communities: for these neuters often differ widely in instinct and
in structure from both the males and fertile females, and yet, from being
sterile, they cannot propagate their kind.

The subject well deserves to be discussed at great length, but I will here
take only a single case, that of working or sterile ants. How the workers
have been rendered sterile is a difficulty; but not much greater than that
of any other striking modification of structure; for it can be shown that
some insects and other articulate animals in a state of nature occasionally
become sterile; and if such insects had been social, and it had been
profitable to the community that a number should have been annually born
capable of work, but incapable of procreation, I can see no very great
difficulty in this being effected by natural selection. But I must pass
over this preliminary difficulty. The great difficulty lies in the working
ants differing widely from both the males and the fertile females in
structure, as in the shape of the thorax and in being destitute of wings
and sometimes of eyes, and in instinct. As far as instinct alone is
concerned, the prodigious difference in this respect between the workers
and the perfect females, would have been far better exemplified by the
hive-bee. If a working ant or other neuter insect had been an animal {237}
in the ordinary state, I should have unhesitatingly assumed that all its
characters had been slowly acquired through natural selection; namely, by
an individual having been born with some slight profitable modification of
structure, this being inherited by its offspring, which again varied and
were again selected, and so onwards. But with the working ant we have an
insect differing greatly from its parents, yet absolutely sterile; so that
it could never have transmitted successively acquired modifications of
structure or instinct to its progeny. It may well be asked how is it
possible to reconcile this case with the theory of natural selection?

First, let it be remembered that we have innumerable instances, both in our
domestic productions and in those in a state of nature, of all sorts of
differences of structure which have become correlated to certain ages, and
to either sex. We have differences correlated not only to one sex, but to
that short period alone when the reproductive system is active, as in the
nuptial plumage of many birds, and in the hooked jaws of the male salmon.
We have even slight differences in the horns of different breeds of cattle
in relation to an artificially imperfect state of the male sex; for oxen of
certain breeds have longer horns than in other breeds, in comparison with
the horns of the bulls or cows of these same breeds. Hence I can see no
real difficulty in any character having become correlated with the sterile
condition of certain members of insect-communities: the difficulty lies in
understanding how such correlated modifications of structure could have
been slowly accumulated by natural selection.

This difficulty, though appearing insuperable, is lessened, or, as I
believe, disappears, when it is remembered that selection may be applied to
the family, as well as to the individual, and may thus gain the {238}
desired end. Thus, a well-flavoured vegetable is cooked, and the individual
is destroyed; but the horticulturist sows seeds of the same stock, and
confidently expects to get nearly the same variety: breeders of cattle wish
the flesh and fat to be well marbled together; the animal has been
slaughtered, but the breeder goes with confidence to the same family. I
have such faith in the powers of selection, that I do not doubt that a
breed of cattle, always yielding oxen with extraordinarily long horns,
could be slowly formed by carefully watching which individual bulls and
cows, when matched, produced oxen with the longest horns; and yet no one ox
could ever have propagated its kind. Thus I believe it has been with social
insects: a slight modification of structure, or instinct, correlated with
the sterile condition of certain members of the community, has been
advantageous to the community: consequently the fertile males and females
of the same community flourished, and transmitted to their fertile
offspring a tendency to produce sterile members having the same
modification. And I believe that this process has been repeated, until that
prodigious amount of difference between the fertile and sterile females of
the same species has been produced, which we see in many social insects.

But we have not as yet touched on the climax of the difficulty; namely, the
fact that the neuters of several ants differ, not only from the fertile
females and males, but from each other, sometimes to an almost incredible
degree, and are thus divided into two or even three castes. The castes,
moreover, do not generally graduate into each other, but are perfectly well
defined; being as distinct from each other, as are any two species of the
same genus, or rather as any two genera of the same family. Thus in Eciton,
there are working and soldier neuters, with jaws and instincts
extraordinarily {239} different: in Cryptocerus, the workers of one caste
alone carry a wonderful sort of shield on their heads, the use of which is
quite unknown: in the Mexican Myrmecocystus, the workers of one caste never
leave the nest; they are fed by the workers of another caste, and they have
an enormously developed abdomen which secretes a sort of honey, supplying
the place of that excreted by the aphides, or the domestic cattle as they
may be called, which our European ants guard or imprison.";

        public static string SpanishSample = @"Y los demás asintieron, despojándolo repentinamente de la ciudadanía que
le habían atribuído poco antes. El consejero, con una rudeza militar, le
había vuelto la espalda, y tomando la baraja, distribuía cartas. Se
reanudó la partida. Desnoyers, viéndose aislado por este menosprecio
silencioso, sintió deseos de interrumpir el juego con una violencia.
Pero la oculta rodilla seguía aconsejándole la calma y una mano no menos
invisible buscó su diestra, oprimiéndola dulcemente. Esto bastó para que
recobrase la serenidad. La «señora consejera» seguía con ojos fijos la
marcha del juego. El miró también, y una sonrisa maligna contrajo
levemente los extremos de su boca, al mismo tiempo que se decía
mentalmente, á guisa de consuelo: «¡Capitán, capitán!... No sabes lo que
te espera.»

En tierra firme no se habría acercado más á estos hombres; pero la vida
en un trasatlántico, con su inevitable promiscuidad, obliga al olvido.
Al otro día, el consejero y sus amigos fueron en busca de él,
extremando sus amabilidades para borrar todo recuerdo enojoso. Era un
joven «distinguido», pertenecía á una familia rica, y todos ellos
poseían en su país tiendas y otros negocios. De lo único que cuidaron
fué de no mencionar más su origen francés. Era argentino, y todos á coro
se interesaban por la grandeza de su nación y de todas las naciones de
la América del Sur, donde tenían corresponsales y empresas, exagerando
su importancia como si fuesen grandes potencias, comentando con gravedad
los hechos y palabras de sus personajes políticos, dando á entender que
en Alemania no había quien no se preocupase de su porvenir, prediciendo
á todas ellas una gloria futura, reflejo de la del Imperio, siempre que
se mantuviesen bajo la influencia germánica.
Lo mismo que los otros!... La guerra había que pagarla con los bienes
de los vencidos. Era el nuevo sistema alemán; la vuelta saludable á la
guerra de los tiempos remotos: tributos impuestos á las ciudades y
saqueo aislado de las casas. De este modo se vencían las resistencias
del enemigo y la guerra terminaba antes. No debía entristecerse por el
despojo. Sus muebles y alhajas serían vendidos en Alemania. Podía hacer
una reclamación al gobierno francés para que le indemnizase después de
la derrota: sus parientes de Berlín apoyarían la demanda.

Las dos mujeres trasladaron ropas y colchones desde el pabellón al
último piso. El conserje estaba ocupado en calentar el segundo baño de
Su Excelencia. Su esposa lamentaba con gestos desesperados el saqueo del
castillo. ¡Qué de cosas ricas desaparecidas!... Deseosa de salvar los
últimos restos, buscaba al dueño para hacerle denuncias, como si éste
pudiese impedir el robo individual y cauteloso. Los ordenanzas y
escribientes del conde se metían en los bolsillos todo lo que resultaba
fácil de ocultar. Decían sonriendo que eran recuerdos. Luego se aproximó
con aire misterioso para hacerle una nueva revelación. Había visto á un
jefe forzar los cajones donde guardaba la señora la ropa blanca, y cómo
formaba un paquete con las prendas más finas y gran cantidad de blondas.

A pesar de estos halagos, Desnoyers no se presentó con la misma
asiduidad que antes á la hora del _poker_. La consejera se retiraba á su
camarote más pronto que de costumbre. La proximidad de la línea
equinoccial le proporcionaba un sueño irresistible, abandonando á su
esposo, que seguía con los naipes en la mano. Julio, por su parte, tenía
misteriosas ocupaciones que sólo le permitían subir á la cubierta
después de media noche. Con la precipitación de un hombre que desea ser
visto para evitar sospechas, entraba en el fumadero hablando alto y
venía á sentarse junto al marido y sus camaradas. La partida había
terminado, y un derroche de cerveza y gruesos cigarros de Hamburgo
servía para festejar el éxito de los gananciosos. Era la hora de las
expansiones germánicas, de la intimidad entre hombres, de las bromas
lentas y pesadas, de los cuentos subidos de color. El consejero presidía
con toda su grandeza estas diabluras de los amigos, sesudos negociantes
de los puertos anseáticos que gozaban de grandes créditos en el
_Deutsche Bank_ ó tenderos instalados en las repúblicas del Plata con
una familia innumerable. El era un guerrero, un capitán, y al celebrar
cada chiste lento con una risa que hinchaba su robusta cerviz, creía
estar en el vivac entre sus compañeros de armas.";

        public static string English2Sample = @"By no means. You could not make a greater mistake. If they are innocent
it would be a cruel injustice, and if they are guilty we should be
giving up all chance of bringing it home to them. No, no, we will
preserve them upon our list of suspects. Then there is a groom at the
Hall, if I remember right. There are two moorland farmers. There is our
friend Dr. Mortimer, whom I believe to be entirely honest, and there is
his wife, of whom we know nothing. There is this naturalist, Stapleton,
and there is his sister, who is said to be a young lady of attractions.
There is Mr. Frankland, of Lafter Hall, who is also an unknown factor,
and there are one or two other neighbours. These are the folk who must
be your very special study. By no means. You could not make a greater mistake. If they are innocent
it would be a cruel injustice, and if they are guilty we should be
giving up all chance of bringing it home to them. No, no, we will
preserve them upon our list of suspects. Then there is a groom at the
Hall, if I remember right. There are two moorland farmers. There is our
friend Dr. Mortimer, whom I believe to be entirely honest, and there is
his wife, of whom we know nothing. There is this naturalist, Stapleton,
and there is his sister, who is said to be a young lady of attractions.
There is Mr. Frankland, of Lafter Hall, who is also an unknown factor,
and there are one or two other neighbours. These are the folk who must
be your very special study. And yet he lied as he said it, for it chanced that after breakfast I met
Mrs. Barrymore in the long corridor with the sun full upon her face. She
was a large, impassive, heavy-featured woman with a stern set expression
of mouth. But her telltale eyes were red and glanced at me from between
swollen lids. It was she, then, who wept in the night, and if she did so
her husband must know it. Yet he had taken the obvious risk of discovery
in declaring that it was not so. Why had he done this? And why did she
weep so bitterly? Already round this pale-faced, handsome, black-bearded
man there was gathering an atmosphere of mystery and of gloom. It was he
who had been the first to discover the body of Sir Charles, and we had
only his word for all the circumstances which led up to the old man's
death. Was it possible that it was Barrymore, after all, whom we had
seen in the cab in Regent Street? The beard might well have been the
same. The cabman had described a somewhat shorter man, but such an
impression might easily have been erroneous. How could I settle the
point forever? Obviously the first thing to do was to see the Grimpen
postmaster and find whether the test telegram had really been placed in
Barrymore's own hands. Be the answer what it might, I should at least
have something to report to Sherlock Holmes.";

        public static string English1Sample = @"They were married; careful cultivation ripened the talents which nature
had bestowed, and Melesigenes soon surpassed his schoolfellows in every
attainment, and, when older, rivalled his preceptor in wisdom. Phemius
died, leaving him sole heir to his property, and his mother soon followed.
Melesigenes carried on his adopted father's school with great success,
exciting the admiration not only of the inhabitants of Smyrna, but also of
the strangers whom the trade carried on there, especially in the
exportation of corn, attracted to that city. Among these visitors, one
Mentes, from Leucadia, the modern Santa Maura, who evinced a knowledge and
intelligence rarely found in those times, persuaded Melesigenes to close
his school, and accompany him on his travels. He promised not only to pay
his expenses, but to furnish him with a further stipend, urging, that,
While he was yet young, it was fitting that he should see with his own
eyes the countries and cities which might hereafter be the subjects of his
discourses. Melesigenes consented, and set out with his patron,
examining all the curiosities of the countries they visited, and
informing himself of everything by interrogating those whom he met. We
may also suppose, that he wrote memoirs of all that he deemed worthy of
preservation Having set sail from Tyrrhenia and Iberia, they reached
Ithaca. Here Melesigenes, who had already suffered in his eyes, became
much worse, and Mentes, who was about to leave for Leucadia, left him to
the medical superintendence of a friend of his, named Mentor, the son of
Alcinor. Under his hospitable and intelligent host, Melesigenes rapidly
became acquainted with the legends respecting Ulysses, which afterwards
formed the subject of the Odyssey. The inhabitants of Ithaca assert, that
it was here that Melesigenes became blind, but the Colophomans make their
city the seat of that misfortune. He then returned to Smyrna, where he
applied himself to the study of poetry.But it is not on words only that grammarians, mere grammarians, will
exercise their elaborate and often tiresome ingenuity. Binding down an
heroic or dramatic poet to the block upon which they have previously
dissected his words and sentences, they proceed to use the axe and the
pruning knife by wholesale, and inconsistent in everything but their wish
to make out a case of unlawful affiliation, they cut out book after book,
passage after passage, till the author is reduced to a collection of
fragments, or till those, who fancied they possessed the works of some
great man, find that they have been put off with a vile counterfeit got up
at second hand. If we compare the theories of Knight, Wolf, Lachmann, and
others, we shall feel better satisfied of the utter uncertainty of
criticism than of the apocryphal position of Homer. One rejects what
another considers the turning-point of his theory. One cuts a supposed
knot by expunging what another would explain by omitting something else.

Nor is this morbid species of sagacity by any means to be looked upon as a
literary novelty. Justus Lipsius, a scholar of no ordinary skill, seems to
revel in the imaginary discovery, that the tragedies attributed to Seneca
are by _four_ different authors.(34) Now, I will venture to assert, that
these tragedies are so uniform, not only in their borrowed phraseology--a
phraseology with which writers like Boethius and Saxo Grammaticus were
more charmed than ourselves--in their freedom from real poetry, and last,
but not least, in an ultra-refined and consistent abandonment of good
taste, that few writers of the present day would question the capabilities
of the same gentleman, be he Seneca or not, to produce not only these, but
a great many more equally bad. With equal sagacity, Father Hardouin
astonished the world with the startling announcement that the Æneid of
Virgil, and the satires of Horace, were literary deceptions. Now, without
wishing to say one word of disrespect against the industry and
learning--nay, the refined acuteness--which scholars, like Wolf, have
bestowed upon this subject, I must express my fears, that many of our
modern Homeric theories will become matter for the surprise and
entertainment, rather than the instruction, of posterity. Nor can I help
thinking, that the literary history of more recent times will account for
many points of difficulty in the transmission of the Iliad and Odyssey to
a period so remote from that of their first creation.";

        public static string LatinSample = @"Uti pondus majus in majori corpore, minus in minore; inq; corpore eodem
majus prope terram, minus in cælis. Hæc vis est corporis totius
centripetentia seu propensio in centrum & (ut ita dicam) pondus, &
innotescit semper per vim ipsi contrariam & æqualem, qua descensus corporis
impediri potest.

Hasce virium quantitates brevitatis gratia nominare licet vires absolutas,
acceleratrices & motrices, & distinctionis gratia referre ad corpora, ad
corporum loca, & ad centrum virium: Nimirum vim motricem ad corpus, tanquam
conatum & propensionem totius in centrum, ex propensionibus omnium partium
compositum; & vim acceleratricem ad locum corporis, tanquam efficaciam
quandam, de centro per loca singula in circuitu diffusam, ad movenda
corpora quæ in ipsis sunt; vim autem absolutam ad centrum, tanquam causa
aliqua præditum, sine qua vires motrices non propagantur per regiones in
circuitu; sive causa illa sit corpus aliquod centrale (quale est Magnes in
centro vis Magneticæ vel Terra in centro vis gravitantis) sive alia aliqua
quæ non apparet. Mathematicus saltem est hic conceptus. Nam virium causas &
sedes physicas jam non expendo.

Est igitur vis acceleratrix ad vim motricem ut celeritas ad motum. Oritur
enim quantitas motus ex celeritate ducta in quantitatem Materiæ, & vis
motrix ex vi acceleratrice ducta in quantitatem ejusdem materiæ. Nam summa
actionum vis acceleratricis in singulas corporis particulas est vis motrix
totius. Unde juxta Superficiem Terræ, ubi gravitas acceleratrix seu vis
gravitans in corporibus universis eadem est, gravitas motrix seu pondus est
ut corpus: at si in regiones ascendatur ubi gravitas acceleratrix fit
minor, pondus pariter minuetur, eritq; semper ut corpus in gravitatem
acceleratricem ductum. Sic in regionibus ubi gravitas acceleratrix duplo
minor est, pondus corporis duplo vel triplo minoris erit quadruplo vel
sextuplo minus.

Porro attractiones et impulsus eodem sensu acceleratrices & motrices
nomino. Voces autem attractionis, impulsus vel propensionis cujuscunq; in
centrum, indifferenter et pro se mutuo promiscue usurpo, has vires non
physice sed Mathematice tantum considerando. Unde caveat lector ne per
hujusmodi voces cogitet me speciem vel modum actionis causamve aut rationem
physicam alicubi definire, vel centris (quæ sunt puncta Mathematica) vires
vere et physice tribuere, si forte aut centra trahere, aut vires centrorum
esse dixero.
Igitur quantitates relativæ non sunt eæ ipsæ quantitates quarum nomina præ
se ferunt, sed earum mensuræ illæ sensibiles (veræ an errantes) quibus
vulgus loco mensuratarum utitur. At si ex usu definiendæ sunt verborum
significationes; per nomina illa Temporis, Spatij, Loci & Motus proprie
intelligendæ erunt hæ mensuræ; & sermo erit insolens & pure Mathematicus si
quantitates mensuratæ hic subintelligantur. Proinde vim inferunt Sacris
literis qui voces hasce de quantitatibus mensuratis ibi interpretantur.
Neq; minus contaminant Mathesin & Philosophiam qui quantitates veras cum
ipsarum relationibus & vulgaribus mensuris confundunt.

Motus quidem veros corporum singulorum cognoscere, & ab apparentibus actu
discriminare, difficillimum est; propterea quod partes spatij illius
immobilis in quo corpora vere moventur, non incurrunt in sensus. Causa
tamen non est prorsus desperata. Nam suppetunt argumenta partim ex motibus
apparentibus, qui sunt motuum verorum differentiæ, partim ex viribus quæ
sunt motuum verorum causæ & effectus. Ut si globi duo ad datam ab invicem
distantiam filo intercedente connexi, revolverentur circa commune
gravitatis centrum; innotesceret ex tensione fili conatus globorum
recedendi ab axe motus, & inde quantitas motus circularis computari posset.
Deinde si vires quælibet æquales in alternas globorum facies ad motum
circularem augendum vel minuendum simul imprimerentur, innotesceret ex
aucta vel diminuta fili tensione augmentum vel decrementum motus; & inde
tandem inveniri possent facies globorum in quas vires imprimi deberent, ut
motus maxime augeretur, id est facies posticæ, sive quæ in motu circulari
sequuntur. Cognitis autem faciebus quæ sequuntur & faciebus oppositis quæ
præcedunt, cognosceretur determinatio motus. In hunc modum inveniri posset
& quantitas & determinatio motus hujus circularis in vacuo quovis immenso,
ubi nihil extaret externum & sensibile, quocum globi conferri possent. Si
jam constituerentur in spatio illo corpora aliqua longinqua datam inter se
positionem servantia, qualia sunt stellæ fixæ in regionibus nostris: sciri
quidem non posset ex relativa globorum translatione inter corpora, utrum
his an illis tribuendus esset motus. At si attenderetur ad filum &
inveniretur tensionem ejus illam ipsam esse quam motus globorum requireret;
concludere liceret motum esse globorum, & tum demum ex translatione
globorum inter corpora, determinationem hujus motus colligere. Motus autem
veros ex eorum causis, effectibus & apparentibus differentijs colligere, &
contra, ex motibus seu veris seu apparentibus, eorum causas & effectus,
docebitur fusius in sequentibus. Hunc enim in finem Tractatum sequentem
composui.";
        public static string PhilosophySample = @"We may carry this farther, and remark, not only that two objects are
connected by the relation of cause and effect, when the one produces
a motion or any action in the other, but also when it has a power
of producing it. And this we may observe to be the source of all the
relation, of interest and duty, by which men influence each other in
society, and are placed in the ties of government and subordination. A
master is such-a-one as by his situation, arising either from force or
agreement, has a power of directing in certain particulars the actions
of another, whom we call servant. A judge is one, who in all disputed
cases can fix by his opinion the possession or property of any thing
betwixt any members of the society. When a person is possessed of any
power, there is no more required to convert it into action, but the
exertion of the will; and that in every case is considered as possible,
and in many as probable; especially in the case of authority, where the
obedience of the subject is a pleasure and advantage to the superior.

These are therefore the principles of union or cohesion among our simple
ideas, and in the imagination supply the place of that inseparable
connexion, by which they are united in our memory. Here is a kind
of ATTRACTION, which in the mental world will be found to have as
extraordinary effects as in the natural, and to shew itself in as many
and as various forms. Its effects are every where conspicuous; but as to
its causes, they are mostly unknown, and must be resolved into original
qualities of human nature, which I pretend not to explain. Nothing is
more requisite for a true philosopher, than to restrain the intemperate
desire of searching into causes, and having established any doctrine
upon a sufficient number of experiments, rest contented with that, when
he sees a farther examination would lead him into obscure and uncertain
speculations. In that case his enquiry would be much better employed in
examining the effects than the causes of his principle.

Amongst the effects of this union or association of ideas, there are
none more remarkable, than those complex ideas, which are the common
subjects of our thoughts and reasoning, and generally arise from some
principle of union among our simple ideas. These complex ideas may be
divided into Relations, Modes, and Substances. We shall briefly examine
each of these in order, and shall subjoin some considerations concerning
our general and particular ideas, before we leave the present subject,
which may be considered as the elements of this philosophy.
Thirdly, it is a principle generally received in philosophy that
everything in nature is individual, and that it is utterly absurd to
suppose a triangle really existent, which has no precise proportion of
sides and angles. If this therefore be absurd in fact and reality, it
must also be absurd in idea; since nothing of which we can form a clear
and distinct idea is absurd and impossible. But to form the idea of an
object, and to form an idea simply, is the same thing; the reference
of the idea to an object being an extraneous denomination, of which in
itself it bears no mark or character. Now as it is impossible to form an
idea of an object, that is possest of quantity and quality, and yet
is possest of no precise degree of either; it follows that there is an
equal impossibility of forming an idea, that is not limited and confined
in both these particulars. Abstract ideas are therefore in themselves
individual, however they may become general in their representation.
The image in the mind is only that of a particular object, though the
application of it in our reasoning be the same, as if it were universal.

This application of ideas beyond their nature proceeds from our
collecting all their possible degrees of quantity and quality in such an
imperfect manner as may serve the purposes of life, which is the second
proposition I proposed to explain. When we have found a resemblance
[Footnote 2.] among several objects, that often occur to us, we apply
the same name to all of them, whatever differences we may observe in the
degrees of their quantity and quality, and whatever other differences
may appear among them. After we have acquired a custom of this kind, the
hearing of that name revives the idea of one of these objects, and makes
the imagination conceive it with all its particular circumstances and
proportions. But as the same word is supposed to have been frequently
applied to other individuals, that are different in many respects from
that idea, which is immediately present to the mind; the word not being
able to revive the idea of all these individuals, but only touches the
soul, if I may be allowed so to speak, and revives that custom, which we
have acquired by surveying them. They are not really and in fact present
to the mind, but only in power; nor do we draw them all out distinctly
in the imagination, but keep ourselves in a readiness to survey any of
them, as we may be prompted by a present design or necessity. The word
raises up an individual idea, along with a certain custom; and that
custom produces any other individual one, for which we may have
occasion. But as the production of all the ideas, to which the name may
be applied, is in most eases impossible, we abridge that work by a more
partial consideration, and find but few inconveniences to arise in our
reasoning from that abridgment.";
    }

}
