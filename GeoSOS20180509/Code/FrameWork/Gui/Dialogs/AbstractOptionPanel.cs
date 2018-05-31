﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

using ICSharpCode.Core.Presentation;

namespace FrameWork.Gui
{
	/// <summary>
	/// Simple implementation of IOptionPanel with support for OptionBinding markup extensions.
	/// </summary>
	public class OptionPanel : UserControl, IOptionPanel, IOptionBindingContainer,INotifyPropertyChanged
	{
		
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
			
		static OptionPanel()
		{
			MarginProperty.OverrideMetadata(typeof(OptionPanel),
			                                new FrameworkPropertyMetadata(new Thickness(2, 0, 4, 0)));
		}
		
		public OptionPanel()
		{
			this.Resources.Add(
				typeof(GroupBox),
				new Style(typeof(GroupBox)) { Setters = {
						new Setter(GroupBox.PaddingProperty, new Thickness(3, 3, 3, 7))
					}});
			this.Resources.Add(typeof(CheckBox), GlobalStyles.WordWrapCheckBoxStyle);
			this.Resources.Add(typeof(RadioButton), GlobalStyles.WordWrapCheckBoxStyle);
		}
		
		public virtual object Owner { get; set; }
		
		readonly List<OptionBinding> bindings = new List<OptionBinding>();
		
		void IOptionBindingContainer.AddBinding(OptionBinding binding)
		{
			this.bindings.Add(binding);
		}
		
		public virtual object Control {
			get {
				return this;
			}
		}
		
		public virtual void LoadOptions()
		{
		}
		
		public virtual bool SaveOptions()
		{
			foreach (OptionBinding b in bindings) {
				if (!b.Save())
					return false;
			}
			
			return true;
		}
		
		
		#region INotifyPropertyChanged implementation
		
		protected void RaisePropertyChanged(string propertyName)
		{
			RaiseInternal(propertyName);
		}
		
		
		protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpresssion)
		{
			var propertyName = ExtractPropertyName(propertyExpresssion);
			RaiseInternal(propertyName);
		}
		
		
		private void RaiseInternal (string propertyName)
		{
			var handler = this.PropertyChanged;
			if (handler != null)
			{
				handler(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}
		
		private static String ExtractPropertyName<T>(Expression<Func<T>> propertyExpresssion)
		{
			if (propertyExpresssion == null)
			{
				throw new ArgumentNullException("propertyExpresssion");
			}

			var memberExpression = propertyExpresssion.Body as MemberExpression;
			if (memberExpression == null)
			{
				throw new ArgumentException("The expression is not a member access expression.", "propertyExpresssion");
			}

			var property = memberExpression.Member as PropertyInfo;
			if (property == null)
			{
				throw new ArgumentException("The member access expression does not access a property.", "propertyExpresssion");
			}

			var getMethod = property.GetGetMethod(true);
			if (getMethod.IsStatic)
			{
				throw new ArgumentException("The referenced property is a static property.", "propertyExpresssion");
			}

			return memberExpression.Member.Name;
		}
		
		#endregion
	}
}
