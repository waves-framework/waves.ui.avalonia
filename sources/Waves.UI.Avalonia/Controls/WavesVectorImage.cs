using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.Svg.Skia;
using Avalonia.Threading;
using Waves.UI.Avalonia.Extensions;
using Waves.UI.Base.Enums;

namespace Waves.UI.Avalonia.Controls
{
    /// <summary>
    /// Waves vector image.
    /// </summary>
    [DefaultProperty(nameof(Source))]
    public sealed class WavesVectorImage : ContentControl, IStyleable
    {
        /// <summary>
        /// Resource regex pattern.
        /// </summary>
        public const string ResourceRegex = "Resource=(.*)";

        /// <summary>
        /// FileName regex pattern.
        /// </summary>
        public const string FileRegex = "File=(.*)";

        /// <summary>
        /// URL regex pattern.
        /// </summary>
        public const string UrlRegex = "URL=(.*)";

        /// <summary>
        /// Geometry path regex pattern.
        /// </summary>
        public const string PathRegex = "Path=(.*)";

        /// <summary>
        /// Geometry path regex pattern.
        /// </summary>
        public const string AssemblyRegex = ";Assembly=(.*)";

        /// <summary>
        /// Defines <see cref="Source"/> property.
        /// </summary>
        public static readonly StyledProperty<object> SourceProperty = AvaloniaProperty.Register<WavesVectorImage, object>(
            nameof(Source));

        /// <summary>
        /// Defines <see cref="SourceDirectory"/> property.
        /// </summary>
        public static readonly StyledProperty<string> SourceDirectoryProperty = AvaloniaProperty.Register<WavesVectorImage, string>(
            nameof(SourceDirectory));

        /// <summary>
        /// Defines <see cref="SourceAssembly"/> property.
        /// </summary>
        public static readonly StyledProperty<string> SourceAssemblyProperty = AvaloniaProperty.Register<WavesVectorImage, string>(
            nameof(SourceAssembly),
            string.Empty);

        /// <summary>
        /// Defines <see cref="SourceType"/> property.
        /// </summary>
        public static readonly StyledProperty<WavesVectorImageSourceType> SourceTypeProperty = AvaloniaProperty.Register<WavesVectorImage, WavesVectorImageSourceType>(
            nameof(SourceType));

        private bool _changed;

        /// <summary>
        /// Creates new instance of <see cref="WavesVectorImage"/>.
        /// </summary>
        public WavesVectorImage()
        {
            AffectsRender<WavesVectorImage>(ForegroundProperty);
            AffectsRender<WavesVectorImage>(SourceProperty);
            AffectsRender<WavesVectorImage>(SourceDirectoryProperty);
            AffectsRender<WavesVectorImage>(SourceAssemblyProperty);
            AffectsRender<WavesVectorImage>(SourceTypeProperty);
        }

