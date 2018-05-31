using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.Core;
using ICSharpCode.Core.WinForms;

namespace FrameWork
{
    public static class ContextMenuService
    {
        static void AddItemsToMenu(ContextMenu.MenuItemCollection collection, List<MenuItemDescriptor> descriptors)
        {
            foreach (MenuItemDescriptor descriptor in descriptors)
            {
                object item = CreateMenuItemFromDescriptor(descriptor);
                if (item is MenuItem)
                {
                    collection.Add((MenuItem)item);
                    if (item is IStatusUpdate)
                        ((IStatusUpdate)item).UpdateStatus();
                }
                else
                {
                    ISubmenuBuilder submenuBuilder = (ISubmenuBuilder)item;
                    collection.AddRange(submenuBuilder.BuildSubmenu(descriptor.Codon, descriptor.Caller));
                }
            }
        }

        static object CreateMenuItemFromDescriptor(MenuItemDescriptor descriptor)
        {
            Codon codon = descriptor.Codon;
            string type = codon.Properties.Contains("type") ? codon.Properties["type"] : "Command";
            bool createCommand = codon.Properties["loadclasslazy"] == "false";

            switch (type)
            {
                case "Separator":
                    return new ContextMenuSeparator(codon, descriptor.Caller, descriptor.Conditions);
                case "Item":
                case "Command":
                    return new ContextMenuCommand(codon, descriptor.Caller, createCommand, descriptor.Conditions);
                case "Menu":
                    return new FrameWork.Menu(codon, descriptor.Caller, ConvertSubItems(descriptor.SubItems), descriptor.Conditions);
                default:
                    throw new System.NotSupportedException("unsupported menu item type : " + type);
            }
        }

        internal static ArrayList ConvertSubItems(IList items)
        {
            ArrayList r = new ArrayList();
            if (items != null)
            {
                foreach (MenuItemDescriptor descriptor in items)
                {
                    r.Add(CreateMenuItemFromDescriptor(descriptor));
                }
            }
            return r;
        }


        public static ContextMenu CreateContextMenu(object owner, string addInTreePath)
        {
            if (addInTreePath == null)
            {
                return null;
            }
            try
            {
                List<MenuItemDescriptor> descriptors = AddInTree.BuildItems<MenuItemDescriptor>(addInTreePath, owner, true);
                ContextMenu contextMenu = new ContextMenu();
                contextMenu.MenuItems.Add(new MenuItem("dummy"));
                contextMenu.Popup += delegate
                {
                    contextMenu.MenuItems.Clear();
                    AddItemsToMenu(contextMenu.MenuItems, descriptors);
                };
                
                //contextMenu.Opened += ContextMenuOpened;
                contextMenu.Disposed += ContextMenuClosed;
                return contextMenu;
            }
            catch (TreePathNotFoundException)
            {
                MessageService.ShowError("Warning tree path '" + addInTreePath + "' not found.");
                return null;
            }
        }

        static bool isContextMenuOpen;

        public static bool IsContextMenuOpen
        {
            get
            {
                return isContextMenuOpen;
            }
        }

        static void ContextMenuOpened(object sender, EventArgs e)
        {
            isContextMenuOpen = true;
            ContextMenu contextMenu = (ContextMenu)sender;
            foreach (object o in contextMenu.MenuItems)
            {
                if (o is IStatusUpdate)
                {
                    ((IStatusUpdate)o).UpdateStatus();
                }
            }
        }

        static void ContextMenuClosed(object sender, EventArgs e)
        {
            isContextMenuOpen = false;
        }

        public static void ShowContextMenu(object owner, string addInTreePath, Control parent, int x, int y)
        {
            ContextMenu menu = CreateContextMenu(owner, addInTreePath);
            if (menu != null)
            {
                menu.Show(parent, new Point(x, y));
            }
        }
    }
}
