using System;
using System.Collections.Generic;
using System.Windows;

namespace WpfApplication1.Common
{
    public enum ThemeType
    {
        Default,
        Enterprise,
        Watch,
    }
    public class XamlResourceLoader : FrameworkElement
    {
        public static XamlResourceLoader Instance
        {
            get;
            private set;

        }

        static XamlResourceLoader()
        {
            if (Instance == null)
                Instance = new XamlResourceLoader();
        }

        public XamlResourceLoader()
        {
            SelectedTheme = ThemeType.Default;
        }

        public static DependencyProperty SelectedThemeProperty = DependencyProperty.Register("SelectedTheme", typeof(ThemeType), typeof(XamlResourceLoader),
          new PropertyMetadata(ThemeType.Default));
        public ThemeType SelectedTheme
        {
            get
            {
                return (ThemeType)GetValue(SelectedThemeProperty);
            }
            set
            {
                SetValue(SelectedThemeProperty, value);
            }
        }

        private void ApplyKeysToDictionary(ResourceDictionary dictionary, Uri url)
        {
            ResourceDictionary source = new ResourceDictionary { Source = url };
            object[] keysArray = new object[source.Keys.Count];
            source.Keys.CopyTo(keysArray, 0);

            for (int i = 0; i < keysArray.Length; i++)
                dictionary.Add(keysArray[i], source[keysArray[i]]);
        }

       
        #region Risks
        public List<string> RisksThemeFiles = new List<string>()
        {
            
             "/WpfApplication1;component/Controls/OptionsControlView.xaml",
             "/WpfApplication1;component/Controls/ExpertsGridView.xaml",
             "/WpfApplication1;component/Controls/RisksGridView.xaml",
             "/WpfApplication1;component/Controls/AssessmentsGridView.xaml",
             "/WpfApplication1;component/Controls/ResultGridView.xaml",
             "/WpfApplication1;component/Controls/MessageBoxWinView.xaml",
             //"/WpfApplication1;component/Controls/OptionsControlView.xaml",

        };
        #endregion
        public void LoadResource(List<string> themeFiles, ResourceDictionary dictionary)
        {
            foreach(string filePath in themeFiles)
                ApplyKeysToDictionary(dictionary, new Uri(filePath, UriKind.RelativeOrAbsolute));
        }
    }
}
