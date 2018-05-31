using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.Core;
using FrameWork.Gui;

namespace FrameWork.AddIns.Common.OptionPanels
{
    public partial class LanguagesPanel : UserControl, IOptionPanel
    {
        public LanguagesPanel()
        {
            InitializeComponent();
            int i=0;
            foreach (Language item in LanguageService.Languages)
            {
                imageList.Images.Add(Image.FromFile(item.ImagePath));
                ListViewItem newItem = new ListViewItem();
                newItem.Tag = item;
                newItem.ImageIndex = i;
                newItem.Name = item.Name;
                newItem.Text = item.Name;
                listView.Items.Add(newItem);
                i++;
            }
        }

        static readonly string langPropName = "CoreProperties.UILanguage";

        public static Language CurrentLanguage
        {
            get { return GetCulture(PropertyService.Get(langPropName, "en")); }
            set { PropertyService.Set(langPropName, value.Code); }
        }

        static Language GetCulture(string languageCode)
        {
            return LanguageService.Languages.FirstOrDefault(x => x.Code.StartsWith(languageCode))
                ?? LanguageService.Languages.First(x => x.Code.StartsWith("en"));
        }

        #region IOptionPanel
        public virtual object Owner { get; set; }

        public virtual object Control
        {
            get
            {
                return this;
            }
        }

        public virtual void LoadOptions()
        {
        }

        public virtual bool SaveOptions()
        {
            Language current = listView.SelectedItems[0].Tag as Language;
            PropertyService.Set(langPropName, current.Code);
            return true;
        }
        #endregion

    }
}
