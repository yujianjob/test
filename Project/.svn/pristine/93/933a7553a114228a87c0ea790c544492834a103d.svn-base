using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Core.Web.Helper
{
    public static class AssetsHelperExtensions
    {
        public static AssetsHelper Assets(this HtmlHelper htmlHelper)
        {
            return AssetsHelper.GetInstance(htmlHelper);
        }
    }

    public static class AssetsConfig
    {
        public static IDictionary<AssetGroupTypes, ItemRegistrar> Config { get; private set; }

        static AssetsConfig()
        {
            Config = new Dictionary<AssetGroupTypes, ItemRegistrar>();

            ItemRegistrar baseCssItemIE7 = new ItemRegistrar("~/Content");
            baseCssItemIE7.Add("LayoutIE7");
            baseCssItemIE7.Add("MainIE7");

            ItemRegistrar baseCssItem = new ItemRegistrar("~/Content");
            baseCssItem.Add("Layout");
            baseCssItem.Add("Main");

            ItemRegistrar uiCssItemIE7 = new ItemRegistrar("~/Content/jquery-ui/redmond");
            uiCssItemIE7.Add("jquery-uiIE7");

            ItemRegistrar uiCssItem = new ItemRegistrar("~/Content/jquery-ui/redmond");
            uiCssItem.Add("jquery-ui");


            ItemRegistrar datatableCssItemIE7 = new ItemRegistrar("~/Content/jquery-datatables");
            datatableCssItemIE7.Add("jquery.dataTables_themerollerIE7");

            ItemRegistrar datatableCssItem = new ItemRegistrar("~/Content/jquery-datatables");
            datatableCssItem.Add("jquery.dataTables_themeroller");

            ItemRegistrar baseJsItem = new ItemRegistrar("~/Scripts/jquery");
            baseJsItem.Add("jquery");
            baseJsItem.Add("jquery.unobtrusive-ajax");
            baseJsItem.Add("jquery.blockUI");

            ItemRegistrar validateJsItem = new ItemRegistrar("~/Scripts/jquery-validate");
            validateJsItem.Add("jquery.validate");
            validateJsItem.Add("jquery.validate.unobtrusive");

            ItemRegistrar uiJsItem = new ItemRegistrar("~/Scripts/jquery-ui");
            uiJsItem.Add("jquery-ui");
            uiJsItem.Add("jquery.ui.datetimepicker");
            uiJsItem.Add("jquery.ui.combobox");
            uiJsItem.Add("jquery.ui.datepicker-zh-CN");
            uiJsItem.Add("jquery.ui.config");



            ItemRegistrar imageZoomJsItem = new ItemRegistrar("~/Content/jquery-imageZoom");
            imageZoomJsItem.Add("jquery.imageZoom");

            ItemRegistrar imageZoomCssItem = new ItemRegistrar("~/Content/jquery-imageZoom");
            imageZoomCssItem.Add("jquery.imageZoom");



            ItemRegistrar datatableJsItem = new ItemRegistrar("~/Scripts/jquery-datatables");
            datatableJsItem.Add("jquery.dataTables");
            datatableJsItem.Add("jquery.dataTables.config");
            datatableJsItem.Add("jquery.jeditable");

            AssetsConfig.Config.Add(AssetGroupTypes.LayoutCssIE7, baseCssItemIE7);
            AssetsConfig.Config.Add(AssetGroupTypes.LayoutCss, baseCssItem);
            AssetsConfig.Config.Add(AssetGroupTypes.UICssIE7, uiCssItemIE7);
            AssetsConfig.Config.Add(AssetGroupTypes.UICss, uiCssItem);
            AssetsConfig.Config.Add(AssetGroupTypes.DatatableCssIE7, datatableCssItemIE7);
            AssetsConfig.Config.Add(AssetGroupTypes.DatatableCss, datatableCssItem);

            AssetsConfig.Config.Add(AssetGroupTypes.JQueryJs, baseJsItem);
            AssetsConfig.Config.Add(AssetGroupTypes.ValidateJs, validateJsItem);
            AssetsConfig.Config.Add(AssetGroupTypes.UIJs, uiJsItem);
            AssetsConfig.Config.Add(AssetGroupTypes.DatatableJs, datatableJsItem);

            AssetsConfig.Config.Add(AssetGroupTypes.ImageZoomJs, imageZoomJsItem);
            AssetsConfig.Config.Add(AssetGroupTypes.ImageZoomCss, imageZoomCssItem);
        }
    }

    public class AssetsHelper
    {
        public static AssetsHelper GetInstance(HtmlHelper htmlHelper)
        {
            var instanceKey = "AssetsHelperInstance";

            var context = htmlHelper.ViewContext.HttpContext;
            if (context == null)
                return null;

            var assetsHelper = (AssetsHelper)context.Items[instanceKey];

            if (assetsHelper == null)
                context.Items.Add(instanceKey, assetsHelper = new AssetsHelper());

            return assetsHelper;
        }

        public ItemGroup Styles { get; private set; }
        public ItemGroup Scripts { get; private set; }

        private bool baseLoaded = false;
        private bool uiLoaded = false;
        private bool databaseLoaded = false;

        public AssetsHelper()
        {
            Styles = new ItemGroup(ItemRegistrarFormatters.StylePath, ItemRegistrarFormatters.StyleFormat, ".css");
            Scripts = new ItemGroup(ItemRegistrarFormatters.ScriptPath, ItemRegistrarFormatters.ScriptFormat, ".js");
        }

        private void CopyFiles(AssetGroupTypes key, ItemGroup group)
        {
            var item = AssetsConfig.Config[key];
            group.Add((int)key, x =>
            {
                x.DefaultPath(item.Path);
                foreach (var name in item.Names)
                {
                    x.Add(name);
                }
            });
        }

        public AssetsHelper LoadBase()
        {
            return LoadBase(false);
        }

        public AssetsHelper LoadBase(bool isIE7)
        {
            if (!baseLoaded)
            {
                if (isIE7)
                {
                    CopyFiles(AssetGroupTypes.LayoutCssIE7, Styles);
                }
                else
                {
                    CopyFiles(AssetGroupTypes.LayoutCss, Styles);
                }
                CopyFiles(AssetGroupTypes.JQueryJs, Scripts);

                baseLoaded = true;
            }
            return this;
        }

        public AssetsHelper LoadValidate()
        {
            CopyFiles(AssetGroupTypes.ValidateJs, Scripts);

            return this;
        }

        public AssetsHelper LoadUI()
        {
            return LoadUI(false);
        }

        public AssetsHelper LoadUI(bool isIE7)
        {
            if (!uiLoaded)
            {
                if (isIE7)
                {
                    CopyFiles(AssetGroupTypes.UICssIE7, Styles);
                }
                else
                {
                    CopyFiles(AssetGroupTypes.UICss, Styles);

                }
                CopyFiles(AssetGroupTypes.UIJs, Scripts);
                uiLoaded = true;
            }
            return this;
        }

        public AssetsHelper LoadDatatable()
        {
            return LoadDatatable(false);
        }

        public AssetsHelper LoadDatatable(bool isIE7)
        {
            if (!databaseLoaded)
            {
                if (isIE7)
                {
                    CopyFiles(AssetGroupTypes.DatatableCssIE7, Styles);
                }
                else
                {
                    CopyFiles(AssetGroupTypes.DatatableCss, Styles);
                }
                CopyFiles(AssetGroupTypes.DatatableJs, Scripts);
                databaseLoaded = true;
            }
            return this;
        }

        public AssetsHelper LoadImageZoom()
        {
            CopyFiles(AssetGroupTypes.ImageZoomCss, Styles);
            CopyFiles(AssetGroupTypes.ImageZoomJs, Scripts);

            return this;
        }
    }

    public class ItemGroup
    {
        private int _defaultKey;
        private readonly string _defaultPath;
        private readonly string _format;
        private readonly string _fileExts;
        private readonly IList<int> _groups;
        private readonly IDictionary<int, ItemRegistrar> _groupItems;

        public ItemGroup(string path, string format, string fileExts)
        {
            _defaultKey = 1000;
            _defaultPath = path;

            _format = format;
            _fileExts = fileExts;

            _groups = new List<int>();
            _groupItems = new Dictionary<int, ItemRegistrar>();

            _groups.Add(_defaultKey);
            _groupItems.Add(_defaultKey, new ItemRegistrar(_defaultPath));

        }

        public ItemGroup Default(Action<ItemRegistrar> configureAction)
        {
            configureAction(_groupItems[_defaultKey]);

            return this;
        }

        public ItemGroup Add(Action<ItemRegistrar> configureAction)
        {
            var key = _defaultKey++;
            if (!_groups.Contains(key))
            {
                _groups.Add(key);
                _groupItems.Add(key, new ItemRegistrar(_defaultPath));
            }
            configureAction(_groupItems[key]);

            return this;
        }

        public ItemGroup Add(int key, Action<ItemRegistrar> configureAction)
        {
            if (!_groups.Contains(key))
            {
                _groups.Add(key);
                _groupItems.Add(key, new ItemRegistrar(_defaultPath));
            }
            configureAction(_groupItems[key]);

            return this;
        }

        public MvcHtmlString Render(HtmlHelper html)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);

            var sb = new StringBuilder();

            ((List<int>)_groups).Sort();

            foreach (int key in _groups)
            {
                var item = _groupItems[key];
                var folderPath = System.Web.Hosting.HostingEnvironment.MapPath(item.Path);
                foreach (string name in item.Names)
                {
                    var fileName = name + _fileExts;
                    var fileFullName = Path.Combine(folderPath, fileName);
                    if (!File.Exists(fileFullName))
                    {
                        fileName = name + ".min" + _fileExts;
                        fileFullName = Path.Combine(folderPath, fileName);
                        if (!File.Exists(fileFullName))
                        {
                            throw new HttpException(500, "Can not found the file\"" + name + _fileExts + "\" or \"" + fileName + "\" in the path \"" + item.Path + "\"");
                        }
                    }

                    var relativePath = urlHelper.Content(VirtualPathUtility.Combine(item.Path, fileName));
                    sb.AppendLine(String.Format(_format, relativePath));

                }
            }

            return new MvcHtmlString(sb.ToString());
        }

    }

    public class ItemRegistrar
    {
        public string Path { get; private set; }
        public IList<string> Names { get; private set; }

        public ItemRegistrar(string path)
        {
            Path = VirtualPathUtility.AppendTrailingSlash(path);
            Names = new List<string>();
        }

        public ItemRegistrar DefaultPath(string path)
        {
            Path = VirtualPathUtility.AppendTrailingSlash(path);

            return this;
        }

        public ItemRegistrar Add(string name)
        {
            if (!Names.Contains(name))
                Names.Add(name);

            return this;
        }
    }

    public class ItemRegistrarFormatters
    {
        public static string StylePath = "~/Content";
        public static string ScriptPath = "~/Scripts";

        public static string StyleFormat = "<link href=\"{0}\" rel=\"stylesheet\" type=\"text/css\" />";
        public static string ScriptFormat = "<script src=\"{0}\" type=\"text/javascript\"></script>";
    }

    public enum AssetGroupTypes
    {
        LayoutCssIE7 = 0,
        LayoutCss = 1,
        UICssIE7 = 2,
        UICss = 3,
        DatatableCssIE7 = 4,
        DatatableCss = 5,
        JQueryJs = 100,
        ValidateJs = 101,
        UIJs = 102,
        DatatableJs = 103,
        OtherJs = 104,
        ImageZoomJs = 105,
        ImageZoomCss = 106,
    }
}
