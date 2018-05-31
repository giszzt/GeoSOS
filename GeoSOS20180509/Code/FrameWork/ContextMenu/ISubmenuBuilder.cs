// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Windows.Forms;
using ICSharpCode.Core;

namespace FrameWork
{
    public interface ISubmenuBuilder
    {
        MenuItem[] BuildSubmenu(Codon codon, object owner);
    }
}