        /// <summary>
        /// Gets or sets source property.
        /// </summary>
        [Category("Waves.UI SDK - Content")]
        public object Source
        {
            get => GetValue(SourceProperty);
            set
            {
                _changed = true;
                SetValue(SourceProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets source assembly.
        /// </summary>
        [Category("Waves.UI SDK - Content")]
        public string SourceAssembly
        {
            get => GetValue(SourceAssemblyProperty);
            set
            {
                _changed = true;
                SetValue(SourceAssemblyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets source directory.
        /// </summary>
        [Category("Waves.UI SDK - Content")]
        public string SourceDirectory
        {
            get => GetValue(SourceDirectoryProperty);
            set
            {
                _changed = true;
                SetValue(SourceDirectoryProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets source type property.
        /// </summary>
        [Category("Waves.UI SDK - Content")]
        public WavesVectorImageSourceType SourceType
        {
            get => GetValue(SourceTypeProperty);
            set
            {
                _changed = true;
                SetValue(SourceTypeProperty, value);
            }
        }

        /// <inheritdoc />
        Type IStyleable.StyleKey => typeof(ContentControl);

        /// <inheritdoc />
        public override void Render(DrawingContext context)
        {
            Refresh();
            base.Render(context);
        }

        /// <summary>
        /// Refreshes image.
        /// </summary>
        public void Refresh()
        {
            if (!_changed)
            {
                return;
            }

            var source = Source;

            if (source is string str)
            {
                ParseSourceString(str);
            }

            if (source is string[] strArray)
            {
                /* if input looks like:

                    <x:Array Type="{x:Type sys:String}">
                     <sys:String>M 22 20 L 2 20 L 2 7 L 5 7 L 5 4 L 11 4 L 11 7 L 13 7 L 13 4 L 19 4 L 19 7 L 22 7 Z</sys:String>
                     <sys:String>M 22 20 L 2 20 L 2 7 L 5 7 L 5 4 L 11 4 L 11 7 L 13 7 L 13 4 L 19 4 L 19 7 L 22 7 Z</sys:String>
                     <sys:String>M 22 20 L 2 20 L 2 7 L 5 7 L 5 4 L 11 4 L 11 7 L 13 7 L 13 4 L 19 4 L 19 7 L 22 7 Z</sys:String>
                    </x:Array>

                 */

                var pathCollection = new WavesVectorImagePathCollection();
                pathCollection.AddRange(strArray.Select(item => new WavesVectorImagePath() { Value = item }));
                Load(pathCollection);
            }

            if (source is WavesVectorImagePath vectorPath)
            {
                Load(vectorPath);
            }

            if (source is WavesVectorImagePathCollection collection)
            {
                Load(collection);
            }

            _changed = false;
        }

        /// <inheritdoc />
        protected override Size MeasureOverride(Size availableSize)
        {
            Refresh();
            return base.MeasureOverride(availableSize);
        }

        /// <summary>
        /// Parses source string.
        /// </summary>
        /// <param name="str">Value.</param>
        private void ParseSourceString(string str)
        {
            if (SourceType == WavesVectorImageSourceType.Unknown)
            {
                if (ParseResourceString(str))
                {
                    return;
                }

                if (ParseFileString(str))
                {
                    return;
                }

                if (ParseUrlString(str))
                {
                    return;
                }

                if (ParsePathString(str))
                {
                    return;
                }
            }

            if (SourceType == WavesVectorImageSourceType.ResourceName)
            {
                LoadFromResources(str);
            }

            if (SourceType == WavesVectorImageSourceType.FilePath)
            {
                LoadFromFile(str);
            }

            if (SourceType == WavesVectorImageSourceType.Url)
            {
                LoadFromUrl(str);
            }

            if (SourceType == WavesVectorImageSourceType.Path)
            {
                Load(new WavesVectorImagePath() { Value = str });
            }
        }

        /// <summary>
        /// Parses resource string.
        /// If string looks like "Resource=Icon-Tool".
        /// </summary>
        /// <param name="str">Value.</param>
        /// <returns>Parsed or not.</returns>
        private bool ParseResourceString(string str)
        {
            var matches = Regex.Matches(str, ResourceRegex, RegexOptions.Singleline);

            if (matches.Count <= 0 ||
                matches[0].Groups.Count <= 1)
            {
                return false;
            }

            var value = matches[0].Groups[1].Value;

            LoadFromResources(value);

            return true;
        }

        /// <summary>
        /// Parses file string.
        /// If string looks like "File=C:\files\icon_tool.svg".
        /// </summary>
        /// <param name="str">Value.</param>
        /// <returns>Parsed or not.</returns>
        private bool ParseFileString(
            string str)
        {
            var matches = Regex.Matches(str, FileRegex, RegexOptions.Singleline);

            if (matches.Count <= 0 ||
                matches[0].Groups.Count <= 1)
            {
                return false;
            }

            var value = matches[0].Groups[1].Value;

            matches = Regex.Matches(value, AssemblyRegex, RegexOptions.Singleline);
            if (matches.Count > 0)
            {
                if (matches[0].Groups.Count <= 1)
                {
                    return false;
                }

                var assembly = matches[0].Groups[1].Value;
                value = value.Replace($";Assembly={assembly}", string.Empty);

                LoadFromFile(value, assembly);
            }
            else
            {
                LoadFromFile(value);
            }

            return true;
        }

        /// <summary>
        /// Parses URL string.
        /// if string looks like "Url=https://test.xyz//icon-tool.svg".
        /// </summary>
        /// <param name="str">Value.</param>
        /// <returns>Parsed or not.</returns>
        private bool ParseUrlString(
            string str)
        {
            var matches = Regex.Matches(str, UrlRegex, RegexOptions.Singleline);

            if (matches.Count <= 0 ||
                matches[0].Groups.Count <= 1)
            {
                return false;
            }

            var value = matches[0].Groups[1].Value;
            LoadFromUrl(value);

            return true;
        }

        /// <summary>
        /// Parses svg path string.
        /// If string looks like "Path=M 22 20 L 2 20 L 2 7 L 5 7 L 5 4 L 11 4 L 11 7 L 13 7 L 13 4 L 19 4 L 19 7 L 22 7 Z".
        /// </summary>
        /// <param name="str">Value.</param>
        /// <returns>Parsed or not.</returns>
        private bool ParsePathString(
            string str)
        {
            var matches = Regex.Matches(str, PathRegex, RegexOptions.Singleline);

            if (matches.Count <= 0 ||
                matches[0].Groups.Count <= 1)
            {
                return false;
            }

            var value = matches[0].Groups[1].Value;
            var path = new WavesVectorImagePath
            {
                Value = value,
            };

            Load(path);

            return true;
        }

        /// <summary>
        /// Loads vector from resources.
        /// </summary>
        /// <param name="resourceName">Resource name.</param>
        private void LoadFromResources(string resourceName)
        {
            object? path = null;

            var exists = Application.Current != null && Application.Current.TryFindResource(resourceName, out path);

            if (!exists)
            {
                return;
            }

            if (path == null)
            {
                return;
            }

            var viewBox = new Viewbox();
            var pathView = new global::Avalonia.Controls.Shapes.Path { Data = (StreamGeometry)path, Fill = Foreground };
            viewBox.Child = pathView;
            viewBox.Height = Height;
            viewBox.Width = Width;
            viewBox.Opacity = Opacity;
            Content = viewBox;
        }

        /// <summary>
        /// Loads vector from resources.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="assemblyName">Assembly name.</param>
        private void LoadFromFile(string fileName, string assemblyName)
        {
            var content = string.Empty;
            var uri = new Uri(fileName, UriKind.RelativeOrAbsolute);

            if (string.IsNullOrEmpty(assemblyName))
            {
                return;
            }

            if (!uri.IsAbsoluteUri)
            {
                fileName = fileName.Replace("/", ".");
                fileName = fileName.Replace(@"\", ".");

                if (fileName.StartsWith("."))
                {
                    fileName = fileName.Substring(1, fileName.Length - 1);
                }

                var resourceName = $"{assemblyName}.{fileName}";
                var assembly = AppDomain.CurrentDomain.GetAssemblies().SingleOrDefault(a => a.GetName().Name == assemblyName);
                using var stream = assembly?.GetManifestResourceStream(resourceName);
                if (stream != null)
                {
                    using var reader = new StreamReader(stream);
                    content = reader.ReadToEnd();

                    var document = XDocument.Parse(content);
                    var count = document.Descendants().Count(p => p.Name.LocalName == "path");

                    var fillAttribute = document.Root?.Attributes().FirstOrDefault(x => x.Name.LocalName.Equals("fill"));
                    if (fillAttribute != null)
                    {
                        if (Foreground is SolidColorBrush brush)
                        {
                            fillAttribute.Value = brush.Color.ToHexString();
                        }
                    }

                    if (count == 1)
                    {
                        var element = document.Descendants().First(p => p.Name.LocalName == "path");
                        var attribute = element.Attributes().FirstOrDefault(x => x.Name.LocalName.Equals("fill"));

                        if (Foreground is SolidColorBrush brush)
                        {
                            if (attribute != null)
                            {
                                attribute.Value = brush.Color.ToHexString();
                            }
                        }

                        content = document.ToString();
                    }
                }
            }
            else
            {
                if (File.Exists(fileName))
                {
                    content = File.ReadAllText(fileName);
                }
            }

            if (string.IsNullOrEmpty(content))
            {
                return;
            }

            var source = new SvgSource();
            var picture = source.FromSvg(content);
            if (picture is null)
            {
                return;
            }

            var image = new Image
            {
                Source = new SvgImage()
                {
                    Source = source,
                },
                Height = Height,
                Width = Width,
            };

            image.Opacity = Opacity;
            Dispatcher.UIThread.InvokeAsync(() => { Content = image; });
        }

        /// <summary>
        /// Loads vector from resources.
        /// </summary>
        /// <param name="fileName">File name.</param>
        private void LoadFromFile(string fileName)
        {
            var assemblyName = SourceAssembly;
            var directory = SourceDirectory;
            LoadFromFile(Path.Combine(directory, $"{fileName}.svg"), assemblyName);
        }

        /// <summary>
        /// Loads vector from URL.
        /// </summary>
        /// <param name="url">URL.</param>
        private void LoadFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            var svgImage = new SvgImage();
            var source = new SvgSource();
            source.Load(url);
            svgImage.Source = source;
            Content = svgImage;
        }

        /// <summary>
        /// Loads vector from resources.
        /// </summary>
        /// <param name="path">Vector path.</param>
        private void Load(WavesVectorImagePath? path)
        {
            if (path == null)
            {
                return;
            }

            var viewBox = new Viewbox();

            var fill = path.Fill == null
                ? Foreground
                : new SolidColorBrush(path.Fill.Value);

            var pathView = new global::Avalonia.Controls.Shapes.Path()
            {
                Data = Geometry.Parse(path.Value),
                Fill = fill,
            };

            viewBox.Child = pathView;
            viewBox.Height = Height;
            viewBox.Width = Width;
            viewBox.Opacity = Opacity;
            Content = viewBox;
        }

        /// <summary>
        /// Loads vector from resources.
        /// </summary>
        /// <param name="collection">Vector path collection.</param>
        private void Load(WavesVectorImagePathCollection? collection)
        {
            if (collection == null)
            {
                return;
            }

            var viewBox = new Viewbox();
            var grid = new Grid();

            foreach (var path in collection)
            {
                var fill = path.Fill == null
                    ? Foreground
                    : new SolidColorBrush(path.Fill.Value);

                var pathView = new global::Avalonia.Controls.Shapes.Path()
                {
                    Data = Geometry.Parse(path.Value),
                    Fill = fill,
                };

                grid.Children.Add(pathView);
            }

            viewBox.Child = grid;
            viewBox.Height = Height;
            viewBox.Width = Width;
            viewBox.Opacity = Opacity;
            Content = viewBox;
        }
    }
}
